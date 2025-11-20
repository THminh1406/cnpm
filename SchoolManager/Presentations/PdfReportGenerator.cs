using iTextSharp.text;
using iTextSharp.text.pdf;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolManager.Presentations
{
    public class PdfReportGenerator
    {
        // Đường dẫn đến file font của bạn (phải copy vào thư mục debug/release)
        // Environment.CurrentDirectory là thư mục chạy file .exe (thường là bin/Debug)
        public static string FONT_PATH = Path.Combine(Environment.CurrentDirectory, "Fonts", "ARIAL.TTF");

        private static BaseFont baseFont;
        private static Font fontTitle;
        private static Font fontHeader;
        private static Font fontBody;

        // Khởi tạo các font Tiếng Việt
        private static void InitializeFonts()
        {
            if (!File.Exists(FONT_PATH))
            {
                throw new FileNotFoundException("Không tìm thấy file font Tiếng Việt tại: " + FONT_PATH);
            }

            // BaseFont.IDENTITY_H = mã hóa ngang cho Unicode
            // BaseFont.EMBEDDED = nhúng font vào file PDF
            baseFont = BaseFont.CreateFont(FONT_PATH, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            fontTitle = new Font(baseFont, 16, Font.BOLD);
            fontHeader = new Font(baseFont, 12, Font.BOLD, BaseColor.WHITE);
            fontBody = new Font(baseFont, 11, Font.NORMAL);
        }

        public static bool CreateAttendanceReport(List<Roll_Call_Records> data, string savePath, string className, DateTime startDate, DateTime endDate)
        {
            try
            {
                // 1. Khởi tạo fonts
                InitializeFonts();

                // 2. Tạo tài liệu
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));

                document.Open();

                // 3. Tiêu đề
                Paragraph title = new Paragraph("BÁO CÁO CHUYÊN CẦN", fontTitle);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                // 4. Thông tin báo cáo (Lớp, Thời gian)
                Paragraph info = new Paragraph($"Lớp: {className}", fontBody);
                info.Alignment = Element.ALIGN_CENTER;
                document.Add(info);

                Paragraph dateRange = new Paragraph($"Từ ngày {startDate.ToShortDateString()} đến ngày {endDate.ToShortDateString()}", fontBody);
                dateRange.Alignment = Element.ALIGN_CENTER;
                dateRange.SpacingAfter = 20f; // Cách lề 20
                document.Add(dateRange);

                // 5. Tạo bảng
                PdfPTable table = new PdfPTable(5); // 5 cột
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 4f, 2f, 2f, 3f }); // Tỷ lệ độ rộng cột

                // 6. Thêm tiêu đề cột (Header)
                AddCellToHeader(table, "STT");
                AddCellToHeader(table, "Tên Học Sinh");
                AddCellToHeader(table, "Ngày");
                AddCellToHeader(table, "Trạng Thái");
                AddCellToHeader(table, "Ghi Chú");

                // 7. Thêm dữ liệu (Data Rows)
                int stt = 1;
                foreach (var record in data)
                {
                    AddCellToBody(table, stt.ToString());
                    AddCellToBody(table, record.name_Student); // <--- Tên Tiếng Việt
                    AddCellToBody(table, record.date.ToShortDateString());
                    AddCellToBody(table, record.status);
                    AddCellToBody(table, record.notes);
                    stt++;
                }

                // 8. Thêm bảng vào document
                document.Add(table);

                // 9. Đóng tài liệu
                document.Close();
                writer.Close();

                return true; // Tạo thành công
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                Console.WriteLine("Lỗi tạo PDF: " + ex.Message);
                return false; // Tạo thất bại
            }
        }

        // Hàm helper thêm Header
        private static void AddCellToHeader(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, fontHeader));
            cell.BackgroundColor = new BaseColor(0, 51, 102); // Màu xanh đậm
            cell.Padding = 5;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
        }

        // Hàm helper thêm Data
        private static void AddCellToBody(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, fontBody));
            cell.Padding = 5;
            table.AddCell(cell);
        }

        public static bool CreateAttendanceSummaryReport(List<AttendanceSummaryDTO> data, string savePath, string className, DateTime startDate, DateTime endDate)
        {
            try
            {
                // 1. Khởi tạo fonts (giống hệt)
                InitializeFonts();

                // 2. Tạo tài liệu (giống hệt)
                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(savePath, FileMode.Create));
                document.Open();

                // 3. Tiêu đề và Thông tin (giống hệt)
                Paragraph title = new Paragraph("BÁO CÁO TỔNG HỢP CHUYÊN CẦN", fontTitle);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                Paragraph info = new Paragraph($"Lớp: {className}", fontBody);
                info.Alignment = Element.ALIGN_CENTER;
                document.Add(info);

                Paragraph dateRange = new Paragraph($"Từ ngày {startDate.ToShortDateString()} đến ngày {endDate.ToShortDateString()}", fontBody);
                dateRange.Alignment = Element.ALIGN_CENTER;
                dateRange.SpacingAfter = 20f;
                document.Add(dateRange);

                // 4. === THAY ĐỔI BẢNG ===
                // Bảng mới chỉ có 4 cột
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 1f, 4f, 1.5f, 3.5f }); // Tỷ lệ (STT, Tên, Số buổi, Ngày vắng)

                // 5. Thêm tiêu đề cột (Header)
                AddCellToHeader(table, "STT");
                AddCellToHeader(table, "Tên Học Sinh");
                AddCellToHeader(table, "Số Buổi Vắng");
                AddCellToHeader(table, "Ngày Vắng");

                // 6. === THAY ĐỔI DỮ LIỆU ===
                int stt = 1;
                foreach (var record in data) // Dùng List<AttendanceSummaryDTO>
                {
                    AddCellToBody(table, stt.ToString());
                    AddCellToBody(table, record.StudentName);
                    AddCellToBody(table, record.TotalAbsent.ToString());
                    AddCellToBody(table, record.AbsentDates); // Thêm chuỗi ngày vắng
                    stt++;
                }

                // 7. Thêm bảng và Đóng document (giống hệt)
                document.Add(table);
                document.Close();
                writer.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tạo PDF Tổng Hợp: " + ex.Message);
                return false;
            }
        }
    }


}