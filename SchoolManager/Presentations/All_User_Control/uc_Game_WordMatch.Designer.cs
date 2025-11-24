namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Game_WordMatch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Game_WordMatch));
            this.btn_ResetGame = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_Score = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.flp_Words = new System.ComponentModel.BackgroundWorker();
            this.btn_CheckAnswer = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_Feedback = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_Images = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2GradientPanel2 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel12 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2GradientPanel1.SuspendLayout();
            this.guna2GradientPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ResetGame
            // 
            this.btn_ResetGame.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
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
            this.btn_ResetGame.Location = new System.Drawing.Point(1072, 789);
            this.btn_ResetGame.Name = "btn_ResetGame";
            this.btn_ResetGame.Size = new System.Drawing.Size(248, 84);
            this.btn_ResetGame.TabIndex = 24;
            this.btn_ResetGame.Text = "Chơi lại";
            this.btn_ResetGame.Click += new System.EventHandler(this.btn_ResetGame_Click);
            // 
            // lbl_Score
            // 
            this.lbl_Score.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Score.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_Score.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbl_Score.Location = new System.Drawing.Point(1673, 59);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(114, 61);
            this.lbl_Score.TabIndex = 51;
            this.lbl_Score.Tag = "NoTheme";
            this.lbl_Score.Text = "Score";
            // 
            // btn_CheckAnswer
            // 
            this.btn_CheckAnswer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_CheckAnswer.BorderRadius = 20;
            this.btn_CheckAnswer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_CheckAnswer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_CheckAnswer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_CheckAnswer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_CheckAnswer.FillColor = System.Drawing.Color.SeaGreen;
            this.btn_CheckAnswer.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold);
            this.btn_CheckAnswer.ForeColor = System.Drawing.Color.White;
            this.btn_CheckAnswer.Image = ((System.Drawing.Image)(resources.GetObject("btn_CheckAnswer.Image")));
            this.btn_CheckAnswer.ImageOffset = new System.Drawing.Point(0, 3);
            this.btn_CheckAnswer.ImageSize = new System.Drawing.Size(40, 40);
            this.btn_CheckAnswer.Location = new System.Drawing.Point(687, 789);
            this.btn_CheckAnswer.Name = "btn_CheckAnswer";
            this.btn_CheckAnswer.Size = new System.Drawing.Size(273, 84);
            this.btn_CheckAnswer.TabIndex = 23;
            this.btn_CheckAnswer.Text = "Kiểm tra kết quả";
            this.btn_CheckAnswer.Click += new System.EventHandler(this.btn_CheckAnswer_Click);
            // 
            // lbl_Feedback
            // 
            this.lbl_Feedback.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Feedback.Font = new System.Drawing.Font("Segoe UI", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_Feedback.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbl_Feedback.Location = new System.Drawing.Point(446, 59);
            this.lbl_Feedback.Name = "lbl_Feedback";
            this.lbl_Feedback.Size = new System.Drawing.Size(68, 73);
            this.lbl_Feedback.TabIndex = 54;
            this.lbl_Feedback.Tag = "NoTheme";
            this.lbl_Feedback.Text = "RS";
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2GradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2GradientPanel1.BorderRadius = 40;
            this.guna2GradientPanel1.Controls.Add(this.flowLayoutPanel1);
            this.guna2GradientPanel1.Controls.Add(this.lbl_Score);
            this.guna2GradientPanel1.Controls.Add(this.lbl_Feedback);
            this.guna2GradientPanel1.Controls.Add(this.flp_Images);
            this.guna2GradientPanel1.Controls.Add(this.btn_CheckAnswer);
            this.guna2GradientPanel1.Controls.Add(this.btn_ResetGame);
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.Azure;
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.PowderBlue;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(13, 178);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(2047, 894);
            this.guna2GradientPanel1.TabIndex = 55;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1076, 159);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(783, 605);
            this.flowLayoutPanel1.TabIndex = 55;
            // 
            // flp_Images
            // 
            this.flp_Images.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flp_Images.BackColor = System.Drawing.Color.Transparent;
            this.flp_Images.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flp_Images.Location = new System.Drawing.Point(214, 159);
            this.flp_Images.Name = "flp_Images";
            this.flp_Images.Size = new System.Drawing.Size(856, 605);
            this.flp_Images.TabIndex = 54;
            // 
            // guna2GradientPanel2
            // 
            this.guna2GradientPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2GradientPanel2.Controls.Add(this.guna2PictureBox1);
            this.guna2GradientPanel2.Controls.Add(this.guna2HtmlLabel2);
            this.guna2GradientPanel2.Controls.Add(this.guna2HtmlLabel12);
            this.guna2GradientPanel2.FillColor = System.Drawing.Color.PaleTurquoise;
            this.guna2GradientPanel2.FillColor2 = System.Drawing.Color.DarkTurquoise;
            this.guna2GradientPanel2.Location = new System.Drawing.Point(-6, -9);
            this.guna2GradientPanel2.Name = "guna2GradientPanel2";
            this.guna2GradientPanel2.Size = new System.Drawing.Size(2089, 164);
            this.guna2GradientPanel2.TabIndex = 56;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.FillColor = System.Drawing.Color.Empty;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(19, 38);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(84, 77);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 9;
            this.guna2PictureBox1.TabStop = false;
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(121, 83);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(290, 39);
            this.guna2HtmlLabel2.TabIndex = 8;
            this.guna2HtmlLabel2.Text = "Chọn ảnh và từ phù hợp";
            // 
            // guna2HtmlLabel12
            // 
            this.guna2HtmlLabel12.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel12.Font = new System.Drawing.Font("Segoe UI Semibold", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel12.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel12.Location = new System.Drawing.Point(121, 25);
            this.guna2HtmlLabel12.Name = "guna2HtmlLabel12";
            this.guna2HtmlLabel12.Size = new System.Drawing.Size(251, 52);
            this.guna2HtmlLabel12.TabIndex = 6;
            this.guna2HtmlLabel12.Text = "Trò chơi nối từ";
            // 
            // uc_Game_WordMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.Controls.Add(this.guna2GradientPanel2);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Name = "uc_Game_WordMatch";
            this.Size = new System.Drawing.Size(2077, 1095);
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.guna2GradientPanel2.ResumeLayout(false);
            this.guna2GradientPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btn_CheckAnswer;
        private Guna.UI2.WinForms.Guna2Button btn_ResetGame;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Score;
        private System.ComponentModel.BackgroundWorker flp_Words;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Feedback;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flp_Images;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel2;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel12;
    }
}
