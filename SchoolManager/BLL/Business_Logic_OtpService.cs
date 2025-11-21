using System;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SchoolManager.BLL
{
    public class Business_Logic_OtpService
    {
        private string GenerateOtp()
        {
            var rand = new Random();
            return rand.Next(100000, 999999).ToString();
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
        public bool SendRegistrationOtp(string email, out string otp)
        {
            otp = null;
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return false;
            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase)) return false;
            otp = GenerateOtp();
            string subject = "Mã xác minh tài khoản";
            string body = $"Mã xác minh của bạn là: {otp}";
            return SendEmail(email, subject, body);
        }
        public bool SendPasswordResetOtp(string email, out string otp)
        {
            otp = null;
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) return false;
            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase)) return false;
            otp = GenerateOtp();
            string subject = "Password reset code";
            string body = $"Your password reset code is: {otp}";
            return SendEmail(email, subject, body);
        }
    }
}
