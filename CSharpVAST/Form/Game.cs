using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;  

namespace CSharpVAST
{
    // "輸出測試" 的字串等完成程式之後可以刪除
    public partial class Game : Form
    {
        Random random = new Random();

        #region 移動方向之類型
        const int LEFT_DOWN = 1;
        const int DOWN = 2;
        const int RIGHT_DOWN = 3;
        const int LEFT = 4;
        const int RIGHT = 6;
        const int LEFT_UP = 7;
        const int UP = 8;
        const int RIGHT_UP = 9;
        #endregion

        #region 初始化之參數設定
        const int MIC_SPEAK = 1;        // 為 speak
        const int MIC_LISTEN = 2;       // 為 listen
        const UInt16 AOI_RADIUS = 200;  // aoi 半徑
        const int AOI_REDUNDANT = 10;   // VAST 中 aoi 半徑多 C# 中 10 個單位
        const int INIT_X = 30;          // 初始位置 x 座標
        const int INIT_Y = 120;         // 初始位置 y 座標
        const int PORT_MINI = 10000;    // port 的最小值
        const int PORT_MAX = 24000;     // port 的最大值
        #endregion

        #region 傳送資料之類型
        const int TYPE_TEXT = 1;            // 傳送文字
        const int TYPE_VOICE = 2;           // 傳送語音
        const int TYPE_POSITION = 3;        // 傳送位置資訊
        const int TYPE_EXIT = 4;            // 傳送離開訊息
        const int TYPE_ADDLISTENER = 5;     // 傳送成為傾聽者訊息
        const int TYPE_EXITLISTENER = 6;    // 傳送要改變傾聽者訊息
        const int TYPE_REBROADCAST = 7;     // 傳送轉播的訊息
        const int TYPE_PORT = 8;            // 傳送 port 訊息
        const int TYPE_TEST = 9;
        #endregion

        #region 變數宣告
        string[] myargs;
        bool g_is_gateway = true;
        string g_gateway;

        ShowInfo show;                      // 顯示自己的資訊之類別
        PanelRender render;                 // 把資訊畫在 panel 上的類別
        Thread tick_thread;
        Thread recmsg_thread;               // 監聽訊息

        Point aoi_center;                   // 本身的位置資訊
        String input;                       // 輸入文字方塊
        ulong SelfID = 0;                   // 自己的 ID
        internal String SelfName = "";      // 自己的名字
        int SelfDirect = DOWN;              // 進入遊戲中面對的方向

        internal List<Node> neighborList = null;                                            // 來儲存鄰居資訊之列表
        Neighbor neighbor = new Neighbor();                                                 // 處理鄰居加入、離開、找尋之功能
        internal List<Relation> relationList = null;                                        // 來儲存鄰居資訊
        Cal_Relation relation = new Cal_Relation(INIT_X, INIT_Y, AOI_RADIUS + AOI_RADIUS);  // 處理鄰居的關係值計算

        List<VoiceRecorder> voicerecevierlist = new List<VoiceRecorder>();                  // 混音播放使用
        VoiceRecorder voicerecorder = null;                                                 // 錄音使用

        List<VoiceNetwork> listenserverlist = new List<VoiceNetwork>();                     // 使用來接收語音的 server 針對不同的 user 所新增或刪減

        List<Node> ListenerChildrenList = null;     // 傾聽者的列表，上限為 Speaker_Capacity
        internal bool AutoSpeak = true;             // 是否為自動選取傾聽者
        internal Node Listener;                     // 傾聽者的 ID
        Node Compare_Listener;                      // 此次的傾聽者的 ID
        internal int Max_Speaker_Capacity;          // 最大的上限值
        internal int Max_Overhearer_capacity;       // 最大的上限值
        internal int Speaker_Capacity;              // """"""假設""""""上傳給我的 Listener 之能力為亂數  (假資料)
        internal int Overhearer_Capacity;           // """"""假設""""""上傳給我的 Overhearer 之能力為亂數  (假資料)

        string LocalIp = Dns.Resolve(Dns.GetHostName()).AddressList[0].ToString();  // 自己本機端的 ip 位置
        string socketip;  // 使用單播初始化的 ip
        Random randport = new Random();  // 使用來讓 port random
        #endregion

        public Game(string[] args)
        {
            InitializeComponent();
            myargs = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form.CheckForIllegalCrossThreadCalls = false;  // 可以允許 thread 更改其他的 ui 元件
            InputName Inputname = new InputName(this);
            Inputname.ShowDialog(this);

            Enviro_Initial();  // 初始化環境
        }

        private void Enviro_Initial()                                           // 初始化 VAST 環境
        {
            aoi_center = new Point(INIT_X, INIT_Y);  // aoi 中心位置
            keyout.Text = "";  // 初始化輸出區塊
            keyout.BackColor = Color.Transparent;  // 把 keyout 背景設為透明
            show = new ShowInfo(SelfInformation, keyout);  // 初始化顯示資訊的類別
            render = new PanelRender(myPanel);  // 初始化

            // 建立 AK-Tree 之初始化
            Listener = new Node(0, "0", null, 0, 0, 0, 0);  // 初始化設定
            Compare_Listener = new Node(0, "0", null, 0, 0, 0, 0);  // 初始化設定
            ListenerChildrenList = new List<Node>();  // 初始化設定
            
            // 假定亂設定一開始的初始值
            Max_Speaker_Capacity = random.Next(1, 5);
            Max_Overhearer_capacity = random.Next(1, 5);
            Speaker_Capacity = Max_Speaker_Capacity;
            Overhearer_Capacity = Max_Overhearer_capacity;

            // 設定是否為 gateway
            string port = "1037";
            string ip = "127.0.0.1";
            //string ip = "140.115.51.246";  // 測試使用
            if (myargs.Length >= 1)
                port = myargs[0];
            if (myargs.Length >= 2)
            {
                ip = myargs[1];
                g_is_gateway = false;
            }
            g_gateway = ip + ":" + port;
            socketip = LocalIp;  // 設定自己本機端的 ip

            // 初始化的功能
            Vast.InitVAST(g_is_gateway, g_gateway);  // 初始化 VAST
            Vast.VASTJoin(1, aoi_center.X, aoi_center.Y, AOI_RADIUS);  // 使節點加入 VAST

            // 啟動加入 VAST 的 Tick
            tick_thread = new Thread(new ThreadStart(Tick_Start));
            tick_thread.IsBackground = true;
            tick_thread.Start();

            // 啟動監聽 VAST 訊息的 thread
            recmsg_thread = new Thread(new ThreadStart(ReceiveBroadcast));
            recmsg_thread.IsBackground = true;
            recmsg_thread.Start();

            //  啟動監聽 VAST Socket 訊息的 thread
            //recsocketmsg_thread = new Thread(new ThreadStart(ReceviceSocket));
            //recsocketmsg_thread.IsBackground = true;
            //recsocketmsg_thread.Start();

            Vast.checkVASTJoin();  // 確保有加入 VAST

            // 以下確保已經確實加入 VAST 之中，並且可以取得所有資訊
            while (SelfID == 0)  // 若未取得 id 則一直取得
                SelfID = Vast.VASTGetSelfID();

            // 在此才會真正加入 VAST 中，上面已確保取得自己的 ID
            neighborList = neighbor.GetList();
            keyin.Text = input = "[" + SelfName + "] : ";  // 初始化輸入區塊
            show.SelfInnformation_Print(aoi_center, Speaker_Capacity, Max_Speaker_Capacity, Overhearer_Capacity, Max_Overhearer_capacity, socketip, Listener, voicerecorder);

            // 第一次進入 VAST 中廣播給大家自己的"位置" 及 "面向"
            PublishPosition(SelfDirect);
        }

        private void Tick_Start()                                               // 進入 VAST 的 Tick
        {
            while (true)
            {
                Thread.Sleep(100);
                Vast.VASTTick(0);
            }
        }

        private void PublishPosition(int direct)                                // 發送自己的位置
        {
            string str;  // 要傳送的訊息
            str = SelfID.ToString() + " " +         // 自己的 ID
                  TYPE_POSITION + " " +             // 訊息型態
                  socketip + " " +                  // 自己的 IP
                  aoi_center.X.ToString() + " " +   // 自己的 X 座標
                  aoi_center.Y.ToString() + " " +   // 自己的 Y 座標
                  direct.ToString() + " " +         // 自己的 面對方向
                  SelfName;                         // 自己的 名字

            Vast.VASTPublish(str, (uint)str.Length, AOI_RADIUS);  
        }

        private void ReceiveBroadcast()                                         // 接收任何(廣播)訊息並且判斷為何種訊息
        {
            while (true)
            {
                UInt64 from = 0;
                uint size = 0;
                IntPtr ptr = Vast.VASTReceive(ref from, ref size);  // !!不要太快接收訊息不會有例外的發生!!
                string str = Marshal.PtrToStringAnsi(ptr);  // 接收遠端傳送過來之訊息

                if (str != null)  // 若沒收到空字串
                    if (from != SelfID)  // 自己不要收到自己的訊息
                    {
                        string[] split_string = str.Split(' ');  // 字串切割，把空白切掉   第一格的字串為傳送之類型
                        switch (int.Parse(split_string[1]))  // 第 2 個為資料類型
                        {
                            case TYPE_TEXT:         // 接收到文字訊息
                                ReceiveTextMsg(split_string);
                                break;
                            case TYPE_VOICE:        // 接收到語音訊息
                                //ReceiveVoiceMsg(split_string);
                                break;
                            case TYPE_POSITION:     // 接收到位置訊息
                                ReceivePosition(split_string);
                                break;
                            case TYPE_EXIT:         // 接收到離開訊息
                                ReceiveExit(split_string);
                                break;
                            case TYPE_ADDLISTENER:  // 接收到想加入 Listener
                                ReceiveAddListener(split_string);
                                break;
                            case TYPE_EXITLISTENER: // 接收到想離開 Listener
                                ReceiveExitListener(split_string);
                                break;
                            case TYPE_PORT:         // 接收到 port
                                ReceivePort(split_string);
                                break;
                        }
                    }
                Thread.Sleep(5);  // 預防處理過慢, 停頓 5ms 再接收訊息
            }
        }

        private void ReceviceSocket()                                           // 接收來自(單播)訊息並且判斷為何種訊息  --------------------------------------------- 單播訊息接收的太慢
        {
            while (true)
            {
                UInt64 from = 0;
                uint size = 0;
                IntPtr ptr = Vast.VASTReceiveSocket(ref from, ref size);  // !!不要太快接收訊息不會有例外的發生!!
                string str = Marshal.PtrToStringAnsi(ptr);  // 接收遠端傳送過來之訊息

                if (str != null)  // 若沒收到空字串
                {
                    string[] split_string = str.Split(' ');  // 字串切割，把空白切掉   第一格的字串為傳送之類型
                    switch (int.Parse(split_string[1]))  // 第 2 個為資料類型
                    {
                        case TYPE_VOICE:            // 接收到語音訊息
                            ReceiveSocketVoiceMsg(split_string);
                            break;
                        case TYPE_REBROADCAST:      // 接收到轉播訊息
                            ReceiveSocketRebroadcast(split_string);
                            break;
                        case TYPE_TEST:             // 測試訊息
                            ReceiveSocketTest(split_string);
                            break;
                    }
                    Console.WriteLine("收到使用單播的訊息");  // 輸出測試
                }
            }
        }

        private void ReceiveTextMsg(string[] split_str)                         // (廣播) 1 接收到文字訊息
        {
            string str = "";
            for (int i = 2; i < split_str.Length; i++)  // 把字串重新組起來 index 0 為 ID , index 1 為辨別傳送資料類型
                str += split_str[i] + " ";
            show.Output_Monitor(str);  // 印到螢幕上
        }

        private void ReceiveVoiceMsg(string[] split_str)                        // (廣播) 2 接收到語音訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID
            byte[] data = new byte[split_str.Length - 3];   // 因為最後會收到空白字元 index 0 為辨別傳送資料類型

            for (int i = 2; i < split_str.Length - 3; i++)  // 把字串陣列轉成byte陣列
                data[i - 2] = byte.Parse(split_str[i]);

            bool isListener = false;  // 是否為傾聽者
            int index = neighbor.FindIndex(ID);  // 尋找此資料是在鄰居中哪個人的 index
            if (index == -1)  // 表示找不到這位鄰居
                return;
            int pan = 0;  // 左右聲道值大小
            int volume = 0;  // 聲音大小聲

            // 判斷資料是否是傾聽者之聲音
            if (ID == Listener.id)
                isListener = true;

            voicerecevierlist[index].GetVoiceData((int)data.Length, data, isListener, pan, volume, ShowVoice);  // 丟入屬於此人的播放器播放, 解碼播出聲音
        }

        private void ReceivePosition(string[] split_str)                        // (廣播) 3 接收到位置訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID
            string SocketIP = split_str[2];                 // index 2 為 IP
            float x = float.Parse(split_str[3]);            // index 3 為 x 座標
            float y = float.Parse(split_str[4]);            // index 4 為 y 座標
            int dir = int.Parse(split_str[5]);              // index 5 為 行走方向
            string name = split_str[6];                     // index 6 為 名字

            if (!neighbor.Find((ulong)ID))  // 若沒有在鄰居清單中
            {
                // 新增一個屬於此臨居的專屬語音接收 server
                int SocketPort = 0;  // 使用初始化的 port
                bool flag = false;  // 用來判斷 port 有沒有 random 到重覆

                while (!flag)  // 重覆 random
                {
                    SocketPort = randport.Next(PORT_MINI, PORT_MAX);  // 隨機給定一個 port
                    for (int i = 0; i < listenserverlist.Count; i++)  // 找尋所有 thread 裡面的 port
                        if (listenserverlist[i].port == SocketPort)
                            flag = true;  // 找到重覆的 socket

                    if (flag)  // 若找到重覆的, 把 flag 改為 false, 再 run 一次 while 
                        flag = false;  // 使得可再進來 while
                    else  // 若沒重覆給, 就跳出迴圈
                        break;
                }

                listenserverlist.Add(new VoiceNetwork());
                listenserverlist[listenserverlist.Count - 1].Listen_Server(ID, SocketPort);  // 建立 server
                listenserverlist[listenserverlist.Count - 1].InitReceiver(this.Handle, (AOI_RADIUS + AOI_REDUNDANT));  // 初始收音設備
                listenserverlist[listenserverlist.Count - 1].Update_Info(aoi_center, SelfDirect, neighborList, Listener.id);  // 初始自己的資訊
                listenserverlist[listenserverlist.Count - 1].Start_Listen();  // 開始接收聲音
                listenserverlist[listenserverlist.Count - 1].ShowVoice = ShowVoice;

                neighbor.Add(ID, name, SocketIP, SocketPort, x, y, dir);  // 增加鄰居

                // 回應自己的座標
                PublishPosition(SelfDirect);  // ** 回應自己收到訊息, 並廣播自己的座標及面對方向

                // 回應 Port  $$$$$$ 封包格式 : 自己的ID + 傳送資料型態 + 欲傳送者的ID + 給他的專屬port
                string back = SelfID.ToString() + " " + TYPE_PORT + " " + ID.ToString() + " " + SocketPort.ToString();
                Vast.VASTPublish(back, (uint)back.Length, AOI_RADIUS);
            }

            // 更新鄰居清單, 此neighbor.Update沒加入在UpdateNeighborInfomation中是因為 "自己移動" 與 "他人移動" 的參數不同
            neighbor.Update(ID, x, y, dir, AOI_RADIUS + AOI_REDUNDANT, (float)aoi_center.X, (float)aoi_center.Y, listenserverlist);  // 更新座標, 因為 VAST 中的距離多 C# 10 個單位
            UpdateNeighborInfomation();  // 更新 function
        }

        private void ReceiveExit(string[] split_str)                            // (廣播) 4 接收到離開訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID

            neighbor.Remove(ID, listenserverlist);  // 刪除該節點, 連同監聽的一併刪除
            neighborList = neighbor.GetList();  // 取得鄰居列表
            if (voicerecorder != null)
                voicerecorder.Update_Info(neighborList);  // 重新設定
            relation.Set_NeighborList(neighborList);  // 重新設定
            relationList = relation.Get_RelationList();  // 重新取得關係列表
            if (Listener != relation.Get_Listener())  // 若為傾聽者離開
            {
                Listener = relation.Get_Listener();  // 重新取得傾聽者
                string str = SelfID.ToString() + " " +
                             TYPE_ADDLISTENER + " " +
                             Listener.ToString();
                Vast.VASTPublish(str, (uint)str.Length, AOI_RADIUS);
            }

            // 顯示在視窗上
            show.SelfInnformation_Print(aoi_center, Speaker_Capacity, Max_Speaker_Capacity, Overhearer_Capacity, Max_Overhearer_capacity, socketip, Listener, voicerecorder);
            myPanel.Refresh();  // 更新畫面
        }

        private void ReceiveAddListener(string[] split_str)                     // (廣播) 5 接收到要作為傾聽者加入的訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID
            ulong Add_ID = ulong.Parse(split_str[2]);         // index 2 為 欲傳送者 ID

            if (Add_ID != SelfID)  // 若訊息非傳送給自己, 就 drop 掉此訊息
                return;

            // 若已經有在傾聽者名單中, 就別再加入
            foreach (Node n in ListenerChildrenList)
                if (n.id == ID)
                    return;

            Console.WriteLine("加入到 傾聽list 中 : " + Add_ID);  // 輸出測試

            // 若不在的話加入 ListenerChildrenList 中
            foreach (Node n in neighborList)  // 尋找在 neighbor list 中
                if (n.id == ID)  // 若在 neighbor list 有找到
                    ListenerChildrenList.Add(n);  // 把它加入 listenerchildrenlist 中, 以便得知此鄰居的所有資訊
        }

        private void ReceiveExitListener(string[] split_str)                    // (廣播) 6 接收到要離開傾聽者的訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID
            ulong Exit_ID = ulong.Parse(split_str[2]);         // index 2 為 欲傳送者 ID

            if (Exit_ID != SelfID)  // 若訊息非傳送給自己, 就 drop 掉此訊息
                return;

            Console.WriteLine("離開 list : " + ID);  // 輸出測試

            // 從 ListenerChildrenList 中移除
            for (int i = 0; i < ListenerChildrenList.Count; i++)
                if (ID == ListenerChildrenList[i].id)
                    ListenerChildrenList.RemoveAt(i);
        }

        private void ReceivePort(string[] split_str)                            // (廣播) 8 接收到專屬的 port 訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID
            ulong Compare_ID = ulong.Parse(split_str[2]);   // index 2
            int socketport = int.Parse(split_str[3]);       // index 3 為 專屬的 port 

            if (SelfID != Compare_ID)  // 若此訊息不是給自己的, 就 drop 掉
                return;

            // 修改自己鄰居的 socket port
            neighbor.Update_SocketPort(ID, socketport);  // 重新設定 port
            neighborList = neighbor.GetList();  // 更改 port , 因此更新 List
        }

        private void ReceiveSocketVoiceMsg(string[] split_str)                  // (單播) 1 接收到語音訊息
        {
            byte[] data = new byte[split_str.Length - 3];  // 因為最後會收到空白字元 index 0 為辨別傳送資料類型
            for (int i = 2; i < split_str.Length - 3; i++)  // 把字串陣列轉成byte陣列
                data[i - 2] = byte.Parse(split_str[i]);

            Console.WriteLine("我的聲音檔 : " + data);  // 輸出測試
            //voicerecevier.GetVoiceData((int)data.Length, data);  // 解碼播出聲音
        }

        private void ReceiveSocketRebroadcast(string[] split_str)               // (單播) 7 接收到轉播訊息
        {
            ulong ID = ulong.Parse(split_str[0]);           // index 0 為 ID
            bool Is_Rebroadcast = bool.Parse(split_str[2]); // index 2 為 是否傳發
            if (!Is_Rebroadcast)  // 若不需要幫忙轉傳
                return;
            int number = int.Parse(split_str[3]);           // index 3 為 轉發人數
            List<ulong> rebroadcast = new List<ulong>();    // 用來儲存轉播者列表
            for (int i = 4; i < 4 + number; i++)
                rebroadcast.Add(ulong.Parse(split_str[i])); // index 4 ~ (4 + number - 1) 為 轉播者 ID
            string data = split_str[4 + number];            // index (4 + number) 為 資料

            List<Node> temp_rebroadcastlist = new List<Node>();  // 暫存要轉發的對象完整資料

            Console.WriteLine("我接收到的資料 : " + data);  // 輸出測試

            for (int i = 0, j = 0; i < neighborList.Count; i++)  // 搜尋鄰居清單, 從鄰居清單中抓取完整資料
                if (neighborList[i].id == rebroadcast[j])  // 若有符合者
                {
                    temp_rebroadcastlist.Add(neighborList[i]);  // 把完整資料加入 list 中
                    j++;
                }

            //Ak_Tree_Reboardcast(temp_rebroadcastlist);  // 傳送資料
        }

        private void ReceiveSocketTest(string[] split_str)                      // (單播) 單純測試@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        {
            foreach (string s in split_str)
            {
                Console.Write(s + " ");  // 輸出測試
                Console.WriteLine();  // 輸出測試
            }
        }

        private void UpdateNeighborInfomation()                                 // 更新鄰居資料 包含 鄰居列表 鄰居關係值 Listener
        {
            neighborList = neighbor.GetList();  // 取得鄰居清單

            // 設定與鄰居之間的關係值  ---------------------------------------------------------- 位置及面對的方向還要改一下，例如，背對背面對的方向是相反的
            relation.Set_NeighborList(neighborList);  // 設定鄰居清單
            relation.Update_RelationList(SelfDirect);  // 更新鄰居之關係值
            relationList = relation.Get_RelationList();  // 取得鄰居清單關係值

            // 依據 AutoSpeak 來選取 Listener
            if (AutoSpeak)  // 只有自動取得 Listener 才會, 選擇最高者
                Listener = relation.Get_Listener();  // 取得傾聽者
            else  // 手動 取得則要判斷是否在自己的範圍中
                if (!neighbor.Find(Listener.id))  // 若已經超出範圍
                    Listener = relation.Get_Listener();  // 自動取得關係值最高之傾聽者

            if (Listener.id != 0)  // 若有 Listener 時
            {
                if (Compare_Listener.id != Listener.id)  // 若先前的傾聽者跟此次的傾聽者不同人時
                {

                    // 先告知先前的 Listener 要離開
                    if (Compare_Listener.id != 0)  // 若先前的傾聽者不為空
                    {
                        // $$$$$$ 封包格式 : 自己的ID + 傳送資料型態 + 欲離開的ListenerID
                        string str_exit = SelfID.ToString() + " " +              // 自己的 ID
                                     TYPE_EXITLISTENER.ToString() + " " +   // 傳送離開訊息類型
                                     Compare_Listener.id.ToString();        // 先前 Listener ID

                        //Vast.VASTPublish(str_exit, (uint)str_exit.Length, AOI_RADIUS);  // 傳送廣播訊息給先前的 Listener 告知我要換 Listener
                    }

                    // 後告知當前的 Listener 要加入
                    // $$$$$$ 封包格式 : 自己的ID + 傳送資料型態 + 欲加入的ListenerID
                    string str_join = SelfID.ToString() + " " +              // 自己的 ID
                                 TYPE_ADDLISTENER.ToString() + " " +    // 傳送加入訊息類型
                                 Listener.id.ToString();                // 目前的 Listener ID

                    Thread.Sleep(50);  // 停止 0.05s 再傳送訊息
                    //Vast.VASTPublish(str_join, (uint)str_join.Length, AOI_RADIUS);  // 傳送廣播訊息給 Listener 告知我成為他的 ListenerChild
                    Compare_Listener = Listener;  // 先前的 Listener 被 目前的 Listener 取代
                }
            }
            else  // 若沒有 Listener 或 離開了 Listener 範圍
                if (Compare_Listener.id != 0)  // 若先前還有傾聽者時
                {
                    // $$$$$$ 封包格式 : 自己的ID + 傳送資料型態 + 欲加入的ListenerID
                    string str = SelfID.ToString() + " " +
                                 TYPE_EXITLISTENER.ToString() + " " +   // 要傳送的訊息
                                 Compare_Listener.id.ToString();        // 先前 Listener ID

                    //Vast.VASTPublish(str, (uint)str.Length, AOI_RADIUS);  // 傳送廣播訊息給先前的 Listener 告知我要換 Listener
                    Compare_Listener = new Node(0, "0", null, 0, 0, 0, 0);
                }

            // 更新監聽 Thread 的資訊
            for (int i = 0; i < listenserverlist.Count; i++)
                listenserverlist[i].Update_Info(aoi_center, SelfDirect, neighborList, Listener.id);

            // 更新傳送語音裡面的資訊
            if (voicerecorder != null)
                voicerecorder.Update_Info(neighborList);

            // 顯示在視窗上
            show.SelfInnformation_Print(aoi_center, Speaker_Capacity, Max_Speaker_Capacity, Overhearer_Capacity, Max_Overhearer_capacity, socketip, Listener, voicerecorder);
            myPanel.Refresh();
        }

        private void GetMovements(KeyEventArgs e)                               // 用方向鍵控制節點上下左右移動
        {
            const int DIS = 15;  // 移動一次的距離

            // 先決定移動之後的座標
            if (e.KeyCode.Equals(Keys.Left) || e.KeyCode.Equals(Keys.End) || e.KeyCode.Equals(Keys.Home))
                aoi_center.X -= DIS;
            if (e.KeyCode.Equals(Keys.Right) || e.KeyCode.Equals(Keys.PageDown) || e.KeyCode.Equals(Keys.PageUp))
                aoi_center.X += DIS;
            if (e.KeyCode.Equals(Keys.Down) || e.KeyCode.Equals(Keys.End) || e.KeyCode.Equals(Keys.PageDown))
                aoi_center.Y += DIS;
            if (e.KeyCode.Equals(Keys.Up) || e.KeyCode.Equals(Keys.Home) || e.KeyCode.Equals(Keys.PageUp))
                aoi_center.Y -= DIS;

            // 再選定移動之後的方向
            switch (e.KeyCode)
            {
                case Keys.End:          // 左下
                    SelfDirect = LEFT_DOWN;
                    break;
                case Keys.Down:         // 下
                    SelfDirect = DOWN;
                    break;
                case Keys.PageDown:     // 右下
                    SelfDirect = RIGHT_DOWN;
                    break;
                case Keys.Left:         // 左
                    SelfDirect = LEFT;
                    break;
                case Keys.Right:        // 右
                    SelfDirect = RIGHT;
                    break;
                case Keys.Home:         // 左上
                    SelfDirect = LEFT_UP;
                    break;
                case Keys.Up:           // 上
                    SelfDirect = UP;
                    break;
                case Keys.PageUp:       // 右上
                    SelfDirect = RIGHT_UP;
                    break;
            }

            // 更新鄰居清單, 此neighbor.Update沒加入在UpdateNeighborInfomation中是因為 "自己移動" 與 "他人移動" 的參數不同
            neighbor.Update(AOI_RADIUS + AOI_REDUNDANT, aoi_center.X, aoi_center.Y, listenserverlist);  // 自己在移動時也要更新自己的鄰居, 因為 VAST 中的距離多 C# 10 個單位
            UpdateNeighborInfomation();  // 更新 function

            // 廣播自己的座標, 避免"離開"時別人畫面會收不到
            PublishPosition(SelfDirect);  // ** 廣播自己的座標, 並廣播自己的面向

            Vast.VASTMove(aoi_center.X, aoi_center.Y);

            // 廣播自己的座標, 避免"加入"時別人畫面會收不到
            PublishPosition(SelfDirect);  // ** 廣播自己的座標, 並廣播自己的面向
        }

        private void Key_Down(object sender, KeyEventArgs e)                    // 只有輸入 enter 鍵 及 backspace 鍵 及 上下左右移動鍵 及 F1鍵時會進來  (處理功能鍵)
        {
            if (e.KeyCode.Equals(Keys.Left) || e.KeyCode.Equals(Keys.Right) || e.KeyCode.Equals(Keys.Down) || e.KeyCode.Equals(Keys.Up) ||
                e.KeyCode.Equals(Keys.End) || e.KeyCode.Equals(Keys.Home) || e.KeyCode.Equals(Keys.PageDown) || e.KeyCode.Equals(Keys.PageUp))
                GetMovements(e);
            else if (e.KeyCode.Equals(Keys.Back))  // 倒退鍵
            {
                if (input.Length > 0)  // 若還有字元就刪除
                    input = input.Remove(input.Length - 1);
                keyin.Text = input;  // 重新顯示在視窗上
            }
            else if (e.KeyCode.Equals(Keys.Enter))  // 輸入鍵
            {
                int len = SelfID.ToString().Length + TYPE_TEXT.ToString().Length + 2;  // 加 2 為 兩格空格 ID + 資料型態的長度字串
                Vast.VASTPublish((SelfID.ToString() + " " + TYPE_TEXT.ToString() + " " + input), (uint)(input.Length + len), AOI_RADIUS);  // 傳送訊息給 aoi 範圍裡的人  +2 因為前面的辨試的關係 
                show.Output_Monitor(input);
                keyin.Text = input = "[" + SelfName + "] : ";  // 清除輸入文字的區塊
            }
            else if (e.KeyCode.Equals(Keys.F1))  // F1 鍵
            {
                // 初始化錄音部份
                if (voicerecorder == null)
                {
                    voicerecorder = new VoiceRecorder();
                    voicerecorder.intptr = this.Handle;
                    voicerecorder.InitiDevice(MIC_SPEAK);  // 初始化 recorder
                    voicerecorder.Update_Info(neighborList);  // 把鄰居資訊丟進去
                    voicerecorder.StartVoiceCapture();
                }
            }
            else if (e.KeyCode.Equals(Keys.F2))  // F2 鍵為關閉語音送收訊息
            {
                if (voicerecorder != null)
                {
                    voicerecorder.Stoprec();
                    voicerecorder = null;
                }
            }
            else if (e.KeyCode.Equals(Keys.F3))
            {
                for (int i = 0; i < neighborList.Count; i++)
                {
                    Console.WriteLine(neighborList[i].name + " " + neighborList[i].socketip + " " + neighborList[i].socketport);
                }

                Console.WriteLine("server numbers : " + listenserverlist.Count);
            }
            else if (e.KeyCode.Equals(Keys.F7))  // 測試 voice thread
            {
                foreach (Node n in neighborList)
                    Console.WriteLine("id : " + n.id + " distance : " + n.distance);
                foreach (Relation r in relationList)
                    Console.WriteLine("id : " + r.id + " relation : " + r.relationship);
                Console.WriteLine(Listener.id);
            }
            else if (e.KeyCode.Equals(Keys.F8))
            {
                Console.WriteLine( Vast.VASTGetLayer());
            }
            else if (e.KeyCode.Equals(Keys.F9))
            {
                Vast.VASTReserveLayer(1);
            }
            else if (e.KeyCode.Equals(Keys.F11))  // 用來選擇 Listener
            {
                Listener New_Select_Speak = new Listener(this);
                New_Select_Speak.ShowDialog(this);  // 強制回應視窗
                myPanel.Refresh();
            }
            else if (e.KeyCode.Equals(Keys.F12))  // 用來調整想上傳之頻寬
            {
                Channel New_Channel_Select = new Channel(this);
                New_Channel_Select.ShowDialog(this);
            }
            // 顯示在視窗上
            show.SelfInnformation_Print(aoi_center, Speaker_Capacity, Max_Speaker_Capacity, Overhearer_Capacity, Max_Overhearer_capacity, socketip, Listener, voicerecorder);
        }

        private void Key_Press(object sender, KeyPressEventArgs e)              // 輸入文字時會進來
        {
            if ((int)e.KeyChar != 13 && (int)e.KeyChar != 8)  // enter 鍵 或 backspace 鍵 就不能增加字元
            {
                input += e.KeyChar;
                keyin.Text = input;
            }
        }

        private void Render(object sender, PaintEventArgs e)                    // 畫在 Form 上的觸發事件
        {
            render.Show(aoi_center, AOI_RADIUS + AOI_REDUNDANT, neighborList, SelfName, SelfDirect, Listener.id);  // 因為 VAST 中的距離多差 10 個單位
        }

        private void Quit(object sender, FormClosingEventArgs e)                // 關閉視窗時候做的事
        {
            string str = Vast.VASTGetSelfID().ToString() + " " + TYPE_EXIT.ToString();
            Vast.VASTPublish(str, (uint)str.Length, AOI_RADIUS);  // 離開時發送離開訊息

            if (tick_thread != null && tick_thread.IsAlive)
            {
                tick_thread.Abort();
                tick_thread.Join();
            }
            if (recmsg_thread != null && recmsg_thread.IsAlive)  // 關閉監聽訊息之 thread
            {
                recmsg_thread.Abort();
                recmsg_thread.Join();
            }
            if (voicerecorder != null)  // 若 recorder 不為 null 時
                voicerecorder.Stoprec();
            Vast.VASTLeave();
            Vast.ShutVAST();
        }

        private void Picture_Click(object sender, EventArgs e)                  // 點擊到圖片時
        {
            int c_pic = 0;  // 選擇點擊到哪個圖片
            int width = 0;
            int height = 0;

            if (sender.Equals(Goya))                // 點擊到 戈耶
            {
                c_pic = 1;
                width = 600;
                height = 416;
            }
            if (sender.Equals(Michelangel))         // 點擊到 米開朗基羅
            {
                c_pic = 2;
                width = 600;
                height = 270;
            }
            if (sender.Equals(Miro))                // 點擊到 米羅
            {
                c_pic = 3;
                width = 600;
                height = 439;
            }
            if (sender.Equals(Monet))               // 點擊到 莫內
            {
                c_pic = 4;
                width = 600;
                height = 457;
            }
            if (sender.Equals(Picasso))             // 點擊到 畢卡索
            {
                c_pic = 5;
                width = 500;
                height = 346;
            }
            if (sender.Equals(Pissarro))            // 點擊到 畢莎羅
            {
                c_pic = 6;
                width = 600;
                height = 495;
            }
            if (sender.Equals(Rousseau))            // 點擊到 盧梭
            {
                c_pic = 7;
                width = 500;
                height = 321;
            }
            if (sender.Equals(VanGogh))             // 點擊到 梵谷
            {
                c_pic = 8;
                width = 600;
                height = 463;
            }

            Picture pic = new Picture(c_pic, width, height);
            pic.Show();
        }
    }
}