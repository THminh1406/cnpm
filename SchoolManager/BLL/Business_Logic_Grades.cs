using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SchoolManager.BLL
{
    public class Business_Logic_Grades
    {
        private data_Access_Grades dal = new data_Access_Grades();

        // 1. Lấy danh sách môn học
        public DataTable GetSubjects() => dal.GetSubjects();

        // 2. Lấy ID môn học từ tên
        public int GetSubjectId(string name) => dal.GetSubjectIdByName(name);

        // 3. Bảng điểm TỔNG HỢP (Toán, Văn, Anh, GPA)
        public List<StudentSummaryDTO> GetSummaryReport(int classId, string semester)
        {
            DataTable rawData = dal.GetRawGrades(classId, semester);
            List<StudentSummaryDTO> result = new List<StudentSummaryDTO>();

            var grouped = rawData.AsEnumerable()
                .GroupBy(row => new {
                    Id = row.Field<int>("id_student"),
                    Name = row.Field<string>("full_name")
                });

            foreach (var group in grouped)
            {
                StudentSummaryDTO dto = new StudentSummaryDTO
                {
                    StudentId = group.Key.Id,
                    StudentName = group.Key.Name,
                    MathAvg = CalculateSubjectAvg(group, "Toán"),
                    LitAvg = CalculateSubjectAvg(group, "Tiếng Việt"),
                    EngAvg = CalculateSubjectAvg(group, "Tiếng Anh")
                };
                result.Add(dto);
            }
            return result;
        }

        private double CalculateSubjectAvg(IGrouping<dynamic, DataRow> rows, string subjectName)
        {
            var subjectRows = rows.Where(r => r["subject_name"]?.ToString() == subjectName);
            double mid = 0, final = 0;
            bool hasMid = false, hasFinal = false;

            foreach (var row in subjectRows)
            {
                string type = row["grade_period"]?.ToString();
                double score = row["grade_score"] != DBNull.Value ? Convert.ToDouble(row["grade_score"]) : 0;

                if (type == "Mid") { mid = score; hasMid = true; }
                if (type == "Final") { final = score; hasFinal = true; }
            }

            if (!hasMid && !hasFinal) return 0;
            return Math.Round((mid + final * 2) / 3, 1);
        }

        // 4. Bảng điểm CHI TIẾT 1 MÔN (Để nhập điểm)
        public List<SubjectResultDTO> GetSubjectDetail(int classId, string semester, string subjectName)
        {
            int subId = dal.GetSubjectIdByName(subjectName);
            if (subId == 0) return new List<SubjectResultDTO>();

            return dal.GetSubjectDetail(classId, subId, semester);
        }

        // 5. LƯU DANH SÁCH ĐIỂM
        public bool SaveGradeList(List<SubjectResultDTO> list, int subjectId, string semester)
        {
            return dal.SaveGradeList(list, subjectId, semester);
        }

        // =============================================================
        // 6. BÁO CÁO HỌC TẬP - TOÀN TRƯỜNG THEO KHỐI (Dành cho Hiệu trưởng)
        // =============================================================
        public List<AcademicReportDTO> GetAcademicReportData(DateTime startDate, DateTime endDate)
        {
            List<RawStudentScoreDTO> rawList = dal.GetAcademicRawData(startDate, endDate);

            Dictionary<int, AcademicReportDTO> reportMap = new Dictionary<int, AcademicReportDTO>();
            for (int i = 1; i <= 5; i++)
            {
                reportMap.Add(i, new AcademicReportDTO { ClassName = "Khối " + i });
            }

            var studentGroups = rawList.GroupBy(x => x.StudentId);

            foreach (var studentGroup in studentGroups)
            {
                var info = studentGroup.First();
                string gradeChar = info.ClassName.Trim().Substring(0, 1);

                if (!int.TryParse(gradeChar, out int gradeIndex)) continue;
                if (!reportMap.ContainsKey(gradeIndex)) continue;

                var stat = reportMap[gradeIndex];
                CalculateStats(stat, studentGroup, info);
            }

            return reportMap.Values.ToList();
        }

        // =============================================================
        // 7. BÁO CÁO HỌC TẬP - 1 LỚP DUY NHẤT (Dành cho GVCN)
        // =============================================================
        public AcademicReportDTO GetReportForSingleClass(int classId, DateTime startDate, DateTime endDate)
        {
            // 1. Lấy dữ liệu điểm thô từ DAL
            List<RawStudentScoreDTO> rawList = dal.GetRawDataByClass(classId, startDate, endDate);

            // Khởi tạo đối tượng báo cáo
            AcademicReportDTO report = new AcademicReportDTO();

            // A. Lấy tên GVCN và tính Năm học (Logic chúng ta đã thêm ở bước trước)
            report.HomeroomTeacher = dal.GetHomeroomTeacherName(classId);

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            if (currentMonth >= 8)
                report.SchoolYear = $"{currentYear} - {currentYear + 1}";
            else
                report.SchoolYear = $"{currentYear - 1} - {currentYear}";

            // Nếu không có dữ liệu điểm, trả về report rỗng (chỉ có tên GV và Năm học)
            if (rawList == null || rawList.Count == 0)
            {
                // Tùy chọn: Có thể gán tên lớp rỗng hoặc lấy từ DB nếu muốn kỹ hơn
                return report;
            }

            // Gán tên lớp (Lấy từ dòng dữ liệu đầu tiên)
            report.ClassName = rawList[0].ClassName;

            // --- KHỞI TẠO BẢNG 1: PHÂN PHỐI ĐIỂM (7 Dòng) ---
            var scoreMap = new Dictionary<string, ScoreDistributionDTO>();
            string[] labels = { "10", "9", "8", "7", "6", "5", "<5" };
            foreach (string label in labels)
            {
                var item = new ScoreDistributionDTO { ScoreLabel = label };
                report.ScoreTable.Add(item);
                scoreMap[label] = item;
            }

            // --- KHỞI TẠO BẢNG 2: CHI TIẾT MÔN (Đã có sẵn trong constructor của DTO, nhưng chắc chắn lại) ---
            if (report.MathStat == null) report.MathStat = new SubjectStat();
            if (report.LitStat == null) report.LitStat = new SubjectStat();
            if (report.EngStat == null) report.EngStat = new SubjectStat();

            // 2. TÍNH TOÁN DỮ LIỆU
            var studentGroups = rawList.GroupBy(x => x.StudentId);

            foreach (var group in studentGroups)
            {
                var info = group.First();

                // Xác định giới tính và dân tộc
                bool isFemale = !string.IsNullOrEmpty(info.Gender) &&
                                (info.Gender.Trim().ToLower() == "female" || info.Gender.Trim().ToLower() == "nữ");

                bool isEthnic = !string.IsNullOrEmpty(info.Ethnicity) &&
                                info.Ethnicity.Trim().ToLower() != "kinh";

                bool isFemaleEthnic = isFemale && isEthnic;

                // Cộng tổng số chung
                report.TotalStudents++;
                if (isFemaleEthnic) report.FemaleEthnics++;

                // Lấy điểm các môn (Nếu chưa có điểm thì tính là 0)
                double math = group.FirstOrDefault(x => x.SubjectName == "Toán")?.Score ?? 0;
                double lit = group.FirstOrDefault(x => x.SubjectName == "Tiếng Việt")?.Score ?? 0;
                double eng = group.FirstOrDefault(x => x.SubjectName == "Tiếng Anh")?.Score ?? 0;

                // --- TÍNH TOÁN BẢNG 1 (Điểm trung bình chung) ---
                double gpa = (math + lit + eng) / 3;
                int roundedScore = (int)Math.Round(gpa, 0, MidpointRounding.AwayFromZero);

                string bucketKey = "";
                if (roundedScore >= 10) bucketKey = "10";
                else if (roundedScore < 5) bucketKey = "<5";
                else bucketKey = roundedScore.ToString();

                if (scoreMap.ContainsKey(bucketKey))
                {
                    var row = scoreMap[bucketKey];
                    row.Total++;
                    if (isFemale) row.Female++;
                    if (isEthnic) row.Ethnic++;
                    if (isFemaleEthnic) row.FemaleEthnic++;
                }

                // --- TÍNH TOÁN BẢNG 2 (Xếp loại từng môn T/H/C) ---
                // Lưu ý: Đã sửa tham số thành SubjectStat để khớp với DTO của bạn
                CountSubjectRating(report.MathStat, math);
                CountSubjectRating(report.LitStat, lit);
                CountSubjectRating(report.EngStat, eng);
            }

            return report;
        }

        // HÀM PHỤ TRỢ: Đếm xếp loại môn học (Đã sửa tham số thành SubjectStat)
        private void CountSubjectRating(SubjectStat stat, double score)
        {
            // Quy ước: >=9 là Tốt(T), 5-8 là Hoàn thành(H), <5 là Cần cố gắng(C)
            if (score >= 9)
                stat.CountT++;
            else if (score >= 5)
                stat.CountH++;
            else
                stat.CountC++;
        }

        // --- HÀM PHỤ TÍNH TOÁN CHUNG CHO CẢ 2 LOẠI BÁO CÁO ---
        private void CalculateStats(AcademicReportDTO report, IGrouping<int, RawStudentScoreDTO> group, RawStudentScoreDTO info)
        {
            // 1. Tổng số
            report.TotalStudents++;

            // 2. Nữ Dân tộc
            bool isFemale = info.Gender.Trim().ToLower() == "female" || info.Gender.Trim().ToLower() == "nữ";
            bool isEthnic = info.Ethnicity.Trim().ToLower() != "kinh";
            if (isFemale && isEthnic) report.FemaleEthnics++;

            // 3. Điểm số
            double math = group.FirstOrDefault(x => x.SubjectName == "Toán")?.Score ?? 0;
            double lit = group.FirstOrDefault(x => x.SubjectName == "Tiếng Việt")?.Score ?? 0;
            double eng = group.FirstOrDefault(x => x.SubjectName == "Tiếng Anh")?.Score ?? 0;

            double gpa = (math + lit + eng) / 3;

            if (gpa >= 5.0) report.CompletedProgram++;
            if (gpa >= 8.0) report.Commendation++;

            // 4. Chi tiết môn
            CountSubjectRating(report.MathStat, math);
            CountSubjectRating(report.LitStat, lit);
            CountSubjectRating(report.EngStat, eng);
        }


        public DataTable GetTeachingAssignments(int teacherId)
        {
            return dal.GetTeachingAssignments(teacherId);
        }

        // 2. Lấy danh sách học sinh kèm điểm hiện tại để hiển thị lên lưới
        public DataTable GetStudentGradesForInput(int classId, int subjectId, string semester, string period)
        {
            return dal.GetStudentGradesForInput(classId, subjectId, semester, period);
        }

        // 3. Lưu điểm cho từng học sinh (Có kiểm tra logic nghiệp vụ)
        public bool SaveSpecificGrade(int studentId, int subjectId, decimal score, string semester, string period)
        {
            // Validate: Kiểm tra điểm hợp lệ tại tầng BLL trước khi gọi xuống Database
            if (score < 0 || score > 10)
            {
                throw new Exception("Điểm số không hợp lệ. Điểm phải nằm trong khoảng từ 0 đến 10.");
            }

            return dal.SaveSpecificGrade(studentId, subjectId, score, semester, period);
        }

    }
}