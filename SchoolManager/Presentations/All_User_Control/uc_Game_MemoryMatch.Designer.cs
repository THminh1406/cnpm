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
            this.pnl_Controls = new Guna.UI2.WinForms.Guna2Panel();
            this.btn_ResetGame = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_Score = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbl_Feedback = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnl_GameArea = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pnl_Controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Controls
            // 
            this.pnl_Controls.Controls.Add(this.btn_ResetGame);
            this.pnl_Controls.Controls.Add(this.lbl_Score);
            this.pnl_Controls.Controls.Add(this.lbl_Feedback);
            this.pnl_Controls.Location = new System.Drawing.Point(97, 86);
            this.pnl_Controls.Name = "pnl_Controls";
            this.pnl_Controls.Size = new System.Drawing.Size(1309, 151);
            this.pnl_Controls.TabIndex = 0;
            // 
            // btn_ResetGame
            // 
            this.btn_ResetGame.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ResetGame.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ResetGame.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_ResetGame.ForeColor = System.Drawing.Color.White;
            this.btn_ResetGame.Location = new System.Drawing.Point(793, 61);
            this.btn_ResetGame.Name = "btn_ResetGame";
            this.btn_ResetGame.Size = new System.Drawing.Size(180, 45);
            this.btn_ResetGame.TabIndex = 2;
            this.btn_ResetGame.Text = "guna2Button1";
            this.btn_ResetGame.Click += new System.EventHandler(this.btn_ResetGame_Click);
            // 
            // lbl_Score
            // 
            this.lbl_Score.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Score.Location = new System.Drawing.Point(435, 46);
            this.lbl_Score.Name = "lbl_Score";
            this.lbl_Score.Size = new System.Drawing.Size(171, 27);
            this.lbl_Score.TabIndex = 1;
            this.lbl_Score.Text = "guna2HtmlLabel2";
            // 
            // lbl_Feedback
            // 
            this.lbl_Feedback.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Feedback.Location = new System.Drawing.Point(163, 46);
            this.lbl_Feedback.Name = "lbl_Feedback";
            this.lbl_Feedback.Size = new System.Drawing.Size(171, 27);
            this.lbl_Feedback.TabIndex = 0;
            this.lbl_Feedback.Text = "guna2HtmlLabel1";
            // 
            // pnl_GameArea
            // 
            this.pnl_GameArea.Location = new System.Drawing.Point(97, 275);
            this.pnl_GameArea.Name = "pnl_GameArea";
            this.pnl_GameArea.Size = new System.Drawing.Size(1336, 675);
            this.pnl_GameArea.TabIndex = 1;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // uc_Game_MemoryMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_GameArea);
            this.Controls.Add(this.pnl_Controls);
            this.Name = "uc_Game_MemoryMatch";
            this.Size = new System.Drawing.Size(2077, 1100);
            this.Load += new System.EventHandler(this.uc_Game_MemoryMatch_Load);
            this.pnl_Controls.ResumeLayout(false);
            this.pnl_Controls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnl_Controls;
        private Guna.UI2.WinForms.Guna2Button btn_ResetGame;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Score;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Feedback;
        private Guna.UI2.WinForms.Guna2Panel pnl_GameArea;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
