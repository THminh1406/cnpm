using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_Notes : data_Access_Base
    {
        // 1. Lấy danh sách ghi chú theo Lớp và Loại (để lọc)
        public List<NoteDTO> GetNotes(int classId, string noteTypeFilter)
        {
            List<NoteDTO> list = new List<NoteDTO>();

            string query = @"SELECT n.*, s.full_name 
                             FROM student_notes n
                             JOIN students s ON n.id_student = s.id_student
                             WHERE s.id_class = @classId";

            // Nếu lọc loại cụ thể (khác "Tất cả")
            if (noteTypeFilter != "Tất cả")
            {
                query += " AND n.note_type = @type";
            }

            query += " ORDER BY n.created_at DESC";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@classId", classId);
                if (noteTypeFilter != "Tất cả") cmd.Parameters.AddWithValue("@type", noteTypeFilter);

                conn.Open();
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
            return list;
        }

        // 2. Thêm ghi chú
        public bool AddNote(NoteDTO note)
        {
            string query = @"INSERT INTO student_notes (id_student, note_content, note_type, priority, created_at) 
                             VALUES (@sid, @content, @type, @priority, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sid", note.StudentId);
                cmd.Parameters.AddWithValue("@content", note.NoteContent);
                cmd.Parameters.AddWithValue("@type", note.NoteType);
                cmd.Parameters.AddWithValue("@priority", note.Priority);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 3. Xóa 1 ghi chú
        public bool DeleteNote(int noteId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM student_notes WHERE id_note = @id", conn);
                cmd.Parameters.AddWithValue("@id", noteId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Xóa tất cả ghi chú theo bộ lọc (Nút Xóa tất cả)
        public bool DeleteNotesByFilter(int classId, string noteTypeFilter)
        {
            string query = @"DELETE n FROM student_notes n
                             JOIN students s ON n.id_student = s.id_student
                             WHERE s.id_class = @classId";

            if (noteTypeFilter != "Tất cả") query += " AND n.note_type = @type";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@classId", classId);
                if (noteTypeFilter != "Tất cả") cmd.Parameters.AddWithValue("@type", noteTypeFilter);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateNoteContent(int noteId, string newContent)
        {
            string query = "UPDATE student_notes SET note_content = @content WHERE id_note = @id";
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", noteId);
                cmd.Parameters.AddWithValue("@content", newContent);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<NoteDTO> GetNotesForExport(int classId, DateTime fromDate, DateTime toDate)
        {
            List<NoteDTO> list = new List<NoteDTO>();

            // Lấy tất cả ghi chú của lớp trong khoảng thời gian
            // Sắp xếp theo ID học sinh để tiện gom nhóm
            string query = @"SELECT n.*, s.full_name 
                     FROM student_notes n
                     JOIN students s ON n.id_student = s.id_student
                     WHERE s.id_class = @classId 
                     AND n.created_at BETWEEN @from AND @to
                     ORDER BY s.id_student ASC, n.created_at DESC";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@from", fromDate);
                cmd.Parameters.AddWithValue("@to", toDate);

                conn.Open();
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
            return list;
        }
    }
}