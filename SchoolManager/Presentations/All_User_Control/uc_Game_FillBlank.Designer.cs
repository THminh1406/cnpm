namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Game_FillBlank
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
            this.lbl_Feedback = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btn_ResetGame = new Guna.UI2.WinForms.Guna2Button();
            this.pnl_QuestionArea = new Guna.UI2.WinForms.Guna2Panel();
            this.lbl_Question = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txt_Answer = new Guna.UI2.WinForms.Guna2TextBox();
            this.btn_CheckAnswer = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.pnl_Controls.SuspendLayout();
            this.pnl_QuestionArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Controls
            // 
            this.pnl_Controls.Controls.Add(this.btn_ResetGame);
            this.pnl_Controls.Controls.Add(this.lbl_Feedback);
            this.pnl_Controls.Location = new System.Drawing.Point(30, 16);
            this.pnl_Controls.Name = "pnl_Controls";
            this.pnl_Controls.Size = new System.Drawing.Size(1939, 168);
            this.pnl_Controls.TabIndex = 0;
            // 
            // lbl_Feedback
            // 
            this.lbl_Feedback.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Feedback.Location = new System.Drawing.Point(355, 84);
            this.lbl_Feedback.Name = "lbl_Feedback";
            this.lbl_Feedback.Size = new System.Drawing.Size(171, 27);
            this.lbl_Feedback.TabIndex = 0;
            this.lbl_Feedback.Text = "guna2HtmlLabel1";
            // 
            // btn_ResetGame
            // 
            this.btn_ResetGame.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_ResetGame.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_ResetGame.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_ResetGame.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_ResetGame.ForeColor = System.Drawing.Color.White;
            this.btn_ResetGame.Location = new System.Drawing.Point(1123, 101);
            this.btn_ResetGame.Name = "btn_ResetGame";
            this.btn_ResetGame.Size = new System.Drawing.Size(180, 45);
            this.btn_ResetGame.TabIndex = 1;
            this.btn_ResetGame.Text = "guna2Button1";
            this.btn_ResetGame.Click += new System.EventHandler(this.btn_ResetGame_Click);
            // 
            // pnl_QuestionArea
            // 
            this.pnl_QuestionArea.Controls.Add(this.btn_CheckAnswer);
            this.pnl_QuestionArea.Controls.Add(this.txt_Answer);
            this.pnl_QuestionArea.Controls.Add(this.lbl_Question);
            this.pnl_QuestionArea.Location = new System.Drawing.Point(30, 239);
            this.pnl_QuestionArea.Name = "pnl_QuestionArea";
            this.pnl_QuestionArea.Size = new System.Drawing.Size(1944, 722);
            this.pnl_QuestionArea.TabIndex = 1;
            // 
            // lbl_Question
            // 
            this.lbl_Question.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Question.Location = new System.Drawing.Point(82, 74);
            this.lbl_Question.Name = "lbl_Question";
            this.lbl_Question.Size = new System.Drawing.Size(171, 27);
            this.lbl_Question.TabIndex = 0;
            this.lbl_Question.Text = "guna2HtmlLabel1";
            // 
            // txt_Answer
            // 
            this.txt_Answer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Answer.DefaultText = "";
            this.txt_Answer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Answer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Answer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Answer.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Answer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Answer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Answer.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Answer.Location = new System.Drawing.Point(82, 182);
            this.txt_Answer.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_Answer.Name = "txt_Answer";
            this.txt_Answer.PlaceholderText = "Gõ câu trả lời của bạn...";
            this.txt_Answer.SelectedText = "";
            this.txt_Answer.Size = new System.Drawing.Size(324, 61);
            this.txt_Answer.TabIndex = 1;
            // 
            // btn_CheckAnswer
            // 
            this.btn_CheckAnswer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_CheckAnswer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_CheckAnswer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_CheckAnswer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_CheckAnswer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_CheckAnswer.ForeColor = System.Drawing.Color.White;
            this.btn_CheckAnswer.Location = new System.Drawing.Point(853, 455);
            this.btn_CheckAnswer.Name = "btn_CheckAnswer";
            this.btn_CheckAnswer.Size = new System.Drawing.Size(180, 45);
            this.btn_CheckAnswer.TabIndex = 2;
            this.btn_CheckAnswer.Text = "guna2Button1";
            this.btn_CheckAnswer.Click += new System.EventHandler(this.btn_CheckAnswer_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // uc_Game_FillBlank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnl_QuestionArea);
            this.Controls.Add(this.pnl_Controls);
            this.Name = "uc_Game_FillBlank";
            this.Size = new System.Drawing.Size(2077, 1095);
            this.Load += new System.EventHandler(this.uc_Game_FillBlank_Load);
            this.pnl_Controls.ResumeLayout(false);
            this.pnl_Controls.PerformLayout();
            this.pnl_QuestionArea.ResumeLayout(false);
            this.pnl_QuestionArea.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnl_Controls;
        private Guna.UI2.WinForms.Guna2Button btn_ResetGame;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Feedback;
        private Guna.UI2.WinForms.Guna2Panel pnl_QuestionArea;
        private Guna.UI2.WinForms.Guna2TextBox txt_Answer;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Question;
        private Guna.UI2.WinForms.Guna2Button btn_CheckAnswer;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
