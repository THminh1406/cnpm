using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolManager.BLL;
using SchoolManager.DTO;
// Thư viện Excel
using Excel = Microsoft.Office.Interop.Excel;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Rp_Statistics : UserControl
    {
        // Các lớp Logic
        private Business_Logic_Classes bll_Classes;
        private Business_Logic_Attendance bll_Attendance;
        private Business_Logic_Notes bll_Notes;
        private Business_Logic_Grades bll_Grades;

        public uc_Rp_Statistics()
        {
            InitializeComponent();
        }

        private void uc_Quick_Report_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            // Khởi tạo BLL
            bll_Classes = new Business_Logic_Classes();
            bll_Attendance = new Business_Logic_Attendance();
            bll_Notes = new Business_Logic_Notes();
            bll_Grades = new Business_Logic_Grades();

            // Tải dữ liệu
            LoadClassComboBox();
            LoadReportTypeComboBox();
            LoadTimeRangeComboBox();

            // Gắn sự kiện thay đổi loại báo cáo (để khóa/mở chọn ngày)
            cbo_TypeReport.SelectedIndexChanged -= cbo_TypeReport_SelectedIndexChanged;
            cbo_TypeReport.SelectedIndexChanged += cbo_TypeReport_SelectedIndexChanged;
        }

        private void LoadClassComboBox()
        {
            cbo_SelectClass.DataSource = bll_Classes.GetAllClasses();
            cbo_SelectClass.DisplayMember = "name_Class";
            cbo_SelectClass.ValueMember = "id_Class";
        }

        private void LoadReportTypeComboBox()
        {
            cbo_TypeReport.Items.Clear();
            cbo_TypeReport.Items.Add("Chuyên cần");
            cbo_TypeReport.Items.Add("Ghi chú");
            cbo_TypeReport.Items.Add("Học tập");
            if (cbo_TypeReport.Items.Count > 0) cbo_TypeReport.SelectedIndex = 0;
        }

        private void LoadTimeRangeComboBox()
        {
            cbo_SelectTime.Items.Clear();
            cbo_SelectTime.Items.Add("Tuần vừa qua");
            cbo_SelectTime.Items.Add("Tháng qua");
            cbo_SelectTime.Items.Add("Kỳ qua");
            if (cbo_SelectTime.Items.Count > 0) cbo_SelectTime.SelectedIndex = 0;
        }

        // --- SỰ KIỆN KHI CHỌN LOẠI BÁO CÁO ---
        private void cbo_TypeReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_TypeReport.SelectedItem != null && cbo_TypeReport.SelectedItem.ToString() == "Học tập")
            {
                // Nếu chọn Học tập -> Tự động chọn "Kỳ qua" và KHÓA lại
                if (cbo_SelectTime.Items.Contains("Kỳ qua"))
                    cbo_SelectTime.SelectedItem = "Kỳ qua";

                cbo_SelectTime.Enabled = false; // Khóa không cho chọn
            }
            else
            {
                cbo_SelectTime.Enabled = true; // Mở khóa
            }
        }

        // --- SỰ KIỆN NÚT TẠO BÁO CÁO ---
        private void btn_Generate_Report_Click(object sender, EventArgs e)
        {
            if (cbo_SelectClass.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbo_TypeReport.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại báo cáo.", "Lỗi");
                return;
            }

            int idLop = (int)cbo_SelectClass.SelectedValue;
            string reportType = cbo_TypeReport.SelectedItem.ToString();
            string timeRange = cbo_SelectTime.SelectedItem != null ? cbo_SelectTime.SelectedItem.ToString() : "Tháng qua";

            (DateTime startDate, DateTime endDate) = GetDateRange(timeRange);

            switch (reportType)
            {
                case "Chuyên cần":
                    GenerateAttendanceReport(idLop, startDate, endDate);
                    break;

                case "Ghi chú":
                    GenerateNotesReport(idLop, startDate, endDate);
                    break;

                case "Học tập":
                    GenerateAcademicReport(idLop, startDate, endDate);
                    break;

                default:
                    MessageBox.Show($"Chức năng báo cáo {reportType} đang phát triển.", "Thông báo");
                    break;
            }
        }

        // =========================================================
        // 1. BÁO CÁO HỌC TẬP (EXCEL - 2 BẢNG) - ĐÃ SỬA LỖI TRÙNG
        // =========================================================
        private void GenerateAcademicReport(int idLop, DateTime startDate, DateTime endDate)
        {
            try
            {
                // 1. Lấy dữ liệu
                List<AcademicReportDTO> dataList = new List<AcademicReportDTO>();
                // Lưu ý: Hàm GetReportForSingleClass trả về 1 object, ta bỏ vào list hoặc dùng trực tiếp
                AcademicReportDTO data = bll_Grades.GetReportForSingleClass(idLop, startDate, endDate);

                if (data == null) { MessageBox.Show("Không có dữ liệu."); return; }

                // 2. Khởi tạo Excel
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "BaoCaoLop";

                // =========================================================
                // BẢNG 1: THỐNG KÊ SỐ LƯỢNG THEO ĐIỂM SỐ
                // =========================================================

                // Tiêu đề
                Excel.Range title1 = worksheet.Range["A1", "E1"];
                title1.Merge();
                title1.Value = $"BÁO CÁO CHẤT LƯỢNG GIÁO DỤC LỚP: {data.ClassName}";
                title1.Font.Bold = true; title1.Font.Size = 16; title1.Font.Color = Color.Red;
                title1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Tên Bảng 1
                Excel.Range subTitle1 = worksheet.Range["A3", "E3"];
                subTitle1.Merge();
                subTitle1.Value = "BẢNG 1: THỐNG KÊ SỐ LƯỢNG THEO ĐIỂM SỐ";
                subTitle1.Font.Bold = true;
                subTitle1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Header Bảng 1
                int row = 4;
                worksheet.Cells[row, 1] = "Điểm";
                worksheet.Cells[row, 2] = "TS";
                worksheet.Cells[row, 3] = "Nữ";
                worksheet.Cells[row, 4] = "Dân tộc";
                worksheet.Cells[row, 5] = "NDT";

                Excel.Range header1 = worksheet.Range[$"A{row}", $"E{row}"];
                header1.Font.Bold = true; header1.Interior.Color = Color.LightBlue;
                header1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                header1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                row++;
                int sumTotal = 0, sumFemale = 0, sumEthnic = 0, sumFemaleEthnic = 0;

                // Đổ dữ liệu ScoreTable (10, 9, 8...)
                foreach (var item in data.ScoreTable)
                {
                    worksheet.Cells[row, 1] = item.ScoreLabel;
                    worksheet.Cells[row, 2] = item.Total;
                    worksheet.Cells[row, 3] = item.Female;
                    worksheet.Cells[row, 4] = item.Ethnic;
                    worksheet.Cells[row, 5] = item.FemaleEthnic;

                    sumTotal += item.Total;
                    sumFemale += item.Female;
                    sumEthnic += item.Ethnic;
                    sumFemaleEthnic += item.FemaleEthnic;
                    row++;
                }

                // Dòng TỔNG CỘNG
                worksheet.Cells[row, 1] = "TỔNG CỘNG";
                worksheet.Cells[row, 2] = sumTotal;
                worksheet.Cells[row, 3] = sumFemale;
                worksheet.Cells[row, 4] = sumEthnic;
                worksheet.Cells[row, 5] = sumFemaleEthnic;
                worksheet.Range[$"A{row}", $"E{row}"].Font.Bold = true;
                worksheet.Range[$"A{row}", $"E{row}"].Interior.Color = Color.LightYellow;

                // Kẻ khung Bảng 1
                worksheet.Range["A4", $"E{row}"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Range["A4", $"E{row}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // =========================================================
                // BẢNG 2: THỐNG KÊ MÔN HỌC (GIỮ NGUYÊN)
                // =========================================================

                row += 3;
                Excel.Range subTitle2 = worksheet.Range[$"A{row}", $"J{row}"];
                subTitle2.Merge();
                subTitle2.Value = "BẢNG 2: THỐNG KÊ CHẤT LƯỢNG CÁC MÔN HỌC";
                subTitle2.Font.Bold = true;
                subTitle2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                row += 1;
                int startHeaderRow = row;

                // Header Bảng 2
                worksheet.Range[$"A{row}", $"A{row + 1}"].Merge(); worksheet.Cells[row, 1] = "Lớp";
                worksheet.Range[$"A{row}", $"A{row + 1}"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                worksheet.Range[$"B{row}", $"D{row}"].Merge(); worksheet.Range[$"B{row}"].Value = "Môn Tiếng Việt";
                worksheet.Range[$"E{row}", $"G{row}"].Merge(); worksheet.Range[$"E{row}"].Value = "Môn Toán";
                worksheet.Range[$"H{row}", $"J{row}"].Merge(); worksheet.Range[$"H{row}"].Value = "Môn Ngoại Ngữ";

                row++;
                worksheet.Cells[row, 2] = "T"; worksheet.Cells[row, 3] = "H"; worksheet.Cells[row, 4] = "C";
                worksheet.Cells[row, 5] = "T"; worksheet.Cells[row, 6] = "H"; worksheet.Cells[row, 7] = "C";
                worksheet.Cells[row, 8] = "T"; worksheet.Cells[row, 9] = "H"; worksheet.Cells[row, 10] = "C";

                Excel.Range header2 = worksheet.Range[$"A{startHeaderRow}", $"J{row}"];
                header2.Font.Bold = true; header2.Interior.Color = Color.LightGreen;
                header2.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                header2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                row++;
                // Đổ dữ liệu Bảng 2 (Chỉ 1 dòng cho lớp hiện tại)
                worksheet.Cells[row, 1] = data.ClassName;

                worksheet.Cells[row, 2] = data.LitStat.CountT;
                worksheet.Cells[row, 3] = data.LitStat.CountH;
                worksheet.Cells[row, 4] = data.LitStat.CountC;

                worksheet.Cells[row, 5] = data.MathStat.CountT;
                worksheet.Cells[row, 6] = data.MathStat.CountH;
                worksheet.Cells[row, 7] = data.MathStat.CountC;

                worksheet.Cells[row, 8] = data.EngStat.CountT;
                worksheet.Cells[row, 9] = data.EngStat.CountH;
                worksheet.Cells[row, 10] = data.EngStat.CountC;

                worksheet.Range[$"A{startHeaderRow}", $"J{row}"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Range[$"A{startHeaderRow}", $"J{row}"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        // =========================================================
        // 2. BÁO CÁO GHI CHÚ (EXCEL - 1 DÒNG/SV)
        // =========================================================
        private void GenerateNotesReport(int idLop, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<NoteDTO> rawList = bll_Notes.GetNotesForExport(idLop, startDate, endDate);
                if (rawList == null || rawList.Count == 0)
                {
                    MessageBox.Show("Không có ghi chú nào trong khoảng thời gian này.");
                    return;
                }

                var groupedList = rawList.GroupBy(n => n.StudentId).ToList();
                string className = cbo_SelectClass.Text;

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "GhiChu";

                worksheet.Range["A1", "E1"].Merge();
                worksheet.Range["A1"].Value = $"BÁO CÁO GHI CHÚ - LỚP {className.ToUpper()}";
                worksheet.Range["A1"].Font.Bold = true;
                worksheet.Range["A1"].Font.Size = 14;

                int row = 3;
                worksheet.Cells[row, 1] = "STT"; worksheet.Cells[row, 2] = "Họ Tên";
                worksheet.Cells[row, 3] = "Tổng số"; worksheet.Cells[row, 4] = "Chi tiết"; worksheet.Cells[row, 5] = "Đánh giá";
                worksheet.Range[$"A{row}", $"E{row}"].Font.Bold = true;
                worksheet.Range[$"A{row}", $"E{row}"].Interior.Color = Color.LightYellow;

                row++;
                int stt = 1;
                foreach (var group in groupedList)
                {
                    string name = group.First().StudentName;
                    int total = group.Count();
                    StringBuilder content = new StringBuilder();
                    int violations = 0;

                    foreach (var n in group)
                    {
                        content.AppendLine($"- [{n.CreatedAt:dd/MM} {n.NoteType}]: {n.NoteContent} ({n.Priority})");
                        if (n.NoteType == "Vi phạm") violations++;
                    }

                    string assessment = violations >= 3 ? "CẦN GẶP PHỤ HUYNH" : (violations > 0 ? "Cần nhắc nhở" : "Tốt");

                    worksheet.Cells[row, 1] = stt++;
                    worksheet.Cells[row, 2] = name;
                    worksheet.Cells[row, 3] = total;
                    worksheet.Cells[row, 4] = content.ToString().TrimEnd();
                    worksheet.Cells[row, 5] = assessment;

                    if (violations >= 3) worksheet.Range[$"E{row}"].Font.Color = Color.Red;
                    row++;
                }

                worksheet.Range["A3", $"E{row - 1}"].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                worksheet.Columns[4].ColumnWidth = 60;
                worksheet.Columns[4].WrapText = true;
                worksheet.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        // =========================================================
        // 3. BÁO CÁO CHUYÊN CẦN (PDF) - GIỮ NGUYÊN
        // =========================================================
        private void GenerateAttendanceReport(int idLop, DateTime startDate, DateTime endDate)
        {
            List<AttendanceSummaryDTO> data = bll_Attendance.GetAttendanceSummaryReport(idLop, startDate, endDate);
            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu chuyên cần.");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveDialog.FileName = $"ChuyenCan_{DateTime.Now:yyyyMMdd}.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Gọi class tạo PDF của bạn (Giả sử class tên PdfReportGenerator)
                    bool success = PdfReportGenerator.CreateAttendanceSummaryReport(data, saveDialog.FileName, cbo_SelectClass.Text, startDate, endDate);
                    if (success)
                    {
                        MessageBox.Show("Xuất PDF thành công!");
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi PDF: " + ex.Message);
                }
            }
        }


        // HÀM HỖ TRỢ NGÀY
        private (DateTime, DateTime) GetDateRange(string timeRange)
        {
            DateTime end = DateTime.Today;
            DateTime start = end.AddMonths(-1);

            if (timeRange == "Tuần vừa qua") start = end.AddDays(-7);
            else if (timeRange == "Tháng qua") start = end.AddMonths(-1);
            else if (timeRange == "Kỳ qua") start = end.AddMonths(-4);

            return (start, end);
        }
    }
}