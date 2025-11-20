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
            this.flp_Images = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_Words = new System.ComponentModel.BackgroundWorker();
            this.btn_CheckAnswer = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_Feedback = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            this.btn_ResetGame.Location = new System.Drawing.Point(1097, 984);
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
            this.lbl_Score.Location = new System.Drawing.Point(1593, 19);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(114, 61);
            this.lbl_Score.TabIndex = 51;
            this.lbl_Score.Text = "Score";
            // 
            // flp_Images
            // 
            this.flp_Images.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flp_Images.BackColor = System.Drawing.Color.White;
            this.flp_Images.Location = new System.Drawing.Point(84, 112);
            this.flp_Images.Name = "flp_Images";
            this.flp_Images.Size = new System.Drawing.Size(871, 850);
            this.flp_Images.TabIndex = 52;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1084, 112);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(939, 850);
            this.flowLayoutPanel1.TabIndex = 53;
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
            this.btn_CheckAnswer.Location = new System.Drawing.Point(682, 984);
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
            this.lbl_Feedback.Location = new System.Drawing.Point(40, 19);
            this.lbl_Feedback.Name = "lbl_Feedback";
            this.lbl_Feedback.Size = new System.Drawing.Size(68, 73);
            this.lbl_Feedback.TabIndex = 54;
            this.lbl_Feedback.Text = "RS";
            // 
            // uc_Game_WordMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.lbl_Feedback);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flp_Images);
            this.Controls.Add(this.lbl_Score);
            this.Controls.Add(this.btn_CheckAnswer);
            this.Controls.Add(this.btn_ResetGame);
            this.Name = "uc_Game_WordMatch";
            this.Size = new System.Drawing.Size(2077, 1095);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btn_CheckAnswer;
        private Guna.UI2.WinForms.Guna2Button btn_ResetGame;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Score;
        private System.Windows.Forms.FlowLayoutPanel flp_Images;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.BackgroundWorker flp_Words;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Feedback;
    }
}
