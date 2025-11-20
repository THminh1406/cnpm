using System;
using System.Collections.Generic;

namespace SchoolManager.DTO
{
    public class QuizDTO
    {
        public int id_quiz { get; set; }
        public int id_teacher { get; set; }
        public int id_class { get; set; }
        public string quiz_title { get; set; }
        public string quiz_type { get; set; }
        public string quiz_config_json { get; set; } // Ví dụ: "{ 'difficulty': '4x4' }"
    }
}