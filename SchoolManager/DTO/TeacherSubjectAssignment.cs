using System;

namespace SchoolManager.DTO
{
    public class TeacherSubjectAssignment
    {
        public int AssignmentId { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
