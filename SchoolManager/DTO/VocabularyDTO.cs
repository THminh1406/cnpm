using System;
using System.Collections.Generic;

namespace SchoolManager.DTO
{
    public class VocabularyDTO
    {
        public int id_vocabulary { get; set; }
        public string WordText { get; set; }
        public byte[] WordImage { get; set; } // Dùng byte[] cho VARBINARY(MAX)
        public int id_category { get; set; }

        public string VocabType { get; set; }

        // (Bạn có thể thêm id_teacher nếu cần)
    }
}