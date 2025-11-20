using System;

namespace SchoolManager.DTO
{
    public class NoteDTO
    {
        public int NoteId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } // Để hiển thị tên
        public string NoteContent { get; set; }
        public string NoteType { get; set; }
        public string Priority { get; set; } // "Thấp", "Trung bình", "Cao"
        public DateTime CreatedAt { get; set; }
    }
}