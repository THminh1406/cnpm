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
                // Bắt đầu Transaction để đảm bảo tính toàn vẹn (Header + Content)
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // === BƯỚC 1: TẠO GAME (Gọi SP: sp_InsertQuiz) ===
                    int newQuizId = 0;
                    using (SqlCommand quizCmd = new SqlCommand("dbo.sp_InsertQuiz", conn, transaction))
                    {
                        quizCmd.CommandType = CommandType.StoredProcedure;
                        quizCmd.Parameters.AddWithValue("@id_teacher", quiz.id_teacher);
                        quizCmd.Parameters.AddWithValue("@id_class", quiz.id_class);
                        quizCmd.Parameters.AddWithValue("@quiz_title", quiz.quiz_title);
                        quizCmd.Parameters.AddWithValue("@quiz_type", quiz.quiz_type);
                        quizCmd.Parameters.AddWithValue("@quiz_config_json", quiz.quiz_config_json);

                        // Lấy ID của game vừa tạo
                        object result = quizCmd.ExecuteScalar();
                        newQuizId = Convert.ToInt32(result);
                    }

                    // === BƯỚC 2: THÊM NỘI DUNG (Gọi SP: sp_InsertQuizContent) ===
                    // Lặp qua danh sách và insert từng dòng
                    foreach (int vocabId in vocabIds)
                    {
                        using (SqlCommand contentCmd = new SqlCommand("dbo.sp_InsertQuizContent", conn, transaction))
                        {
                            contentCmd.CommandType = CommandType.StoredProcedure;
                            contentCmd.Parameters.AddWithValue("@id_quiz", newQuizId);
                            contentCmd.Parameters.AddWithValue("@id_vocabulary", vocabId);
                            contentCmd.ExecuteNonQuery();
                        }
                    }

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
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Gọi SP: dbo.sp_GetQuizContentIds
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetQuizContentIds", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_quiz", quizId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Dùng 'while' để lấy tất cả các dòng
                        while (reader.Read())
                        {
                            idList.Add((int)reader["id_vocabulary"]);
                        }
                    }
                }
            }
            return idList;
        }

        public DataTable GetAllQuizzes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connection_String))
            {
                conn.Open();
                // Gọi SP: dbo.sp_GetAllQuizzes
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetAllQuizzes", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public bool DeleteQuiz(int quizId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection_String))
                {
                    conn.Open();
                    // Gọi SP: dbo.sp_DeleteQuiz (SP này đã bao gồm Transaction xóa Content trước rồi xóa Header)
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_DeleteQuiz", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", quizId);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}