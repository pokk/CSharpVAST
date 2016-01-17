using System;
using System.Windows.Forms;

namespace CSharpVAST
{
    public partial class InputName : Form
    {
        internal Game Form_Game = null;  // VAST GAME的主視窗

        public InputName()
        {
            InitializeComponent();
        }

        public InputName(Game temp)
        {
            InitializeComponent();
            Form_Game = temp;  // 接收 Vast 的視窗資料
        }

        private void Enter_Button(object sender, EventArgs e)
        {
            if (textbox_name.Text == "")  // 若輸入名字為空的
            {
                MessageBox.Show("請輸入一個有用的名字", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Form_Game.SelfName = textbox_name.Text;
            this.Dispose();  // 關閉視窗
        }

        private void textbox_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                Enter_Button(sender, new EventArgs());
        }

    }
}
