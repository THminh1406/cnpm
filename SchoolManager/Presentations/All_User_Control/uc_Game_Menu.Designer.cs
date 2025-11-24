namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Game_Menu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgv_GameList = new Guna.UI2.WinForms.Guna2DataGridView();
            this.id_quiz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quiz_title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quiz_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GameList)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(42, 16);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(299, 39);
            this.guna2HtmlLabel5.TabIndex = 52;
            this.guna2HtmlLabel5.Text = "Chọn game để bắt đầu:";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BorderRadius = 40;
            this.guna2Panel1.Controls.Add(this.dgv_GameList);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel5);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(29, 23);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(2019, 1037);
            this.guna2Panel1.TabIndex = 63;
            // 
            // dgv_GameList
            // 
            this.dgv_GameList.AllowDrop = true;
            this.dgv_GameList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_GameList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_GameList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_GameList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_GameList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_GameList.ColumnHeadersHeight = 100;
            this.dgv_GameList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_GameList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_quiz,
            this.quiz_title,
            this.quiz_type,
            this.playButton,
            this.deleteButton});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_GameList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_GameList.GridColor = System.Drawing.Color.DarkGray;
            this.dgv_GameList.Location = new System.Drawing.Point(42, 76);
            this.dgv_GameList.Name = "dgv_GameList";
            this.dgv_GameList.RowHeadersVisible = false;
            this.dgv_GameList.RowHeadersWidth = 82;
            this.dgv_GameList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_GameList.RowTemplate.Height = 120;
            this.dgv_GameList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_GameList.ShowCellErrors = false;
            this.dgv_GameList.ShowCellToolTips = false;
            this.dgv_GameList.ShowEditingIcon = false;
            this.dgv_GameList.ShowRowErrors = false;
            this.dgv_GameList.Size = new System.Drawing.Size(1936, 924);
            this.dgv_GameList.TabIndex = 63;
            this.dgv_GameList.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_GameList.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv_GameList.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv_GameList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgv_GameList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_GameList.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv_GameList.ThemeStyle.GridColor = System.Drawing.Color.DarkGray;
            this.dgv_GameList.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_GameList.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_GameList.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_GameList.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_GameList.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_GameList.ThemeStyle.HeaderStyle.Height = 100;
            this.dgv_GameList.ThemeStyle.ReadOnly = false;
            this.dgv_GameList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_GameList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_GameList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_GameList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_GameList.ThemeStyle.RowsStyle.Height = 120;
            this.dgv_GameList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_GameList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_GameList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_GameList_CellClick);
            this.dgv_GameList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_GameList_CellPainting);
            // 
            // id_quiz
            // 
            this.id_quiz.DataPropertyName = "id_quiz";
            this.id_quiz.HeaderText = "Column1";
            this.id_quiz.MinimumWidth = 10;
            this.id_quiz.Name = "id_quiz";
            this.id_quiz.Visible = false;
            // 
            // quiz_title
            // 
            this.quiz_title.DataPropertyName = "quiz_title";
            this.quiz_title.HeaderText = "Tên bài chơi";
            this.quiz_title.MinimumWidth = 10;
            this.quiz_title.Name = "quiz_title";
            // 
            // quiz_type
            // 
            this.quiz_type.DataPropertyName = "quiz_type";
            this.quiz_type.HeaderText = "Loại game";
            this.quiz_type.MinimumWidth = 10;
            this.quiz_type.Name = "quiz_type";
            // 
            // playButton
            // 
            this.playButton.HeaderText = "Chơi";
            this.playButton.MinimumWidth = 10;
            this.playButton.Name = "playButton";
            this.playButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.playButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // deleteButton
            // 
            this.deleteButton.HeaderText = "Xóa";
            this.deleteButton.MinimumWidth = 10;
            this.deleteButton.Name = "deleteButton";
            // 
            // uc_Game_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2Panel1);
            this.Name = "uc_Game_Menu";
            this.Size = new System.Drawing.Size(2077, 1100);
            this.Load += new System.EventHandler(this.uc_Game_Menu_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GameList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_GameList;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_quiz;
        private System.Windows.Forms.DataGridViewTextBoxColumn quiz_title;
        private System.Windows.Forms.DataGridViewTextBoxColumn quiz_type;
        private System.Windows.Forms.DataGridViewButtonColumn playButton;
        private System.Windows.Forms.DataGridViewButtonColumn deleteButton;
    }
}
