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
        public DataTable GetRawGrades(int classId, string semester)
        {
            DataTable dt = new DataTable();
            string query = @"
                SELECT 
                    s.id_student, 
                    s.full_name, 
                    sub.subject_name, 
                    g.grade_period, 
                    g.grade_score
                FROM students s
                LEFT JOIN grades g ON s.id_student = g.id_student AND g.semester = @semester
                LEFT JOIN subjects sub ON g.id_subject = sub.id_subject
                WHERE s.id_class = @classId";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@semester", semester);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }

        // 2. Lấy danh sách môn học
        public DataTable GetSubjects()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM subjects", conn);
                da.Fill(dt);
            }
            return dt;
        }

        // 3. Lấy ID môn học từ tên (Dùng khi Lưu điểm)
        public int GetSubjectIdByName(string subjectName)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                string query = "SELECT id_subject FROM subjects WHERE subject_name = @name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", subjectName);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        // 4. Lưu danh sách điểm (Transaction an toàn)
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
                        SaveSingleGrade(conn, transaction, item.StudentId, subjectId, semester, "Mid", item.ScoreMid);
                        SaveSingleGrade(conn, transaction, item.StudentId, subjectId, semester, "Final", item.ScoreFinal);
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi SQL: " + ex.Message);
                }
            }
        }

        // 5. Hàm phụ lưu từng điểm
        private void SaveSingleGrade(SqlConnection conn, SqlTransaction trans, int sid, int subId, string sem, string type, double score)
        {
            string checkQuery = "SELECT COUNT(*) FROM grades WHERE id_student = @sid AND id_subject = @subId AND semester = @sem AND grade_period = @type";
            SqlCommand checkCmd = new SqlCommand(checkQuery, conn, trans);
            checkCmd.Parameters.AddWithValue("@sid", sid);
            checkCmd.Parameters.AddWithValue("@subId", subId);
            checkCmd.Parameters.AddWithValue("@sem", sem);
            checkCmd.Parameters.AddWithValue("@type", type);

            int count = (int)checkCmd.ExecuteScalar();
            string query;

            if (count > 0)
                query = "UPDATE grades SET grade_score = @score, grade_date = GETDATE() WHERE id_student = @sid AND id_subject = @subId AND semester = @sem AND grade_period = @type";
            else
                query = "INSERT INTO grades (id_student, id_subject, semester, grade_period, grade_score, grade_date) VALUES (@sid, @subId, @sem, @type, @score, GETDATE())";

            SqlCommand cmd = new SqlCommand(query, conn, trans);
            cmd.Parameters.AddWithValue("@sid", sid);
            cmd.Parameters.AddWithValue("@subId", subId);
            cmd.Parameters.AddWithValue("@sem", sem);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@score", score);
            cmd.ExecuteNonQuery();
        }

        // 6. Lấy dữ liệu thô báo cáo TOÀN TRƯỜNG (Theo Khối)
        public List<RawStudentScoreDTO> GetAcademicRawData(DateTime startDate, DateTime endDate)
        {
            List<RawStudentScoreDTO> list = new List<RawStudentScoreDTO>();
            string query = @"
                SELECT 
                    s.id_student,
                    c.class_name, 
                    s.gender,
                    s.ethnicity,
                    sub.subject_name,
                    g.grade_score
                FROM students s
                JOIN classes c ON s.id_class = c.id_class
                JOIN grades g ON s.id_student = g.id_student
                JOIN subjects sub ON g.id_subject = sub.id_subject
                WHERE g.grade_period = 'Final' 
                AND g.grade_date BETWEEN @from AND @to";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@from", startDate);
                cmd.Parameters.AddWithValue("@to", endDate);
                conn.Open();
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
            return list;
        }

        // 7. Lấy dữ liệu thô báo cáo 1 LỚP (Dùng cho báo cáo chi tiết lớp)
        public List<RawStudentScoreDTO> GetRawDataByClass(int classId, DateTime startDate, DateTime endDate)
        {
            List<RawStudentScoreDTO> list = new List<RawStudentScoreDTO>();
            string query = @"
                SELECT 
                    s.id_student,
                    c.class_name, 
                    s.gender,
                    s.ethnicity,
                    sub.subject_name,
                    g.grade_score
                FROM students s
                JOIN classes c ON s.id_class = c.id_class
                JOIN grades g ON s.id_student = g.id_student
                JOIN subjects sub ON g.id_subject = sub.id_subject
                WHERE s.id_class = @classId
                AND g.grade_period = 'Final' 
                AND g.grade_date BETWEEN @from AND @to";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@from", startDate);
                cmd.Parameters.AddWithValue("@to", endDate);
                conn.Open();
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
            return list;
        }
    }
}