using SchoolManager.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace SchoolManager.DAL
{
    public class data_Access_Subjects : data_Access_Base
    {
        public List<Subject> GetAllSubjects()
        {
            var list = new List<Subject>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetAllSubjects", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var s = new Subject();
                            s.id_Subject = reader.IsDBNull(reader.GetOrdinal("id_subject")) ? 0 : Convert.ToInt32(reader["id_subject"]);
                            s.subject_Name = reader.IsDBNull(reader.GetOrdinal("subject_name")) ? string.Empty : reader["subject_name"].ToString();
                            s.AssignedTeacherId = reader.IsDBNull(reader.GetOrdinal("id_teacher")) ? (int?)null : Convert.ToInt32(reader["id_teacher"]);
                            list.Add(s);
                        }
                    }
                }
            }
            catch { }

            return list;
        }

        public bool SetTeacherSubject(int teacherId, string subjectName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_SetTeacherSubject", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_teacher", teacherId);
                    cmd.Parameters.AddWithValue("@subject_name", (object)subjectName ?? DBNull.Value);
                    conn.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected != 0;
                }
            }
            catch { return false; }
        }
        
        public int GetSubjectIdByName(string subjectName)
        {
            if (string.IsNullOrWhiteSpace(subjectName)) return 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetSubjectIdByName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", subjectName);
                    conn.Open();
                    var res = cmd.ExecuteScalar();
                    if (res == null || res == DBNull.Value) return 0;
                    return Convert.ToInt32(res);
                }
            }
            catch { return 0; }
        }
    }
}
