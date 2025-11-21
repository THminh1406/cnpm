using System;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using System.Collections.Generic;
using SchoolManager.DAL;
using SchoolManager.DTO;

namespace SchoolManager.BLL
{
    public class Business_Logic_Register
    {
        private data_Access_Account dal = new data_Access_Account();
        private Business_Logic_OtpService otpService = new Business_Logic_OtpService();

        public bool SendOtpToEmail(string email, out string otp)
        {
            return otpService.SendRegistrationOtp(email, out otp);
        }

        public bool SendActivationEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            string adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
            if (string.IsNullOrWhiteSpace(adminEmail)) return false;
            string subject = "Yêu cầu kích hoạt tài khoản mới";
            string body = "Có yêu cầu kích hoạt tài khoản mới từ email: " + email + "\nVui lòng đăng nhập vào hệ thống để xử lý.";
            return SendEmail(adminEmail, subject, body);
        }

        public bool IsUsernameAvailable(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            if (!Regex.IsMatch(username, "^[a-zA-Z0-9_]{4,20}$")) return false;
            return !dal.UsernameExists(username);
        }

        public bool IsEmailAvailable(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return false;
            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase)) return false;
            return !dal.EmailExists(email);
        }

        public bool ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password.Length < 8) return false;
            bool hasLower = Regex.IsMatch(password, "[a-z]");
            bool hasUpper = Regex.IsMatch(password, "[A-Z]");
            bool hasDigit = Regex.IsMatch(password, "[0-9]");
            bool hasSpecial = Regex.IsMatch(password, "[^a-zA-Z0-9]");
            return hasLower && hasUpper && hasDigit && hasSpecial;
        }

        public bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return true;
            var digits = Regex.Replace(phone, "[^0-9]", "");
            return digits.Length == 10 ;
        }

        private bool SendEmail(string to, string subject, string body)
        {
            try
            {
                string host = ConfigurationManager.AppSettings["SmtpHost"];
                string portStr = ConfigurationManager.AppSettings["SmtpPort"] ?? "587";
                string user = ConfigurationManager.AppSettings["SmtpUser"];
                string pass = ConfigurationManager.AppSettings["SmtpPass"];
                string smtpFrom = ConfigurationManager.AppSettings["SmtpFrom"];
                string from = !string.IsNullOrWhiteSpace(smtpFrom) ? smtpFrom.Trim() : (user ?? string.Empty);

                bool enableSsl = (ConfigurationManager.AppSettings["SmtpEnableSsl"] ?? "true").ToLower() == "true";
                bool useDefaultCred = (ConfigurationManager.AppSettings["SmtpUseDefaultCredentials"] ?? "false").ToLower() == "true";

                if (string.IsNullOrEmpty(host) || string.IsNullOrWhiteSpace(from)) return false;

                int port = int.Parse(portStr);

                using (var message = new MailMessage(from, to))
                {
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = false;

                    using (var client = new SmtpClient(host, port))
                    {
                        client.EnableSsl = enableSsl;
                        client.UseDefaultCredentials = useDefaultCred;
                        if (!useDefaultCred && !string.IsNullOrEmpty(user))
                        {
                            client.Credentials = new System.Net.NetworkCredential(user, pass);
                        }

                        client.Send(message);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Register(string name, string email, string username, string password, string phoneNumber, string otpProvided, string otpExpected)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return false;
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!string.Equals(otpProvided, otpExpected)) return false;
            if (dal.UsernameExists(username)) return false;
            if (dal.EmailExists(email)) return false;
            string hash = dal.CreatePasswordHash(password);
            return dal.RegisterTeacher(name, email, username, hash, phoneNumber, "teacher");
        }

        public int GetUsernameCountForDiagnostics(string username)
        {
            try
            {
                return dal.GetUsernameCount(username);
            }
            catch
            {
                return -1;
            }
        }

        public int GetEmailCountForDiagnostics(string email)
        {
            try
            {
                return dal.GetEmailCount(email);
            }
            catch
            {
                return -1;
            }
        }

        public string GetConnectionStringForDiagnostics()
        {
            try
            {
                return dal.GetConnectionStringForDiagnostics();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
