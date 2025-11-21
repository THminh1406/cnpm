using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SchoolManager.BLL;
using SchoolManager.DTO;
using SchoolManager.Properties;

namespace SchoolManager
{
    public partial class login_Form : Form
    {
        private Business_Logic_Account bll = new Business_Logic_Account();
        private bool tokenIsValid = false;
        private Accounts tokenAccount = null;

        public login_Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // If a saved token exists, validate it but do not auto-open main form
            string savedToken = Settings.Default.RememberToken;
            if (!string.IsNullOrEmpty(savedToken))
            {
                try
                {
                    // Use the form-level BLL instance
                    Accounts acc = bll.LoginWithToken(savedToken);
                    if (acc != null)
                    {
                        tokenIsValid = true;
                        tokenAccount = acc;

                        guna2TextBox1.Text = acc.Username;
                        guna2CustomCheckBox1.Checked = true;
                    }
                    else
                    {
                        // Clear invalid token
                        Settings.Default.RememberToken = string.Empty;
                        Settings.Default.RememberTeacherId = 0;
                        Settings.Default.Save();

                        tokenIsValid = false;
                        tokenAccount = null;
                    }
                }
                catch
                {
                    tokenIsValid = false;
                    tokenAccount = null;
                }
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string username = guna2TextBox1.Text.Trim();
                string password = guna2TextBox2.Text; // Consider hashing in real app

                Accounts account = null;

                if (tokenIsValid && tokenAccount != null && string.IsNullOrEmpty(password))
                {
                    if (string.IsNullOrEmpty(username) || username.Equals(tokenAccount.Username, StringComparison.OrdinalIgnoreCase))
                    {
                        account = tokenAccount;
                    }
                }

                if (account == null)
                {
                    account = bll.Login(username, password);
                }

                // If account was loaded via token, set session here as well
                if (account != null && SchoolManager.Session.CurrentTeacherId == 0)
                {
                    SchoolManager.Session.CurrentTeacherId = account.IdTeacher;
                    SchoolManager.Session.CurrentUserRole = account.UserRole ?? string.Empty;
                    SchoolManager.Session.CurrentUsername = account.Username ?? string.Empty;
                }

                if (account != null)
                {
                    // set session
                    SchoolManager.Session.CurrentTeacherId = account.IdTeacher;
                    SchoolManager.Session.CurrentUserRole = account.UserRole ?? string.Empty;
                    SchoolManager.Session.CurrentUsername = account.Username ?? string.Empty;

                    if (guna2CustomCheckBox1.Checked)
                    {
                        if (bll.CreateRememberMeToken(account, out string token))
                        {
                            Settings.Default.RememberToken = token;
                            Settings.Default.RememberTeacherId = account.IdTeacher;
                            Settings.Default.Save();
                        }
                    }
                    else
                    {
                        Settings.Default.RememberToken = string.Empty;
                        Settings.Default.RememberTeacherId = 0;
                        Settings.Default.Save();
                    }

                    if (!string.IsNullOrEmpty(account.UserRole) && account.UserRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        var adminForm = new SchoolManager.Presentations.Forms.admin_Form();
                        adminForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        var mainForm = new SchoolManager.Presentations.Forms.index();
                        mainForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    // New logic: check password first, then activation
                    bool passwordCorrect = bll.VerifyPassword(username, password);
                    if (!passwordCorrect)
                    {
                        MessageBox.Show("Incorrect account or password!", "Sign in failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int activation = bll.GetActivationState(username);
                    if (activation == 0)
                    {
                        MessageBox.Show("Tài khoản của bạn chưa được kích hoạt. Vui lòng liên hệ quản trị viên.", "Tài khoản chưa kích hoạt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (activation == -1)
                    {
                        MessageBox.Show("Không tìm thấy tài khoản hoặc đã xảy ra lỗi.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Fallback
                        MessageBox.Show("Incorrect account or password!", "Sign in failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show exception message for debugging
                MessageBox.Show("An error occurred while trying to sign in: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var forget = new SchoolManager.Presentations.Forms.forget_Password_Form();
            forget.Show();
            this.Hide();
        }

        private void guna2CustomCheckBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var register = new SchoolManager.Presentations.Forms.register_Form();
            register.Show();
            this.Hide();
        }
    }
}
