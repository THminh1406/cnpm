using System;
using System.Collections.Generic;

namespace SchoolManager.DTO
{
    // 1. Class cho từng dòng của Bảng 1 (Thống kê điểm 10, 9, 8...)
    public class ScoreDistributionDTO
    {
        public string ScoreLabel { get; set; } // "10", "9", "<5"
        public int Total { get; set; } = 0;        // Tổng số
        public int Female { get; set; } = 0;       // Nữ
        public int Ethnic { get; set; } = 0;       // Dân tộc
        public int FemaleEthnic { get; set; } = 0; // Nữ Dân tộc
    }

    // 2. Class cho Bảng 2 (Chi tiết T/H/C của môn học)
    public class SubjectStat
    {
        public int CountT { get; set; } = 0; // Tốt
        public int CountH { get; set; } = 0; // Hoàn thành
        public int CountC { get; set; } = 0; // Cần cố gắng
    }

    // 3. Class TỔNG HỢP (Dùng để hứng dữ liệu cuối cùng xuất ra Excel)
    public class AcademicReportDTO
    {
        public string ClassName { get; set; }
        public string GradeName { get; set; } // Dùng cho báo cáo khối nếu cần

        // --- Bảng 1: Tổng hợp chung ---
        public int TotalStudents { get; set; } = 0;
        public int FemaleEthnics { get; set; } = 0;
        public int CompletedProgram { get; set; } = 0;
        public int Commendation { get; set; } = 0;

        // --- Dữ liệu chi tiết Bảng 1 (List 10, 9, 8...) ---
        public List<ScoreDistributionDTO> ScoreTable { get; set; } = new List<ScoreDistributionDTO>();

        // --- Dữ liệu Bảng 2 (Từng môn) ---
        public SubjectStat MathStat { get; set; } = new SubjectStat();
        public SubjectStat LitStat { get; set; } = new SubjectStat();
        public SubjectStat EngStat { get; set; } = new SubjectStat();
    }

    // 4. Class HỨNG DỮ LIỆU THÔ TỪ SQL (Nguyên nhân gây lỗi trùng của bạn)
    public class RawStudentScoreDTO
    {
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string Gender { get; set; }
        public string Ethnicity { get; set; }
        public string SubjectName { get; set; }
        public double Score { get; set; }
    }
}