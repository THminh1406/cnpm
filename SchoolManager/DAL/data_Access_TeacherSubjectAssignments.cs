using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_TeacherSubjectAssignments : data_Access_Base
    {
        // Note: method signature kept as (teacherId, subjectId, classId)
        public bool AssignTeacherToSubjectInClass(int teacherId, int subjectId, int classId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddClassTeacher", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
                    cmd.Parameters.AddWithValue("@subjectId", subjectId);
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<TeacherSubjectAssignment> GetAssignmentsForTeacher(int teacherId)
        {
            var list = new List<TeacherSubjectAssignment>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("SELECT t.id_assign, t.id_teacher, t.id_subject, s.subject_name, t.id_class, c.class_name, t.created_at FROM dbo.class_subject_teachers t JOIN dbo.subjects s ON t.id_subject = s.id_subject JOIN dbo.classes c ON t.id_class = c.id_class WHERE t.id_teacher = @tid", conn))
                {
                    cmd.Parameters.AddWithValue("@tid", teacherId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new TeacherSubjectAssignment();
                            item.AssignmentId = reader.IsDBNull(reader.GetOrdinal("id_assign")) ? 0 : Convert.ToInt32(reader["id_assign"]);
                            item.TeacherId = reader.IsDBNull(reader.GetOrdinal("id_teacher")) ? 0 : Convert.ToInt32(reader["id_teacher"]);
                            item.SubjectId = reader.IsDBNull(reader.GetOrdinal("id_subject")) ? 0 : Convert.ToInt32(reader["id_subject"]);
                            item.SubjectName = reader.IsDBNull(reader.GetOrdinal("subject_name")) ? string.Empty : reader["subject_name"].ToString();
                            item.ClassId = reader.IsDBNull(reader.GetOrdinal("id_class")) ? 0 : Convert.ToInt32(reader["id_class"]);
                            item.ClassName = reader.IsDBNull(reader.GetOrdinal("class_name")) ? string.Empty : reader["class_name"].ToString();
                            try { item.CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("created_at")); } catch { item.CreatedAt = null; }
                            list.Add(item);
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        public bool DeleteAssignment(int assignmentId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.class_subject_teachers WHERE id_assign = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", assignmentId);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch { return false; }
        }
    }
}
