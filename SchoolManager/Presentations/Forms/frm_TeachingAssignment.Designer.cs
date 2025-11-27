namespace SchoolManager.Presentations
{
    partial class frm_TeachingAssignment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.cboClass = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dgvAssignments = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cboSemester = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboSubject = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2HtmlLabel16 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BorderRadius = 20;
            this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(1274, 95);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(215, 76);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Phân công";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboClass
            // 
            this.cboClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClass.BackColor = System.Drawing.Color.Transparent;
            this.cboClass.BorderColor = System.Drawing.Color.Gray;
            this.cboClass.BorderRadius = 20;
            this.cboClass.BorderThickness = 2;
            this.cboClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClass.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboClass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboClass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboClass.ItemHeight = 70;
            this.cboClass.Location = new System.Drawing.Point(52, 95);
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(376, 76);
            this.cboClass.TabIndex = 44;
            // 
            // dgvAssignments
            // 
            this.dgvAssignments.AllowDrop = true;
            this.dgvAssignments.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAssignments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAssignments.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssignments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAssignments.ColumnHeadersHeight = 100;
            this.dgvAssignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssignments.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAssignments.GridColor = System.Drawing.Color.DarkGray;
            this.dgvAssignments.Location = new System.Drawing.Point(52, 194);
            this.dgvAssignments.Name = "dgvAssignments";
            this.dgvAssignments.RowHeadersVisible = false;
            this.dgvAssignments.RowHeadersWidth = 82;
            this.dgvAssignments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAssignments.RowTemplate.Height = 120;
            this.dgvAssignments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAssignments.ShowCellErrors = false;
            this.dgvAssignments.ShowCellToolTips = false;
            this.dgvAssignments.ShowEditingIcon = false;
            this.dgvAssignments.ShowRowErrors = false;
            this.dgvAssignments.Size = new System.Drawing.Size(1437, 484);
            this.dgvAssignments.TabIndex = 54;
            this.dgvAssignments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAssignments.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvAssignments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvAssignments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvAssignments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAssignments.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvAssignments.ThemeStyle.GridColor = System.Drawing.Color.DarkGray;
            this.dgvAssignments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvAssignments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAssignments.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgvAssignments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvAssignments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvAssignments.ThemeStyle.HeaderStyle.Height = 100;
            this.dgvAssignments.ThemeStyle.ReadOnly = false;
            this.dgvAssignments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvAssignments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAssignments.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAssignments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvAssignments.ThemeStyle.RowsStyle.Height = 120;
            this.dgvAssignments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvAssignments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // cboSemester
            // 
            this.cboSemester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSemester.BackColor = System.Drawing.Color.Transparent;
            this.cboSemester.BorderColor = System.Drawing.Color.Gray;
            this.cboSemester.BorderRadius = 20;
            this.cboSemester.BorderThickness = 2;
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSemester.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSemester.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSemester.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSemester.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSemester.ItemHeight = 70;
            this.cboSemester.Location = new System.Drawing.Point(862, 95);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(386, 76);
            this.cboSemester.TabIndex = 55;
            // 
            // cboSubject
            // 
            this.cboSubject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSubject.BackColor = System.Drawing.Color.Transparent;
            this.cboSubject.BorderColor = System.Drawing.Color.Gray;
            this.cboSubject.BorderRadius = 20;
            this.cboSubject.BorderThickness = 2;
            this.cboSubject.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubject.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSubject.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSubject.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSubject.ItemHeight = 70;
            this.cboSubject.Location = new System.Drawing.Point(458, 95);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(383, 76);
            this.cboSubject.TabIndex = 56;
            // 
            // guna2HtmlLabel16
            // 
            this.guna2HtmlLabel16.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel16.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel16.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel16.Location = new System.Drawing.Point(52, 50);
            this.guna2HtmlLabel16.Name = "guna2HtmlLabel16";
            this.guna2HtmlLabel16.Size = new System.Drawing.Size(118, 39);
            this.guna2HtmlLabel16.TabIndex = 57;
            this.guna2HtmlLabel16.Text = "Chọn lớp:";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(458, 50);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(114, 39);
            this.guna2HtmlLabel1.TabIndex = 58;
            this.guna2HtmlLabel1.Text = "Môn học:";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(862, 50);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(89, 39);
            this.guna2HtmlLabel2.TabIndex = 59;
            this.guna2HtmlLabel2.Text = "Học kỳ:";
            // 
            // frm_TeachingAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1524, 718);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.guna2HtmlLabel16);
            this.Controls.Add(this.cboSubject);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.dgvAssignments);
            this.Controls.Add(this.cboClass);
            this.Controls.Add(this.btnAdd);
            this.Name = "frm_TeachingAssignment";
            this.Text = "frm_TeachingAssignment";
            this.Load += new System.EventHandler(this.Frm_TeachingAssignment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssignments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2ComboBox cboClass;
        private Guna.UI2.WinForms.Guna2DataGridView dgvAssignments;
        private Guna.UI2.WinForms.Guna2ComboBox cboSemester;
        private Guna.UI2.WinForms.Guna2ComboBox cboSubject;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel16;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
    }
}