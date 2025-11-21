using SchoolManager.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace SchoolManager.DAL
{
    public class data_Access_Classes : data_Access_Base
    {
        public List<Classes> GetAllClasses()
        {
            var classes_List = new List<Classes>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_GetAllClasses", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cls = new Classes
                            {
                                id_Class = reader.IsDBNull(reader.GetOrdinal("id_class")) ? 0 : Convert.ToInt32(reader["id_class"]),
                                name_Class = reader.IsDBNull(reader.GetOrdinal("class_name")) ? string.Empty : reader["class_name"].ToString(),
                                AssignedTeacherId = reader.IsDBNull(reader.GetOrdinal("id_teacher")) ? (int?)null : Convert.ToInt32(reader["id_teacher"]) 
                            };

                            classes_List.Add(cls);
                        }
                    }
                }
            }
            catch
            {}

            return classes_List;
        }

        public bool AssignTeacherToClass(int classId, int teacherId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                using (SqlCommand cmd = new SqlCommand("sp_AssignTeacherToClass", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@classId", classId);
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
    }
}
