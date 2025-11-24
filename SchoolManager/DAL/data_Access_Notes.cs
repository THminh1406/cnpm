using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_Notes : data_Access_Base
    {
        // 1. Lấy danh sách ghi chú theo Lớp và Loại
        // Gọi SP: dbo.sp_GetNotesByClass
        public List<NoteDTO> GetNotes(int classId, string noteTypeFilter)
        {
            List<NoteDTO> list = new List<NoteDTO>();

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetNotesByClass", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
                    // Truyền thẳng giá trị lọc ("Tất cả" hoặc tên loại) xuống SQL
                    cmd.Parameters.AddWithValue("@noteType", noteTypeFilter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NoteDTO
                            {
                                NoteId = (int)reader["id_note"],
                                StudentId = (int)reader["id_student"],
                                StudentName = reader["full_name"].ToString(),
                                NoteContent = reader["note_content"].ToString(),
                                NoteType = reader["note_type"].ToString(),
                                Priority = reader["priority"] != DBNull.Value ? reader["priority"].ToString() : "Thấp",
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // 2. Thêm ghi chú
        // Gọi SP: dbo.sp_InsertNote
        public bool AddNote(NoteDTO note)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertNote", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sid", note.StudentId);
                    cmd.Parameters.AddWithValue("@content", note.NoteContent);
                    cmd.Parameters.AddWithValue("@type", note.NoteType);
                    cmd.Parameters.AddWithValue("@priority", note.Priority);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 3. Xóa 1 ghi chú
        // Gọi SP: dbo.sp_DeleteNote
        public bool DeleteNote(int noteId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteNote", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", noteId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 4. Xóa tất cả ghi chú theo bộ lọc
        // Gọi SP: dbo.sp_DeleteNotesByFilter
        public bool DeleteNotesByFilter(int classId, string noteTypeFilter)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteNotesByFilter", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
                    cmd.Parameters.AddWithValue("@noteType", noteTypeFilter);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 5. Cập nhật nội dung ghi chú
        // Gọi SP: dbo.sp_UpdateNoteContent
        public bool UpdateNoteContent(int noteId, string newContent)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_UpdateNoteContent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", noteId);
                    cmd.Parameters.AddWithValue("@content", newContent);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. Lấy dữ liệu xuất báo cáo
        // Gọi SP: dbo.sp_GetNotesForExport
        public List<NoteDTO> GetNotesForExport(int classId, DateTime fromDate, DateTime toDate)
        {
            List<NoteDTO> list = new List<NoteDTO>();

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetNotesForExport", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
                    cmd.Parameters.AddWithValue("@from", fromDate);
                    cmd.Parameters.AddWithValue("@to", toDate);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NoteDTO
                            {
                                NoteId = (int)reader["id_note"],
                                StudentId = (int)reader["id_student"],
                                StudentName = reader["full_name"].ToString(),
                                NoteContent = reader["note_content"].ToString(),
                                NoteType = reader["note_type"].ToString(),
                                Priority = reader["priority"] != DBNull.Value ? reader["priority"].ToString() : "Thấp",
                                CreatedAt = Convert.ToDateTime(reader["created_at"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}