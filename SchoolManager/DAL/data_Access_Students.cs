using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_Students : data_Access_Base
    {
        // 1. HÀM LẤY DANH SÁCH THEO LỚP
        // Gọi SP: dbo.sp_GetStudentsByClassId
        public List<Students> GetStudentsByClassId(int id_Class)
        {
            List<Students> list = new List<Students>();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetStudentsByClassId", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", id_Class);

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
            }
            return list;
        }

        // 2. HÀM LẤY HỌC SINH THEO MÃ SỐ
        // Gọi SP: dbo.sp_GetStudentByCode
        public Students GetStudentByCode(string studentCode)
        {
            Students student = null;
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetStudentByCode", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@code", studentCode);

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
            }
            return student;
        }

        // 3. HÀM IMPORT TỪ EXCEL (Transaction + Loop Insert SP)
        // Gọi SP: dbo.sp_InsertStudent (trong vòng lặp)
        public bool ImportStudentList(List<Students> listStudents)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    foreach (var s in listStudents)
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertStudent", conn, trans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@code", s.code_Student);
                            cmd.Parameters.AddWithValue("@name", s.name_Student);
                            cmd.Parameters.AddWithValue("@dob", s.birthday);
                            cmd.Parameters.AddWithValue("@gender", s.gender);
                            cmd.Parameters.AddWithValue("@ethnicity", s.ethnicity ?? "");
                            cmd.Parameters.AddWithValue("@classId", s.id_Class);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
        }

        // 4. HÀM UPDATE (Sửa)
        // Gọi SP: dbo.sp_UpdateStudent
        public bool UpdateStudent(Students s)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_UpdateStudent", conn))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", s.id_Student);
                        cmd.Parameters.AddWithValue("@code", s.code_Student);
                        cmd.Parameters.AddWithValue("@name", s.name_Student);
                        cmd.Parameters.AddWithValue("@dob", s.birthday);
                        cmd.Parameters.AddWithValue("@gender", s.gender);
                        cmd.Parameters.AddWithValue("@ethnicity", s.ethnicity ?? "");

                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        // 5. HÀM XÓA
        // Gọi SP: dbo.sp_DeleteStudent
        public bool DeleteStudent(int id)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteStudent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. HÀM THÊM 1 HỌC SINH (Insert lẻ)
        // Gọi SP: dbo.sp_InsertStudent
        public bool InsertStudent(Students s)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertStudent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@code", s.code_Student);
                        cmd.Parameters.AddWithValue("@name", s.name_Student);

                        // Xử lý ngày sinh: Đảm bảo không truyền MinValue
                        if (s.birthday == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@dob", DateTime.Now);
                        else
                            cmd.Parameters.AddWithValue("@dob", s.birthday);

                        cmd.Parameters.AddWithValue("@gender", s.gender);

                        // Xử lý null cho dân tộc
                        // Lưu ý: DB bạn đang để ethnicity NVARCHAR(50), nếu null thì truyền chuỗi rỗng
                        object ethValue = string.IsNullOrEmpty(s.ethnicity) ? (object)DBNull.Value : s.ethnicity;
                        cmd.Parameters.AddWithValue("@ethnicity", ethValue);

                        cmd.Parameters.AddWithValue("@classId", s.id_Class);

                        // Vì đã bỏ SET NOCOUNT ON, hàm này sẽ trả về 1 (số dòng thêm được)
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log lỗi ra để biết chi tiết nếu có
                    throw new Exception("Lỗi SQL Insert: " + ex.Message);
                }
            }
        }

        // 7. KIỂM TRA TRÙNG MÃ
        // Gọi SP: dbo.sp_CheckStudentCodeExists
        public bool CheckStudentCodeExists(string code)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_CheckStudentExists", conn);
                    cmd.CommandType = CommandType.StoredProcedure; // QUAN TRỌNG

                    cmd.Parameters.AddWithValue("@code", code);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}