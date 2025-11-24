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

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Gọi Stored Procedure đã tạo trong SQL
                using (SqlCommand cmd = new SqlCommand("sp_GetAllCategories", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 

                    // Truyền tham số lọc (nếu = true thì SQL tự lọc những chủ đề có từ vựng)
                    cmd.Parameters.AddWithValue("@OnlyShowNonEmpty", onlyShowNonEmpty);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new CategoryDTO
                            {
                                id_category = Convert.ToInt32(reader["id_category"]),
                                category_name = reader["category_name"].ToString()
                            });
                        }
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
                using (SqlCommand cmd = new SqlCommand("sp_InsertCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", categoryName);

                    // ExecuteScalar lấy giá trị SCOPE_IDENTITY() trả về từ SQL
                    object result = cmd.ExecuteScalar();

                    return (result != null) ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", categoryId);

                    // Trả về true nếu số dòng bị xóa > 0
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // (Bạn có thể thêm hàm UpdateCategory và DeleteCategory ở đây)
    }
}