using SchoolManager.DAL; // Phải using DAL
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManager.BLL
{
    public class Business_Logic_Attendance
    {
        // Khởi tạo DAL
        private data_Access_Manual_Roll_Call dal_Attendance;

        private const string ABSENT_STATUS = "absent_unpermitted";

        public Business_Logic_Attendance()
        {
            dal_Attendance = new data_Access_Manual_Roll_Call();
        }

        /// <summary>
        /// Lấy danh sách điểm danh cho báo cáo
        /// </summary>
        public List<Roll_Call_Records> GetAttendanceForReport(int id_Class, DateTime startDate, DateTime endDate)
        {
            // BLL gọi DAL
            // (Sau này có thể thêm logic kiểm tra ở đây)
            return dal_Attendance.GetAttendanceForReport(id_Class, startDate, endDate);
        }

        // (Bạn có thể chuyển các hàm BLL khác liên quan đến điểm danh vào đây)

        public List<AttendanceSummaryDTO> GetAttendanceSummaryReport(int id_Class, DateTime startDate, DateTime endDate)
        {
            // 1. Lấy dữ liệu thô (giống như trước)
            List<Roll_Call_Records> rawData = dal_Attendance.GetAttendanceForReport(id_Class, startDate, endDate);

            if (rawData == null || rawData.Count == 0)
                return new List<AttendanceSummaryDTO>(); // Trả về danh sách rỗng

            // 2. Dùng LINQ để nhóm dữ liệu thô
            var summaryData = rawData
                .GroupBy(record => new { record.id_Student, record.name_Student }) // Nhóm theo ID và Tên
                .Select(group => {

                    // Lọc ra các ngày vắng của học sinh này
                    var absences = group
                        .Where(r => r.status.Equals(ABSENT_STATUS, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    // Tạo danh sách ngày vắng
                    string absentDates = string.Join(", ", absences.Select(a => a.date.ToShortDateString()));

                    return new AttendanceSummaryDTO
                    {
                        StudentId = group.Key.id_Student,
                        StudentName = group.Key.name_Student,
                        TotalAbsent = absences.Count, // Đếm số lần vắng
                        AbsentDates = absentDates // Chuỗi ngày vắng
                    };
                })
                .OrderBy(s => s.StudentName) // Sắp xếp theo tên
                .ToList();

            return summaryData;
        }
    }
}