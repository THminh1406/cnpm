using System;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using SchoolManager.DAL;

namespace SchoolManager.BLL
{
    public class Business_Logic_PasswordReset
    {
        private data_Access_Account dal = new data_Access_Account();
        public bool ResetPassword(string email, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(newPassword)) return false;
            if (newPassword.Length < 8) return false;
            if (!Regex.IsMatch(newPassword, "[a-z]")) return false;
            if (!Regex.IsMatch(newPassword, "[A-Z]")) return false;
            if (!Regex.IsMatch(newPassword, "[0-9]")) return false;
            if (!Regex.IsMatch(newPassword, "[^a-zA-Z0-9]")) return false;

            string hash = dal.CreatePasswordHash(newPassword);
            return dal.UpdatePasswordByEmail(email, hash);
        }

        private Business_Logic_OtpService otpService = new Business_Logic_OtpService();
        public bool SendOtp(string email, out string otp)
        {
            return otpService.SendPasswordResetOtp(email, out otp);
        }
    }
}
