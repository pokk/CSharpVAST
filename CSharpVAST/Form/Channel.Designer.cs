namespace CSharpVAST
{
    partial class Channel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LISTEN_CHANNEL = new System.Windows.Forms.Label();
            this.OVERHEARER_CHANNEL = new System.Windows.Forms.Label();
            this.ListenerBar = new System.Windows.Forms.TrackBar();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.OverheaerBar = new System.Windows.Forms.TrackBar();
            this.show_listener_channel = new System.Windows.Forms.Label();
            this.show_overheaer_channel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ListenerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OverheaerBar)).BeginInit();
            this.SuspendLayout();
            // 
            // LISTEN_CHANNEL
            // 
            this.LISTEN_CHANNEL.AutoSize = true;
            this.LISTEN_CHANNEL.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LISTEN_CHANNEL.Location = new System.Drawing.Point(12, 25);
            this.LISTEN_CHANNEL.Name = "LISTEN_CHANNEL";
            this.LISTEN_CHANNEL.Size = new System.Drawing.Size(114, 16);
            this.LISTEN_CHANNEL.TabIndex = 0;
            this.LISTEN_CHANNEL.Text = "Listener Channel";
            // 
            // OVERHEARER_CHANNEL
            // 
            this.OVERHEARER_CHANNEL.AutoSize = true;
            this.OVERHEARER_CHANNEL.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.OVERHEARER_CHANNEL.Location = new System.Drawing.Point(12, 76);
            this.OVERHEARER_CHANNEL.Name = "OVERHEARER_CHANNEL";
            this.OVERHEARER_CHANNEL.Size = new System.Drawing.Size(134, 16);
            this.OVERHEARER_CHANNEL.TabIndex = 1;
            this.OVERHEARER_CHANNEL.Text = "Overhearer Channel";
            // 
            // ListenerBar
            // 
            this.ListenerBar.Location = new System.Drawing.Point(151, 25);
            this.ListenerBar.Name = "ListenerBar";
            this.ListenerBar.Size = new System.Drawing.Size(135, 45);
            this.ListenerBar.TabIndex = 2;
            this.ListenerBar.Scroll += new System.EventHandler(this.ListenerBar_Scroll);
            // 
            // Btn_OK
            // 
            this.Btn_OK.Location = new System.Drawing.Point(51, 127);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 3;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(211, 127);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // OverheaerBar
            // 
            this.OverheaerBar.Location = new System.Drawing.Point(151, 76);
            this.OverheaerBar.Name = "OverheaerBar";
            this.OverheaerBar.Size = new System.Drawing.Size(135, 45);
            this.OverheaerBar.TabIndex = 5;
            this.OverheaerBar.Scroll += new System.EventHandler(this.OverheaerBar_Scroll);
            // 
            // show_listener_channel
            // 
            this.show_listener_channel.AutoSize = true;
            this.show_listener_channel.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.show_listener_channel.Location = new System.Drawing.Point(292, 25);
            this.show_listener_channel.Name = "show_listener_channel";
            this.show_listener_channel.Size = new System.Drawing.Size(91, 16);
            this.show_listener_channel.TabIndex = 6;
            this.show_listener_channel.Text = "NOW / MAX";
            // 
            // show_overheaer_channel
            // 
            this.show_overheaer_channel.AutoSize = true;
            this.show_overheaer_channel.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.show_overheaer_channel.Location = new System.Drawing.Point(292, 76);
            this.show_overheaer_channel.Name = "show_overheaer_channel";
            this.show_overheaer_channel.Size = new System.Drawing.Size(91, 16);
            this.show_overheaer_channel.TabIndex = 7;
            this.show_overheaer_channel.Text = "NOW / MAX";
            // 
            // Channel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 162);
            this.Controls.Add(this.show_overheaer_channel);
            this.Controls.Add(this.show_listener_channel);
            this.Controls.Add(this.OverheaerBar);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.ListenerBar);
            this.Controls.Add(this.OVERHEARER_CHANNEL);
            this.Controls.Add(this.LISTEN_CHANNEL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Channel";
            this.Text = "Channel Select";
            this.Load += new System.EventHandler(this.Channel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListenerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OverheaerBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LISTEN_CHANNEL;
        private System.Windows.Forms.Label OVERHEARER_CHANNEL;
        private System.Windows.Forms.TrackBar ListenerBar;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.TrackBar OverheaerBar;
        private System.Windows.Forms.Label show_listener_channel;
        private System.Windows.Forms.Label show_overheaer_channel;
    }
}