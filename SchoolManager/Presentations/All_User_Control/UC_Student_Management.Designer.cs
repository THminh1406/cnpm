namespace SchoolManager.Presentations.All_User_Control
{
    partial class UC_Student_Management
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Student_Management));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.dgv_ClassList = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.cbo_SelectClass = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_Class = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.txt_Search = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.student_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name_Student = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthday = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ethnicity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ClassList)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 0;
            this.guna2Elipse1.TargetControl = this;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.AutoScroll = true;
            this.guna2Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.guna2Panel1.Controls.Add(this.guna2Panel3);
            this.guna2Panel1.Controls.Add(this.guna2Panel2);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(2077, 1095);
            this.guna2Panel1.TabIndex = 1;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 40;
            this.guna2Panel3.Controls.Add(this.dgv_ClassList);
            this.guna2Panel3.FillColor = System.Drawing.Color.White;
            this.guna2Panel3.Location = new System.Drawing.Point(27, 295);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(1992, 1258);
            this.guna2Panel3.TabIndex = 9;
            // 
            // dgv_ClassList
            // 
            this.dgv_ClassList.AllowDrop = true;
            this.dgv_ClassList.AllowUserToAddRows = false;
            this.dgv_ClassList.AllowUserToResizeColumns = false;
            this.dgv_ClassList.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_ClassList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgv_ClassList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_ClassList.ColumnHeadersHeight = 100;
            this.dgv_ClassList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.student_Code,
            this.name_Student,
            this.birthday,
            this.gender,
            this.ethnicity,
            this.action});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgv_ClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgv_ClassList.Location = new System.Drawing.Point(32, 52);
            this.dgv_ClassList.Name = "dgv_ClassList";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgv_ClassList.RowHeadersVisible = false;
            this.dgv_ClassList.RowHeadersWidth = 82;
            this.dgv_ClassList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgv_ClassList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.DarkGray;
            this.dgv_ClassList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ClassList.RowTemplate.Height = 100;
            this.dgv_ClassList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_ClassList.ShowCellErrors = false;
            this.dgv_ClassList.ShowCellToolTips = false;
            this.dgv_ClassList.ShowEditingIcon = false;
            this.dgv_ClassList.ShowRowErrors = false;
            this.dgv_ClassList.Size = new System.Drawing.Size(1926, 1140);
            this.dgv_ClassList.TabIndex = 8;
            this.dgv_ClassList.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.WhiteGrid;
            this.dgv_ClassList.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgv_ClassList.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv_ClassList.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv_ClassList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.DarkGray;
            this.dgv_ClassList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_ClassList.ThemeStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgv_ClassList.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgv_ClassList.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.AliceBlue;
            this.dgv_ClassList.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_ClassList.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_ClassList.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_ClassList.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_ClassList.ThemeStyle.HeaderStyle.Height = 100;
            this.dgv_ClassList.ThemeStyle.ReadOnly = false;
            this.dgv_ClassList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_ClassList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_ClassList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_ClassList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_ClassList.ThemeStyle.RowsStyle.Height = 100;
            this.dgv_ClassList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgv_ClassList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_ClassList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.guna2DataGridView2_CellMouseClick);
            this.dgv_ClassList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.guna2DataGridView2_CellPainting);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.BorderRadius = 40;
            this.guna2Panel2.Controls.Add(this.cbo_SelectClass);
            this.guna2Panel2.Controls.Add(this.guna2CirclePictureBox1);
            this.guna2Panel2.Controls.Add(this.guna2Button4);
            this.guna2Panel2.Controls.Add(this.txt_Search);
            this.guna2Panel2.Controls.Add(this.guna2Button1);
            this.guna2Panel2.Controls.Add(this.lbl_Class);
            this.guna2Panel2.FillColor = System.Drawing.Color.White;
            this.guna2Panel2.Location = new System.Drawing.Point(30, 37);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1989, 227);
            this.guna2Panel2.TabIndex = 8;
            // 
            // cbo_SelectClass
            // 
            this.cbo_SelectClass.BackColor = System.Drawing.Color.Transparent;
            this.cbo_SelectClass.BorderColor = System.Drawing.Color.Gray;
            this.cbo_SelectClass.BorderRadius = 20;
            this.cbo_SelectClass.BorderThickness = 2;
            this.cbo_SelectClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_SelectClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_SelectClass.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_SelectClass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_SelectClass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbo_SelectClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbo_SelectClass.ItemHeight = 77;
            this.cbo_SelectClass.Location = new System.Drawing.Point(1704, 112);
            this.cbo_SelectClass.Name = "cbo_SelectClass";
            this.cbo_SelectClass.Size = new System.Drawing.Size(251, 83);
            this.cbo_SelectClass.TabIndex = 42;
            this.cbo_SelectClass.SelectedIndexChanged += new System.EventHandler(this.cbo_SelectClass_SelectedIndexChanged);
            // 
            // lbl_Class
            // 
            this.lbl_Class.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Class.Font = new System.Drawing.Font("Segoe UI", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_Class.ForeColor = System.Drawing.Color.Black;
            this.lbl_Class.Location = new System.Drawing.Point(93, 14);
            this.lbl_Class.Name = "lbl_Class";
            this.lbl_Class.Size = new System.Drawing.Size(586, 61);
            this.lbl_Class.TabIndex = 11;
            this.lbl_Class.Text = "Danh sách học sinh lớp 10A1";
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CirclePictureBox1.Image")));
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(29, 14);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(58, 61);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2CirclePictureBox1.TabIndex = 41;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // guna2Button4
            // 
            this.guna2Button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button4.BorderRadius = 20;
            this.guna2Button4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button4.FillColor = System.Drawing.Color.MediumAquamarine;
            this.guna2Button4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2Button4.ForeColor = System.Drawing.Color.White;
            this.guna2Button4.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button4.Image")));
            this.guna2Button4.ImageOffset = new System.Drawing.Point(1, 2);
            this.guna2Button4.ImageSize = new System.Drawing.Size(50, 50);
            this.guna2Button4.Location = new System.Drawing.Point(1704, 14);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.Size = new System.Drawing.Size(251, 69);
            this.guna2Button4.TabIndex = 15;
            this.guna2Button4.Text = "Import Excel";
            this.guna2Button4.TextOffset = new System.Drawing.Point(-3, 0);
            this.guna2Button4.Click += new System.EventHandler(this.guna2Button4_Click);
            // 
            // txt_Search
            // 
            this.txt_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Search.AutoScroll = true;
            this.txt_Search.BackColor = System.Drawing.Color.Transparent;
            this.txt_Search.BorderRadius = 20;
            this.txt_Search.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_Search.DefaultText = "";
            this.txt_Search.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_Search.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_Search.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Search.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_Search.FillColor = System.Drawing.Color.WhiteSmoke;
            this.txt_Search.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Search.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_Search.ForeColor = System.Drawing.Color.Black;
            this.txt_Search.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_Search.IconLeft = ((System.Drawing.Image)(resources.GetObject("txt_Search.IconLeft")));
            this.txt_Search.IconLeftSize = new System.Drawing.Size(40, 40);
            this.txt_Search.Location = new System.Drawing.Point(35, 113);
            this.txt_Search.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_Search.Name = "txt_Search";
            this.txt_Search.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.txt_Search.PlaceholderText = "Tìm kiếm học sinh theo tên, lớp, địa chỉ...";
            this.txt_Search.SelectedText = "";
            this.txt_Search.Size = new System.Drawing.Size(1641, 82);
            this.txt_Search.TabIndex = 14;
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button1.BorderRadius = 20;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.RoyalBlue;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageOffset = new System.Drawing.Point(-2, 2);
            this.guna2Button1.ImageSize = new System.Drawing.Size(40, 40);
            this.guna2Button1.Location = new System.Drawing.Point(1426, 14);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(250, 69);
            this.guna2Button1.TabIndex = 12;
            this.guna2Button1.Text = "Thêm mới";
            this.guna2Button1.TextOffset = new System.Drawing.Point(-2, 0);
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // STT
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STT.DefaultCellStyle = dataGridViewCellStyle9;
            this.STT.FillWeight = 15F;
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 10;
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            this.STT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // student_Code
            // 
            this.student_Code.DataPropertyName = "code_Student";
            this.student_Code.FillWeight = 23F;
            this.student_Code.HeaderText = "Mã số";
            this.student_Code.MinimumWidth = 10;
            this.student_Code.Name = "student_Code";
            this.student_Code.ReadOnly = true;
            this.student_Code.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // name_Student
            // 
            this.name_Student.DataPropertyName = "name_Student";
            this.name_Student.FillWeight = 50F;
            this.name_Student.HeaderText = "Họ và tên";
            this.name_Student.MinimumWidth = 10;
            this.name_Student.Name = "name_Student";
            this.name_Student.ReadOnly = true;
            this.name_Student.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // birthday
            // 
            this.birthday.DataPropertyName = "birthday";
            this.birthday.FillWeight = 30F;
            this.birthday.HeaderText = "Ngày sinh";
            this.birthday.MinimumWidth = 10;
            this.birthday.Name = "birthday";
            this.birthday.ReadOnly = true;
            this.birthday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // gender
            // 
            this.gender.DataPropertyName = "GenderText";
            this.gender.FillWeight = 20F;
            this.gender.HeaderText = "Giới tính";
            this.gender.MinimumWidth = 10;
            this.gender.Name = "gender";
            this.gender.ReadOnly = true;
            this.gender.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ethnicity
            // 
            this.ethnicity.DataPropertyName = "ethnicity";
            this.ethnicity.FillWeight = 20F;
            this.ethnicity.HeaderText = "Địa chỉ";
            this.ethnicity.MinimumWidth = 10;
            this.ethnicity.Name = "ethnicity";
            this.ethnicity.ReadOnly = true;
            this.ethnicity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // action
            // 
            this.action.FillWeight = 20F;
            this.action.HeaderText = "Thao tác";
            this.action.MinimumWidth = 10;
            this.action.Name = "action";
            this.action.ReadOnly = true;
            this.action.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UC_Student_Management
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.guna2Panel1);
            this.Name = "UC_Student_Management";
            this.Size = new System.Drawing.Size(2077, 1095);
            this.Load += new System.EventHandler(this.UC_Student_Management_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ClassList)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2Button guna2Button4;
        private Guna.UI2.WinForms.Guna2TextBox txt_Search;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_Class;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_ClassList;
        private Guna.UI2.WinForms.Guna2ComboBox cbo_SelectClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn student_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name_Student;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthday;
        private System.Windows.Forms.DataGridViewTextBoxColumn gender;
        private System.Windows.Forms.DataGridViewTextBoxColumn ethnicity;
        private System.Windows.Forms.DataGridViewTextBoxColumn action;
    }
}
