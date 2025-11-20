using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace SchoolManager.BLL
{
    public class Business_Logic_Quizzes
    {
        private data_Access_Quizzes dal_Quizzes = new data_Access_Quizzes();

        /// <summary>
        /// Tạo game (BLL)
        /// </summary>
        public bool CreateQuizWithContent(QuizDTO quiz, List<int> vocabIds)
        {
            // Kiểm tra logic
            if (string.IsNullOrWhiteSpace(quiz.quiz_title) ||
                string.IsNullOrWhiteSpace(quiz.quiz_type) ||
                vocabIds == null ||
                vocabIds.Count == 0)
            {
                throw new Exception("Thông tin game hoặc nội dung không hợp lệ.");
            }

            return dal_Quizzes.CreateQuizWithContent(quiz, vocabIds);
        }

        /// <summary>
        /// Lấy nội dung game (BLL)
        /// </summary>
        public List<int> GetQuizContentIds(int quizId)
        {
            // Chỉ cần gọi DAL
            return dal_Quizzes.GetQuizContentIds(quizId);
        }

        public DataTable GetAllQuizzes()
        {
            return dal_Quizzes.GetAllQuizzes();
        }

        public bool DeleteQuiz(int quizId)
        {
            if (quizId <= 0)
            {
                return false;
            }
            // Chỉ cần gọi DAL
            return dal_Quizzes.DeleteQuiz(quizId);
        }
    }

}