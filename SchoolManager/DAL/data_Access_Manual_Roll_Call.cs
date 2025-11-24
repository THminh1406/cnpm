using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SchoolManager.DTO;
using System.Data;

namespace SchoolManager.DAL
{
    public class data_Access_Manual_Roll_Call : data_Access_Base
    {
        // 1. Lưu danh sách điểm danh (Xóa cũ -> Thêm mới)
        public bool SaveRollCallRecord(List<Roll_Call_Records> list_Roll_Call)
        {
            if (list_Roll_Call == null || list_Roll_Call.Count == 0) return true;

            int id_Class = list_Roll_Call[0].id_Class;
            DateTime date = list_Roll_Call[0].date.Date;

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    // Bước 1: Xóa dữ liệu cũ của ngày hôm đó (Gọi SP: dbo.sp_DeleteAttendanceByClassDate)
                    using (SqlCommand deleteCmd = new SqlCommand("dbo.sp_DeleteAttendanceByClassDate", conn, tran))
                    {
                        deleteCmd.CommandType = CommandType.StoredProcedure;
                        deleteCmd.Parameters.AddWithValue("@classId", id_Class);
                        deleteCmd.Parameters.AddWithValue("@date", date);
                        deleteCmd.ExecuteNonQuery();
                    }

                    // Bước 2: Insert dữ liệu mới (Gọi SP: dbo.sp_InsertManualAttendance)
                    // Sử dụng lại command object hoặc tạo mới trong vòng lặp đều được, tạo mới cho rõ ràng
                    foreach (var record in list_Roll_Call)
                    {
                        using (SqlCommand insertCmd = new SqlCommand("dbo.sp_InsertManualAttendance", conn, tran))
                        {
                            insertCmd.CommandType = CommandType.StoredProcedure;
                            insertCmd.Parameters.AddWithValue("@studentId", record.id_Student);
                            insertCmd.Parameters.AddWithValue("@date", record.date.Date);
                            insertCmd.Parameters.AddWithValue("@status", record.status);

                            // Xử lý Notes null
                            if (string.IsNullOrEmpty(record.notes))
                                insertCmd.Parameters.AddWithValue("@notes", DBNull.Value);
                            else
                                insertCmd.Parameters.AddWithValue("@notes", record.notes);

                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    // Có thể throw lại lỗi hoặc log lại tùy nhu cầu
                    throw new Exception("Lỗi khi lưu điểm danh: " + ex.Message);
                }
            }
        }

        // 2. Lấy dữ liệu điểm danh để hiển thị lên Grid
        public List<Roll_Call_Records> GetRollCallRecords(int id_Class, DateTime date)
        {
            List<Roll_Call_Records> records = new List<Roll_Call_Records>();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Gọi SP: dbo.sp_GetAttendanceByClassDate
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAttendanceByClassDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", id_Class);
                    cmd.Parameters.AddWithValue("@date", date.Date);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new Roll_Call_Records
                            {
                                id_Record = (int)reader["id_attendance"], // Lưu ý DTO có field này
                                id_Student = (int)reader["id_student"],
                                id_Class = (int)reader["id_class"],
                                date = (DateTime)reader["attendance_date"],
                                status = reader["attendance_status"].ToString(),
                                notes = reader["attendance_notes"] != DBNull.Value ? reader["attendance_notes"].ToString() : string.Empty
                            });
                        }
                    }
                }
            }
            return records;
        }

        // 3. Lấy dữ liệu báo cáo điểm danh
        public List<Roll_Call_Records> GetAttendanceForReport(int id_Class, DateTime startDate, DateTime endDate)
        {
            List<Roll_Call_Records> records = new List<Roll_Call_Records>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    // Gọi SP: dbo.sp_GetAttendanceReportByClass
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAttendanceReportByClass", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@classId", id_Class);
                        cmd.Parameters.AddWithValue("@startDate", startDate.Date);
                        cmd.Parameters.AddWithValue("@endDate", endDate.Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                records.Add(new Roll_Call_Records
                                {
                                    id_Record = (int)reader["id_attendance"],
                                    id_Student = (int)reader["id_student"],
                                    id_Class = (int)reader["id_class"],
                                    name_Student = reader["full_name"].ToString(),
                                    date = (DateTime)reader["attendance_date"],
                                    status = reader["attendance_status"].ToString(),
                                    notes = reader["attendance_notes"] != DBNull.Value ? reader["attendance_notes"].ToString() : string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi DAL GetAttendanceForReport: " + ex.Message);
                // Tùy chọn: ném lỗi ra ngoài để UI xử lý
                // throw; 
            }
            return records;
        }
    }
}