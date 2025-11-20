using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_QR_Roll_Call : data_Access_Base
    {
        /// <summary>
        /// Điểm danh MỘT học sinh.
        /// Dùng MERGE (UPSERT) để Cập nhật nếu đã điểm danh, hoặc Thêm mới nếu chưa.
        /// </summary>
        public bool MarkStudentAttendance(int studentId, DateTime date, string status, string method)
        {
            // Lệnh MERGE kiểm tra (id_student, date)
            // 1. NẾU TỒN TẠI (MATCHED): Cập nhật trạng thái
            // 2. NẾU CHƯA TỒN TẠI (NOT MATCHED): Thêm mới
            string query = @"
                MERGE INTO attendance AS T
                USING (SELECT @id_Student AS id_student, @date AS attendance_date) AS S
                ON T.id_student = S.id_student AND T.attendance_date = S.attendance_date
                WHEN MATCHED THEN
                    UPDATE SET attendance_status = @status, attendance_method = @method
                WHEN NOT MATCHED THEN
                    INSERT (id_student, attendance_date, attendance_status, attendance_method)
                    VALUES (@id_Student, @date, @status, @method);";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_Student", studentId);
                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@method", method);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh ĐÃ ĐƯỢC ĐIỂM DANH (bằng QR hoặc Thủ công)
        /// </summary>
        public List<Students> GetTodaysAttendance(int id_Class, DateTime date)
        {
            List<Students> studentsList = new List<Students>();
            string query = @"SELECT 
                                s.id_student AS id_Student, 
                                s.student_code AS code_Student, 
                                s.full_name AS name_Student,
                                a.attendance_status,
                                a.attendance_method
                             FROM students s
                             JOIN attendance a ON s.id_student = a.id_student
                             WHERE s.id_class = @id_Class AND a.attendance_date = @date";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_Class", id_Class);
                cmd.Parameters.AddWithValue("@date", date.Date);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    studentsList.Add(new Students
                    {
                        //id_Student = (int)reader["id_Student"],
                        code_Student = reader["code_Student"].ToString(),
                        name_Student = reader["name_Student"].ToString()
                        // (Bạn có thể thêm thuộc tính 'Status' vào DTO 'Students'
                        // nếu muốn hiển thị trạng thái trên bảng)
                    });
                }
            }
            return studentsList;
        }

        public bool DeleteAttendanceByClassAndDate(int id_Class, DateTime date)
        {
            // Câu lệnh DELETE này JOIN với bảng students
            // để tìm ra id_student nào thuộc id_Class
            string query = @"DELETE A
                     FROM attendance A
                     JOIN students S ON A.id_student = S.id_student
                     WHERE S.id_class = @id_Class 
                       AND A.attendance_date = @date";

            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_Class", id_Class);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    conn.Open();
                    cmd.ExecuteNonQuery(); // Chạy lệnh xóa
                    return true; // Xóa thành công (ngay cả khi 0 dòng bị ảnh hưởng)
                }
            }
            catch (Exception)
            {
                return false; // Có lỗi xảy ra
            }
        }
    }
}