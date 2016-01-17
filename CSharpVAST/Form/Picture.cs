using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharpVAST
{
    public partial class Picture : Form
    {
        int number;

        public Picture()
        {
            InitializeComponent();
        }

        public Picture(int number, int width, int height)
        {
            InitializeComponent();
            this.number = number;
            // 設定 Form 大小
            this.Size = new Size(width + 5, height + 25);
            // 設定 PictureBox 大小
            pictureBox1.Size = new Size(width, height);
        }

        private void Picture_Load(object sender, EventArgs e)
        {
            Bitmap bmp = null;

            switch (number)
            {
                case 1:
                    bmp = new Bitmap("Goya.jpg");
                    break;
                case 2:
                    bmp = new Bitmap("Michelangel.jpg");
                    break;
                case 3:
                    bmp = new Bitmap("Miro.jpg");
                    break;
                case 4:
                    bmp = new Bitmap("Monet.jpg");
                    break;
                case 5:
                    bmp = new Bitmap("Picasso.jpg");
                    break;
                case 6:
                    bmp = new Bitmap("Pissarro.jpg");
                    break;
                case 7:
                    bmp = new Bitmap("RousseauT.jpg");
                    break;
                case 8:
                    bmp = new Bitmap("VanGogh.jpg");
                    break;
            }
            pictureBox1.Image = bmp;
        }

        private void Picture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
                this.Dispose();
        }
    }
}
