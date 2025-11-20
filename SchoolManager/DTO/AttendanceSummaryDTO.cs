using System;
using System.Collections.Generic;

namespace SchoolManager.DTO
{
    // DTO này dùng để chứa dữ liệu ĐÃ TỔNG HỢP
    public class AttendanceSummaryDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int TotalAbsent { get; set; } // Tổng số buổi vắng
        public string AbsentDates { get; set; } // Chuỗi ngày vắng "10/11, 12/11"
    }
}