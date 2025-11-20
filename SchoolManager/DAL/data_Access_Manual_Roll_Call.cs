using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SchoolManager.DTO;
using System.Data; // Cần cho DBNull

namespace SchoolManager.DAL
{
    public class data_Access_Manual_Roll_Call : data_Access_Base
    {
        // SỬA LỖI: Tên hàm (IDE1006)
        public bool SaveRollCallRecord(List<Roll_Call_Records> list_Roll_Call)
        {
            if (list_Roll_Call == null || list_Roll_Call.Count == 0) return true;

            // SỬA LỖI: Phải lấy id_Class từ DTO
            int id_Class = list_Roll_Call[0].id_Class;
            DateTime date = list_Roll_Call[0].date.Date;

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // 1. Lệnh DELETE (đã đúng)
                    string deleteQuery = @"DELETE FROM attendance 
                                           WHERE attendance_date = @date
                                           AND id_student IN (SELECT id_student FROM students WHERE id_class = @id_Class)";

                    // SỬA LỖI: Gán Transaction
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, tran);
                    deleteCmd.Parameters.AddWithValue("@id_Class", id_Class);
                    deleteCmd.Parameters.AddWithValue("@date", date);
                    deleteCmd.ExecuteNonQuery();

                    // 2. SỬA LỖI INSERT: Dùng đúng bảng 'attendance' và các cột
                    string insertQuery = @"INSERT INTO attendance 
                                           (id_student, attendance_date, attendance_status, attendance_method, attendance_notes) 
                                           VALUES (@id_Student, @date, @status, 'manual', @notes)";

                    foreach (var record in list_Roll_Call)
                    {
                        // SỬA LỖI: Gán Transaction
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn, tran);

                        insertCmd.Parameters.AddWithValue("@id_Student", record.id_Student);
                        insertCmd.Parameters.AddWithValue("@date", record.date.Date);
                        insertCmd.Parameters.AddWithValue("@status", record.status); // Dùng string (đã sửa ở DTO)
                        insertCmd.Parameters.AddWithValue("@notes", (object)record.notes ?? DBNull.Value);

                        insertCmd.ExecuteNonQuery();
                    }

                    // SỬA LỖI: Thiếu Commit
                    tran.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // SỬA LỖI: Thiếu Rollback
                    tran.Rollback();
                    return false;
                }
            }
        }

        public List<Roll_Call_Records> GetRollCallRecords(int id_Class, DateTime date)
        {
            List<Roll_Call_Records> records = new List<Roll_Call_Records>();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                string query = @"SELECT 
                                B.id_attendance,
                                B.id_student, 
                                A.id_class, 
                                B.attendance_date, 
                                B.attendance_status, 
                                B.attendance_notes
                            FROM students A
                            JOIN attendance B ON A.id_student = B.id_student
                            WHERE A.id_class = @id_Class AND B.attendance_date = @date";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_Class", id_Class);
                cmd.Parameters.AddWithValue("@date", date.Date);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        records.Add(new Roll_Call_Records
                        {
                            id_Student = (int)reader["id_student"],
                            //id_Class = (int)reader["id_class"],
                            date = (DateTime)reader["attendance_date"],
                            status = reader["attendance_status"].ToString(),
                            notes = reader["attendance_notes"] != DBNull.Value ? reader["attendance_notes"].ToString() : string.Empty
                        });
                    }
                }
            }
            return records;
        }

        public List<Roll_Call_Records> GetAttendanceForReport(int id_Class, DateTime startDate, DateTime endDate)
        {
            List<Roll_Call_Records> records = new List<Roll_Call_Records>();

            // Câu lệnh SQL này JOIN 'attendance' và 'students'
            // Lấy tên cột chính xác từ các file DAL của bạn
            string query = @"
                SELECT 
                    a.id_attendance,  -- Dùng cho DTO.id_Record
                    s.id_student,     -- Dùng cho DTO.id_Student
                    s.id_class,       -- Dùng cho DTO.id_Class
                    s.full_name,      -- Dùng cho DTO.name_Student (Đã thêm ở Bước 1)
                    a.attendance_date, -- Dùng cho DTO.date
                    a.attendance_status, -- Dùng cho DTO.status
                    a.attendance_notes -- Dùng cho DTO.notes
                FROM 
                    attendance AS a
                INNER JOIN 
                    students AS s ON a.id_student = s.id_student
                WHERE 
                    s.id_class = @id_Class
                    AND a.attendance_date BETWEEN @startDate AND @endDate
                ORDER BY 
                    s.full_name, a.attendance_date;
            ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id_Class", id_Class);
                    cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                    cmd.Parameters.AddWithValue("@endDate", endDate.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Bây giờ bạn có thể gán giá trị cho name_Student
                            // vì DTO đã được sửa ở Bước 1
                            Roll_Call_Records record = new Roll_Call_Records
                            {
                                id_Record = (int)reader["id_attendance"],
                                id_Student = (int)reader["id_student"],
                                id_Class = (int)reader["id_class"],
                                name_Student = reader["full_name"].ToString(), // <--- HOẠT ĐỘNG
                                date = (DateTime)reader["attendance_date"],
                                status = reader["attendance_status"].ToString(),
                                notes = reader["attendance_notes"] != DBNull.Value ? reader["attendance_notes"].ToString() : string.Empty
                            };
                            records.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ví dụ: ghi log)
                Console.WriteLine("Lỗi DAL GetAttendanceForReport: " + ex.Message);
            }
            return records;
        }
    }
}