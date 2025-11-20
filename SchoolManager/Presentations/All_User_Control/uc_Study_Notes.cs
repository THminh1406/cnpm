using Guna.UI2.WinForms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Study_Notes : UserControl
    {
        private Business_Logic_Classes bll_Classes;
        private Business_Logic_Students bll_Students;
        private Business_Logic_Notes bll_Notes;

        public uc_Study_Notes()
        {
            InitializeComponent();
        }

        private void uc_Study_Notes_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            bll_Classes = new Business_Logic_Classes();
            bll_Students = new Business_Logic_Students();
            bll_Notes = new Business_Logic_Notes();
            LoadInitData();
        }

        private void LoadInitData()
        {
            // 1. Load Danh sách Lớp
            cbo_SelectClass.DataSource = bll_Classes.GetAllClasses();
            cbo_SelectClass.DisplayMember = "name_Class";
            cbo_SelectClass.ValueMember = "id_Class";
            // Sự kiện: Chọn lớp -> Load HS -> Load Ghi chú
            cbo_SelectClass.SelectedIndexChanged += (s, ev) => { LoadStudents(); LoadNotes(); };

            // 2. Load ComboBox Lọc Loại Ghi chú (Filter)
            cbo_FilterNote.Items.Clear();
            cbo_FilterNote.Items.AddRange(new object[] { "Tất cả", "Nhắc nhở", "Vi phạm", "Học tập" });
            cbo_FilterNote.SelectedIndex = 0;
            cbo_FilterNote.SelectedIndexChanged += (s, ev) => LoadNotes();

            // 3. Load ComboBox Chọn Loại (Để thêm mới)
            if (cbo_TypeOfNote != null)
            {
                cbo_TypeOfNote.Items.Clear();
                cbo_TypeOfNote.Items.AddRange(new object[] { "Nhắc nhở", "Vi phạm", "Học tập" });
                cbo_TypeOfNote.SelectedIndex = 0;
            }
            flp_ContainerNote.SizeChanged += Flp_ContainerNote_SizeChanged;

            // Load lần đầu
            LoadStudents();
        }

        private void Flp_ContainerNote_SizeChanged(object sender, EventArgs e)
        {
            // Tạm dừng vẽ để đỡ giật
            flp_ContainerNote.SuspendLayout();

            int newWidth = flp_ContainerNote.ClientSize.Width - 25; // Trừ hao thanh cuộn

            foreach (Control ctrl in flp_ContainerNote.Controls)
            {
                if (ctrl is Guna2Panel pnl)
                {
                    // Chỉ thay đổi chiều rộng, chiều cao giữ nguyên
                    pnl.Width = newWidth;
                }
            }

            flp_ContainerNote.ResumeLayout();
        }

        // Tải danh sách học sinh theo lớp vào ComboBox
        private void LoadStudents()
        {
            if (cbo_SelectClass.SelectedValue != null && int.TryParse(cbo_SelectClass.SelectedValue.ToString(), out int classId))
            {
                var listStudents = bll_Students.GetStudentsByClassId(classId);
                cbo_SelectStudent.DataSource = listStudents;
                cbo_SelectStudent.DisplayMember = "name_Student";
                cbo_SelectStudent.ValueMember = "id_Student";
            }
        }

        // === HÀM TẠO GIAO DIỆN ĐỘNG (ĐÃ TÍCH HỢP SỬA TRỰC TIẾP) ===
        // === HÀM TẠO GIAO DIỆN ĐỘNG (ĐÃ SỬA LỖI FONT) ===
        private Guna2Panel GenerateNotePanel(NoteDTO note)
        {
            // Lấy chiều rộng thực tế
            int containerWidth = flp_ContainerNote.ClientSize.Width - 35;
            if (containerWidth < 800) containerWidth = 800;

            // 1. PANEL CHÍNH
            Guna2Panel pnlMain = new Guna2Panel();
            pnlMain.Size = new Size(containerWidth, 244);
            pnlMain.BackColor = Color.Transparent;
            pnlMain.BorderRadius = 40;
            pnlMain.BorderThickness = 3;
            pnlMain.FillColor = Color.FloralWhite;
            pnlMain.Margin = new Padding(0, 0, 0, 25);
            pnlMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            pnlMain.ShadowDecoration.Enabled = true;
            pnlMain.ShadowDecoration.Shadow = new Padding(0, 0, 5, 5);
            pnlMain.ShadowDecoration.Color = Color.LightGray;
            pnlMain.ShadowDecoration.BorderRadius = 40;

            Color statusColor = Color.ForestGreen;
            if (note.Priority == "Cao") statusColor = Color.Crimson;
            else if (note.Priority == "Trung bình") statusColor = Color.Goldenrod;
            pnlMain.BorderColor = statusColor;

            int btnSize = 50;
            int imgSize = 32;
            int btnY = 20;

            // 2. NÚT XÓA
            Guna2CircleButton btnDelete = new Guna2CircleButton();
            btnDelete.Size = new Size(btnSize, btnSize);
            btnDelete.ImageSize = new Size(imgSize, imgSize);
            btnDelete.Location = new Point(pnlMain.Width - 75, btnY);
            btnDelete.FillColor = Color.IndianRed;
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            if (Properties.Resources.delete != null) btnDelete.Image = Properties.Resources.delete;
            btnDelete.Tag = note.NoteId;
            btnDelete.Click += BtnDelete_Click;
            pnlMain.Controls.Add(btnDelete);

            // 3. NÚT SỬA
            Guna2CircleButton btnEdit = new Guna2CircleButton();
            btnEdit.Size = new Size(btnSize, btnSize);
            btnEdit.ImageSize = new Size(imgSize, imgSize);
            btnEdit.Location = new Point(pnlMain.Width - 140, btnY);
            btnEdit.FillColor = Color.CornflowerBlue;
            btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            if (Properties.Resources.edit != null) btnEdit.Image = Properties.Resources.edit;
            btnEdit.Tag = note.NoteId;
            btnEdit.Click += BtnEdit_Click;
            pnlMain.Controls.Add(btnEdit);

            // 4. TEXTBOX NỘI DUNG
            Guna2TextBox txtContent = new Guna2TextBox();
            txtContent.Name = "txtNoteContent";
            txtContent.Tag = note.NoteId;
            txtContent.Location = new Point(19, 100);
            txtContent.Size = new Size(pnlMain.Width - 300, 47);

            txtContent.BorderRadius = 15;
            txtContent.FillColor = Color.White;
            txtContent.ForeColor = Color.Black;

            // --- SỬA LỖI Ở ĐÂY: Dùng System.Drawing.Font ---
            txtContent.Font = new System.Drawing.Font("Segoe UI", 11);
            // -----------------------------------------------

            txtContent.Text = note.NoteContent;
            txtContent.Multiline = true;
            txtContent.TextOffset = new Point(5, 5);
            txtContent.ReadOnly = true;
            txtContent.BorderColor = Color.LightGray;
            txtContent.BorderThickness = 1;
            txtContent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            txtContent.KeyDown += TxtContent_KeyDown;
            pnlMain.Controls.Add(txtContent);

            // 5. LABEL THỜI GIAN
            Guna2HtmlLabel lblTimeCreated = new Guna2HtmlLabel();
            lblTimeCreated.Location = new Point(25, 205);
            lblTimeCreated.ForeColor = Color.DimGray;

            // --- SỬA LỖI Ở ĐÂY ---
            lblTimeCreated.Font = new System.Drawing.Font("Segoe UI Italic", 10);
            // ---------------------

            lblTimeCreated.Text = $"Tạo lúc: {note.CreatedAt:HH:mm:ss dd/MM/yyyy}";
            lblTimeCreated.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pnlMain.Controls.Add(lblTimeCreated);

            // 6. LABEL NGÀY NGẮN
            Guna2HtmlLabel lblDateShort = new Guna2HtmlLabel();
            lblDateShort.Location = new Point(229, 70);

            // --- SỬA LỖI Ở ĐÂY ---
            lblDateShort.Font = new System.Drawing.Font("Segoe UI", 9);
            // ---------------------

            lblDateShort.Text = $"• {note.CreatedAt:dd/MM/yyyy}";
            pnlMain.Controls.Add(lblDateShort);

            // 7. LABEL MỨC ĐỘ
            Guna2HtmlLabel lblPriority = new Guna2HtmlLabel();
            lblPriority.Location = new Point(85, 70);

            // --- SỬA LỖI Ở ĐÂY ---
            lblPriority.Font = new System.Drawing.Font("Segoe UI", 9, FontStyle.Bold);
            // ---------------------

            lblPriority.Text = note.Priority;
            lblPriority.ForeColor = statusColor;
            pnlMain.Controls.Add(lblPriority);

            // 8. AVATAR
            Guna2CirclePictureBox picAvatar = new Guna2CirclePictureBox();
            picAvatar.Size = new Size(60, 61);
            picAvatar.Location = new Point(20, 30);
            picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
            if (Properties.Resources.studentnote != null)
                picAvatar.Image = Properties.Resources.studentnote;
            pnlMain.Controls.Add(picAvatar);

            // 9. LABEL TÊN HỌC SINH
            Guna2HtmlLabel lblName = new Guna2HtmlLabel();
            lblName.Location = new Point(85, 25);

            // --- SỬA LỖI Ở ĐÂY ---
            lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 12, FontStyle.Bold);
            // ---------------------

            lblName.Text = note.StudentName;
            pnlMain.Controls.Add(lblName);

            return pnlMain;
        }

        // === SỰ KIỆN KHI ẤN NÚT SỬA ===
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            Guna2CircleButton btn = (Guna2CircleButton)sender;
            Guna2Panel pnlMain = (Guna2Panel)btn.Parent;

            // Tìm TextBox trong Panel đó
            foreach (Control c in pnlMain.Controls)
            {
                if (c is Guna2TextBox && c.Name == "txtNoteContent")
                {
                    Guna2TextBox txt = (Guna2TextBox)c;

                    // Bật chế độ sửa
                    txt.ReadOnly = false;
                    txt.BorderColor = Color.Blue;
                    txt.BorderThickness = 2;
                    txt.Focus();
                    txt.SelectAll(); // Bôi đen chữ cho tiện
                    return;
                }
            }
        }

        // === SỰ KIỆN KHI ẤN ENTER TRONG TEXTBOX ===
        private void TxtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Chặn xuống dòng

                Guna2TextBox txt = (Guna2TextBox)sender;
                int noteId = (int)txt.Tag;
                string newContent = txt.Text;

                if (string.IsNullOrWhiteSpace(newContent))
                {
                    MessageBox.Show("Nội dung không được để trống!");
                    return;
                }

                // Gọi BLL cập nhật
                if (bll_Notes.UpdateNoteContent(noteId, newContent))
                {
                    // Tắt chế độ sửa
                    txt.ReadOnly = true;
                    txt.BorderColor = Color.LightGray;
                    txt.BorderThickness = 1;
                    this.Focus(); // Bỏ focus khỏi textbox
                    MessageBox.Show("Cập nhật thành công!");
                }
                else
                {
                    MessageBox.Show("Lỗi khi cập nhật.");
                }
            }
        }

        // === TẢI DANH SÁCH GHI CHÚ ===
        private void LoadNotes()
        {
            flp_ContainerNote.Controls.Clear();

            if (cbo_SelectClass.SelectedValue == null) return;

            int classId = 0;
            int.TryParse(cbo_SelectClass.SelectedValue.ToString(), out classId);

            string typeFilter = "Tất cả";
            if (cbo_FilterNote.SelectedItem != null)
                typeFilter = cbo_FilterNote.SelectedItem.ToString();

            List<NoteDTO> list = bll_Notes.GetNotes(classId, typeFilter);

            foreach (var note in list)
            {
                Guna2Panel notePanel = GenerateNotePanel(note);
                flp_ContainerNote.Controls.Add(notePanel);
            }
        }

        // === CÁC SỰ KIỆN BUTTON KHÁC (THÊM, XÓA) ===
        private void btn_AddNote_Click(object sender, EventArgs e)
        {
            if (cbo_SelectStudent.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh!", "Thông báo"); return;
            }

            if (string.IsNullOrWhiteSpace(txt_Note.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung!", "Thông báo"); return;
            }

            string priority = "Thấp";
            if (rad_Medium.Checked) priority = "Trung bình";
            if (rad_Hight.Checked) priority = "Cao";

            string noteType = "Nhắc nhở";
            if (cbo_TypeOfNote != null && cbo_TypeOfNote.SelectedItem != null)
                noteType = cbo_TypeOfNote.SelectedItem.ToString();

            NoteDTO newNote = new NoteDTO
            {
                StudentId = (int)cbo_SelectStudent.SelectedValue,
                NoteContent = txt_Note.Text,
                NoteType = noteType,
                Priority = priority
            };

            if (bll_Notes.AddNote(newNote))
            {
                MessageBox.Show("Đã thêm thành công!");
                txt_Note.Clear();
                LoadNotes();
            }
            else MessageBox.Show("Lỗi khi thêm.");
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Guna2CircleButton btn = (Guna2CircleButton)sender;
            int noteId = (int)btn.Tag;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bll_Notes.DeleteNote(noteId)) LoadNotes();
            }
        }

        private void btn_RemoveAll_Click(object sender, EventArgs e)
        {
            if (cbo_SelectClass.SelectedValue == null) return;
            int classId = (int)cbo_SelectClass.SelectedValue;
            string filter = cbo_FilterNote.SelectedItem.ToString();

            if (MessageBox.Show($"Xóa TẤT CẢ ghi chú loại '{filter}' của lớp này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (bll_Notes.DeleteNotesByFilter(classId, filter))
                {
                    MessageBox.Show("Đã xóa sạch!");
                    LoadNotes();
                }
            }
        }

        private void btn_ExportNote_Click(object sender, EventArgs e)
        {
            if (cbo_SelectClass.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn lớp cần xuất báo cáo!", "Thông báo");
                return;
            }

            int classId = (int)cbo_SelectClass.SelectedValue;
            string className = cbo_SelectClass.Text;

            // --- 1. XỬ LÝ THỜI GIAN (Mặc định 1 tháng) ---
            DateTime toDate = DateTime.Now;
            DateTime fromDate = DateTime.Now.AddMonths(-1); // Mặc định

            try
            {
                // --- 2. LẤY DỮ LIỆU ---
                List<NoteDTO> rawList = bll_Notes.GetNotesForExport(classId, fromDate, toDate);

                if (rawList.Count == 0)
                {
                    MessageBox.Show("Không có ghi chú nào trong khoảng thời gian này.", "Thông báo");
                    return;
                }

                // --- 3. GOM NHÓM DỮ LIỆU (Mỗi sinh viên 1 dòng) ---
                // GroupBy StudentId
                var groupedList = rawList.GroupBy(n => n.StudentId).ToList();

                // --- 4. KHỞI TẠO EXCEL ---
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "BaoCaoGhiChu";

                // Tiêu đề
                Excel.Range titleRange = worksheet.Range["A1", "E1"];
                titleRange.Merge();
                titleRange.Value = $"BÁO CÁO TÌNH HÌNH GHI CHÚ - LỚP {className.ToUpper()}";
                titleRange.Font.Size = 14;
                titleRange.Font.Bold = true;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Range["A2", "E2"].Merge();
                worksheet.Range["A2"].Value = $"Thời gian: {fromDate:dd/MM/yyyy} - {toDate:dd/MM/yyyy}";
                worksheet.Range["A2"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Header cột
                int row = 4;
                worksheet.Cells[row, 1] = "STT";
                worksheet.Cells[row, 2] = "Họ và Tên";
                worksheet.Cells[row, 3] = "Tổng số ghi chú";
                worksheet.Cells[row, 4] = "Chi tiết các ghi chú";
                worksheet.Cells[row, 5] = "Đánh giá chung"; // Cột tự động đánh giá dựa trên loại note

                // Style Header
                Excel.Range headerRange = worksheet.Range["A4", "E4"];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = Color.LightYellow;
                headerRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // --- 5. ĐỔ DỮ LIỆU (VÒNG LẶP) ---
                row = 5;
                int stt = 1;

                foreach (var group in groupedList)
                {
                    // Lấy thông tin học sinh (Lấy từ item đầu tiên của nhóm)
                    string studentName = group.First().StudentName;
                    int totalNotes = group.Count();

                    // Gom tất cả nội dung ghi chú thành 1 chuỗi dài (xuống dòng)
                    string contentCombined = "";
                    int violationCount = 0; // Đếm số lần vi phạm để đánh giá

                    foreach (var note in group)
                    {
                        // Định dạng: "- [01/10 - Vi phạm] : Mất trật tự (Cao)"
                        contentCombined += $"- [{note.CreatedAt:dd/MM} - {note.NoteType}]: {note.NoteContent} ({note.Priority})\n";

                        if (note.NoteType == "Vi phạm") violationCount++;
                    }
                    // Xóa ký tự xuống dòng thừa ở cuối
                    contentCombined = contentCombined.TrimEnd('\n');

                    // Ghi vào Excel
                    worksheet.Cells[row, 1] = stt++;
                    worksheet.Cells[row, 2] = studentName;
                    worksheet.Cells[row, 3] = totalNotes;
                    worksheet.Cells[row, 4] = contentCombined;

                    // Tô màu nếu vi phạm nhiều
                    if (violationCount >= 3)
                        worksheet.Range[$"A{row}", $"E{row}"].Font.Color = Color.Red;

                    row++;
                }

                // --- 6. ĐỊNH DẠNG LẠI BẢNG ---
                Excel.Range contentRange = worksheet.Range["A4", $"E{row - 1}"];
                contentRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                contentRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter; // Căn giữa dọc

                // Chỉnh độ rộng cột
                worksheet.Columns[1].ColumnWidth = 5;  // STT
                worksheet.Columns[2].ColumnWidth = 25; // Tên
                worksheet.Columns[3].ColumnWidth = 15; // Tổng số
                worksheet.Columns[4].ColumnWidth = 60; // Nội dung (Rất rộng)
                worksheet.Columns[5].ColumnWidth = 20; // Đánh giá

                // QUAN TRỌNG: Bật WrapText cho cột Nội dung để nó tự xuống dòng
                worksheet.Columns[4].WrapText = true;

                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
    }
}