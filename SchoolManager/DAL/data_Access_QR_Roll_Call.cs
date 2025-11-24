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
        /// Gọi SP: dbo.sp_MarkStudentAttendance (Dùng MERGE trong SQL)
        /// </summary>
        public bool MarkStudentAttendance(int studentId, DateTime date, string status, string method)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_MarkStudentAttendance", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Student", studentId);
                    cmd.Parameters.AddWithValue("@date", date.Date);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@method", method);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // MERGE luôn trả về số dòng insert hoặc update
                }
            }
        }

        /// <summary>
        /// Lấy danh sách học sinh ĐÃ ĐƯỢC ĐIỂM DANH (bằng QR hoặc Thủ công)
        /// Gọi SP: dbo.sp_GetTodaysAttendance
        /// </summary>
        public List<Students> GetTodaysAttendance(int id_Class, DateTime date)
        {
            List<Students> studentsList = new List<Students>();

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetTodaysAttendance", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_Class", id_Class);
                    cmd.Parameters.AddWithValue("@date", date.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentsList.Add(new Students
                            {
                                // Chú ý: DTO Students của bạn cần khớp với các cột trả về
                                // id_Student = (int)reader["id_Student"], // Nếu DTO có field này thì uncomment
                                code_Student = reader["code_Student"].ToString(),
                                name_Student = reader["name_Student"].ToString()
                                // Bạn có thể map thêm status/method vào DTO nếu cần hiển thị
                            });
                        }
                    }
                }
            }
            return studentsList;
        }

        /// <summary>
        /// Xóa dữ liệu điểm danh của lớp trong ngày
        /// Gọi SP: dbo.sp_DeleteAttendanceByClassDate
        /// </summary>
        public bool DeleteAttendanceByClassAndDate(int id_Class, DateTime date)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteAttendanceByClassDate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@classId", id_Class);
                        cmd.Parameters.AddWithValue("@date", date.Date);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false; // Có lỗi xảy ra
            }
        }
    }
}