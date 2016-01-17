using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CSharpVAST
{
    public partial class Listener : Form
    {
        internal Game Form_Game = null;  // VAST GAME的主視窗
        List<Relation> temp_relationlist = null;  // 暫存的 relationlist
        List<Node> temp_neighborlist = null;  // 暫存的 neighborlist
        ulong SelectListener;  // 選到的傾聽者
        string SelectListenerName;  // 選到的傾聽者名字

        public Listener()
        {
            InitializeComponent();
        }

        public Listener(Game temp)
        {
            InitializeComponent();
            Form_Game = temp;  // 接收 Vast 的視窗資料
        }

        private void Speaker_Load(object sender, EventArgs e)  // 開啟視窗必需先設定好
        {
            if (Form_Game.AutoSpeak)
                AutoRelation.Select();  // 自動 被選取
            else
                ManualRelation.Select();  // 手動 被選取
            temp_relationlist = Form_Game.relationList;  // 先把關係值list讀取進來
            temp_neighborlist = Form_Game.neighborList;  // 先把鄰居list讀取進來
            temp_relationlist.Sort(new IcpRelation());  // 把關係值由大排到小

            // 把鄰居的關係值都顯示在 RankBox 中
            foreach (Relation r in temp_relationlist)
                RankList.Items.Add(r.name + "\t\t" + Math.Round(r.relationship, 3));
            Listener_Now.Text = "Now Listener is :       " + Form_Game.Listener.name;  // 印出現在的傾聽者
        }

        private void RankList_SelectedIndexChanged(object sender, EventArgs e)  // 點選到 RankBox 會發生的事
        {
            if (ManualRelation.Checked)  // 若 手動 被選取
                if (RankList.SelectedIndex != -1)  // 因為剛開始沒點選到為 -1, 要確保沒點選到時亂點其他地區
                {
                    SelectListener = temp_relationlist[RankList.SelectedIndex].id;  // 先取得 id
                    SelectListenerName = temp_relationlist[RankList.SelectedIndex].name;  // 先取得 名字
                    Listener_Selected.Text = "Select Lietenter is :    " + SelectListenerName;  // 印出選到的傾聽者
                }
        }

        private void ManualRelation_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoRelation.Checked)  // 只要 Auto 被選取到, Select Listener 都不顯示
                Listener_Selected.Text = "Select Lietenter is :";
            else
                if (RankList.SelectedIndex > 0)  // 確保沒有選到不會發生例外
                    Listener_Selected.Text = "Select Lietenter is :    " + Form_Game.relationList[RankList.SelectedIndex].name;  // 印出選到的傾聽者　
        }

        private void Btn_OK_Click(object sender, EventArgs e)  // 按下 OK 按鍵
        {
            if (RankList.SelectedIndex < 0 && ManualRelation.Checked)  // 若沒有選取到任何一位玩家 及 手動 被選取
            {
                MessageBox.Show("必需選取一個玩家才行...");
                return;
            }
            if (ManualRelation.Checked)  // 若 手動 被選取
                foreach (Node n in temp_neighborlist)
                    if (n.id == SelectListener)
                        Form_Game.Listener = n;  // 回傳 傾聽者為被選取到的玩家
            if (AutoRelation.Checked)  // 若 自動 被選取
                if (temp_relationlist.Count > 0)
                    foreach (Node n in temp_neighborlist)
                        if (n.id == temp_relationlist[0].id)
                            Form_Game.Listener = n;
            Form_Game.AutoSpeak = AutoRelation.Checked;  // 回傳 是否為自動選取
            
            this.Dispose();  // 關閉視窗
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)  // 按下 Cancel 按鍵, 任何事都取消
        {
            this.Dispose();  // 關閉視窗
        }
    }

    class IcpRelation : IComparer<Relation>  // 拿來比較關係值的類別, 從大排到小
    {
        // 按關係值排序
        public int Compare(Relation x, Relation y)
        {
            return y.relationship.CompareTo(x.relationship);
        }
    }
}
