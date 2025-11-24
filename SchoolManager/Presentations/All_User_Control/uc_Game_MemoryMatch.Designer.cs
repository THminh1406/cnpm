namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Game_MemoryMatch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Game_MemoryMatch));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btn_ResetGame = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_Feedback = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_Score = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnl_GameArea = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2GradientPanel2 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel12 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_Instructions = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2GradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.BorderRadius = 40;
            this.guna2GradientPanel1.Controls.Add(this.btn_ResetGame);
            this.guna2GradientPanel1.Controls.Add(this.lbl_Feedback);
            this.guna2GradientPanel1.Controls.Add(this.lbl_Score);
            this.guna2GradientPanel1.Controls.Add(this.pnl_GameArea);
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.OldLace;
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.Ivory;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(24, 170);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(2028, 905);
            this.guna2GradientPanel1.TabIndex = 2;
            // 
            // btn_ResetGame
            // 
            this.btn_ResetGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ResetGame.BorderRadius = 20;
            this.btn_ResetGame.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ResetGame.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ResetGame.FillColor = System.Drawing.Color.RoyalBlue;
            this.btn_ResetGame.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold);
            this.btn_ResetGame.ForeColor = System.Drawing.Color.White;
            this.btn_ResetGame.Image = ((System.Drawing.Image)(resources.GetObject("btn_ResetGame.Image")));
            this.btn_ResetGame.ImageOffset = new System.Drawing.Point(0, 3);
            this.btn_ResetGame.ImageSize = new System.Drawing.Size(40, 40);
            this.btn_ResetGame.Location = new System.Drawing.Point(1542, 40);
            this.btn_ResetGame.Name = "btn_ResetGame";
            this.btn_ResetGame.Size = new System.Drawing.Size(248, 84);
            this.btn_ResetGame.TabIndex = 50;
            this.btn_ResetGame.Text = "Chơi lại";
            this.btn_ResetGame.Click += new System.EventHandler(this.btn_ResetGame_Click);
            // 
            // lbl_Feedback
            // 
            this.lbl_Feedback.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Feedback.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_Feedback.Location = new System.Drawing.Point(1542, 165);
            this.lbl_Feedback.Name = "lbl_Feedback";
            this.lbl_Feedback.Size = new System.Drawing.Size(233, 39);
            this.lbl_Feedback.TabIndex = 0;
            this.lbl_Feedback.Tag = "NoTheme";
            this.lbl_Feedback.Text = "guna2HtmlLabel1";
            // 
            // lbl_Score
            // 
            this.lbl_Score.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Score.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_Score.Location = new System.Drawing.Point(1813, 40);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(48, 34);
            this.lbl_Score.TabIndex = 1;
            this.lbl_Score.Tag = "NoTheme";
            this.lbl_Score.Text = "gun";
            // 
            // pnl_GameArea
            // 
            this.pnl_GameArea.Location = new System.Drawing.Point(41, 40);
            this.pnl_GameArea.Name = "pnl_GameArea";
            this.pnl_GameArea.Size = new System.Drawing.Size(1467, 835);
            this.pnl_GameArea.TabIndex = 3;
            this.pnl_GameArea.Tag = "NoTheme";
            // 
            // guna2GradientPanel2
            // 
            this.guna2GradientPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2GradientPanel2.Controls.Add(this.guna2PictureBox1);
            this.guna2GradientPanel2.Controls.Add(this.guna2HtmlLabel12);
            this.guna2GradientPanel2.Controls.Add(this.lbl_Instructions);
            this.guna2GradientPanel2.FillColor = System.Drawing.Color.Khaki;
            this.guna2GradientPanel2.FillColor2 = System.Drawing.Color.DarkGoldenrod;
            this.guna2GradientPanel2.Location = new System.Drawing.Point(-5, -4);
            this.guna2GradientPanel2.Name = "guna2GradientPanel2";
            this.guna2GradientPanel2.Size = new System.Drawing.Size(2087, 152);
            this.guna2GradientPanel2.TabIndex = 12;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Empty;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(29, 45);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(84, 77);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 9;
            this.guna2PictureBox1.TabStop = false;
            // 
            // guna2HtmlLabel12
            // 
            this.guna2HtmlLabel12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2HtmlLabel12.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel12.Font = new System.Drawing.Font("Segoe UI Semibold", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel12.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel12.Location = new System.Drawing.Point(119, 25);
            this.guna2HtmlLabel12.Name = "guna2HtmlLabel12";
            this.guna2HtmlLabel12.Size = new System.Drawing.Size(347, 52);
            this.guna2HtmlLabel12.TabIndex = 6;
            this.guna2HtmlLabel12.Text = "Trò chơi sắp xếp câu ";
            // 
            // lbl_Instructions
            // 
            this.lbl_Instructions.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Instructions.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_Instructions.ForeColor = System.Drawing.Color.White;
            this.lbl_Instructions.Location = new System.Drawing.Point(121, 83);
            this.lbl_Instructions.Name = "lbl_Instructions";
            this.lbl_Instructions.Size = new System.Drawing.Size(537, 39);
            this.lbl_Instructions.TabIndex = 5;
            this.lbl_Instructions.Text = "Hãy sắp xếp các từ sau thành câu hoàn chỉnh.";
            // 
            // uc_Game_MemoryMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.guna2GradientPanel2);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Name = "uc_Game_MemoryMatch";
            this.Size = new System.Drawing.Size(2077, 1100);
            this.Load += new System.EventHandler(this.uc_Game_MemoryMatch_Load);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.guna2GradientPanel2.ResumeLayout(false);
            this.guna2GradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel pnl_GameArea;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Score;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Feedback;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel12;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Instructions;
        private Guna.UI2.WinForms.Guna2Button btn_ResetGame;
    }
}
