using System;

namespace SchoolManager.DTO
{
    // 1. DTO cho Bảng Tổng Hợp (Tất cả các môn)
    public class StudentSummaryDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public double MathAvg { get; set; } // TB môn Toán
        public double LitAvg { get; set; }  // TB môn Văn (Tiếng Việt)
        public double EngAvg { get; set; }  // TB môn Anh

        // Điểm Trung Bình Chung = (Toán + Văn + Anh) / 3
        public double GPA
        {
            get { return Math.Round((MathAvg + LitAvg + EngAvg) / 3, 1); }
        }

        // Xếp loại
        public string Rank
        {
            get
            {
                if (GPA >= 7) return "T"; // Tốt
                if (GPA >= 5) return "H"; // Khá
                return "C";               // Yếu
            }
        }
    }

    // 2. DTO cho Bảng Chi Tiết Môn (Toán/Văn/Anh)
    public class StudentSubjectDetailDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public double ScoreMid { get; set; }   // Giữa kỳ
        public double ScoreFinal { get; set; } // Cuối kỳ

        // Trung bình môn = (Giữa kỳ + Cuối kỳ * 2) / 3
        // (Bạn có thể sửa công thức tùy quy định trường)
        public double SubjectAvg
        {
            get { return Math.Round((ScoreMid + ScoreFinal * 2) / 3, 1); }
        }
    }
}