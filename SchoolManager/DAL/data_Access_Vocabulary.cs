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
        /// </summary>
        public bool SaveVocabulary(VocabularyDTO vocab)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                string query;
                if (vocab.id_vocabulary == 0) // Thêm mới
                {
                    query = "INSERT INTO Vocabulary (WordText, WordImage, id_category, VocabType) VALUES (@WordText, @WordImage, @id_category, @VocabType)";
                }
                else // Cập nhật
                {
                    // Chỉ cập nhật ảnh nếu nó KHÔNG NULL
                    if (vocab.WordImage != null)
                    {
                        query = "UPDATE Vocabulary SET WordText = @WordText, WordImage = @WordImage, id_category = @id_category, VocabType = @VocabType WHERE id_vocabulary = @id";
                    }
                    else // Nếu ảnh NULL (khi sửa 1 Câu, hoặc sửa 1 Từ mà không đổi ảnh)
                    {
                        query = "UPDATE Vocabulary SET WordText = @WordText, id_category = @id_category, VocabType = @VocabType WHERE id_vocabulary = @id";
                    }
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@WordText", vocab.WordText);
                cmd.Parameters.AddWithValue("@id_category", vocab.id_category);
                cmd.Parameters.AddWithValue("@VocabType", vocab.VocabType);

                // Xử lý giá trị NULL cho WordImage
                if (vocab.WordImage != null)
                {
                    cmd.Parameters.Add("@WordImage", SqlDbType.VarBinary).Value = vocab.WordImage;
                }
                else
                {
                    cmd.Parameters.Add("@WordImage", SqlDbType.VarBinary).Value = DBNull.Value;
                }

                if (vocab.id_vocabulary > 0)
                {
                    cmd.Parameters.AddWithValue("@id", vocab.id_vocabulary);
                }

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Lấy từ vựng cho DataGridView (có lọc)
        /// </summary>
        public DataTable GetVocabulary(int categoryId, string searchTerm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                string query = @"
            SELECT 
                V.id_vocabulary, 
                V.WordText AS Word, 
                V.WordImage AS WordImage, 
                C.category_name AS CategoryName,
                V.VocabType -- THÊM CỘT NÀY
            FROM Vocabulary V
            JOIN Categories C ON V.id_category = C.id_category
            WHERE 1=1";

                if (categoryId > 0)
                {
                    query += " AND V.id_category = @categoryId";
                }
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += " AND V.WordText LIKE @searchTerm";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (categoryId > 0)
                {
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                }
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Lấy từ vựng cho game (chỉ lấy theo ID chủ đề)
        /// </summary>
        public List<VocabularyDTO> GetVocabularyByCategory(int categoryId, string vocabType)
        {
            List<VocabularyDTO> vocabularyList = new List<VocabularyDTO>();

            // Thêm "AND VocabType = @VocabType" vào câu lệnh
            string query = @"
        SELECT id_vocabulary, WordText, WordImage, id_category, VocabType 
        FROM Vocabulary 
        WHERE id_category = @id_category AND VocabType = @VocabType";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_category", categoryId);
                cmd.Parameters.AddWithValue("@VocabType", vocabType); // Thêm tham số mới

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vocabularyList.Add(new VocabularyDTO
                        {
                            id_vocabulary = (int)reader["id_vocabulary"],
                            WordText = reader["WordText"].ToString(),
                            // Xử lý ảnh (có thể NULL nếu là "Sentence")
                            WordImage = (reader["WordImage"] != DBNull.Value) ? (byte[])reader["WordImage"] : null,
                            id_category = (int)reader["id_category"],
                            VocabType = reader["VocabType"].ToString()
                        });
                    }
                }
            }
            return vocabularyList;
        }

        /// <summary>
        /// Lấy 1 danh sách từ vựng CỤ THỂ dựa trên List<int> ID (Dùng khi chơi game)
        /// </summary>
        public List<VocabularyDTO> GetVocabularyByIds(List<int> ids)
        {
            List<VocabularyDTO> vocabularyList = new List<VocabularyDTO>();
            if (ids == null || ids.Count == 0) return vocabularyList;

            var parameters = new string[ids.Count];
            for (int i = 0; i < ids.Count; i++)
            {
                parameters[i] = string.Format("@id{0}", i);
            }

            string query = string.Format(@"
        SELECT id_vocabulary, WordText, WordImage, id_category, VocabType 
        FROM Vocabulary 
        WHERE id_vocabulary IN ({0})", string.Join(", ", parameters));

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);

                for (int i = 0; i < ids.Count; i++)
                {
                    cmd.Parameters.AddWithValue(parameters[i], ids[i]);
                }

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vocabularyList.Add(new VocabularyDTO
                        {
                            id_vocabulary = (int)reader["id_vocabulary"],
                            WordText = reader["WordText"].ToString(),
                            // === Đảm bảo đã sửa lỗi DBNull ở đây ===
                            WordImage = (reader["WordImage"] != DBNull.Value) ? (byte[])reader["WordImage"] : null,
                            id_category = (int)reader["id_category"],
                            VocabType = reader["VocabType"].ToString()
                        });
                    }
                }
            }
            return vocabularyList;
        }

        public bool DeleteVocabulary(int vocabId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();

                // Nhờ có "ON DELETE CASCADE" (từ CSDL),
                // khi xóa 1 từ vựng, nó cũng tự động bị xóa khỏi bảng Quiz_Content.
                string query = "DELETE FROM Vocabulary WHERE id_vocabulary = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", vocabId);

                // Trả về true nếu có 1 dòng bị ảnh hưởng (xóa thành công)
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int GetCategoryIdForVocab(int vocabId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                string query = "SELECT id_category FROM Vocabulary WHERE id_vocabulary = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", vocabId);
                object result = cmd.ExecuteScalar();
                return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
            }
        }

        /// <summary>
        /// Đếm xem còn bao nhiêu từ vựng trong chủ đề
        /// </summary>
        public int GetVocabularyCountByCategory(int categoryId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Vocabulary WHERE id_category = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", categoryId);
                return (int)cmd.ExecuteScalar();
            }
        }

    }
}