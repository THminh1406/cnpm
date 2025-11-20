using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_Categories : data_Access_Base
    {
        /// <summary>
        /// Lấy TẤT CẢ chủ đề để đổ vào ComboBox
        /// </summary>
        public List<CategoryDTO> GetAllCategories(bool onlyShowNonEmpty = false)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            string query;

            if (onlyShowNonEmpty)
            {
                // Chỉ lấy chủ đề có ít nhất 1 từ vựng
                query = @"SELECT C.id_category, C.category_name 
                  FROM Categories C 
                  WHERE EXISTS (SELECT 1 FROM Vocabulary V WHERE V.id_category = C.id_category)
                  ORDER BY C.category_name";
            }
            else
            {
                // Lấy tất cả
                query = "SELECT id_category, category_name FROM Categories ORDER BY category_name";
            }

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryDTO
                        {
                            id_category = (int)reader["id_category"],
                            category_name = reader["category_name"].ToString()
                        });
                    }
                }
            }
            return categories;
        }

        /// <summary>
        /// Lưu một chủ đề mới và TRẢ VỀ ID mới của nó
        /// </summary>
        public int SaveCategory(string categoryName)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Dùng OUTPUT INSERTED.id_category để lấy ID vừa tạo
                string query = "INSERT INTO Categories (category_name) OUTPUT INSERTED.id_category VALUES (@name)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", categoryName);

                // ExecuteScalar dùng để lấy về giá trị đơn (chính là ID mới)
                int newId = (int)cmd.ExecuteScalar();
                return newId;
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                string query = "DELETE FROM Categories WHERE id_category = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", categoryId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // (Bạn có thể thêm hàm UpdateCategory và DeleteCategory ở đây)
    }
}