using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO;
using SchoolManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager
{
    public partial class Login_Form : Form
    {
        private Business_Logic_Account bll = new Business_Logic_Account();
        public Login_Form()
        {
            InitializeComponent();
        }

        private void login(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.Username != string.Empty)
            {
                guna2TextBox1.Text = Properties.Settings.Default.Username;
                guna2TextBox2.Text = Properties.Settings.Default.Password;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy thông tin từ form
                string username = guna2TextBox1.Text.Trim();
                string password = guna2TextBox2.Text;

                // 2. Gọi hàm đăng nhập trực tiếp (Bỏ qua phần check Token)
                Accounts account = bll.Login(username, password);

                // 3. Kiểm tra kết quả
                if (account != null)
                {
                    // --- ĐĂNG NHẬP THÀNH CÔNG ---
                    if (checkBox1.Checked == true)
                    {
                        Properties.Settings.Default.Username = username;
                        Properties.Settings.Default.Password = password;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Username = "";
                        Properties.Settings.Default.Password = "";
                        Properties.Settings.Default.Save();
                    }

                    SchoolManager.Session.CurrentTeacherId = account.IdTeacher;
                    SchoolManager.Session.CurrentUsername = account.Username;
                    SchoolManager.Session.CurrentUserRole = account.UserRole;

                    // Phân quyền và chuyển form
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
                    // --- ĐĂNG NHẬP THẤT BẠI ---
                    // Kiểm tra chi tiết lỗi để báo cho người dùng
                    bool passwordCorrect = bll.VerifyPassword(username, password);

                    if (!passwordCorrect)
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi đăng nhập: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox2.PasswordChar == '*')
            {
                guna2Button2. BringToFront();
                guna2TextBox2.PasswordChar = '\0';
            }
            else
            {
                guna2Button2.BringToFront();
                guna2TextBox2.PasswordChar = '*';
            }
        }
    }
}
