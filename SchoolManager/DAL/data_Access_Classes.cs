using SchoolManager.DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace SchoolManager.DAL
{
    public class data_Access_Classes : data_Access_Base
    {
        // Return all classes including current assigned teacher (nullable) via stored procedure
        public List<Classes> GetAllClasses()
        {
            List<Classes> classes_List = new List<Classes>();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();

                string query = "SELECT id_class AS id_Class, class_name AS name_Class FROM classes";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    classes_List.Add(new Classes
                    {
                        id_Class = (int)reader["id_Class"],
                        name_Class = reader["name_Class"].ToString()
                    });
                }
            }
            return classes_List;
        }

        // Assign selected classes to a teacher via stored procedure sp_AssignClassesToTeacher
    }
}
