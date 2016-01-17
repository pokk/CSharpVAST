using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms; 
using Microsoft.DirectX.DirectSound;

namespace CSharpVAST
{
    class VoiceRecorder
    {
        public IntPtr intptr { get; set; }  // 窗口句柄
        private int iNotifySize = 4410;  // 通知所在區域大小
        private int iNotifyNum = 10;  // 通知的個數
        private int MIC_SPEAK = 1;  // 初始化錄音裝置
        private int MIC_LISTEN = 2;  // 初始化播放中置
        private int ACCURACY = 10;  // 聲音大小標準
        private List<Node> neighborlist = null;  // 設定鄰居列表

        #region -----------------------播放設置變數-----------------------
        private Device PlayDev = null;  // 播放設備對象
        private MemoryStream memstream;  // 内存流
        private SecondaryBuffer secBuffer;  // 輔助缓衝區
        private BufferDescription buffDiscript;
        #endregion

        #region -----------------------錄音設置變數-----------------------
        private Notify myNotify = null;  // 缓衝區提示事件
        private int iBufferOffset = 0;  // 本次數據起始點，上一次數據的终點。
        private int iBufferSize = 0;  // 缓衝區大小
        private Capture capture = null;  // 捕捉設備對象
        private CaptureBuffer capturebuffer = null;  // 捕捉緩衝區
        private AutoResetEvent notifyevent = null;
        private Thread notifythread = null;
        private WaveFormat mWavFormat;  // PCM格式
        private IPEndPoint cip = null;  // 設定ip傳送
        public Socket client { get; set; }
        #endregion

        #region 初始化這裡面的動作  ------------------------------------------------------------------------------------ First Inition
        public void InitiDevice(int ch)
        {
            // 初始化的部份 (1) 補抓緩衝區 (2) 播放緩衝區
            // ==============(1)==============
            if (ch == MIC_SPEAK)
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                if (!CreateCaputerDevice())  // 初始抓錄音設備的  --------------------------------------------------- 1
                    throw new Exception();
                CreateCaptureBuffer();  // 創建一個錄音緩衝，開始錄音  ---------------------------------------------- 2
                CreateNotification();  // 建立通知消息，當緩衝滿了的時候處理方法  ----------------------------------- 3
                Console.WriteLine("Start to prepare recorder...");
            }
            // ==============(2)==============
            if (ch == MIC_LISTEN)
            {
                if (!CreatePlayDevice())  // 初始播放設備的  -------------------------------------------------------- 4
                    throw new Exception();
                CreateSecondaryBuffer();  //  ----------------------------------------------------------------------- 5
                Console.WriteLine("Start to prepare listener...");
            }
        }
        #endregion

        #region 創建補抓錄音設備對象   <returns>如果創建成功返回true</returns>  ---------------------------------------- (1)
        public bool CreateCaputerDevice()
        {
            // 首先要枚舉可用的捕捉設備
            CaptureDevicesCollection capturedev = new CaptureDevicesCollection();
            Guid devguid;
            if (capturedev.Count > 0)
            {
                devguid = capturedev[0].DriverGuid;  // 0 為音效卡本身之錄音
                //devguid = capturedev[2].DriverGuid;  // *********************** 這台電腦的錄音設備為 2
            }
            else
            {
                Console.WriteLine("當前沒有可用於音頻捕捉的設備", "系统提示");
                return false;
            }
            // 利用設備 GUID 來建立一個捕捉設備對象
            capture = new Capture(devguid);
            return true;
        }
        #endregion

        #region 設定音頻格式，如採樣率等    <returns>設置完成後的格式</returns>  --------------------------------------- (2-1)
        public WaveFormat SetWaveFormat()
        {
            WaveFormat format = new WaveFormat();
            format.FormatTag = WaveFormatTag.Pcm;  // 設置音頻類型
            format.SamplesPerSecond = 11025;  // 採樣率（單位：赫兹）典型值：11025、22050、44100Hz
            format.BitsPerSample = 16;  // 採樣位數
            format.Channels = 1;  // 聲道
            format.BlockAlign = (short)(format.Channels * (format.BitsPerSample / 8));  // 單位採樣點的字節數
            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond;
            return format;
            // 按照以上採樣規格，可知採樣1秒鐘的字節數為 22050*2=55100 Bits 約為 53KBytes
        }
        #endregion

        #region 創建補抓錄音緩衝區對象  -------------------------------------------------------------------------------- (2)
        public void CreateCaptureBuffer()
        {
            // 想要創建一個捕捉緩衝區必须要兩個參數：缓衝區信息（描述這個緩衝區中的格式等），緩衝設備。
            mWavFormat = SetWaveFormat();  // 先設定waveformat格式
            CaptureBufferDescription bufferdescription = new CaptureBufferDescription();
            bufferdescription.Format = mWavFormat;  // 設置缓衝區要捕捉的數據格式
            //iNotifySize = 1024;  // 設置通知大小
            iNotifySize = mWavFormat.AverageBytesPerSecond / iNotifyNum;  // 1秒的數據量 / 設置的通知數得到的每個通知大小小於0.2s的數據量，語音延遲小於200ms為優質語音
            iBufferSize = iNotifyNum * iNotifySize;
            bufferdescription.BufferBytes = iBufferSize;

            bufferdescription.ControlEffects = true;
            bufferdescription.WaveMapped = true;

            capturebuffer = new CaptureBuffer(bufferdescription, capture);  // 建立設備緩衝區對象
        }
        #endregion

        #region  設置通知  ---------------------------------------------------------------------------------------------- (3)
        public void CreateNotification()
        {
            BufferPositionNotify[] bpn = new BufferPositionNotify[iNotifyNum];  // 設置缓衝區通知個數
            // 設置通知事件
            notifyevent = new AutoResetEvent(false);
            notifythread = new Thread(new ThreadStart(RecodeData));  // 通知觸發事件
            notifythread.Start();

            for (int i = 0; i < iNotifyNum; i++)
            {
                bpn[i].Offset = iNotifySize + i * iNotifySize - 1;  // 設置具體每個的位置
                bpn[i].EventNotifyHandle = notifyevent.SafeWaitHandle.DangerousGetHandle();
            }
            myNotify = new Notify(capturebuffer);
            myNotify.SetNotificationPositions(bpn);
        }
        #endregion

        #region thread中的事件 --> 錄音  ------------------------------------------------------------------------------- (3-1)
        public void RecodeData()
        {
            while (true)
            {
                // 等待缓衝區的通知消息
                notifyevent.WaitOne(Timeout.Infinite, true);
                // 錄製數據  及  傳輸數據
                RecordCapturedData();
            }
        }
        #endregion

        #region    真正轉移數據的事件，其實就是把數據傳送到網路上去。  ---------------------------------------------------- (3-2)
        public void RecordCapturedData()
        {
            byte[] capturedata = null;
            int readpos = 0, capturepos = 0, locksize = 0;
            capturebuffer.GetCurrentPosition(out capturepos, out readpos);
            locksize = readpos - iBufferOffset;  // 這個大小就是我門可以安全索取的大小
            if (locksize == 0)
            {
                return;
            }
            if (locksize < 0)
            {  // 因為我們是循環的使用缓衝區，所以有一種情况下為負：當文以載讀指針回到第一個通知點，而Ibuffeoffset還在最後一個通知處
                locksize += iBufferSize;
            }

            capturedata = (byte[])capturebuffer.Read(iBufferOffset, typeof(byte), LockFlag.FromWriteCursor, locksize);

            iBufferOffset += capturedata.Length;
            iBufferOffset %= iBufferSize;  // 取mod是因為缓衝區是循環的。

            double[] _waveLeft = new double[(capturedata.Length / 4) + 1];

            int h = 0;
            for (int i = 0; i < capturedata.Length - 1; i += 4)
            {
                _waveLeft[h] = (double)BitConverter.ToInt16(capturedata, i);
                h++;
            }

            double sum = 0;
            foreach (double item in _waveLeft)
            {
                if (item > 0)
                    sum += item;
                else
                    sum -= item;
            }
            int result = (int)(((sum / _waveLeft.Length)) / 32768 * 100);

            if (result >= ACCURACY)  // 聲音夠大聲, 表示有講話才傳送資料
                try
                {
                    //Vast.VASTPublish((Vast.VASTGetSelfID().ToString() + " 2 " + voice_data), (uint)(voice_data.Length + len), aoi_radiuis);  // 廣播
                    //GetVoiceData(capturedata.Length, capturedata);  // 直接接收語音訊息播放

                    for (int i = 0; i < neighborlist.Count; i++)  // 對每位鄰居做廣播的動作
                    {
                        cip = new IPEndPoint(IPAddress.Parse(neighborlist[i].socketip), neighborlist[i].socketport);
                        EndPoint EpServer = (EndPoint)(cip);
                        client.SendTo(capturedata, EpServer);  // 傳送語音
                    }
                }
                catch (Exception e)
                {
                    throw new Exception();
                }
        }
        #endregion

        private int intPosWrite = 0;  // 内存流中寫指針位移
        private int intPosPlay = 0;  // 内存流中播放指針位移
        private int intNotifySize = 5000;  // 設置通知大小

        #region 以自節數組中獲取音頻數據，並進行播放    <param name="intRecv">字節數組長度</param>    <param name="bytRecv">包含音頻數據的字節數組</param>  ------------------------------------- (3-3)
        public void GetVoiceData(int intRecv, byte[] bytRecv, bool listener, int pan_val, int vol, PictureBox ShowVoice)
        {
            //new Bitmap(Properties.Resources.Voice);
            // intPosWrite指示最新的數據寫好後的末尾。 intPosPlay指示本次播放開始的位置。
            if (intPosWrite + intRecv <= memstream.Capacity)
            {
                // 如果當前寫指針所在的位移 + 將要寫入到緩衝區長度小於缓衝區總大小
                if ((intPosWrite - intPosPlay >= 0 && intPosWrite - intPosPlay < intNotifySize) || (intPosWrite - intPosPlay < 0 && intPosWrite - intPosPlay + memstream.Capacity < intNotifySize))
                {
                    memstream.Write(bytRecv, 0, intRecv);
                    intPosWrite += intRecv;
                }
                else if (intPosWrite - intPosPlay >= 0)
                {
                    // 先儲存一定量的數據，當達到一定數據量時就播放聲音。
                    buffDiscript.BufferBytes = intPosWrite - intPosPlay;  // 缓衝區大小為播放指針到寫指針之間的距離。
                    SecondaryBuffer sec = new SecondaryBuffer(buffDiscript, PlayDev);  // 建立一個合適的缓衝區用於播放這段數據。
                    sec.Volume = vol;  // 聲音大小, 設定為最大聲
                    if (!listener)  // 若此資料是偶聽者的資料
                        if (sec.Volume == -500)  // 若為在後面者
                            sec.Volume = -1000;  // 聲音大小聲再降低
                    sec.Pan = pan_val;  // 左聲道 max_value -10000, 右聲道 max_value 10000
                    memstream.Position = intPosPlay;  // 先將memstream的指針定位到達一次播放開始的位置
                    sec.Write(0, memstream, intPosWrite - intPosPlay, LockFlag.FromWriteCursor);
                    sec.Play(0, BufferPlayFlags.Default);  // 播放聲音
                    memstream.Position = intPosWrite;  // 寫完後重新將memstream的指針定位到將要寫下去的位置。
                    intPosPlay = intPosWrite;
                    ShowVoice.Image = new Bitmap(Properties.Resources.Voice);
                }
                else if (intPosWrite - intPosPlay < 0)
                {
                    buffDiscript.BufferBytes = intPosWrite - intPosPlay + memstream.Capacity;  // 缓衝區大小為播放指針到寫指針之間的距離。
                    SecondaryBuffer sec = new SecondaryBuffer(buffDiscript, PlayDev);  // 建立一個合適的缓衝區用於播放這段數據。
                    sec.Volume = vol;  // 聲音大小, 設定為最大聲
                    if (!listener)  // 若此資料是偶聽者的資料
                        if (sec.Volume == -500)  // 若為在後面者
                            sec.Volume = -1000;  // 聲音大小聲降低
                    sec.Pan = pan_val;  // 左聲道, 右聲道
                    memstream.Position = intPosPlay;
                    sec.Write(0, memstream, memstream.Capacity - intPosPlay, LockFlag.FromWriteCursor);
                    memstream.Position = 0;
                    sec.Write(memstream.Capacity - intPosPlay, memstream, intPosWrite, LockFlag.FromWriteCursor);
                    sec.Play(0, BufferPlayFlags.Default);  // 播放聲音
                    memstream.Position = intPosWrite;
                    intPosPlay = intPosWrite;
                    ShowVoice.Image = new Bitmap(Properties.Resources.Voice);
                }
            }
            else
            {
                // 當數據將要大於memstream可容納的大小時
                int irest = memstream.Capacity - intPosWrite;  // memstream中剩下的可容纳的字節數。
                memstream.Write(bytRecv, 0, irest);  // 先寫完這個內存流。
                memstream.Position = 0;  // 然後讓新的數據從memstream的0位置開始記錄
                memstream.Write(bytRecv, irest, intRecv - irest);  // 覆蓋舊的數據
                intPosWrite = intRecv - irest;  // 更新寫指針位置。寫指針指示下一個開始寫入的位置而不是上一次结束的位置，因此不用减一
            }
        }
        #endregion

        #region 創建用於播放的音頻設備對象     <returns>创建成功返回true</returns>  ------------------------------------ (4)
        public bool CreatePlayDevice()
        {
            DevicesCollection dc = new DevicesCollection();
            Guid g;
            if (dc.Count > 0)
                g = dc[0].DriverGuid;  // 0 為音效卡本身之播放
            else
                return false;
            PlayDev = new Device(g);
            PlayDev.SetCooperativeLevel(intptr, CooperativeLevel.Normal);
            return true;
        }
        #endregion

        #region 創建播放輔助緩衝區  ------------------------------------------------------------------------------------ (5)
        public void CreateSecondaryBuffer()
        {
            buffDiscript = new BufferDescription();
            WaveFormat mWavFormat = SetWaveFormat();
            buffDiscript.Format = mWavFormat;
            iNotifySize = mWavFormat.AverageBytesPerSecond / iNotifyNum;  // 設置通知大小
            iBufferSize = iNotifyNum * iNotifySize;
            buffDiscript.BufferBytes = iBufferSize;
            buffDiscript.ControlPan = true;
            buffDiscript.ControlFrequency = true;
            buffDiscript.ControlVolume = true;
            buffDiscript.GlobalFocus = true;
            byte[] bytMemory = new byte[100000];
            memstream = new MemoryStream(bytMemory, 0, 100000, true, true);
            secBuffer = new SecondaryBuffer(buffDiscript, PlayDev);
        }
        #endregion

        #region 開始擷取聲音  ------------------------------------------------------------------------------------------ Second Start
        public void StartVoiceCapture()  // 開始錄音
        {
            capturebuffer.Start(true);
        }
        #endregion

        #region 停止錄音  ---------------------------------------------------------------------------------------------- Finally
        public void Stoprec()  
        {
            capturebuffer.Stop();  // 條用缓衝區的停止方法。停止採集聲音
            if (notifyevent != null)
                notifyevent.Set();
            if (notifythread != null && notifythread.IsAlive == true)
            {
                notifythread.Abort();
                notifythread.Join();
            }
        }
        #endregion

        public void Update_Info(List<Node> neighbor)  // 更新資訊 鄰居
        {
            neighborlist = neighbor;
        }
    }
}
