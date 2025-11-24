using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace SchoolManager.DAL
{
    public class data_Access_Students : data_Access_Base
    {
        // 1. HÀM LẤY DANH SÁCH THEO LỚP
        public List<Students> GetStudentsByClassId(int id_Class)
        {
            List<Students> list = new List<Students>();
            string query = @"SELECT id_student, student_code, full_name, date_of_birth, gender, ethnicity, id_class 
                             FROM students 
                             WHERE id_class = @idClass";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idClass", id_Class);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Students
                        {
                            id_Student = (int)reader["id_student"],
                            code_Student = reader["student_code"].ToString(),
                            name_Student = reader["full_name"].ToString(),
                            birthday = reader["date_of_birth"] != DBNull.Value ? Convert.ToDateTime(reader["date_of_birth"]) : DateTime.Now,
                            gender = reader["gender"].ToString(),
                            ethnicity = reader["ethnicity"] != DBNull.Value ? reader["ethnicity"].ToString() : "",
                            id_Class = (int)reader["id_class"]
                        });
                    }
                }
            }
            return list;
        }

        // 2. HÀM LẤY HỌC SINH THEO MÃ SỐ (ĐÃ KHÔI PHỤC VÀ CẬP NHẬT)
        public Students GetStudentByCode(string studentCode)
        {
            Students student = null;
            // Cập nhật query để không lấy các cột đã xóa (address, parent...)
            string query = @"SELECT id_student, student_code, full_name, date_of_birth, gender, ethnicity, id_class 
                             FROM students 
                             WHERE student_code = @code";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@code", studentCode);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Students
                        {
                            id_Student = (int)reader["id_student"],
                            code_Student = reader["student_code"].ToString(),
                            name_Student = reader["full_name"].ToString(),
                            birthday = reader["date_of_birth"] != DBNull.Value ? Convert.ToDateTime(reader["date_of_birth"]) : DateTime.Now,
                            gender = reader["gender"].ToString(),
                            ethnicity = reader["ethnicity"] != DBNull.Value ? reader["ethnicity"].ToString() : "",
                            id_Class = (int)reader["id_class"]
                        };
                    }
                }
            }
            return student;
        }

        // 3. HÀM IMPORT TỪ EXCEL
        public bool ImportStudentList(List<Students> listStudents)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    string query = @"INSERT INTO students (student_code, full_name, date_of_birth, gender, ethnicity, id_class)
                                     VALUES (@code, @name, @dob, @gender, @ethnicity, @idClass)";

                    foreach (var s in listStudents)
                    {
                        SqlCommand cmd = new SqlCommand(query, conn, trans);
                        cmd.Parameters.AddWithValue("@code", s.code_Student);
                        cmd.Parameters.AddWithValue("@name", s.name_Student);
                        cmd.Parameters.AddWithValue("@dob", s.birthday);
                        cmd.Parameters.AddWithValue("@gender", s.gender);
                        cmd.Parameters.AddWithValue("@ethnicity", s.ethnicity ?? "");
                        cmd.Parameters.AddWithValue("@idClass", s.id_Class);
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return true;
                }
                catch { trans.Rollback(); return false; }
            }
        }

        // 4. HÀM UPDATE (Sửa)
        public bool UpdateStudent(Students s)
        {
            string query = @"UPDATE students 
                             SET student_code = @code, full_name = @name, date_of_birth = @dob, gender = @gender, ethnicity = @ethnicity
                             WHERE id_student = @id";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", s.id_Student);
                cmd.Parameters.AddWithValue("@code", s.code_Student);
                cmd.Parameters.AddWithValue("@name", s.name_Student);
                cmd.Parameters.AddWithValue("@dob", s.birthday);
                cmd.Parameters.AddWithValue("@gender", s.gender);
                cmd.Parameters.AddWithValue("@ethnicity", s.ethnicity ?? "");
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 5. HÀM XÓA
        public bool DeleteStudent(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                string query = "DELETE FROM students WHERE id_student = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool InsertStudent(Students s)
        {
            // --- SỬA Ở ĐÂY: Đổi 'birthday' thành 'date_of_birth' ---
            string query = @"INSERT INTO students (student_code, full_name, date_of_birth, gender, ethnicity, id_class) 
                     VALUES (@code, @name, @dob, @gender, @eth, @classId)";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@code", s.code_Student);
                cmd.Parameters.AddWithValue("@name", s.name_Student);

                // DTO của bạn dùng tên biến là 'birthday', nhưng khi lưu xuống SQL phải vào cột 'date_of_birth'
                cmd.Parameters.AddWithValue("@dob", s.birthday);

                cmd.Parameters.AddWithValue("@gender", s.gender);
                cmd.Parameters.AddWithValue("@eth", s.ethnicity);
                cmd.Parameters.AddWithValue("@classId", s.id_Class);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Kiểm tra trùng mã
        public bool CheckStudentCodeExists(string code)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                string query = "SELECT COUNT(*) FROM students WHERE student_code = @code";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@code", code);
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
    }
}