using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms; 

namespace CSharpVAST
{
    class ShowInfo
    {
        const int SIZE_TEXTAREA = 7;  // 顯示之文字最多行數
        Label SelfInformation;
        Label keyout;
        Queue<String> inputMsg;  // 來儲存顯示文字的

        public ShowInfo(Label selfinfo, Label keyout)  // 把 Form 裡面所需要顯示的 Label 都傳進來
        {
            SelfInformation = selfinfo;
            this.keyout = keyout;
            inputMsg = new Queue<String>();
        }

        public void Output_Monitor(String output)  // 把訊息印在視窗上面
        {
            // 顯示在視窗上的聊天文字
            inputMsg.Enqueue(output);
            if (inputMsg.Count > SIZE_TEXTAREA)  // 超過 SIZE_TEXTAREA 以上就要刪減
                inputMsg.Dequeue();
            // 印出文字
            keyout.Text = "";
            foreach (String str in inputMsg)
                keyout.Text += str + "\n";
        }

        public void SelfInnformation_Print(Point center, int sp_ca, int max_sp_ca, int oh_ca, int max_oh_ca, string selfsocketip, Node listener, VoiceRecorder MIC)  // 把自己的資訊印出來
        {
            SelfInformation.Text = "OriginID : " + Vast.VASTGetSelfID().ToString()
                                 + "  x : " + center.X.ToString()
                                 + "  y : " + center.Y.ToString()
                                 + "  Listen Capacity : " + sp_ca.ToString() + " / " + max_sp_ca.ToString()
                                 + "  Overheaer Capacity : " + oh_ca.ToString() + " / " + max_oh_ca.ToString()
                                 + "\n"
                                 + "IP : " + selfsocketip;
            if (listener != null)
                SelfInformation.Text += "  Listener : " + listener.name.ToString();
            else
                SelfInformation.Text += "  Listener : 0";
            if (MIC != null)
                SelfInformation.Text += "  MIC : ON";
            else
                SelfInformation.Text += "  MIC : OFF";
        }
    }
}
