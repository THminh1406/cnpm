using System;
using System.Linq;
using System.Windows.Forms;
using SchoolManager.BLL;

namespace SchoolManager.Presentations.Forms
{
    public partial class register_Form : Form
    {
        private Business_Logic_Register bll = new Business_Logic_Register();
        private string currentOtp = null;

        public register_Form()
        {
            InitializeComponent();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show login form again if it was hidden by the caller
            var login = Application.OpenForms.OfType<Login_Form>().FirstOrDefault();
            if (login != null)
            {
                login.Show();
                login.BringToFront();
            }
            else
            {
                // fallback: open a new login form
                var lf = new Login_Form();
                lf.Show();
            }

            this.Close();
        }

        private void Sendcode_Click(object sender, EventArgs e)
        {
            string email = this.guna2TextBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Nhập email để nhận code.");
                return;
            }

            bool sent = bll.SendOtpToEmail(email, out string otp);
            // always keep generated OTP for validation even if sending fails (useful for development)
            currentOtp = otp;

            if (sent)
            {
                // In production you would not show the OTP. For dev, we confirm email was sent.
                MessageBox.Show("Kiểm tra email của bạn");
            }
            else
            {
                // SMTP may not be configured in dev environment; show OTP so testing is possible.
                MessageBox.Show($"Failed to send OTP by email. Use this code for testing: {otp}", "OTP (dev)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            string name = this.guna2TextBox3.Text.Trim();
            string email = this.guna2TextBox4.Text.Trim();
            string phone = this.guna2TextBox5.Text.Trim();
            string username = this.guna2TextBox1.Text.Trim();
            string password = this.guna2TextBox2.Text;
            string confirm = this.guna2TextBox6.Text;
            string otp = this.guna2TextBox7.Text.Trim();

            // Basic required validations
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin.");
                return;
            }

            if (name.Length < 2)
            {
                MessageBox.Show("Tên phải có ít nhất 2 ký tự.");
                return;
            }

            // Username validation and availability
            if (!bll.IsUsernameAvailable(username))
            {
                // Diagnostic: check counts from DAL when username reported unavailable
                try
                {
                    int cnt = bll.GetUsernameCountForDiagnostics(username);
                    MessageBox.Show($"Username invalid/used. DB count={cnt}. ConnectionString={bll.GetConnectionStringForDiagnostics()}");
                }
                catch { }

                MessageBox.Show("Username is invalid or already taken. Use 4-20 letters, numbers or underscores.");
                return;
            }

            // Email validation and availability
            if (!bll.IsEmailAvailable(email))
            {
                try
                {
                    int cnt = bll.GetEmailCountForDiagnostics(email);
                    MessageBox.Show($"Email invalid/used. DB count={cnt}. ConnectionString={bll.GetConnectionStringForDiagnostics()}");
                }
                catch { }

                MessageBox.Show("Email is invalid or already used.");
                return;
            }

            // Phone validation (optional)
            if (!bll.ValidatePhone(phone))
            {
                MessageBox.Show("Phone number is invalid. Use 10 digits.");
                return;
            }

            // Password strength
            if (!bll.ValidatePasswordStrength(password))
            {
                MessageBox.Show("Mật khẩu quá yếu! Yêu cầu:\n" +
                    "- Ít nhất 8 ký tự\n" +
                    "- Có chữ Hoa, chữ thường\n" +
                    "- Có số (0-9)\n" +
                    "- Có ký tự đặc biệt (ví dụ @, #, !)",
                    "Cảnh báo bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu không khớp.");
                return;
            }

            if (string.IsNullOrEmpty(currentOtp))
            {
                MessageBox.Show("Hãy nhập OTP.");
                return;
            }

            if (!string.Equals(otp, currentOtp))
            {
                MessageBox.Show("Invalid OTP code.");
                return;
            }

            bool ok = bll.Register(name, email, username, password, phone, otp, currentOtp);
            if (ok)
            {

                bool sent = bll.SendActivationEmail(email);

                if (sent)
                {
                    MessageBox.Show("Tài khoản đã được tạo.");
                }
                else
                {
                    MessageBox.Show("Account created but failed to notify admin. Please contact support.");
                }

                // after successful registration, show login form
                var existingLogin = Application.OpenForms.OfType<Login_Form>().FirstOrDefault();
                if (existingLogin != null)
                {
                    existingLogin.Show();
                    existingLogin.BringToFront();
                }
                else
                {
                    var lf = new Login_Form();
                    lf.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Tài khoản hoặc mật khẩu đã tồn tại.");
            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.PasswordChar == '*')
            {
                guna2Button2.BringToFront();
                guna2TextBox2.PasswordChar = '\0';
            }
            else
            {
                guna2Button2.BringToFront();
                guna2TextBox2.PasswordChar = '*';
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2TextBox6.PasswordChar == '*')
            {
                guna2Button3.BringToFront();
                guna2TextBox6.PasswordChar = '\0';
            }
            else
            {
                guna2Button3.BringToFront();
                guna2TextBox6.PasswordChar = '*';
            }
        }
    }
}
