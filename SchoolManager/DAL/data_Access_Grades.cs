using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_Grades : data_Access_Base
    {
        // 1. Lấy điểm thô (Dùng cho màn hình nhập điểm trên GridView)
        // Gọi SP: dbo.sp_GetRawGrades
        public DataTable GetRawGrades(int classId, string semester)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetRawGrades", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
                    cmd.Parameters.AddWithValue("@semester", semester);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Lấy danh sách môn học
        // Gọi SP: dbo.sp_GetAllSubjects
            public DataTable GetSubjects()
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllSubjects", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                return dt;
            }

        // 3. Lấy ID môn học từ tên (Dùng khi Lưu điểm)
        // Gọi SP: dbo.sp_GetSubjectIdByName
        public int GetSubjectIdByName(string subjectName)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetSubjectIdByName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", subjectName);

                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        // 4. Lưu danh sách điểm (Transaction an toàn)
        // Giữ nguyên logic vòng lặp và Transaction để đảm bảo an toàn dữ liệu
        public bool SaveGradeList(List<SubjectResultDTO> listGrades, int subjectId, string semester)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    foreach (var item in listGrades)
                    {
                        // Lưu điểm Giữa kỳ
                        SaveSingleGrade(conn, transaction, item.StudentId, subjectId, semester, "Mid", item.ScoreMid);
                        // Lưu điểm Cuối kỳ
                        SaveSingleGrade(conn, transaction, item.StudentId, subjectId, semester, "Final", item.ScoreFinal);
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi hệ thống khi lưu điểm: " + ex.Message);
                }
            }
        }

        // 5. Hàm phụ lưu từng điểm
        // Đã thay đổi: Gọi SP "dbo.sp_UpsertGrade" để tự động quyết định Insert hay Update
        private void SaveSingleGrade(SqlConnection conn, SqlTransaction trans, int sid, int subId, string sem, string type, double score)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.sp_UpsertGrade", conn, trans))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sid", sid);
                cmd.Parameters.AddWithValue("@subId", subId);
                cmd.Parameters.AddWithValue("@sem", sem);
                cmd.Parameters.AddWithValue("@type", type);
                cmd.Parameters.AddWithValue("@score", score);

                cmd.ExecuteNonQuery();
            }
        }

        // 6. Lấy dữ liệu thô báo cáo TOÀN TRƯỜNG (Theo Khối)
        // Gọi SP: dbo.sp_GetAcademicRawData
        public List<RawStudentScoreDTO> GetAcademicRawData(DateTime startDate, DateTime endDate)
        {
            List<RawStudentScoreDTO> list = new List<RawStudentScoreDTO>();

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAcademicRawData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@from", startDate);
                    cmd.Parameters.AddWithValue("@to", endDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RawStudentScoreDTO
                            {
                                StudentId = Convert.ToInt32(reader["id_student"]),
                                ClassName = reader["class_name"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Ethnicity = reader["ethnicity"] != DBNull.Value ? reader["ethnicity"].ToString() : "",
                                SubjectName = reader["subject_name"].ToString(),
                                Score = reader["grade_score"] != DBNull.Value ? Convert.ToDouble(reader["grade_score"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }

        // 7. Lấy dữ liệu thô báo cáo 1 LỚP
        // Gọi SP: dbo.sp_GetRawDataByClass
        public List<RawStudentScoreDTO> GetRawDataByClass(int classId, DateTime startDate, DateTime endDate)
        {
            List<RawStudentScoreDTO> list = new List<RawStudentScoreDTO>();

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetRawDataByClass", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
                    cmd.Parameters.AddWithValue("@from", startDate);
                    cmd.Parameters.AddWithValue("@to", endDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RawStudentScoreDTO
                            {
                                StudentId = Convert.ToInt32(reader["id_student"]),
                                ClassName = reader["class_name"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Ethnicity = reader["ethnicity"] != DBNull.Value ? reader["ethnicity"].ToString() : "",
                                SubjectName = reader["subject_name"].ToString(),
                                Score = reader["grade_score"] != DBNull.Value ? Convert.ToDouble(reader["grade_score"]) : 0
                            });
                        }
                    }
                }
            }
            return list;
        }

        public string GetHomeroomTeacherName(int classId)
        {
            string teacherName = "Chưa phân công";
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    // Join bảng classes và teachers để lấy tên
                    string query = @"
                SELECT t.full_name 
                FROM classes c
                JOIN teachers t ON c.id_teacher = t.id_teacher
                WHERE c.id_class = @classId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@classId", classId);
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            teacherName = result.ToString();
                        }
                    }
                }
            }
            catch { }
            return teacherName;
        }

        public DataTable GetTeachingAssignments(int teacherId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAssignmentsByTeacher", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
                // Tùy chọn: throw ex; nếu muốn hiện lỗi ra UI
            }
            return dt;
        }

        // 1.2. Lấy bảng điểm học sinh của 1 lớp - môn - học kỳ - loại điểm cụ thể
        // Gọi SP: dbo.sp_GetStudentGradesByClass
        public DataTable GetStudentGradesForInput(int classId, int subjectId, string semester, string period)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_GetStudentGradesByClass", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClassId", classId);
                        cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                        cmd.Parameters.AddWithValue("@Semester", semester);
                        cmd.Parameters.AddWithValue("@GradePeriod", period);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch { }
            return dt;
        }

        // 1.3. Lưu điểm lẻ (Dùng cho tính năng nhập điểm trực tiếp trên lưới)
        // Gọi SP: dbo.sp_SaveStudentGrade
        public bool SaveSpecificGrade(int studentId, int subjectId, decimal score, string semester, string period)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_SaveStudentGrade", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                        cmd.Parameters.AddWithValue("@Score", score);
                        cmd.Parameters.AddWithValue("@Semester", semester);
                        cmd.Parameters.AddWithValue("@GradePeriod", period);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lưu điểm: " + ex.Message);
            }
        }

        public List<SubjectResultDTO> GetSubjectDetail(int classId, int subjectId, string semester)
        {
            List<SubjectResultDTO> list = new List<SubjectResultDTO>();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetSubjectScoreForEdit", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClassId", classId);
                    cmd.Parameters.AddWithValue("@SubjectId", subjectId);
                    cmd.Parameters.AddWithValue("@Semester", semester);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SubjectResultDTO
                            {
                                StudentId = Convert.ToInt32(reader["id_student"]),
                                StudentCode = reader["student_code"].ToString(),
                                StudentName = reader["full_name"].ToString(),
                                ScoreMid = Convert.ToDouble(reader["ScoreMid"]),
                                ScoreFinal = Convert.ToDouble(reader["ScoreFinal"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public int GetSubjectId(string subjectName)
        {
            // (Giữ nguyên hàm GetSubjectIdByName bạn đã viết ở bài trước)
            return GetSubjectIdByName(subjectName);
        }
    }
}