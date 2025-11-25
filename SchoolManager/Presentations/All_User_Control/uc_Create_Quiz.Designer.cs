namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Create_Quiz
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Create_Quiz));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnl_MemoryLevels = new Guna.UI2.WinForms.Guna2Panel();
            this.rad_LevelHard = new System.Windows.Forms.RadioButton();
            this.rad_LevelEasy = new System.Windows.Forms.RadioButton();
            this.rad_LevelMedium = new System.Windows.Forms.RadioButton();
            this.cbo_QuizType = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txt_QuizTitle = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel_ContentSelection = new Guna.UI2.WinForms.Guna2Panel();
            this.dgv_Word = new Guna.UI2.WinForms.Guna2DataGridView();
            this.id_vocabulary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WordText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WordImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.col_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_AddToQuiz = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_PairCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cbo_FilterBank = new Guna.UI2.WinForms.Guna2ComboBox();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Panel1.SuspendLayout();
            this.pnl_MemoryLevels.SuspendLayout();
            this.panel_ContentSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Word)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 40;
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel1);
            this.guna2Panel1.Controls.Add(this.guna2HtmlLabel5);
            this.guna2Panel1.Controls.Add(this.pnl_MemoryLevels);
            this.guna2Panel1.Controls.Add(this.cbo_QuizType);
            this.guna2Panel1.Controls.Add(this.txt_QuizTitle);
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(31, 31);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(2011, 328);
            this.guna2Panel1.TabIndex = 0;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(43, 175);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(143, 39);
            this.guna2HtmlLabel1.TabIndex = 52;
            this.guna2HtmlLabel1.Text = "Loại game:";
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.guna2HtmlLabel5.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(43, 20);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(138, 39);
            this.guna2HtmlLabel5.TabIndex = 51;
            this.guna2HtmlLabel5.Text = "Tên Game:";
            // 
            // pnl_MemoryLevels
            // 
            this.pnl_MemoryLevels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_MemoryLevels.Controls.Add(this.rad_LevelHard);
            this.pnl_MemoryLevels.Controls.Add(this.rad_LevelEasy);
            this.pnl_MemoryLevels.Controls.Add(this.rad_LevelMedium);
            this.pnl_MemoryLevels.Location = new System.Drawing.Point(1714, 68);
            this.pnl_MemoryLevels.Name = "pnl_MemoryLevels";
            this.pnl_MemoryLevels.Size = new System.Drawing.Size(257, 228);
            this.pnl_MemoryLevels.TabIndex = 47;
            this.pnl_MemoryLevels.Visible = false;
            // 
            // rad_LevelHard
            // 
            this.rad_LevelHard.AutoSize = true;
            this.rad_LevelHard.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rad_LevelHard.ForeColor = System.Drawing.Color.OrangeRed;
            this.rad_LevelHard.Location = new System.Drawing.Point(20, 165);
            this.rad_LevelHard.Name = "rad_LevelHard";
            this.rad_LevelHard.Size = new System.Drawing.Size(87, 36);
            this.rad_LevelHard.TabIndex = 56;
            this.rad_LevelHard.TabStop = true;
            this.rad_LevelHard.Text = "Khó";
            this.rad_LevelHard.UseVisualStyleBackColor = true;
            this.rad_LevelHard.CheckedChanged += new System.EventHandler(this.UpdatePairCount);
            // 
            // rad_LevelEasy
            // 
            this.rad_LevelEasy.AutoSize = true;
            this.rad_LevelEasy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rad_LevelEasy.ForeColor = System.Drawing.Color.ForestGreen;
            this.rad_LevelEasy.Location = new System.Drawing.Point(20, 22);
            this.rad_LevelEasy.Name = "rad_LevelEasy";
            this.rad_LevelEasy.Size = new System.Drawing.Size(75, 36);
            this.rad_LevelEasy.TabIndex = 54;
            this.rad_LevelEasy.TabStop = true;
            this.rad_LevelEasy.Text = "Dễ";
            this.rad_LevelEasy.UseVisualStyleBackColor = true;
            this.rad_LevelEasy.CheckedChanged += new System.EventHandler(this.UpdatePairCount);
            // 
            // rad_LevelMedium
            // 
            this.rad_LevelMedium.AutoSize = true;
            this.rad_LevelMedium.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.rad_LevelMedium.ForeColor = System.Drawing.Color.Goldenrod;
            this.rad_LevelMedium.Location = new System.Drawing.Point(20, 93);
            this.rad_LevelMedium.Name = "rad_LevelMedium";
            this.rad_LevelMedium.Size = new System.Drawing.Size(161, 36);
            this.rad_LevelMedium.TabIndex = 55;
            this.rad_LevelMedium.TabStop = true;
            this.rad_LevelMedium.Text = "Trung bình";
            this.rad_LevelMedium.UseVisualStyleBackColor = true;
            this.rad_LevelMedium.CheckedChanged += new System.EventHandler(this.UpdatePairCount);
            // 
            // cbo_QuizType
            // 
            this.cbo_QuizType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_QuizType.BackColor = System.Drawing.Color.Transparent;
            this.cbo_QuizType.BorderColor = System.Drawing.Color.Gray;
            this.cbo_QuizType.BorderRadius = 20;
            this.cbo_QuizType.BorderThickness = 2;
            this.cbo_QuizType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_QuizType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_QuizType.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_QuizType.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_QuizType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbo_QuizType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbo_QuizType.ItemHeight = 70;
            this.cbo_QuizType.Location = new System.Drawing.Point(43, 220);
            this.cbo_QuizType.Name = "cbo_QuizType";
            this.cbo_QuizType.Size = new System.Drawing.Size(1638, 76);
            this.cbo_QuizType.TabIndex = 45;
            this.cbo_QuizType.SelectedIndexChanged += new System.EventHandler(this.cbo_QuizType_SelectedIndexChanged);
            // 
            // txt_QuizTitle
            // 
            this.txt_QuizTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_QuizTitle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txt_QuizTitle.BorderRadius = 20;
            this.txt_QuizTitle.BorderThickness = 2;
            this.txt_QuizTitle.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_QuizTitle.DefaultText = "";
            this.txt_QuizTitle.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_QuizTitle.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_QuizTitle.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_QuizTitle.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_QuizTitle.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_QuizTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_QuizTitle.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_QuizTitle.Location = new System.Drawing.Point(43, 68);
            this.txt_QuizTitle.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txt_QuizTitle.Name = "txt_QuizTitle";
            this.txt_QuizTitle.PlaceholderText = "Nhập tựa đề game của bạn...";
            this.txt_QuizTitle.SelectedText = "";
            this.txt_QuizTitle.Size = new System.Drawing.Size(1638, 77);
            this.txt_QuizTitle.TabIndex = 0;
            this.txt_QuizTitle.Tag = "NoTheme";
            // 
            // panel_ContentSelection
            // 
            this.panel_ContentSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_ContentSelection.BackColor = System.Drawing.Color.Transparent;
            this.panel_ContentSelection.BorderRadius = 40;
            this.panel_ContentSelection.Controls.Add(this.dgv_Word);
            this.panel_ContentSelection.Controls.Add(this.btn_AddToQuiz);
            this.panel_ContentSelection.Controls.Add(this.lbl_PairCount);
            this.panel_ContentSelection.Controls.Add(this.cbo_FilterBank);
            this.panel_ContentSelection.FillColor = System.Drawing.Color.White;
            this.panel_ContentSelection.Location = new System.Drawing.Point(31, 381);
            this.panel_ContentSelection.Name = "panel_ContentSelection";
            this.panel_ContentSelection.Size = new System.Drawing.Size(2011, 677);
            this.panel_ContentSelection.TabIndex = 1;
            // 
            // dgv_Word
            // 
            this.dgv_Word.AllowDrop = true;
            this.dgv_Word.AllowUserToAddRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_Word.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_Word.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Word.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Word.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_Word.ColumnHeadersHeight = 100;
            this.dgv_Word.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_Word.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_vocabulary,
            this.WordText,
            this.WordImage,
            this.col_Select});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Word.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Word.GridColor = System.Drawing.Color.DarkGray;
            this.dgv_Word.Location = new System.Drawing.Point(43, 29);
            this.dgv_Word.Name = "dgv_Word";
            this.dgv_Word.RowHeadersVisible = false;
            this.dgv_Word.RowHeadersWidth = 82;
            this.dgv_Word.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Word.RowTemplate.Height = 120;
            this.dgv_Word.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_Word.ShowCellErrors = false;
            this.dgv_Word.ShowCellToolTips = false;
            this.dgv_Word.ShowEditingIcon = false;
            this.dgv_Word.ShowRowErrors = false;
            this.dgv_Word.Size = new System.Drawing.Size(1638, 616);
            this.dgv_Word.TabIndex = 61;
            this.dgv_Word.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_Word.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgv_Word.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgv_Word.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgv_Word.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgv_Word.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgv_Word.ThemeStyle.GridColor = System.Drawing.Color.DarkGray;
            this.dgv_Word.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_Word.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_Word.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.dgv_Word.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_Word.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgv_Word.ThemeStyle.HeaderStyle.Height = 100;
            this.dgv_Word.ThemeStyle.ReadOnly = false;
            this.dgv_Word.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgv_Word.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgv_Word.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_Word.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgv_Word.ThemeStyle.RowsStyle.Height = 120;
            this.dgv_Word.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv_Word.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgv_Word.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Word_CellMouseUp);
            this.dgv_Word.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Word_CellValueChanged);
            // 
            // id_vocabulary
            // 
            this.id_vocabulary.DataPropertyName = "id_vocabulary";
            this.id_vocabulary.HeaderText = "Column1";
            this.id_vocabulary.MinimumWidth = 10;
            this.id_vocabulary.Name = "id_vocabulary";
            this.id_vocabulary.Visible = false;
            // 
            // WordText
            // 
            this.WordText.DataPropertyName = "WordText";
            this.WordText.HeaderText = "Từ vựng đã thêm";
            this.WordText.MinimumWidth = 10;
            this.WordText.Name = "WordText";
            // 
            // WordImage
            // 
            this.WordImage.DataPropertyName = "WordImage";
            this.WordImage.HeaderText = "Hình ảnh";
            this.WordImage.MinimumWidth = 10;
            this.WordImage.Name = "WordImage";
            // 
            // col_Select
            // 
            this.col_Select.HeaderText = "Chọn";
            this.col_Select.MinimumWidth = 10;
            this.col_Select.Name = "col_Select";
            this.col_Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btn_AddToQuiz
            // 
            this.btn_AddToQuiz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddToQuiz.BorderRadius = 20;
            this.btn_AddToQuiz.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddToQuiz.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_AddToQuiz.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_AddToQuiz.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_AddToQuiz.FillColor = System.Drawing.Color.RoyalBlue;
            this.btn_AddToQuiz.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold);
            this.btn_AddToQuiz.ForeColor = System.Drawing.Color.White;
            this.btn_AddToQuiz.Image = ((System.Drawing.Image)(resources.GetObject("btn_AddToQuiz.Image")));
            this.btn_AddToQuiz.ImageOffset = new System.Drawing.Point(0, 3);
            this.btn_AddToQuiz.ImageSize = new System.Drawing.Size(40, 40);
            this.btn_AddToQuiz.Location = new System.Drawing.Point(1714, 573);
            this.btn_AddToQuiz.Name = "btn_AddToQuiz";
            this.btn_AddToQuiz.Size = new System.Drawing.Size(251, 62);
            this.btn_AddToQuiz.TabIndex = 60;
            this.btn_AddToQuiz.Text = "Tạo trò chơi";
            this.btn_AddToQuiz.Click += new System.EventHandler(this.btn_AddToQuiz_Click);
            // 
            // lbl_PairCount
            // 
            this.lbl_PairCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_PairCount.BackColor = System.Drawing.Color.Transparent;
            this.lbl_PairCount.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl_PairCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_PairCount.Location = new System.Drawing.Point(1745, 163);
            this.lbl_PairCount.Name = "lbl_PairCount";
            this.lbl_PairCount.Size = new System.Drawing.Size(104, 39);
            this.lbl_PairCount.TabIndex = 53;
            this.lbl_PairCount.Text = "đã chọn";
            // 
            // cbo_FilterBank
            // 
            this.cbo_FilterBank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_FilterBank.BackColor = System.Drawing.Color.Transparent;
            this.cbo_FilterBank.BorderColor = System.Drawing.Color.Gray;
            this.cbo_FilterBank.BorderRadius = 20;
            this.cbo_FilterBank.BorderThickness = 2;
            this.cbo_FilterBank.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbo_FilterBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_FilterBank.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_FilterBank.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbo_FilterBank.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbo_FilterBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbo_FilterBank.ItemHeight = 70;
            this.cbo_FilterBank.Location = new System.Drawing.Point(1736, 42);
            this.cbo_FilterBank.Name = "cbo_FilterBank";
            this.cbo_FilterBank.Size = new System.Drawing.Size(235, 76);
            this.cbo_FilterBank.TabIndex = 46;
            this.cbo_FilterBank.SelectedIndexChanged += new System.EventHandler(this.cbo_FilterBank_SelectedIndexChanged);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // uc_Create_Quiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.panel_ContentSelection);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "uc_Create_Quiz";
            this.Size = new System.Drawing.Size(2077, 1095);
            this.Load += new System.EventHandler(this.uc_Create_Quiz_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.pnl_MemoryLevels.ResumeLayout(false);
            this.pnl_MemoryLevels.PerformLayout();
            this.panel_ContentSelection.ResumeLayout(false);
            this.panel_ContentSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Word)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox txt_QuizTitle;
        private Guna.UI2.WinForms.Guna2ComboBox cbo_QuizType;
        private Guna.UI2.WinForms.Guna2Panel panel_ContentSelection;
        private Guna.UI2.WinForms.Guna2ComboBox cbo_FilterBank;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel pnl_MemoryLevels;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_PairCount;
        private System.Windows.Forms.RadioButton rad_LevelHard;
        private System.Windows.Forms.RadioButton rad_LevelEasy;
        private System.Windows.Forms.RadioButton rad_LevelMedium;
        private Guna.UI2.WinForms.Guna2Button btn_AddToQuiz;
        private Guna.UI2.WinForms.Guna2DataGridView dgv_Word;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_vocabulary;
        private System.Windows.Forms.DataGridViewTextBoxColumn WordText;
        private System.Windows.Forms.DataGridViewImageColumn WordImage;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_Select;
    }
}
