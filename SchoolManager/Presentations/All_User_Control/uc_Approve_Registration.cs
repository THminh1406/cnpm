using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO;
using System.Drawing;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Approve_Registration : UserControl
    {
        private Business_Logic_ApproveRegistration bllApprove;

        public uc_Approve_Registration()
        {
            InitializeComponent();
            this.Load += Uc_Approve_Registration_Load;
        }

        private void Uc_Approve_Registration_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            bllApprove = new Business_Logic_ApproveRegistration();
            try { this.flowLayoutPanelList.BringToFront(); } catch { }
            LoadPendingRegistrations();
            this.flowLayoutPanelList.SizeChanged += (s, ev) => AdjustPanelsWidth();
        }

        private void AdjustPanelsWidth()
        {
            int width = flowLayoutPanelList.ClientSize.Width - 25; // padding for scrollbar
            foreach (Control c in flowLayoutPanelList.Controls)
            {
                if (c is Guna2Panel pnl)
                {
                    pnl.Width = Math.Max(800, width);
                }
            }
        }

        private void LoadPendingRegistrations()
        {
            flowLayoutPanelList.Controls.Clear();

            List<Accounts> list = null;
            try
            {
                list = bllApprove.GetPendingRegistrations();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách duyệt:\n" + ex.Message + (ex.InnerException != null ? "\nInner: " + ex.InnerException.Message : ""), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                var lblErr = new Guna2HtmlLabel();
                lblErr.Text = "Lỗi khi tải dữ liệu. Xem thông báo lỗi.";
                lblErr.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                lblErr.ForeColor = Color.Red;
                lblErr.Margin = new Padding(20);
                flowLayoutPanelList.Controls.Add(lblErr);
                return;
            }
            if (list == null || list.Count == 0)
            {
                var lbl = new Guna2HtmlLabel();
                lbl.Text = "Không có yêu cầu duyệt";
                lbl.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                lbl.ForeColor = Color.DimGray;
                lbl.Margin = new Padding(20);
                flowLayoutPanelList.Controls.Add(lbl);
                return;
            }

            foreach (var acc in list)
            {
                AddTeacherCard(acc.IdTeacher, acc.FullName, acc.Subject ?? string.Empty, acc.CreatedAt.HasValue ? acc.CreatedAt.Value.ToString("HH:mm:ss dd/MM/yyyy") : string.Empty, acc.Email ?? string.Empty, acc.Phone ?? string.Empty);
            }

            AdjustPanelsWidth();
        }

        private void AddTeacherCard(int teacherId, string name, string className, string date, string email, string phone)
         {
             // 1. Tạo Panel chính
             Guna2Panel panel = new Guna2Panel();
             panel.Size = new System.Drawing.Size(1300, 200); 
             panel.FillColor = System.Drawing.Color.FloralWhite;
             panel.BorderColor = System.Drawing.Color.Red;
             panel.BorderRadius = 40;
             panel.BorderThickness = 1;
             panel.Margin = new Padding(0, 0, 0, 20);

             // 2. Avatar
             Guna2CirclePictureBox picAvatar = new Guna2CirclePictureBox();
             picAvatar.Size = new System.Drawing.Size(60, 60);
             picAvatar.Location = new Point(20, 20);
             picAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
             try
             {
                 int diameter = Math.Min(picAvatar.Size.Width, picAvatar.Size.Height);
                 picAvatar.Image = CreateAvatarFromName(name, diameter);
             }
             catch
             {
                 picAvatar.FillColor = Color.Gray;
             }
             panel.Controls.Add(picAvatar);

             // 3. Name
             Guna2HtmlLabel lblName = new Guna2HtmlLabel();
             lblName.Text = name;
             // set larger font for name
             lblName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
             lblName.Location = new Point(100, 20);
             panel.Controls.Add(lblName);

             // 4. Subject / Class
             Guna2HtmlLabel lblSubject = new Guna2HtmlLabel();
             lblSubject.Text = className; // subject field kept
             lblSubject.ForeColor = Color.Goldenrod;
             lblSubject.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
             lblSubject.Location = new Point(100, 140);
             panel.Controls.Add(lblSubject);

             // 5. Contact (phone and email shown under the name)
             Guna2HtmlLabel lblPhone = new Guna2HtmlLabel();
             lblPhone.Text = $"Điện thoại: <span style='color:dimGray'>{phone}</span>";
             // set larger font for phone
             lblPhone.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
             lblPhone.Location = new Point(100, 60);
             panel.Controls.Add(lblPhone);

             Guna2HtmlLabel lblEmail = new Guna2HtmlLabel();
             lblEmail.Text = $"Email: <span style='color:dimGray'>{email}</span>";
             lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
             lblEmail.Location = new Point(100, 90);
             panel.Controls.Add(lblEmail);

             // 6. Approve button
             Guna2Button btnApprove = new Guna2Button();
             btnApprove.Text = "Đồng ý";
             btnApprove.FillColor = Color.SeaGreen;
             btnApprove.BorderRadius = 20;
             btnApprove.Size = new Size(172, 71);
             // Hardcoded button positions (fixed, not auto)
             btnApprove.Location = new Point(1200, 15);
             btnApprove.Click += (s, e) => { OnApprove(teacherId, panel); };
             panel.Controls.Add(btnApprove);

             // 7. Reject button
             Guna2Button btnReject = new Guna2Button();
             btnReject.Text = "Từ chối";
             btnReject.FillColor = Color.Crimson;
             btnReject.BorderRadius = 20;
             btnReject.Size = new Size(172, 71);
             btnReject.Location = new Point(1200, 105);
             btnReject.Click += (s, e) => { OnReject(teacherId, panel); };
             panel.Controls.Add(btnReject);

             // Add to flow
             flowLayoutPanelList.Controls.Add(panel);
         }

        private void OnApprove(int teacherId, Control panel)
        {
            if (MessageBox.Show("Bạn có chắc muốn duyệt tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bllApprove.ApproveTeacher(teacherId))
                {
                    MessageBox.Show("Đã duyệt thành công.");
                    flowLayoutPanelList.Controls.Remove(panel);
                }
                else
                {
                    MessageBox.Show("Duyệt thất bại.");
                }
            }
        }

        private void OnReject(int teacherId, Control panel)
        {
            if (MessageBox.Show("Bạn có chắc muốn từ chối tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (bllApprove.RejectTeacher(teacherId))
                {
                    MessageBox.Show("Đã từ chối.");
                    flowLayoutPanelList.Controls.Remove(panel);
                }
                else
                {
                    MessageBox.Show("Từ chối thất bại.");
                }
            }
        }

        // Create a circular avatar image with initials from the provided name.
        private Image CreateAvatarFromName(string name, int diameter)
        {
            if (diameter <= 0) diameter = 60;

            // Get initials
            // Strategy A: use middle name (đệm) + given name (tên).
            // Example: "Nguyễn Thị Lan" -> "TL" (Thị + Lan).
            string initials = "";
            if (!string.IsNullOrWhiteSpace(name))
            {
                var parts = name.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 1)
                {
                    // Single word: take up to first two letters
                    initials = parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();
                }
                else
                {
                    // If there are >=2 parts, take middle (or family if only 2) and given name
                    string middle;
                    if (parts.Length >= 3)
                        middle = parts[parts.Length - 2]; // đệm
                    else
                        middle = parts[0]; // fallback to family name when no explicit middle

                    string given = parts[parts.Length - 1];

                    char m = middle.FirstOrDefault(c => char.IsLetter(c));
                    char g = given.FirstOrDefault(c => char.IsLetter(c));
                    initials = (char.ToUpper(m).ToString() + char.ToUpper(g).ToString()).Trim();
                }
            }
            if (string.IsNullOrEmpty(initials)) initials = "?";

            // Deterministic background color based on name hash
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

                // Draw initials
                float fontSize = diameter * 0.42f;
                using (Font font = new Font("Segoe UI", fontSize, FontStyle.Bold, GraphicsUnit.Pixel))
                using (Brush fb = new SolidBrush(Color.White))
                {
                    SizeF textSize = g.MeasureString(initials, font);
                    float tx = (diameter - textSize.Width) / 2f;
                    float ty = (diameter - textSize.Height) / 2f - 1; // small vertical tweak
                    g.DrawString(initials, font, fb, tx, ty);
                }
            }

            return bmp;
        }
    }
}
