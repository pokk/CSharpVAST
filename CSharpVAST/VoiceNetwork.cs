using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CSharpVAST
{
    class VoiceNetwork
    {
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

        const int MAX_PAN = 2500;       // 為左右聲道最大值
        private ulong ID = 0;  // 為誰開的監聽 thread
        private ulong listener = 0;  // 目前傾聽者
        private List<Node> neighborlist;  // 目前鄰居列表
        private IPEndPoint sip;  // 當server用的
        private IPEndPoint cip;  // 當client用的
        private Socket socket;
        private IPEndPoint sender;  // 當server用的接收client的
        private EndPoint remote;  // 遠端的接口
        private byte[] data;  
        private Thread listenthread;
        private VoiceRecorder voicereceiver;  // 用來播放聲音
        private int aoi_radius;  // 傳送半徑
        private int SelfDirect;  // 自己的面向
        private Point aoi_center;  // 自己的位置
        public PictureBox ShowVoice { get; set; }
        public IntPtr intprt { get; set; }
        public int port { get; set; }
        

        public void Listen_Server(ulong id, int socketport)  // 開啟接收聲音的 server
        {
            ID = id;  // 設定為誰開的 thread
            port = socketport;
            sip = new IPEndPoint(IPAddress.Any, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(sip);
            // 以上設定 server 完成, 針對於每個 user 設定不同的 port
        }

        public void Voice_Client(string ip, int socketport)  // 設定聲音傳輸的對象
        {
            cip = new IPEndPoint(IPAddress.Parse(ip), socketport);  // server ip
            // 以上設定 client 完成, 針對每個 user 設定不同 ip 及 port
        }

        public void InitReceiver(IntPtr intptr, int radius)  // 初始化接收聲音者
        {
            aoi_radius = radius;
            voicereceiver = new VoiceRecorder();
            voicereceiver.intptr = intptr;
            voicereceiver.InitiDevice(2);
        }

        public void InitSender(IntPtr intptr)  // 初始化傳送聲音者
        {
            voicereceiver = new VoiceRecorder();
            voicereceiver.client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            voicereceiver.intptr = intptr;
            voicereceiver.InitiDevice(1);
        }

        public void Start_Listen()  // 使 server 用 thread 去監聽訊息
        {
            listenthread = new Thread(new ThreadStart(Server_Receive));
            listenthread.IsBackground = true;
            listenthread.Start();
        }

        public void Server_Receive()  // 接收訊息的部份
        {
            data = new byte[10000];
            sender = new IPEndPoint(IPAddress.Any, 0);  // 可以接收任何訊息
            remote = (EndPoint)(sender);

            while (true)
            {
                if (socket.Poll(5000, SelectMode.SelectRead))
                {
                    socket.BeginReceiveFrom(data, 0, data.Length, SocketFlags.None, ref remote, new AsyncCallback(ReceiveData), null);  // 每5ms查詢一下網路, 如果有可讀取的數據就接收
                }
            }
        }

        private void ReceiveData(IAsyncResult iar)  // 接收數據
        {
            int intRecv = 0;
            try
            {
                intRecv = socket.EndReceiveFrom(iar, ref remote);
            }
            catch
            {
                throw new Exception();
            }
            if (intRecv > 0)
            {
                byte[] bytReceivedData = new byte[intRecv];
                Array.Copy(data, bytReceivedData, intRecv);
                
                // 做傾聽者的判斷
                bool isListener = false;  // 是否為傾聽者
                if (ID == listener)  // 判斷資料是否是傾聽者之聲音
                    isListener = true;

                // 做左右聲道, 音量大小的控制
                int pan = 0;  // 左右聲道值大小
                int volume = 0;  // 聲音大小聲
                int index = -1;  // 尋找此資料是在鄰居中哪個人的 index

                // 找尋為此 thread 開啟者在鄰居列表中的 index 為何
                for (int i = 0; i < neighborlist.Count; i++ )
                    if (neighborlist[i].id == ID)
                    {
                        index = i;
                        break;
                    }
                if (index == -1)  // 表示找不到這位鄰居
                    return;

                Voice_PanVolume(index, ref pan, ref volume);  // 計算聲音大小, 左右聲道

                voicereceiver.GetVoiceData(intRecv, bytReceivedData, isListener, pan, volume, ShowVoice);  // 調用聲音模組中的 GetVoiceData 來從字節數中獲取聲音並播放
            }
        }

        public void Stop_Thread()  // 停止 thread 的監聽
        {
            if (listenthread != null && listenthread.IsAlive == true)
            {
                listenthread.Abort();
            }
        }

        public void Update_Info(Point pos, int direct, List<Node> neighbor, ulong listener)  // 更新目前位置, 目前面向, 鄰居資訊, 目前傾聽者 
        {
            aoi_center = pos;
            SelfDirect = direct;
            this.listener = listener;
            neighborlist = neighbor;
        }

        public ulong Get_ID()  // 取得此物件是為誰開的 thread
        {
            return ID;
        }

        private void Voice_PanVolume(int index, ref int pan, ref int volume)    // 判斷左右聲道及位置的大小聲
        {
            // 計算送者為在自身的左邊還是右邊, 以方便來計算左右聲道的大小聲
            // (收送者之間距離 / AOI距離) * 2500    (雖然左右聲道最大最小值為 -10000~10000, 但到 2500 之後就不明顯, 所以採用到 2500)
            const int REDUCE_VOLUME = -500;  // 降低聲低
            float other_x = neighborlist[index].x;
            float other_y = neighborlist[index].y;

            if (SelfDirect == DOWN)  // 面向朝下
                if (other_y >= aoi_center.Y)  // 傳送聲音者在自己的前方, 分左右聲道但不分大小聲
                {
                    pan = (int)((((float)aoi_center.X - other_x) / (aoi_radius)) * MAX_PAN);
                    volume = 0;
                }
                else  // 傳送聲音者在自己的後方, 不分左右聲道但聲音大小降低
                {
                    pan = 0;
                    volume = REDUCE_VOLUME;
                }
            else if (SelfDirect == LEFT)  // 面向朝左
                if (other_x <= aoi_center.X)  // 傳送聲音者在自己的前方, 分左右聲道但不分大小聲
                {
                    pan = (int)((((float)aoi_center.Y - other_y) / (aoi_radius)) * MAX_PAN);
                    volume = 0;
                }
                else  // 傳送聲音者在自己的後方, 不分左右聲道但聲音大小降低
                {
                    pan = 0;
                    volume = REDUCE_VOLUME;
                }
            else if (SelfDirect == RIGHT)  // 面向朝右
                if (other_x >= aoi_center.X)  // 傳送聲音者在自己的前方, 分左右聲道但不分大小聲
                {
                    pan = (int)(((other_y - (float)aoi_center.Y) / (aoi_radius)) * MAX_PAN);
                    volume = 0;
                }
                else  // 傳送聲音者在自己的後方, 不分左右聲道但聲音大小降低
                {
                    pan = 0;
                    volume = REDUCE_VOLUME;
                }
            else if (SelfDirect == UP)  // 面向朝上
                if (other_y <= aoi_center.Y)  // 傳送聲音者在自己的前方, 分左右聲道但不分大小聲
                {
                    pan = (int)(((other_x - (float)aoi_center.X) / (aoi_radius)) * MAX_PAN);
                    volume = 0;
                }
                else  // 傳送聲音者在自己的後方, 不分左右聲道但聲音大小降低
                {
                    pan = 0;
                    volume = REDUCE_VOLUME;
                }
        }
    }
}
