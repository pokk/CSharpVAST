using System.Windows.Forms;

namespace CSharpVAST
{
    public partial class Channel : Form
    {
        internal Game Form_Game = null;  // VAST GAME的主視窗
        int sp_cap;  // 目前 listener 上傳能力
        int ov_cap;  // 目前 overheaer 上傳能力

        public Channel()
        {
            InitializeComponent();
        }

        public Channel(Game temp)
        {
            InitializeComponent();
            Form_Game = temp;  // 接收 Vast 的視窗資料
        }

        private void Channel_Load(object sender, System.EventArgs e)
        {
            sp_cap = Form_Game.Speaker_Capacity;
            ListenerBar.Maximum = Form_Game.Max_Speaker_Capacity;
            ListenerBar.Value = sp_cap;
            ov_cap = Form_Game.Overhearer_Capacity;
            OverheaerBar.Maximum = Form_Game.Max_Overhearer_capacity;
            OverheaerBar.Value = ov_cap;
            Show_Value();
        }

        private void Show_Value()  // 顯示目前上傳能力及最大值上傳能力
        {
            show_listener_channel.Text = sp_cap.ToString() + "\t/\t" + Form_Game.Max_Speaker_Capacity.ToString();
            show_overheaer_channel.Text = ov_cap.ToString() + "\t/\t" + Form_Game.Max_Overhearer_capacity.ToString();
        }

        private void Btn_OK_Click(object sender, System.EventArgs e)  // 按下 ok
        {
            Form_Game.Speaker_Capacity = sp_cap;
            Form_Game.Overhearer_Capacity = ov_cap;
            this.Dispose();  // 關閉視窗
        }

        private void Btn_Cancel_Click(object sender, System.EventArgs e)  // 按下 cancel
        {
            this.Dispose();  // 關閉視窗
        }

        private void ListenerBar_Scroll(object sender, System.EventArgs e)  // 移動 bar 時發生的事
        {
            sp_cap = ListenerBar.Value;
            Show_Value();
        }

        private void OverheaerBar_Scroll(object sender, System.EventArgs e)  // 移動 bar 時發生的事
        {
            ov_cap = OverheaerBar.Value;
            Show_Value();
        }
    }
}
