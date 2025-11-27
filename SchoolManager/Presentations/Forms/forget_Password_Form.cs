using System;
using System.Windows.Forms;
using SchoolManager.BLL;

namespace SchoolManager.Presentations.Forms
{
    public partial class forget_Password_Form : Form
    {
        private Business_Logic_PasswordReset bll = new Business_Logic_PasswordReset();
        private string currentOtp = null;
        private string sentEmail = null;

        public forget_Password_Form()
        {
            InitializeComponent();
        }

        private void EnterRecoveryEmail(object sender, EventArgs e)
        {
            string email = this.guna2TextBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email của bạn.");
                return;
            }

            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Email phải là địa chỉ gmail.");
                return;
            }

            if (bll.SendOtp(email, out string otp))
            {
                currentOtp = otp;
                sentEmail = email;

                // Lock email to avoid retyping and enable Continue button
                this.guna2TextBox4.ReadOnly = true;
                this.guna2Button1.Enabled = true;

                MessageBox.Show("Đã gửi OTP. Vui lòng kiểm tra email của bạn.");
            }
            else
            {
                MessageBox.Show("Không gửi được OTP. Kiểm tra cấu hình SMTP hoặc email.");
            }
        }

        private void ResetPassword(object sender, EventArgs e)
        {
            string email = sentEmail ?? this.guna2TextBox4.Text.Trim();
            string otp = this.guna2TextBox1.Text.Trim();
            string newPass = this.guna2TextBox2.Text;
            string confirm = this.guna2TextBox3.Text;
           
            if (currentOtp == null)
            {
                MessageBox.Show("Vui lòng yêu cầu nhận mã trước.");
                return;
            }

            if (!string.Equals(otp, currentOtp))
            {
                MessageBox.Show("Mã OTP không hợp lệ.");
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu và xác nhận không khớp.");
                return;
            }

            if (!bll.ResetPassword(email, newPass))
            {
                MessageBox.Show("Không đặt lại được mật khẩu. Hãy đảm bảo email tồn tại và mật khẩu đáp ứng yêu cầu.");
                return;
            }

            MessageBox.Show("Đã đặt lại mật khẩu thành công. Bây giờ bạn có thể đăng nhập.");
            this.Close();
        }

        private void DisplayForgotPasswordForm(object sender, EventArgs e)
        {

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
            if (guna2TextBox3.PasswordChar == '*')
            {
                guna2Button3.BringToFront();
                guna2TextBox3.PasswordChar = '\0';
            }
            else
            {
                guna2Button3.BringToFront();
                guna2TextBox3.PasswordChar = '*';
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Login_Form().Show();
            this.Hide();
        }
    }
}
