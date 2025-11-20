using System;

namespace SchoolManager.DTO
{
    // Class này dùng để lưu trữ dữ liệu hiển thị chi tiết 1 môn học
    public class SubjectResultDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public double ScoreMid { get; set; }   // Điểm Giữa kỳ
        public double ScoreFinal { get; set; } // Điểm Cuối kỳ

        // Điểm Trung bình môn (Tự động tính)
        // Công thức: (Giữa kỳ + Cuối kỳ * 2) / 3
        public double SubjectAvg
        {
            get { return Math.Round((ScoreMid + ScoreFinal * 2) / 3, 1); }
        }
    }
}