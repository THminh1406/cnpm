using System;
using SchoolManager.DAL;
using SchoolManager.DTO;

namespace SchoolManager.BLL
{
    public class Business_Logic_Account
    {
        private data_Access_Account dal = new data_Access_Account();
        public Accounts Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;
            return dal.Login(username.Trim(), password);
        }

        public Accounts LoginWithToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            return dal.LoginWithToken(token);
        }

        public bool CreateRememberMeToken(Accounts account, out string token)
        {
            token = null;
            if (account == null || account.IdTeacher <= 0) return false;
            token = Guid.NewGuid().ToString("N");
            DateTime expires = DateTime.Now.AddDays(30);

            return dal.SaveRememberToken(account.IdTeacher, token, expires);
        }

        public int GetActivationState(string username)
        {
            try
            {
                return dal.GetActivationState(username);
            }
            catch
            {
                return -1;
            }
        }

        public bool VerifyPassword(string username, string password)
        {
            try
            {
                return dal.VerifyPassword(username?.Trim(), password);
            }
            catch
            {
                return false;
            }
        }

        public Accounts GetTeacherById(int teacherId)
        {
            try
            {
                return dal.GetTeacherById(teacherId);
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateTeacherInfo(int teacherId, string fullName, string email, string phoneNumber)
        {
            try
            {
                return dal.UpdateTeacherInfo(teacherId, fullName, email, phoneNumber);
            }
            catch
            {
                return false;
            }
        }
        public string CreatePasswordHash(string password)
        {
            try
            {
                return dal.CreatePasswordHash(password);
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool SetPasswordHashById(int teacherId, string passwordHash)
        {
            try
            {
                return dal.SetPasswordHashById(teacherId, passwordHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
