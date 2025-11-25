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

        // Existing public handlers with PascalCase
        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string email = this.guna2TextBox4.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter your email.");
                return;
            }

            if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Email must be a gmail address.");
                return;
            }

            if (bll.SendOtp(email, out string otp))
            {
                currentOtp = otp;
                sentEmail = email;

                // Lock email to avoid retyping and enable Continue button
                this.guna2TextBox4.ReadOnly = true;
                this.guna2Button1.Enabled = true;

                MessageBox.Show("OTP sent. Please check your email.");
            }
            else
            {
                MessageBox.Show("Failed to send OTP. Check SMTP configuration or email.");
            }
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            string email = sentEmail ?? this.guna2TextBox4.Text.Trim();
            string otp = this.guna2TextBox1.Text.Trim();
            string newPass = this.guna2TextBox2.Text;
            string confirm = this.guna2TextBox3.Text;
           
            if (currentOtp == null)
            {
                MessageBox.Show("Please request a code first.");
                return;
            }

            if (!string.Equals(otp, currentOtp))
            {
                MessageBox.Show("Invalid OTP code.");
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Password and confirm do not match.");
                return;
            }

            if (!bll.ResetPassword(email, newPass))
            {
                MessageBox.Show("Failed to reset password. Ensure the email exists and password meets requirements.");
                return;
            }

            MessageBox.Show("Password reset successfully. You can now login.");
            this.Close();
        }

        private void forget_Password_Form_Load(object sender, EventArgs e)
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
