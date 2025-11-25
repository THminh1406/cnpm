namespace SchoolManager.Presentations.Forms
{
    partial class Form_EditStudent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_EditStudent));
            this.dtp_Birthday = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cbo_Gender = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txt_Code = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel8 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel7 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btn_Cancel = new Guna.UI2.WinForms.Guna2Button();
            this.btn_Save = new Guna.UI2.WinForms.Guna2Button();
            this.txt_Name = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // dtp_Birthday
            // 
            this.dtp_Birthday.BackColor = System.Drawing.Color.Transparent;
            this.dtp_Birthday.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtp_Birthday.BorderRadius = 20;
            this.dtp_Birthday.BorderThickness = 1;
            this.dtp_Birthday.Checked = true;
            this.dtp_Birthday.FillColor = System.Drawing.Color.White;
            this.dtp_Birthday.Font = new System.Drawing.Font("Segoe UI", 10.125F);
            this.dtp_Birthday.ForeColor = System.Drawing.Color.Black;
            this.dtp_Birthday.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_Birthday.Location = new System.Drawing.Point(48, 425);
            this.dtp_Birthday.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_Birthday.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_Birthday.Name = "dtp_Birthday";
            this.dtp_Birthday.Size = new System.Drawing.Size(1530, 78);
            this.dtp_Birthday.TabIndex = 54;
            this.dtp_Birthday.Value = new System.DateTime(2025, 10, 21, 20, 45, 31, 764);
            // 
            // cbo_Gender
            // 
            this.cbo_Gender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_Gender.BackColor = System.Drawing.Color.Transparent;
            this.cbo_Gender.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbo_Gender.BorderRadius = 20;
            this.cbo_Gender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_Gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Gender.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_Gender.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_Gender.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbo_Gender.ForeColor = System.Drawing.Color.Black;
            this.cbo_Gender.ItemHeight = 77;
            this.cbo_Gender.Location = new System.Drawing.Point(585, 246);
            this.cbo_Gender.Name = "cbo_Gender";
            this.cbo_Gender.Size = new System.Drawing.Size(993, 83);
            this.cbo_Gender.TabIndex = 51;
            // 
            // txt_Code
            // 
            this.txt_Code.BorderColor = System.Drawing.Color.Gray;
            this.txt_Code.BorderRadius = 20;
            this.txt_Code.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Code.DefaultText = "";
            this.txt_Code.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Code.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Code.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Code.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Code.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Code.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Code.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Code.Location = new System.Drawing.Point(48, 249);
            this.txt_Code.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txt_Code.PlaceholderText = "Mã số học sinh...";
            this.txt_Code.SelectedText = "";
            this.txt_Code.Size = new System.Drawing.Size(491, 83);
            this.txt_Code.TabIndex = 48;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(48, 201);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(171, 39);
            this.guna2HtmlLabel1.TabIndex = 47;
            this.guna2HtmlLabel1.Text = "Mã học sinh *";
            // 
            // guna2HtmlLabel8
            // 
            this.guna2HtmlLabel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2HtmlLabel8.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel8.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel8.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel8.Location = new System.Drawing.Point(43, 380);
            this.guna2HtmlLabel8.Name = "guna2HtmlLabel8";
            this.guna2HtmlLabel8.Size = new System.Drawing.Size(145, 39);
            this.guna2HtmlLabel8.TabIndex = 45;
            this.guna2HtmlLabel8.Text = "Ngày sinh *";
            // 
            // guna2HtmlLabel7
            // 
            this.guna2HtmlLabel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2HtmlLabel7.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel7.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel7.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel7.Location = new System.Drawing.Point(585, 201);
            this.guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            this.guna2HtmlLabel7.Size = new System.Drawing.Size(128, 39);
            this.guna2HtmlLabel7.TabIndex = 44;
            this.guna2HtmlLabel7.Text = "Giới tính *";
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(48, 25);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(140, 39);
            this.guna2HtmlLabel4.TabIndex = 41;
            this.guna2HtmlLabel4.Text = "Họ và tên *";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BorderRadius = 20;
            this.btn_Cancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Cancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Cancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Cancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Cancel.FillColor = System.Drawing.Color.Gray;
            this.btn_Cancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.ImageOffset = new System.Drawing.Point(0, 3);
            this.btn_Cancel.ImageSize = new System.Drawing.Size(40, 40);
            this.btn_Cancel.Location = new System.Drawing.Point(1420, 543);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(158, 84);
            this.btn_Cancel.TabIndex = 56;
            this.btn_Cancel.Text = "Hủy";
            this.btn_Cancel.TextOffset = new System.Drawing.Point(-2, 0);
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.BorderRadius = 20;
            this.btn_Save.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_Save.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_Save.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_Save.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_Save.FillColor = System.Drawing.Color.RoyalBlue;
            this.btn_Save.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Save.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageOffset = new System.Drawing.Point(0, 2);
            this.btn_Save.ImageSize = new System.Drawing.Size(40, 40);
            this.btn_Save.Location = new System.Drawing.Point(1081, 543);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(291, 84);
            this.btn_Save.TabIndex = 55;
            this.btn_Save.Text = "Lưu học sinh";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_Name
            // 
            this.txt_Name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Name.BorderColor = System.Drawing.Color.Gray;
            this.txt_Name.BorderRadius = 20;
            this.txt_Name.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Name.DefaultText = "";
            this.txt_Name.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Name.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Name.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Name.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Name.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Name.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Name.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Name.Location = new System.Drawing.Point(48, 73);
            this.txt_Name.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txt_Name.PlaceholderText = "Nhập họ và tên học sinh...";
            this.txt_Name.SelectedText = "";
            this.txt_Name.Size = new System.Drawing.Size(1530, 83);
            this.txt_Name.TabIndex = 46;
            // 
            // Form_EditStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1627, 658);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.dtp_Birthday);
            this.Controls.Add(this.cbo_Gender);
            this.Controls.Add(this.txt_Code);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.guna2HtmlLabel8);
            this.Controls.Add(this.guna2HtmlLabel7);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_EditStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.Form_EditStudent_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DateTimePicker dtp_Birthday;
        private Guna.UI2.WinForms.Guna2ComboBox cbo_Gender;
        private Guna.UI2.WinForms.Guna2TextBox txt_Code;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel8;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel7;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2Button btn_Cancel;
        private Guna.UI2.WinForms.Guna2Button btn_Save;
        private Guna.UI2.WinForms.Guna2TextBox txt_Name;
    }
}
