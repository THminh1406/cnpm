using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SchoolManager.DAL
{
    public class data_Access_Vocabulary : data_Access_Base
    {
        /// <summary>
        /// Lưu (Thêm mới hoặc Cập nhật) một từ vựng
        /// Gọi SP: dbo.sp_InsertVocabulary hoặc dbo.sp_UpdateVocabulary
        /// </summary>
        public bool SaveVocabulary(VocabularyDTO vocab)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlCommand cmd;

                if (vocab.id_vocabulary == 0) // Thêm mới
                {
                    cmd = new SqlCommand("dbo.sp_InsertVocabulary", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else // Cập nhật
                {
                    cmd = new SqlCommand("dbo.sp_UpdateVocabulary", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", vocab.id_vocabulary);
                }

                cmd.Parameters.AddWithValue("@WordText", vocab.WordText);
                cmd.Parameters.AddWithValue("@id_category", vocab.id_category);
                cmd.Parameters.AddWithValue("@VocabType", vocab.VocabType);

                // Xử lý giá trị NULL cho WordImage (Logic IF/ELSE đã nằm trong SP Update)
                if (vocab.WordImage != null)
                {
                    cmd.Parameters.Add("@WordImage", SqlDbType.VarBinary).Value = vocab.WordImage;
                }
                else
                {
                    cmd.Parameters.Add("@WordImage", SqlDbType.VarBinary).Value = DBNull.Value;
                }

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Lấy từ vựng cho DataGridView (có lọc)
        /// Gọi SP: dbo.sp_GetVocabularyList
        /// </summary>
        public DataTable GetVocabulary(int categoryId, string searchTerm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetVocabularyList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);

                    if (!string.IsNullOrWhiteSpace(searchTerm))
                        cmd.Parameters.AddWithValue("@searchTerm", searchTerm);
                    else
                        cmd.Parameters.AddWithValue("@searchTerm", DBNull.Value);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy từ vựng cho game (chỉ lấy theo ID chủ đề)
        /// Gọi SP: dbo.sp_GetVocabularyByCategory
        /// </summary>
        public List<VocabularyDTO> GetVocabularyByCategory(int categoryId, string vocabType)
        {
            List<VocabularyDTO> vocabularyList = new List<VocabularyDTO>();

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetVocabularyByCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.Parameters.AddWithValue("@vocabType", vocabType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vocabularyList.Add(new VocabularyDTO
                            {
                                id_vocabulary = (int)reader["id_vocabulary"],
                                WordText = reader["WordText"].ToString(),
                                WordImage = (reader["WordImage"] != DBNull.Value) ? (byte[])reader["WordImage"] : null,
                                id_category = (int)reader["id_category"],
                                VocabType = reader["VocabType"].ToString()
                            });
                        }
                    }
                }
            }
            return vocabularyList;
        }

        /// <summary>
        /// Lấy 1 danh sách từ vựng CỤ THỂ dựa trên List<int> ID (Dùng khi chơi game)
        /// Gọi SP: dbo.sp_GetVocabularyByIds (Truyền chuỗi ID "1,2,3")
        /// </summary>
        public List<VocabularyDTO> GetVocabularyByIds(List<int> ids)
        {
            List<VocabularyDTO> vocabularyList = new List<VocabularyDTO>();
            if (ids == null || ids.Count == 0) return vocabularyList;

            // Chuyển List<int> thành chuỗi "1,2,3" để truyền vào SP
            string idString = string.Join(",", ids);

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetVocabularyByIds", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ids", idString);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vocabularyList.Add(new VocabularyDTO
                            {
                                id_vocabulary = (int)reader["id_vocabulary"],
                                WordText = reader["WordText"].ToString(),
                                WordImage = (reader["WordImage"] != DBNull.Value) ? (byte[])reader["WordImage"] : null,
                                id_category = (int)reader["id_category"],
                                VocabType = reader["VocabType"].ToString()
                            });
                        }
                    }
                }
            }
            return vocabularyList;
        }

        // Gọi SP: dbo.sp_DeleteVocabulary
        public bool DeleteVocabulary(int vocabId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteVocabulary", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", vocabId);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Gọi SP: dbo.sp_GetCategoryIdForVocab
        public int GetCategoryIdForVocab(int vocabId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetCategoryIdForVocab", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", vocabId);

                    object result = cmd.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }
            }
        }

        /// <summary>
        /// Đếm xem còn bao nhiêu từ vựng trong chủ đề
        /// Gọi SP: dbo.sp_GetVocabularyCountByCategory
        /// </summary>
        public int GetVocabularyCountByCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetVocabularyCountByCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", categoryId);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}