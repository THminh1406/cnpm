using System;

namespace SchoolManager.DTO
{
    // Simple DTO for subject metadata
    public class Subject
    {
        public int id_Subject { get; set; }
        public string subject_Name { get; set; }
        public int? AssignedTeacherId { get; set; }
    }
}
