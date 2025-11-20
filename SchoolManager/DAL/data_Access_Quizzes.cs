using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolManager.DAL
{
    public class data_Access_Quizzes : data_Access_Base
    {
        /// <summary>
        /// Tạo 1 Game Mới VÀ Nội dung của nó (Dùng Transaction)
        /// </summary>
        public bool CreateQuizWithContent(QuizDTO quiz, List<int> vocabIds)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Bắt đầu Transaction
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // === BƯỚC 1: TẠO GAME (Bảng Quizzes) ===
                    string quizQuery = @"
                        INSERT INTO Quizzes (id_teacher, id_class, quiz_title, quiz_type, quiz_config_json) 
                        OUTPUT INSERTED.id_quiz 
                        VALUES (@id_teacher, @id_class, @quiz_title, @quiz_type, @quiz_config_json)";

                    SqlCommand quizCmd = new SqlCommand(quizQuery, conn, transaction);
                    quizCmd.Parameters.AddWithValue("@id_teacher", quiz.id_teacher);
                    quizCmd.Parameters.AddWithValue("@id_class", quiz.id_class);
                    quizCmd.Parameters.AddWithValue("@quiz_title", quiz.quiz_title);
                    quizCmd.Parameters.AddWithValue("@quiz_type", quiz.quiz_type);
                    quizCmd.Parameters.AddWithValue("@quiz_config_json", quiz.quiz_config_json);

                    // Lấy ID của game vừa tạo
                    int newQuizId = (int)quizCmd.ExecuteScalar();

                    // === BƯỚC 2: THÊM NỘI DUNG (Bảng Quiz_Content) ===
                    // (Dùng 1 lệnh INSERT lớn thay vì vòng lặp)
                    string contentQuery = "INSERT INTO Quiz_Content (id_quiz, id_vocabulary) VALUES ";
                    List<string> rows = new List<string>();

                    foreach (int vocabId in vocabIds)
                    {
                        rows.Add($"({newQuizId}, {vocabId})");
                    }
                    contentQuery += string.Join(",", rows);

                    SqlCommand contentCmd = new SqlCommand(contentQuery, conn, transaction);
                    contentCmd.ExecuteNonQuery();

                    // === KẾT THÚC: Nếu mọi thứ OK ===
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // Nếu có lỗi, hủy bỏ mọi thay đổi
                    transaction.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// Lấy danh sách ID của các từ vựng thuộc về 1 game (Dùng khi chơi)
        /// </summary>
        public List<int> GetQuizContentIds(int quizId)
        {
            List<int> idList = new List<int>();
            string query = "SELECT id_vocabulary FROM Quiz_Content WHERE id_quiz = @id_quiz";

            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_quiz", quizId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // === SỬA LỖI Ở ĐÂY ===
                    // Phải dùng 'while' (trong khi) để lặp qua TẤT CẢ các câu
                    // (Code cũ của bạn có thể đang dùng 'if' - chỉ lấy 1 câu)
                    while (reader.Read())
                    {
                        idList.Add((int)reader["id_vocabulary"]);
                    }
                    // ===================
                }
            }
            return idList;
        }

        public DataTable GetAllQuizzes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                // (Sau này bạn có thể lọc theo id_class)
                string query = "SELECT id_quiz, quiz_title, quiz_type FROM Quizzes";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return dt;
        }

        // (Thêm các hàm GetQuizById, GetAllQuizzes... ở đây)

        public bool DeleteQuiz(int quizId)
        {
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Bắt đầu 1 Transaction để đảm bảo cả 2 lệnh cùng thành công
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Xóa nội dung (Quiz_Content)
                    string queryContent = "DELETE FROM Quiz_Content WHERE id_quiz = @id";
                    SqlCommand cmdContent = new SqlCommand(queryContent, conn, transaction);
                    cmdContent.Parameters.AddWithValue("@id", quizId);
                    cmdContent.ExecuteNonQuery();

                    // 2. Xóa game (Quizzes)
                    string queryQuiz = "DELETE FROM Quizzes WHERE id_quiz = @id";
                    SqlCommand cmdQuiz = new SqlCommand(queryQuiz, conn, transaction);
                    cmdQuiz.Parameters.AddWithValue("@id", quizId);
                    int rowsAffected = cmdQuiz.ExecuteNonQuery();

                    // 3. Nếu mọi thứ OK, lưu thay đổi
                    transaction.Commit();

                    // Trả về true nếu game (bảng Quizzes) đã bị xóa
                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    // Nếu có lỗi, hủy bỏ tất cả thay đổi
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}