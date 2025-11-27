using System;

namespace SchoolManager.DTO
{
    // Class này dùng để lưu trữ dữ liệu hiển thị chi tiết 1 môn học
    public class SubjectResultDTO
    {
        public int StudentId { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }

        // Điểm số (Cho phép null hoặc để 0 mặc định)
        public double ScoreMid { get; set; }   // Giữa kỳ
        public double ScoreFinal { get; set; } // Cuối kỳ

        // Điểm trung bình (Tính toán)
        public double SubjectAvg
        {
            get { return Math.Round((ScoreMid + ScoreFinal * 2) / 3, 2); }
        }
    }
}