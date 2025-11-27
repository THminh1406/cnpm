using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SchoolManager.Presentations
{
    public partial class uc_Study_Result : UserControl
    {

        private DataTable dtTeachingAssignments;
        Business_Logic_Classes bll_Classes;
        Business_Logic_Grades bll_Grades;
        List<StudentSummaryDTO> summaryListForExport;
        public uc_Study_Result()
        {
            InitializeComponent();
        }


        private void uc_Study_Result_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            bll_Classes = new Business_Logic_Classes();
            bll_Grades = new Business_Logic_Grades();

            // 1. Setup Học kỳ
            cbo_Semester.Items.Clear();
            cbo_Semester.Items.Add("HocKy1");
            cbo_Semester.Items.Add("HocKy2");
            cbo_Semester.SelectedIndex = 0;

            // 2. Lấy danh sách phân công (QUAN TRỌNG)
            int currentTeacherId = SchoolManager.Session.CurrentTeacherId;

            // Lưu vào biến toàn cục để dùng lại sau này
            this.dtTeachingAssignments = bll_Grades.GetTeachingAssignments(currentTeacherId);

            // Lọc ra danh sách Lớp (Distinct) để đổ vào cbo_SelectClass
            if (dtTeachingAssignments.Rows.Count > 0)
            {
                DataView view = new DataView(dtTeachingAssignments);
                DataTable distinctClasses = view.ToTable(true, "id_class", "class_name");

                // Gỡ sự kiện trước khi gán DataSource để tránh kích hoạt sự kiện khi chưa load xong
                cbo_SelectClass.SelectedIndexChanged -= cbo_SelectClass_SelectedIndexChanged;

                cbo_SelectClass.DataSource = distinctClasses;
                cbo_SelectClass.DisplayMember = "class_name";
                cbo_SelectClass.ValueMember = "id_class";

                // Gắn lại sự kiện
                cbo_SelectClass.SelectedIndexChanged += cbo_SelectClass_SelectedIndexChanged;
            }

            // 3. XÓA ĐOẠN CODE CŨ LOAD TOÀN BỘ MÔN HỌC Ở ĐÂY ĐI NHÉ
            // (Vì chúng ta sẽ load môn theo lớp ở sự kiện SelectedIndexChanged bên dưới)

            // 4. Cấu hình Grid
            dgv_Result.AutoGenerateColumns = false;
            dgv_Result.AllowUserToAddRows = false;
            dgv_Result.ReadOnly = false;

            // Gắn các sự kiện lọc khác
            cbo_Semester.SelectedIndexChanged += Filter_Changed;
            cbo_Subject.SelectedIndexChanged += Filter_Changed;

            // Kích hoạt lần đầu để load môn cho lớp đầu tiên
            if (cbo_SelectClass.Items.Count > 0)
            {
                cbo_SelectClass.SelectedIndex = 0;
                LoadSubjectsBySelectedClass(); // Gọi hàm load môn
            }
        }

        private void cbo_SelectClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubjectsBySelectedClass();
            Filter_Changed(sender, e); // Load lại dữ liệu lưới
        }

        // Hàm lọc môn học theo lớp đã chọn
        private void LoadSubjectsBySelectedClass()
        {
            if (cbo_SelectClass.SelectedValue == null || dtTeachingAssignments == null) return;

            int selectedClassId;
            if (!int.TryParse(cbo_SelectClass.SelectedValue.ToString(), out selectedClassId)) return;

            // Lọc các dòng trong bảng phân công có id_class trùng với lớp đang chọn
            DataRow[] rows = dtTeachingAssignments.Select("id_class = " + selectedClassId);

            // Xóa danh sách môn cũ
            cbo_Subject.Items.Clear();

            // Tùy chọn: Vẫn giữ "Tổng hợp" để xem báo cáo (nếu muốn), 
            // nhưng nếu muốn chặt chẽ chỉ cho nhập điểm thì có thể bỏ dòng này.
            cbo_Subject.Items.Add("Tổng hợp");

            // Chỉ thêm các môn CÓ TRONG PHÂN CÔNG
            foreach (DataRow row in rows)
            {
                string subjectName = row["subject_name"].ToString();
                // Kiểm tra để không add trùng (dù logic Distinct lớp đã lọc, nhưng an toàn vẫn hơn)
                if (!cbo_Subject.Items.Contains(subjectName))
                {
                    cbo_Subject.Items.Add(subjectName);
                }
            }

            // Chọn môn đầu tiên mặc định
            if (cbo_Subject.Items.Count > 0) cbo_Subject.SelectedIndex = 0;
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            LoadGridData();
        }

        private void LoadGridData()
        {
            if (cbo_SelectClass.SelectedValue == null || cbo_Subject.SelectedItem == null) return;

            int classId;
            // Xử lý an toàn khi lấy value
            if (!int.TryParse(cbo_SelectClass.SelectedValue.ToString(), out classId)) return;

            string semester = cbo_Semester.SelectedItem.ToString();
            string subject = cbo_Subject.SelectedItem.ToString();

            // Xóa sạch cột cũ để vẽ lại
            dgv_Result.Columns.Clear();
            dgv_Result.DataSource = null;

            if (subject == "Tổng hợp")
            {
                // --- CHẾ ĐỘ 1: TỔNG HỢP ---
                CreateColumns_Summary(); // Tạo cột: Toán, Văn, Anh...

                var list = bll_Grades.GetSummaryReport(classId, semester);
                this.summaryListForExport = list; // Lưu để xuất Excel
                dgv_Result.DataSource = list;

                UpdateLabels(list); // Cập nhật Label thống kê
            }
            else
            {
                // === CHẾ ĐỘ 2: CHI TIẾT MÔN ===
                CreateColumns_Detail();

                var list = bll_Grades.GetSubjectDetail(classId, semester, subject);
                dgv_Result.DataSource = list;

                // --- SỬA PHẦN HIỂN THỊ LABEL (CHỈ HIỆN SỐ) ---

                // 1. Sĩ số
                lbl_Student.Text = list.Count.ToString();

                // 2. Điểm TB của môn đó (Cả lớp)
                if (list.Count > 0)
                {
                    double avgSubject = list.Average(x => x.SubjectAvg);
                    lbl_GPA.Text = Math.Round(avgSubject, 2).ToString();
                }
                else
                {
                    lbl_GPA.Text = "0";
                }

                // 3. Xuất sắc / Cần cải thiện (Không áp dụng cho 1 môn -> Hiện 0)
                lbl_Excllent.Text = "0";
                lbl_Improve.Text = "0";
            }
        }

        // Tạo cột cho bảng Tổng hợp
        private void CreateColumns_Summary()
        {
            dgv_Result.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ và Tên", DataPropertyName = "StudentName", Width = 250 });
            dgv_Result.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Toán", DataPropertyName = "MathAvg" });
            dgv_Result.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tiếng Việt", DataPropertyName = "LitAvg" });
            dgv_Result.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tiếng Anh", DataPropertyName = "EngAvg" });
            dgv_Result.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ĐTB", DataPropertyName = "GPA" });
            dgv_Result.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Xếp loại", DataPropertyName = "Rank" });
        }

        // Tạo cột cho bảng Chi tiết môn
        private void CreateColumns_Detail()
        {
            dgv_Result.Columns.Clear();

            var colName = new DataGridViewTextBoxColumn { HeaderText = "Họ và Tên", DataPropertyName = "StudentName", Width = 200 };
            colName.ReadOnly = true; // Không cho sửa tên
            dgv_Result.Columns.Add(colName);

            var colMid = new DataGridViewTextBoxColumn { HeaderText = "Điểm Giữa Kỳ", DataPropertyName = "ScoreMid" };
            colMid.ReadOnly = false; // [CHO PHÉP SỬA]
            dgv_Result.Columns.Add(colMid);

            var colFinal = new DataGridViewTextBoxColumn { HeaderText = "Điểm Cuối Kỳ", DataPropertyName = "ScoreFinal" };
            colFinal.ReadOnly = false; // [CHO PHÉP SỬA]
            dgv_Result.Columns.Add(colFinal);

            var colAvg = new DataGridViewTextBoxColumn { HeaderText = "TB Môn", DataPropertyName = "SubjectAvg" };
            colAvg.ReadOnly = true; // Điểm TB tự tính, không cho sửa
            dgv_Result.Columns.Add(colAvg);
        }

        // Cập nhật các Label thống kê
        private void UpdateLabels(List<StudentSummaryDTO> list)
        {
            if (list.Count == 0)
            {
                lbl_Student.Text = "0";
                lbl_GPA.Text = "0";
                lbl_Excllent.Text = "0";
                lbl_Improve.Text = "0";
                return;
            }

            // 1. Sĩ số
            lbl_Student.Text = list.Count.ToString();

            // 2. Điểm trung bình lớp (Làm tròn 1 hoặc 2 số thập phân)
            double classAvg = list.Average(x => x.GPA);
            lbl_GPA.Text = Math.Round(classAvg, 2).ToString(); // Ví dụ: "8.25"

            // 3. Số lượng Xuất sắc (Rank T)
            int countGood = list.Count(x => x.Rank == "T");
            lbl_Excllent.Text = countGood.ToString();

            // 4. Số lượng Cần cải thiện (Rank C)
            int countBad = list.Count(x => x.Rank == "C");
            lbl_Improve.Text = countBad.ToString();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (cbo_Subject.SelectedItem.ToString() != "Tổng hợp")
            {
                MessageBox.Show("Chức năng này chỉ xuất bảng điểm Tổng hợp.", "Thông báo");
                return;
            }

            if (summaryListForExport == null || summaryListForExport.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            try
            {
                // Khởi tạo Excel
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "BangDiemLop";

                // Tiêu đề
                worksheet.Cells[1, 1] = "STT";
                worksheet.Cells[1, 2] = "Họ Tên";
                worksheet.Cells[1, 3] = "Toán";
                worksheet.Cells[1, 4] = "Tiếng Việt";
                worksheet.Cells[1, 5] = "Tiếng Anh";
                worksheet.Cells[1, 6] = "ĐTB";
                worksheet.Cells[1, 7] = "Xếp loại";

                // Đổ dữ liệu
                for (int i = 0; i < summaryListForExport.Count; i++)
                {
                    var item = summaryListForExport[i];
                    worksheet.Cells[i + 2, 1] = i + 1;
                    worksheet.Cells[i + 2, 2] = item.StudentName;
                    worksheet.Cells[i + 2, 3] = item.MathAvg;
                    worksheet.Cells[i + 2, 4] = item.LitAvg;
                    worksheet.Cells[i + 2, 5] = item.EngAvg;
                    worksheet.Cells[i + 2, 6] = item.GPA;
                    worksheet.Cells[i + 2, 7] = item.Rank;
                }

                worksheet.Columns.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            // Kiểm tra điều kiện
            if (cbo_Subject.SelectedItem.ToString() == "Tổng hợp")
            {
                MessageBox.Show("Không thể lưu ở chế độ Tổng hợp. Vui lòng chọn từng môn để nhập điểm.");
                return;
            }

            if (dgv_Result.Rows.Count == 0) return;

            try
            {
                // Lấy thông tin chung
                string semester = cbo_Semester.SelectedItem.ToString();
                string subjectName = cbo_Subject.SelectedItem.ToString();
                int subjectId = bll_Grades.GetSubjectId(subjectName);

                // Tạo danh sách chứa dữ liệu từ bảng
                List<SubjectResultDTO> listToSave = new List<SubjectResultDTO>();

                // Duyệt qua từng dòng trong DataGridView
                foreach (DataGridViewRow row in dgv_Result.Rows)
                {
                    // Lấy dữ liệu từ DataBoundItem (An toàn hơn lấy Cells)
                    SubjectResultDTO dto = (SubjectResultDTO)row.DataBoundItem;

                    // (Tùy chọn) Kiểm tra điểm hợp lệ lần cuối
                    if (dto.ScoreMid < 0 || dto.ScoreMid > 10 || dto.ScoreFinal < 0 || dto.ScoreFinal > 10)
                    {
                        MessageBox.Show($"Điểm của học sinh {dto.StudentName} không hợp lệ (0-10).");
                        return; // Dừng lại không lưu
                    }

                    listToSave.Add(dto);
                }

                // GỌI BLL ĐỂ LƯU TOÀN BỘ
                bool success = bll_Grades.SaveGradeList(listToSave, subjectId, semester);

                if (success)
                {
                    MessageBox.Show("Đã lưu bảng điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGridData(); // Load lại để cập nhật các cột tính toán nếu cần
                }
                else
                {
                    MessageBox.Show("Lưu thất bại. Vui lòng thử lại.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
