using ExcelDataReader; // Cần cài NuGet: ExcelDataReader và ExcelDataReader.DataSet
using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Thêm cái này để nhận diện Guna2ComboBox

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uC_Import_FIile_Excel : UserControl
    {
        private Business_Logic_Students bll_Students;
        private Business_Logic_Classes bll_Classes;

        public uC_Import_FIile_Excel()
        {
            InitializeComponent();
        }

        private void uC_Import_FIile_Excel_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            bll_Students = new Business_Logic_Students();
            bll_Classes = new Business_Logic_Classes();

            // Cấu hình Kéo thả
            pnl_ContainerFileExcel.AllowDrop = true;
            pnl_ContainerFileExcel.DragEnter += pnl_ContainerFileExcel_DragEnter;
            pnl_ContainerFileExcel.DragDrop += pnl_ContainerFileExcel_DragDrop;

            LoadClasses();
        }

        // Tải danh sách lớp vào ComboBox (cbo_SelectClass)
        private void LoadClasses()
        {
            try
            {
                // Kiểm tra xem trong form có ComboBox tên cbo_SelectClass không
                if (cbo_SelectClass != null)
                {
                    var classes = bll_Classes.GetAllClasses();
                    cbo_SelectClass.DataSource = classes;
                    cbo_SelectClass.DisplayMember = "name_Class";
                    cbo_SelectClass.ValueMember = "id_Class";
                }
            }
            catch { }
        }

        // --- SỰ KIỆN KÉO THẢ FILE ---
        private void pnl_ContainerFileExcel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void pnl_ContainerFileExcel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                string filePath = files[0];
                string ext = Path.GetExtension(filePath).ToLower();
                // Chỉ nhận file Excel
                if (ext == ".xlsx" || ext == ".xls")
                {
                    ProcessExcelFile(filePath);
                }
                else
                {
                    MessageBox.Show("Vui lòng chỉ thả file Excel (.xlsx, .xls)");
                }
            }
        }

        // --- SỰ KIỆN NÚT CHỌN FILE ---
        private void btn_AddExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx;*.xls";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ProcessExcelFile(ofd.FileName);
            }
        }

        // --- HÀM XỬ LÝ IMPORT (QUAN TRỌNG NHẤT) ---
        private void ProcessExcelFile(string filePath)
        {
            // 1. Lấy ID lớp đang chọn
            int selectedClassId = 0;
            if (cbo_SelectClass != null && cbo_SelectClass.SelectedValue != null)
            {
                int.TryParse(cbo_SelectClass.SelectedValue.ToString(), out selectedClassId);
            }

            if (selectedClassId == 0)
            {
                MessageBox.Show("Vui lòng chọn Lớp trước khi nhập file!", "Cảnh báo");
                return;
            }

            try
            {
                List<Students> listImport = new List<Students>();

                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        DataTable table = result.Tables[0]; // Lấy Sheet 1

                        // Duyệt từ dòng 1 (dòng 0 là tiêu đề cột)
                        for (int i = 1; i < table.Rows.Count; i++)
                        {
                            DataRow row = table.Rows[i];

                            // Nếu Mã HS (Cột 1) trống thì bỏ qua
                            if (row[1] == null || string.IsNullOrEmpty(row[1].ToString())) continue;

                            Students s = new Students();

                            // === ÁNH XẠ CỘT ===
                            // Cột 0: STT -> Bỏ qua (Hệ thống tự sinh)

                            // Cột 1: Mã HS
                            s.code_Student = row[1].ToString();

                            // Cột 2: Họ Tên
                            s.name_Student = row[2].ToString();

                            // Cột 3: Ngày sinh
                            if (table.Columns.Count > 3 && row[3] != null)
                            {
                                if (DateTime.TryParse(row[3].ToString(), out DateTime dob))
                                    s.birthday = dob;
                                else
                                    s.birthday = DateTime.Now;
                            }
                            else s.birthday = DateTime.Now;

                            // Cột 4: Giới tính
                            if (table.Columns.Count > 4 && row[4] != null)
                            {
                                string genderText = row[4].ToString().Trim().ToLower();
                                s.gender = (genderText == "nam") ? "male" : "female";
                            }
                            else s.gender = "other";

                            // Cột 5: Dân tộc
                            if (table.Columns.Count > 5 && row[5] != null)
                            {
                                s.ethnicity = row[5].ToString();
                            }
                            else s.ethnicity = "";

                            // Gán ID Lớp
                            s.id_Class = selectedClassId;

                            // Thêm vào danh sách chuẩn bị lưu
                            listImport.Add(s);
                        }
                    }
                }

                // 2. Gọi BLL để lưu xuống CSDL
                if (listImport.Count > 0)
                {
                    bool success = bll_Students.ImportStudentList(listImport);
                    if (success)
                    {
                        MessageBox.Show($"Đã thêm thành công {listImport.Count} học sinh!", "Thành công");
                        // (Tùy chọn) Nếu muốn load lại danh sách trên form chính ngay, 
                        // bạn cần cơ chế reload (ví dụ dùng delegate hoặc event).
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi lưu vào CSDL. Có thể trùng Mã học sinh.", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("File Excel không có dữ liệu hoặc sai định dạng.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đọc file Excel: " + ex.Message + "\n(Hãy đóng file Excel trước khi import)", "Lỗi");
            }
        }
    }
}