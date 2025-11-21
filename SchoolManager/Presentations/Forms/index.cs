using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.Forms
{
    public partial class index : Form
    {
        private Guna.UI2.WinForms.Guna2Panel settingsPanel;
        private SchoolManager.BLL.Business_Logic_Account bll = new SchoolManager.BLL.Business_Logic_Account();

        public index()
        {
            InitializeComponent();
            this.Load += Index_Load;
        }

        private void Index_Load(object sender, EventArgs e)
        {
            // If there is a logged-in user in Session, load their info into guna2Panel3
            try
            {
                int curId = SchoolManager.Session.CurrentTeacherId;
                if (curId > 0)
                {
                    var acc = bll.GetTeacherById(curId);
                    if (acc != null)
                    {
                        this.guna2HtmlLabel8.Text = acc.FullName;
                        this.guna2HtmlLabel1.Text = acc.Username;

                        // create avatar from name similar to other UCs
                        try
                        {
                            var bmp = CreateAvatarFromName(acc.FullName, 80);
                            if (bmp != null)
                            {
                                // dispose previous image if any
                                if (this.guna2CirclePictureBox2.Image != null) this.guna2CirclePictureBox2.Image.Dispose();
                                this.guna2CirclePictureBox2.Image = bmp;
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }

            // hook settings button (guna2GradientButton13) click
            this.guna2GradientButton13.Click += SettingsButton_Click;
            // also allow clicking the small settings circle button to open settings
            try { this.guna2CircleButton2.Click += SettingsButton_Click; this.guna2CircleButton2.Cursor = Cursors.Hand; } catch { }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (settingsPanel != null && !settingsPanel.IsDisposed && settingsPanel.Visible)
            {
                settingsPanel.Visible = false;
                return;
            }

            // create panel if not exists
            if (settingsPanel == null || settingsPanel.IsDisposed)
            {
                settingsPanel = new Guna.UI2.WinForms.Guna2Panel();
                settingsPanel.Size = new Size(260, 140);
                settingsPanel.BackColor = Color.White;
                settingsPanel.BorderRadius = 10;
                settingsPanel.ShadowDecoration.Enabled = true;

                // create buttons
                var btnEdit = new Guna.UI2.WinForms.Guna2Button() { Text = "Đổi thông tin", Size = new Size(240, 36), Location = new Point(10, 10), BorderRadius = 8 };
                var btnPass = new Guna.UI2.WinForms.Guna2Button() { Text = "Đổi mật khẩu", Size = new Size(240, 36), Location = new Point(10, 52), BorderRadius = 8 };
                var btnLogout = new Guna.UI2.WinForms.Guna2Button() { Text = "Đăng xuất", Size = new Size(240, 36), Location = new Point(10, 94), BorderRadius = 8, FillColor = Color.IndianRed };

                btnEdit.Click += BtnEdit_Click;
                btnPass.Click += BtnPass_Click;
                btnLogout.Click += BtnLogout_Click;

                settingsPanel.Controls.Add(btnEdit);
                settingsPanel.Controls.Add(btnPass);
                settingsPanel.Controls.Add(btnLogout);

                // place settingsPanel above guna2Panel3 near gradient button
                var panel = this.guna2Panel3;
                settingsPanel.Location = panel.PointToClient(this.guna2GradientButton13.PointToScreen(new Point(0, -150)));

                // ensure it is inside main form
                settingsPanel.Location = new Point(panel.Left + 12, panel.Top - 150);

                this.Controls.Add(settingsPanel);
                settingsPanel.BringToFront();
            }

            settingsPanel.Visible = true;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            // open styled dialog to edit name/email/phone using Guna2 controls
            int curId = SchoolManager.Session.CurrentTeacherId;
            if (curId <= 0) return;

            var acc = bll.GetTeacherById(curId);
            if (acc == null) return;

            var dlg = new Form();
            dlg.Text = "Ð?i thông tin";
            dlg.FormBorderStyle = FormBorderStyle.None;
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ClientSize = new Size(420, 320);
            dlg.BackColor = Color.White;

            // Header - lime green frame with close
            var dlgHeader = new Guna.UI2.WinForms.Guna2GradientPanel();
            dlgHeader.Size = new Size(dlg.ClientSize.Width, 60);
            dlgHeader.Dock = DockStyle.Top;
            dlgHeader.FillColor = Color.FromArgb(86, 180, 72);
            dlgHeader.FillColor2 = Color.FromArgb(118, 222, 114);
            dlgHeader.BorderRadius = 8;

            var hdrTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            hdrTitle.Text = "Cập nhật thông tin";
            hdrTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            hdrTitle.ForeColor = Color.White;
            hdrTitle.Location = new Point(16, 14);
            dlgHeader.Controls.Add(hdrTitle);

            var hdrClose = new Guna.UI2.WinForms.Guna2CircleButton();
            hdrClose.Size = new Size(34, 34);
            hdrClose.Location = new Point(dlg.ClientSize.Width - 50, 12);
            hdrClose.FillColor = Color.White;
            hdrClose.Image = null; // keep simple
            hdrClose.Text = "X";
            hdrClose.ForeColor = Color.FromArgb(86, 180, 72);
            hdrClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            hdrClose.Click += (s, ev) => { dlg.Close(); };
            dlgHeader.Controls.Add(hdrClose);

            dlg.Controls.Add(dlgHeader);

            var card = new Guna.UI2.WinForms.Guna2Panel();
            card.Dock = DockStyle.Fill;
            card.FillColor = Color.White;
            card.Padding = new Padding(16);
            dlg.Controls.Add(card);

            int y = 54;
            var lblName = new Guna.UI2.WinForms.Guna2HtmlLabel() { Text = "Họ và tên", Location = new Point(16, y), ForeColor = Color.DimGray };
            card.Controls.Add(lblName);
            var txtName = new Guna.UI2.WinForms.Guna2TextBox() { Location = new Point(16, y + 26), Width = 368, Text = acc.FullName, PlaceholderText = "Nhập họ và tên" };
            card.Controls.Add(txtName);

            y += 72;
            var lblEmail = new Guna.UI2.WinForms.Guna2HtmlLabel() { Text = "Email", Location = new Point(16, y), ForeColor = Color.DimGray };
            card.Controls.Add(lblEmail);
            var txtEmail = new Guna.UI2.WinForms.Guna2TextBox() { Location = new Point(16, y + 26), Width = 368, Text = acc.Email, PlaceholderText = "Nhập email" };
            card.Controls.Add(txtEmail);

            y += 72;
            var lblPhone = new Guna.UI2.WinForms.Guna2HtmlLabel() { Text = "SĐT", Location = new Point(16, y), ForeColor = Color.DimGray };
            card.Controls.Add(lblPhone);
            var txtPhone = new Guna.UI2.WinForms.Guna2TextBox() { Location = new Point(16, y + 26), Width = 368, Text = acc.Phone, PlaceholderText = "Nhập số điện thoại" };
            card.Controls.Add(txtPhone);

            var btnSave = new Guna.UI2.WinForms.Guna2Button() { Text = "Lưu", Size = new Size(120, 40), Location = new Point(264, dlg.ClientSize.Height - 56), BorderRadius = 8, FillColor = Color.DodgerBlue, ForeColor = Color.White };
            var btnCancel = new Guna.UI2.WinForms.Guna2Button() { Text = "Hủy", Size = new Size(120, 40), Location = new Point(136, dlg.ClientSize.Height - 56), BorderRadius = 8, FillColor = Color.LightGray, ForeColor = Color.Black };
            card.Controls.Add(btnSave);
            card.Controls.Add(btnCancel);

            btnCancel.Click += (s, ev) => { dlg.Close(); };
            btnSave.Click += (s, ev) => {
                // validation
                if (string.IsNullOrWhiteSpace(txtName.Text)) { MessageBox.Show("Họ tên không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                // basic email check
                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@")) { MessageBox.Show("Email không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                bool ok = bll.UpdateTeacherInfo(curId, txtName.Text.Trim(), txtEmail.Text.Trim(), txtPhone.Text.Trim());
                if (ok)
                {
                    MessageBox.Show("Cập nhật thông tin thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.guna2HtmlLabel8.Text = txtName.Text.Trim();
                    this.guna2HtmlLabel1.Text = acc.Username;
                    dlg.Close();
                }
                else MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // ensure header close positions are correct after show
            dlg.Shown += (s, ev) => {
                try { dlgHeader.Controls.OfType<Guna.UI2.WinForms.Guna2CircleButton>().First().Location = new Point(dlg.ClientSize.Width - 50, 12); } catch { }
            };
            dlg.ShowDialog(this);
            settingsPanel.Visible = false;
        }

        private void BtnPass_Click(object sender, EventArgs e)
        {
            int curId = SchoolManager.Session.CurrentTeacherId;
            if (curId <= 0) return;

            var dlg = new Form();
            dlg.FormBorderStyle = FormBorderStyle.None;
            dlg.StartPosition = FormStartPosition.CenterParent;
            dlg.ClientSize = new Size(420, 260);
            dlg.BackColor = Color.White;

            // header
            var dlgHeader = new Guna.UI2.WinForms.Guna2GradientPanel();
            dlgHeader.Size = new Size(dlg.ClientSize.Width, 60);
            dlgHeader.Dock = DockStyle.Top;
            dlgHeader.FillColor = Color.FromArgb(86, 180, 72);
            dlgHeader.FillColor2 = Color.FromArgb(118, 222, 114);
            dlgHeader.BorderRadius = 8;

            var hdrTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            hdrTitle.Text = "Đổi mật khẩu";
            hdrTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            hdrTitle.ForeColor = Color.White;
            hdrTitle.Location = new Point(16, 14);
            dlgHeader.Controls.Add(hdrTitle);

            var hdrClose = new Guna.UI2.WinForms.Guna2CircleButton();
            hdrClose.Size = new Size(34, 34);
            hdrClose.Location = new Point(dlg.ClientSize.Width - 50, 12);
            hdrClose.FillColor = Color.White;
            hdrClose.Text = "X";
            hdrClose.ForeColor = Color.FromArgb(86, 180, 72);
            hdrClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            hdrClose.Click += (s, ev) => { dlg.Close(); };
            dlgHeader.Controls.Add(hdrClose);

            dlg.Controls.Add(dlgHeader);

            var card = new Guna.UI2.WinForms.Guna2Panel();
            card.Dock = DockStyle.Fill;
            card.FillColor = Color.White;
            card.Padding = new Padding(16);
            dlg.Controls.Add(card);

            int y = 54;
            var lblOld = new Guna.UI2.WinForms.Guna2HtmlLabel() { Text = "Mật khẩu hiện tại", Location = new Point(16, y), ForeColor = Color.DimGray };
            card.Controls.Add(lblOld);
            var txtOld = new Guna.UI2.WinForms.Guna2TextBox() { Location = new Point(16, y + 26), Width = 368, UseSystemPasswordChar = true, PlaceholderText = "Mật khẩu hiện tại" };
            card.Controls.Add(txtOld);

            y += 72;
            var lblNew = new Guna.UI2.WinForms.Guna2HtmlLabel() { Text = "Mật khẩu mới", Location = new Point(16, y), ForeColor = Color.DimGray };
            card.Controls.Add(lblNew);
            var txtNew = new Guna.UI2.WinForms.Guna2TextBox() { Location = new Point(16, y + 26), Width = 368, UseSystemPasswordChar = true, PlaceholderText = "Mật khẩu mới (ít nhất 6 ký tự)" };
            card.Controls.Add(txtNew);

            var btnSave = new Guna.UI2.WinForms.Guna2Button() { Text = "Đổi", Size = new Size(120, 40), Location = new Point(264, dlg.ClientSize.Height - 56), BorderRadius = 8, FillColor = Color.DodgerBlue, ForeColor = Color.White };
            var btnCancel = new Guna.UI2.WinForms.Guna2Button() { Text = "Hủy", Size = new Size(120, 40), Location = new Point(136, dlg.ClientSize.Height - 56), BorderRadius = 8, FillColor = Color.LightGray, ForeColor = Color.Black };
            card.Controls.Add(btnSave);
            card.Controls.Add(btnCancel);

            btnCancel.Click += (s, ev) => { dlg.Close(); };
            btnSave.Click += (s, ev) => {
                if (string.IsNullOrEmpty(txtOld.Text) || string.IsNullOrEmpty(txtNew.Text)) { MessageBox.Show("Vui lòng điền đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtNew.Text.Length < 6) { MessageBox.Show("Mật khẩu mới phải ít nhất 6 ký tự", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                if (!bll.VerifyPassword(this.guna2HtmlLabel1.Text, txtOld.Text)) { MessageBox.Show("Mật khẩu hiện tại không đúng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                string newHash = bll.CreatePasswordHash(txtNew.Text);
                if (bll.SetPasswordHashById(curId, newHash))
                {
                    MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dlg.Close();
                }
                else MessageBox.Show("Đổi mật khẩu thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            dlg.ShowDialog(this);
            settingsPanel.Visible = false;
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            // clear session and return to login form
            SchoolManager.Session.CurrentTeacherId = 0;
            SchoolManager.Session.CurrentUserRole = string.Empty;
            SchoolManager.Session.CurrentUsername = string.Empty;

            var login = new SchoolManager.login_Form();
            login.Show();
            this.Close();
        }

        // Helper to create avatar image (same algorithm used in other UCs)
        private Image CreateAvatarFromName(string name, int diameter)
        {
            if (diameter <= 0) diameter = 60;

            string initials = "";
            if (!string.IsNullOrWhiteSpace(name))
            {
                var parts = name.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1)
                {
                    initials = parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();
                }
                else
                {
                    string middle;
                    if (parts.Length >= 3)
                        middle = parts[parts.Length - 2];
                    else
                        middle = parts[0];

                    string given = parts[parts.Length - 1];
                    char m = middle.FirstOrDefault(c => char.IsLetter(c));
                    char g = given.FirstOrDefault(c => char.IsLetter(c));
                    initials = (char.ToUpper(m).ToString() + char.ToUpper(g).ToString()).Trim();
                }
            }
            if (string.IsNullOrEmpty(initials)) initials = "?";
 
            int hash = name != null ? name.GetHashCode() : 0;
            Random rnd = new Random(hash);
            Color bg = Color.FromArgb(255, (byte)rnd.Next(64, 200), (byte)rnd.Next(64, 200), (byte)rnd.Next(64, 200));

            Bitmap bmp = new Bitmap(diameter, diameter);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush b = new SolidBrush(bg))
                {
                    g.FillEllipse(b, 0, 0, diameter - 1, diameter - 1);
                }
                float fontSize = diameter * 0.42f;
                using (Font font = new Font("Segoe UI", fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
                using (Brush fb = new SolidBrush(Color.White))
                {
                    SizeF textSize = g.MeasureString(initials, font);
                    float tx = (diameter - textSize.Width) / 2f;
                    float ty = (diameter - textSize.Height) / 2f - 1;
                    g.DrawString(initials, font, fb, tx, ty);
                }
            }

            return bmp;
        }

        public void showUC(UserControl uc)
        {
            // Ẩn tất cả UC khác
            foreach (Control c in panelContainer.Controls)
                if (c is UserControl) c.Visible = false;

            uc.Visible = true;
            uc.BringToFront();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            showUC(uc_Manual_Roll_Call);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            showUC(qR_Roll_Call);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            showUC(uc_Show_Tools);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            showUC(uc_Manage_Vocabulary);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            showUC(uc_Show_Study_Management1);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            showUC(uc_Create_Quiz);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            uc_Game_Menu1.RefreshGameList();
            showUC(uc_Game_Menu1);
        }

        private void uc_Game_Menu1_OnPlayGameClicked(int quizId, string quizType)
        {
            uc_Game_Menu1.SendToBack();
            uc_Game_Menu1.Visible = false;

            // (Giả sử bạn có 4 UC game: uc_Game_WordMatch1, uc_Game_MemoryFlip1, ...)

            // Dựa vào loại game, gọi UC tương ứng
            switch (quizType)
            {
                case "Nối hình":
                    //1.Hiển thị UC "Nối hình"
                    uc_Game_WordMatch1.Visible = true;
                    uc_Game_WordMatch1.BringToFront();

                    // 2. GỌI HÀM LOADGAME
                    uc_Game_WordMatch1.LoadGame(quizId);
                    break;

                case "Lật thẻ":
                    uc_Game_MemoryMatch1.Visible = true;
                    uc_Game_MemoryMatch1.BringToFront();
                    uc_Game_MemoryMatch1.LoadGame(quizId);
                    break;

                case "Điền từ":
                    uc_Game_FillBlank1.Visible = true;
                    uc_Game_FillBlank1.BringToFront();
                    uc_Game_FillBlank1.LoadGame(quizId);
                    break;

                case "Sắp xếp câu":
                    uc_Game_SentenceScramble1.Visible = true;
                    uc_Game_SentenceScramble1.BringToFront();
                    uc_Game_SentenceScramble1.LoadGame(quizId);
                    break;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            showUC(uC_Student_Management1);
        }
    }
}
