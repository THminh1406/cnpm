namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Game_SentenceScramble
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Game_SentenceScramble));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lbl_Instructions = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnl_Answer = new Guna.UI2.WinForms.Guna2Panel();
            this.flp_Answer = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.flp_Words = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_Controls = new Guna.UI2.WinForms.Guna2Panel();
            this.btn_CheckAnswer = new Guna.UI2.WinForms.Guna2Button();
            this.btn_ResetGame = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_Feedback = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1.SuspendLayout();
            this.pnl_Answer.SuspendLayout();
            this.pnl_Controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.pnl_Controls);
            this.guna2Panel1.Controls.Add(this.flp_Words);
            this.guna2Panel1.Controls.Add(this.guna2Separator1);
            this.guna2Panel1.Controls.Add(this.pnl_Answer);
            this.guna2Panel1.Controls.Add(this.lbl_Instructions);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(2077, 1100);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lbl_Instructions
            // 
            this.lbl_Instructions.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Instructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Instructions.Location = new System.Drawing.Point(0, 0);
            this.lbl_Instructions.Name = "lbl_Instructions";
            this.lbl_Instructions.Size = new System.Drawing.Size(2077, 27);
            this.lbl_Instructions.TabIndex = 0;
            this.lbl_Instructions.Text = "Hãy sắp xếp các từ sau thành câu hoàn chỉnh.";
            // 
            // pnl_Answer
            // 
            this.pnl_Answer.Controls.Add(this.flp_Answer);
            this.pnl_Answer.FillColor = System.Drawing.Color.WhiteSmoke;
            this.pnl_Answer.Location = new System.Drawing.Point(83, 115);
            this.pnl_Answer.Name = "pnl_Answer";
            this.pnl_Answer.Size = new System.Drawing.Size(1843, 120);
            this.pnl_Answer.TabIndex = 1;
            // 
            // flp_Answer
            // 
            this.flp_Answer.BackColor = System.Drawing.Color.Transparent;
            this.flp_Answer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_Answer.Location = new System.Drawing.Point(0, 0);
            this.flp_Answer.Name = "flp_Answer";
            this.flp_Answer.Size = new System.Drawing.Size(1843, 120);
            this.flp_Answer.TabIndex = 2;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(83, 294);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1843, 71);
            this.guna2Separator1.TabIndex = 2;
            // 
            // flp_Words
            // 
            this.flp_Words.Location = new System.Drawing.Point(93, 465);
            this.flp_Words.Name = "flp_Words";
            this.flp_Words.Size = new System.Drawing.Size(1833, 188);
            this.flp_Words.TabIndex = 3;
            // 
            // pnl_Controls
            // 
            this.pnl_Controls.Controls.Add(this.lbl_Feedback);
            this.pnl_Controls.Controls.Add(this.btn_CheckAnswer);
            this.pnl_Controls.Controls.Add(this.btn_ResetGame);
            this.pnl_Controls.Location = new System.Drawing.Point(93, 785);
            this.pnl_Controls.Name = "pnl_Controls";
            this.pnl_Controls.Size = new System.Drawing.Size(1833, 159);
            this.pnl_Controls.TabIndex = 4;
            // 
            // btn_CheckAnswer
            // 
            this.btn_CheckAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btn_CheckAnswer.Location = new System.Drawing.Point(651, 31);
            this.btn_CheckAnswer.Name = "btn_CheckAnswer";
            this.btn_CheckAnswer.Size = new System.Drawing.Size(248, 84);
            this.btn_CheckAnswer.TabIndex = 46;
            this.btn_CheckAnswer.Text = "Tất cả có mặt";
            this.btn_CheckAnswer.Click += new System.EventHandler(this.btn_CheckAnswer_Click);
            // 
            // btn_ResetGame
            // 
            this.btn_ResetGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ResetGame.BorderRadius = 20;
            this.btn_ResetGame.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ResetGame.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ResetGame.FillColor = System.Drawing.Color.IndianRed;
            this.btn_ResetGame.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold);
            this.btn_ResetGame.ForeColor = System.Drawing.Color.White;
            this.btn_ResetGame.ImageOffset = new System.Drawing.Point(0, 3);
            this.btn_ResetGame.ImageSize = new System.Drawing.Size(40, 40);
            this.btn_ResetGame.Location = new System.Drawing.Point(973, 31);
            this.btn_ResetGame.Name = "btn_ResetGame";
            this.btn_ResetGame.Size = new System.Drawing.Size(248, 84);
            this.btn_ResetGame.TabIndex = 47;
            this.btn_ResetGame.Text = "Tất cả vắng";
            this.btn_ResetGame.Click += new System.EventHandler(this.btn_ResetGame_Click);
            // 
            // lbl_Feedback
            // 
            this.lbl_Feedback.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Feedback.Location = new System.Drawing.Point(286, 59);
            this.lbl_Feedback.Name = "lbl_Feedback";
            this.lbl_Feedback.Size = new System.Drawing.Size(171, 27);
            this.lbl_Feedback.TabIndex = 48;
            this.lbl_Feedback.Text = "guna2HtmlLabel1";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // uc_Game_SentenceScramble
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel1);
            this.Name = "uc_Game_SentenceScramble";
            this.Size = new System.Drawing.Size(2077, 1100);
            this.Load += new System.EventHandler(this.uc_Game_SentenceScramble_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.pnl_Answer.ResumeLayout(false);
            this.pnl_Controls.ResumeLayout(false);
            this.pnl_Controls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel pnl_Answer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Instructions;
        private Guna.UI2.WinForms.Guna2Panel pnl_Controls;
        private System.Windows.Forms.FlowLayoutPanel flp_Words;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.FlowLayoutPanel flp_Answer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Feedback;
        private Guna.UI2.WinForms.Guna2Button btn_CheckAnswer;
        private Guna.UI2.WinForms.Guna2Button btn_ResetGame;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
