namespace CSharpVAST
{
    partial class Game
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.myPanel = new System.Windows.Forms.Panel();
            this.Picasso = new System.Windows.Forms.PictureBox();
            this.Monet = new System.Windows.Forms.PictureBox();
            this.Pissarro = new System.Windows.Forms.PictureBox();
            this.Rousseau = new System.Windows.Forms.PictureBox();
            this.VanGogh = new System.Windows.Forms.PictureBox();
            this.Miro = new System.Windows.Forms.PictureBox();
            this.Michelangel = new System.Windows.Forms.PictureBox();
            this.keyout = new System.Windows.Forms.Label();
            this.RIGHTDOWN = new System.Windows.Forms.Label();
            this.LEFTDOWN = new System.Windows.Forms.Label();
            this.RIGHTUP = new System.Windows.Forms.Label();
            this.LEFTUP = new System.Windows.Forms.Label();
            this.TEXT_RIGHTUP = new System.Windows.Forms.Label();
            this.TEXT_RIGHT = new System.Windows.Forms.Label();
            this.TEXT_RIGHTDOWN = new System.Windows.Forms.Label();
            this.TEXT_DOWN = new System.Windows.Forms.Label();
            this.TEXT_LEFTDOWN = new System.Windows.Forms.Label();
            this.TEXT_LEFTUP = new System.Windows.Forms.Label();
            this.TEXT_LEFT = new System.Windows.Forms.Label();
            this.TEXT_UP = new System.Windows.Forms.Label();
            this.SelfInformation = new System.Windows.Forms.Label();
            this.keyin = new System.Windows.Forms.Label();
            this.Goya = new System.Windows.Forms.PictureBox();
            this.ShowVoice = new System.Windows.Forms.PictureBox();
            this.myPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picasso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Monet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pissarro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rousseau)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VanGogh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Miro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Michelangel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Goya)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowVoice)).BeginInit();
            this.SuspendLayout();
            // 
            // myPanel
            // 
            this.myPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.myPanel.BackgroundImage = global::CSharpVAST.Properties.Resources.Map1;
            this.myPanel.Controls.Add(this.ShowVoice);
            this.myPanel.Controls.Add(this.Picasso);
            this.myPanel.Controls.Add(this.Monet);
            this.myPanel.Controls.Add(this.Pissarro);
            this.myPanel.Controls.Add(this.Rousseau);
            this.myPanel.Controls.Add(this.VanGogh);
            this.myPanel.Controls.Add(this.Miro);
            this.myPanel.Controls.Add(this.Michelangel);
            this.myPanel.Controls.Add(this.keyout);
            this.myPanel.Controls.Add(this.RIGHTDOWN);
            this.myPanel.Controls.Add(this.LEFTDOWN);
            this.myPanel.Controls.Add(this.RIGHTUP);
            this.myPanel.Controls.Add(this.LEFTUP);
            this.myPanel.Controls.Add(this.TEXT_RIGHTUP);
            this.myPanel.Controls.Add(this.TEXT_RIGHT);
            this.myPanel.Controls.Add(this.TEXT_RIGHTDOWN);
            this.myPanel.Controls.Add(this.TEXT_DOWN);
            this.myPanel.Controls.Add(this.TEXT_LEFTDOWN);
            this.myPanel.Controls.Add(this.TEXT_LEFTUP);
            this.myPanel.Controls.Add(this.TEXT_LEFT);
            this.myPanel.Controls.Add(this.TEXT_UP);
            this.myPanel.Controls.Add(this.SelfInformation);
            this.myPanel.Controls.Add(this.keyin);
            this.myPanel.Controls.Add(this.Goya);
            this.myPanel.Location = new System.Drawing.Point(-2, 0);
            this.myPanel.Name = "myPanel";
            this.myPanel.Size = new System.Drawing.Size(872, 723);
            this.myPanel.TabIndex = 0;
            this.myPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Render);
            // 
            // Picasso
            // 
            this.Picasso.Image = global::CSharpVAST.Properties.Resources._Picasso;
            this.Picasso.Location = new System.Drawing.Point(687, 408);
            this.Picasso.Name = "Picasso";
            this.Picasso.Size = new System.Drawing.Size(100, 70);
            this.Picasso.TabIndex = 23;
            this.Picasso.TabStop = false;
            this.Picasso.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Monet
            // 
            this.Monet.Image = global::CSharpVAST.Properties.Resources._Monet;
            this.Monet.Location = new System.Drawing.Point(510, 253);
            this.Monet.Name = "Monet";
            this.Monet.Size = new System.Drawing.Size(100, 76);
            this.Monet.TabIndex = 22;
            this.Monet.TabStop = false;
            this.Monet.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Pissarro
            // 
            this.Pissarro.Image = global::CSharpVAST.Properties.Resources._Pissarro;
            this.Pissarro.Location = new System.Drawing.Point(598, 590);
            this.Pissarro.Name = "Pissarro";
            this.Pissarro.Size = new System.Drawing.Size(100, 81);
            this.Pissarro.TabIndex = 21;
            this.Pissarro.TabStop = false;
            this.Pissarro.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Rousseau
            // 
            this.Rousseau.Image = global::CSharpVAST.Properties.Resources._RousseauT;
            this.Rousseau.Location = new System.Drawing.Point(352, 477);
            this.Rousseau.Name = "Rousseau";
            this.Rousseau.Size = new System.Drawing.Size(100, 65);
            this.Rousseau.TabIndex = 20;
            this.Rousseau.TabStop = false;
            this.Rousseau.Click += new System.EventHandler(this.Picture_Click);
            // 
            // VanGogh
            // 
            this.VanGogh.Image = global::CSharpVAST.Properties.Resources._VanGogh;
            this.VanGogh.Location = new System.Drawing.Point(137, 593);
            this.VanGogh.Name = "VanGogh";
            this.VanGogh.Size = new System.Drawing.Size(100, 77);
            this.VanGogh.TabIndex = 19;
            this.VanGogh.TabStop = false;
            this.VanGogh.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Miro
            // 
            this.Miro.Image = global::CSharpVAST.Properties.Resources._Miro;
            this.Miro.Location = new System.Drawing.Point(598, 57);
            this.Miro.Name = "Miro";
            this.Miro.Size = new System.Drawing.Size(100, 74);
            this.Miro.TabIndex = 18;
            this.Miro.TabStop = false;
            this.Miro.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Michelangel
            // 
            this.Michelangel.Image = global::CSharpVAST.Properties.Resources._Michelangel;
            this.Michelangel.Location = new System.Drawing.Point(352, 67);
            this.Michelangel.Name = "Michelangel";
            this.Michelangel.Size = new System.Drawing.Size(100, 51);
            this.Michelangel.TabIndex = 17;
            this.Michelangel.TabStop = false;
            this.Michelangel.Click += new System.EventHandler(this.Picture_Click);
            // 
            // keyout
            // 
            this.keyout.AutoSize = true;
            this.keyout.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.keyout.Location = new System.Drawing.Point(14, 526);
            this.keyout.Name = "keyout";
            this.keyout.Size = new System.Drawing.Size(136, 16);
            this.keyout.TabIndex = 15;
            this.keyout.Text = "接收文字顯示區塊";
            // 
            // RIGHTDOWN
            // 
            this.RIGHTDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RIGHTDOWN.AutoSize = true;
            this.RIGHTDOWN.Location = new System.Drawing.Point(804, 72);
            this.RIGHTDOWN.Name = "RIGHTDOWN";
            this.RIGHTDOWN.Size = new System.Drawing.Size(58, 12);
            this.RIGHTDOWN.TabIndex = 14;
            this.RIGHTDOWN.Text = "Page Down";
            // 
            // LEFTDOWN
            // 
            this.LEFTDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LEFTDOWN.AutoSize = true;
            this.LEFTDOWN.Location = new System.Drawing.Point(763, 72);
            this.LEFTDOWN.Name = "LEFTDOWN";
            this.LEFTDOWN.Size = new System.Drawing.Size(24, 12);
            this.LEFTDOWN.TabIndex = 13;
            this.LEFTDOWN.Text = "End";
            // 
            // RIGHTUP
            // 
            this.RIGHTUP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RIGHTUP.AutoSize = true;
            this.RIGHTUP.Location = new System.Drawing.Point(804, 12);
            this.RIGHTUP.Name = "RIGHTUP";
            this.RIGHTUP.Size = new System.Drawing.Size(44, 12);
            this.RIGHTUP.TabIndex = 12;
            this.RIGHTUP.Text = "Page Up";
            // 
            // LEFTUP
            // 
            this.LEFTUP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LEFTUP.AutoSize = true;
            this.LEFTUP.BackColor = System.Drawing.SystemColors.Control;
            this.LEFTUP.Location = new System.Drawing.Point(757, 13);
            this.LEFTUP.Name = "LEFTUP";
            this.LEFTUP.Size = new System.Drawing.Size(33, 12);
            this.LEFTUP.TabIndex = 11;
            this.LEFTUP.Text = "Home";
            // 
            // TEXT_RIGHTUP
            // 
            this.TEXT_RIGHTUP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_RIGHTUP.AutoSize = true;
            this.TEXT_RIGHTUP.Location = new System.Drawing.Point(813, 24);
            this.TEXT_RIGHTUP.Name = "TEXT_RIGHTUP";
            this.TEXT_RIGHTUP.Size = new System.Drawing.Size(17, 12);
            this.TEXT_RIGHTUP.TabIndex = 10;
            this.TEXT_RIGHTUP.Text = "↗";
            // 
            // TEXT_RIGHT
            // 
            this.TEXT_RIGHT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_RIGHT.AutoSize = true;
            this.TEXT_RIGHT.Location = new System.Drawing.Point(813, 43);
            this.TEXT_RIGHT.Name = "TEXT_RIGHT";
            this.TEXT_RIGHT.Size = new System.Drawing.Size(17, 12);
            this.TEXT_RIGHT.TabIndex = 9;
            this.TEXT_RIGHT.Text = "→";
            // 
            // TEXT_RIGHTDOWN
            // 
            this.TEXT_RIGHTDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_RIGHTDOWN.AutoSize = true;
            this.TEXT_RIGHTDOWN.Location = new System.Drawing.Point(813, 62);
            this.TEXT_RIGHTDOWN.Name = "TEXT_RIGHTDOWN";
            this.TEXT_RIGHTDOWN.Size = new System.Drawing.Size(17, 12);
            this.TEXT_RIGHTDOWN.TabIndex = 8;
            this.TEXT_RIGHTDOWN.Text = "↘";
            // 
            // TEXT_DOWN
            // 
            this.TEXT_DOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_DOWN.AutoSize = true;
            this.TEXT_DOWN.Location = new System.Drawing.Point(790, 62);
            this.TEXT_DOWN.Name = "TEXT_DOWN";
            this.TEXT_DOWN.Size = new System.Drawing.Size(17, 12);
            this.TEXT_DOWN.TabIndex = 7;
            this.TEXT_DOWN.Text = "↓";
            // 
            // TEXT_LEFTDOWN
            // 
            this.TEXT_LEFTDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_LEFTDOWN.AutoSize = true;
            this.TEXT_LEFTDOWN.Location = new System.Drawing.Point(767, 62);
            this.TEXT_LEFTDOWN.Name = "TEXT_LEFTDOWN";
            this.TEXT_LEFTDOWN.Size = new System.Drawing.Size(17, 12);
            this.TEXT_LEFTDOWN.TabIndex = 6;
            this.TEXT_LEFTDOWN.Text = "↙";
            // 
            // TEXT_LEFTUP
            // 
            this.TEXT_LEFTUP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_LEFTUP.AutoSize = true;
            this.TEXT_LEFTUP.Location = new System.Drawing.Point(767, 24);
            this.TEXT_LEFTUP.Name = "TEXT_LEFTUP";
            this.TEXT_LEFTUP.Size = new System.Drawing.Size(17, 12);
            this.TEXT_LEFTUP.TabIndex = 5;
            this.TEXT_LEFTUP.Text = "↖";
            // 
            // TEXT_LEFT
            // 
            this.TEXT_LEFT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_LEFT.AutoSize = true;
            this.TEXT_LEFT.Location = new System.Drawing.Point(767, 43);
            this.TEXT_LEFT.Name = "TEXT_LEFT";
            this.TEXT_LEFT.Size = new System.Drawing.Size(17, 12);
            this.TEXT_LEFT.TabIndex = 4;
            this.TEXT_LEFT.Text = "←";
            // 
            // TEXT_UP
            // 
            this.TEXT_UP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TEXT_UP.AutoSize = true;
            this.TEXT_UP.Location = new System.Drawing.Point(790, 24);
            this.TEXT_UP.Name = "TEXT_UP";
            this.TEXT_UP.Size = new System.Drawing.Size(17, 12);
            this.TEXT_UP.TabIndex = 3;
            this.TEXT_UP.Text = "↑";
            // 
            // SelfInformation
            // 
            this.SelfInformation.AutoSize = true;
            this.SelfInformation.BackColor = System.Drawing.SystemColors.Control;
            this.SelfInformation.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SelfInformation.Location = new System.Drawing.Point(29, 9);
            this.SelfInformation.Name = "SelfInformation";
            this.SelfInformation.Size = new System.Drawing.Size(106, 16);
            this.SelfInformation.TabIndex = 2;
            this.SelfInformation.Text = "SelfInformation";
            // 
            // keyin
            // 
            this.keyin.AutoSize = true;
            this.keyin.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.keyin.Location = new System.Drawing.Point(14, 699);
            this.keyin.Name = "keyin";
            this.keyin.Size = new System.Drawing.Size(104, 16);
            this.keyin.TabIndex = 1;
            this.keyin.Text = "輸入文字區塊";
            // 
            // Goya
            // 
            this.Goya.Image = global::CSharpVAST.Properties.Resources._Goya;
            this.Goya.Location = new System.Drawing.Point(137, 180);
            this.Goya.Name = "Goya";
            this.Goya.Size = new System.Drawing.Size(100, 68);
            this.Goya.TabIndex = 16;
            this.Goya.TabStop = false;
            this.Goya.Click += new System.EventHandler(this.Picture_Click);
            // 
            // ShowVoice
            // 
            this.ShowVoice.BackColor = System.Drawing.Color.Transparent;
            this.ShowVoice.Location = new System.Drawing.Point(756, 633);
            this.ShowVoice.Name = "ShowVoice";
            this.ShowVoice.Size = new System.Drawing.Size(62, 45);
            this.ShowVoice.TabIndex = 24;
            this.ShowVoice.TabStop = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 724);
            this.Controls.Add(this.myPanel);
            this.Name = "Game";
            this.Text = "VastGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Quit);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Key_Down);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Key_Press);
            this.myPanel.ResumeLayout(false);
            this.myPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picasso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Monet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pissarro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rousseau)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VanGogh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Miro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Michelangel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Goya)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShowVoice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel myPanel;
        private System.Windows.Forms.Label keyin;
        private System.Windows.Forms.Label SelfInformation;
        private System.Windows.Forms.Label TEXT_RIGHTUP;
        private System.Windows.Forms.Label TEXT_RIGHT;
        private System.Windows.Forms.Label TEXT_RIGHTDOWN;
        private System.Windows.Forms.Label TEXT_DOWN;
        private System.Windows.Forms.Label TEXT_LEFTDOWN;
        private System.Windows.Forms.Label TEXT_LEFTUP;
        private System.Windows.Forms.Label TEXT_LEFT;
        private System.Windows.Forms.Label TEXT_UP;
        private System.Windows.Forms.Label RIGHTDOWN;
        private System.Windows.Forms.Label LEFTDOWN;
        private System.Windows.Forms.Label RIGHTUP;
        private System.Windows.Forms.Label LEFTUP;
        private System.Windows.Forms.Label keyout;
        private System.Windows.Forms.PictureBox Picasso;
        private System.Windows.Forms.PictureBox Monet;
        private System.Windows.Forms.PictureBox Pissarro;
        private System.Windows.Forms.PictureBox Rousseau;
        private System.Windows.Forms.PictureBox VanGogh;
        private System.Windows.Forms.PictureBox Miro;
        private System.Windows.Forms.PictureBox Michelangel;
        private System.Windows.Forms.PictureBox Goya;
        private System.Windows.Forms.PictureBox ShowVoice;
    }
}

