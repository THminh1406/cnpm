using System;
using System.Data;

namespace SchoolManager.DTO
{
    public class Accounts
    {
        public int IdTeacher { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserRole { get; set; }
        public string RememberToken { get; set; }
        public DateTime? RememberExpires { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string Subject { get; set; }

        public Accounts(int id, string name, string user, string role)
        {
            this.IdTeacher = id;
            this.FullName = name;
            this.Username = user;
            this.UserRole = role;
            this.RememberToken = string.Empty;
            this.RememberExpires = null;
            this.CreatedAt = null;
            this.Subject = string.Empty;
        }
    }
}
