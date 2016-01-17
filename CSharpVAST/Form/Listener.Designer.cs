namespace CSharpVAST
{
    partial class Listener
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
            this.AutoRelation = new System.Windows.Forms.RadioButton();
            this.ManualRelation = new System.Windows.Forms.RadioButton();
            this.ListenerSelectionGroupBox = new System.Windows.Forms.GroupBox();
            this.RankingGroupBox = new System.Windows.Forms.GroupBox();
            this.RELATIONSHIP = new System.Windows.Forms.Label();
            this.USER = new System.Windows.Forms.Label();
            this.RankList = new System.Windows.Forms.ListBox();
            this.Listener_Selected = new System.Windows.Forms.Label();
            this.Listener_Now = new System.Windows.Forms.Label();
            this.Btn_OK = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.ListenerSelectionGroupBox.SuspendLayout();
            this.RankingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoRelation
            // 
            this.AutoRelation.AutoSize = true;
            this.AutoRelation.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AutoRelation.Location = new System.Drawing.Point(16, 26);
            this.AutoRelation.Name = "AutoRelation";
            this.AutoRelation.Size = new System.Drawing.Size(57, 20);
            this.AutoRelation.TabIndex = 0;
            this.AutoRelation.TabStop = true;
            this.AutoRelation.Text = "Auto";
            this.AutoRelation.UseVisualStyleBackColor = true;
            // 
            // ManualRelation
            // 
            this.ManualRelation.AutoSize = true;
            this.ManualRelation.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ManualRelation.Location = new System.Drawing.Point(16, 52);
            this.ManualRelation.Name = "ManualRelation";
            this.ManualRelation.Size = new System.Drawing.Size(73, 20);
            this.ManualRelation.TabIndex = 1;
            this.ManualRelation.TabStop = true;
            this.ManualRelation.Text = "Manual";
            this.ManualRelation.UseVisualStyleBackColor = true;
            this.ManualRelation.CheckedChanged += new System.EventHandler(this.ManualRelation_CheckedChanged);
            // 
            // ListenerSelectionGroupBox
            // 
            this.ListenerSelectionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListenerSelectionGroupBox.Controls.Add(this.RankingGroupBox);
            this.ListenerSelectionGroupBox.Controls.Add(this.Listener_Selected);
            this.ListenerSelectionGroupBox.Controls.Add(this.Listener_Now);
            this.ListenerSelectionGroupBox.Controls.Add(this.AutoRelation);
            this.ListenerSelectionGroupBox.Controls.Add(this.ManualRelation);
            this.ListenerSelectionGroupBox.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ListenerSelectionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ListenerSelectionGroupBox.Name = "ListenerSelectionGroupBox";
            this.ListenerSelectionGroupBox.Size = new System.Drawing.Size(275, 330);
            this.ListenerSelectionGroupBox.TabIndex = 2;
            this.ListenerSelectionGroupBox.TabStop = false;
            this.ListenerSelectionGroupBox.Text = "Listener Selection";
            // 
            // RankingGroupBox
            // 
            this.RankingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RankingGroupBox.Controls.Add(this.RELATIONSHIP);
            this.RankingGroupBox.Controls.Add(this.USER);
            this.RankingGroupBox.Controls.Add(this.RankList);
            this.RankingGroupBox.Location = new System.Drawing.Point(16, 145);
            this.RankingGroupBox.Name = "RankingGroupBox";
            this.RankingGroupBox.Size = new System.Drawing.Size(242, 179);
            this.RankingGroupBox.TabIndex = 5;
            this.RankingGroupBox.TabStop = false;
            this.RankingGroupBox.Text = "Ranking";
            // 
            // RELATIONSHIP
            // 
            this.RELATIONSHIP.AutoSize = true;
            this.RELATIONSHIP.Location = new System.Drawing.Point(148, 30);
            this.RELATIONSHIP.Name = "RELATIONSHIP";
            this.RELATIONSHIP.Size = new System.Drawing.Size(88, 16);
            this.RELATIONSHIP.TabIndex = 7;
            this.RELATIONSHIP.Text = "RelationShip";
            // 
            // USER
            // 
            this.USER.AutoSize = true;
            this.USER.Location = new System.Drawing.Point(6, 30);
            this.USER.Name = "USER";
            this.USER.Size = new System.Drawing.Size(78, 16);
            this.USER.TabIndex = 6;
            this.USER.Text = "User Name";
            // 
            // RankList
            // 
            this.RankList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RankList.FormattingEnabled = true;
            this.RankList.ItemHeight = 16;
            this.RankList.Location = new System.Drawing.Point(6, 58);
            this.RankList.Name = "RankList";
            this.RankList.Size = new System.Drawing.Size(230, 100);
            this.RankList.TabIndex = 2;
            this.RankList.SelectedIndexChanged += new System.EventHandler(this.RankList_SelectedIndexChanged);
            // 
            // Listener_Selected
            // 
            this.Listener_Selected.AutoSize = true;
            this.Listener_Selected.Location = new System.Drawing.Point(13, 115);
            this.Listener_Selected.Name = "Listener_Selected";
            this.Listener_Selected.Size = new System.Drawing.Size(126, 16);
            this.Listener_Selected.TabIndex = 4;
            this.Listener_Selected.Text = "Select Lietenter is :";
            // 
            // Listener_Now
            // 
            this.Listener_Now.AutoSize = true;
            this.Listener_Now.Location = new System.Drawing.Point(13, 85);
            this.Listener_Now.Name = "Listener_Now";
            this.Listener_Now.Size = new System.Drawing.Size(114, 16);
            this.Listener_Now.TabIndex = 3;
            this.Listener_Now.Text = "Now Listener is :";
            // 
            // Btn_OK
            // 
            this.Btn_OK.Location = new System.Drawing.Point(63, 351);
            this.Btn_OK.Name = "Btn_OK";
            this.Btn_OK.Size = new System.Drawing.Size(75, 23);
            this.Btn_OK.TabIndex = 2;
            this.Btn_OK.Text = "OK";
            this.Btn_OK.UseVisualStyleBackColor = true;
            this.Btn_OK.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Location = new System.Drawing.Point(162, 351);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Listener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 383);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_OK);
            this.Controls.Add(this.ListenerSelectionGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Listener";
            this.Text = "Listener Select";
            this.Load += new System.EventHandler(this.Speaker_Load);
            this.ListenerSelectionGroupBox.ResumeLayout(false);
            this.ListenerSelectionGroupBox.PerformLayout();
            this.RankingGroupBox.ResumeLayout(false);
            this.RankingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton AutoRelation;
        private System.Windows.Forms.RadioButton ManualRelation;
        private System.Windows.Forms.GroupBox ListenerSelectionGroupBox;
        private System.Windows.Forms.GroupBox RankingGroupBox;
        private System.Windows.Forms.ListBox RankList;
        private System.Windows.Forms.Label Listener_Selected;
        private System.Windows.Forms.Label Listener_Now;
        private System.Windows.Forms.Button Btn_OK;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Label RELATIONSHIP;
        private System.Windows.Forms.Label USER;
    }
}