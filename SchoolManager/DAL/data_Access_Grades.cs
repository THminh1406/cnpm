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
    }
}