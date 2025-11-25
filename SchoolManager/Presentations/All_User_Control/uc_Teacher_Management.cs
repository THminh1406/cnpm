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
using Guna.UI2.WinForms;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Teacher_Management : UserControl
    {
        private Business_Logic_ApproveRegistration bllManage;
        private List<Classes> classesList;

        public uc_Teacher_Management()
        {
            InitializeComponent();
            this.Load += Uc_Teacher_Management_Load;
        }

        private void Uc_Teacher_Management_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            bllManage = new Business_Logic_ApproveRegistration();
            guna2TextBox1.TextChanged += (s, ev) => RefreshGrid();
            try { classesList = bllManage.GetAllClasses(); } catch { classesList = new List<Classes>(); }
            guna2DataGridView2.CellContentClick += Guna2DataGridView2_CellContentClick;
            try { this.flowLayoutPanelList.BringToFront(); } catch { }
            this.flowLayoutPanelList.SizeChanged += (s, ev) => AdjustPanelsWidth();

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            guna2DataGridView2.Rows.Clear();
            flowLayoutPanelList.Controls.Clear();
             string filter = guna2TextBox1.Text?.Trim().ToLower() ?? string.Empty;

             List<Accounts> teachers = new List<Accounts>();
             try
             {
                 teachers = bllManage.GetAllTeachers();
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Lỗi khi tải danh sách giáo viên:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }

             foreach (var t in teachers)
             {
                 if (!string.IsNullOrEmpty(filter))
                 {
                     if (!(t.FullName?.ToLower().Contains(filter) == true || t.Username?.ToLower().Contains(filter) == true || t.Email?.ToLower().Contains(filter) == true))
                         continue;
                 }
                 try
                 {
                     if (!string.IsNullOrEmpty(t.UserRole) && t.UserRole.Equals("admin", StringComparison.OrdinalIgnoreCase)
                         && SchoolManager.Session.CurrentTeacherId == t.IdTeacher)
                     {
                         continue;
                     }
                 }
                 catch { }
                AddTeacherCard(t.IdTeacher, t.FullName, t.Username, t.Email ?? string.Empty, t.Phone ?? string.Empty, t.UserRole ?? string.Empty);
            }
            AdjustPanelsWidth();
         }

        private void Guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != 5) return;
            var row = guna2DataGridView2.Rows[e.RowIndex];
            var acc = row.Tag as Accounts;
            if (acc == null) return;
            if (!string.IsNullOrEmpty(acc.UserRole) && acc.UserRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể thao tác trên tài khoản admin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Khóa tài khoản", null, (s, ev) => { PerformLock(acc); });
            menu.Items.Add("Mở khóa", null, (s, ev) => { PerformUnlock(acc); });
            menu.Items.Add("Xóa tài khoản", null, (s, ev) => { PerformDelete(acc); });
            if (classesList != null && classesList.Count > 0)
            {
                ToolStripMenuItem assign = new ToolStripMenuItem("Phân lớp");
                foreach (var c in classesList)
                {
                    assign.DropDownItems.Add(c.name_Class, null, (s, ev) => { PerformAssignClass(acc, c); });
                }
                menu.Items.Add(assign);
            }
            var ctrl = (Control)sender;
            var pt = ctrl.PointToClient(Cursor.Position);
            menu.Show(ctrl, pt);
        }

        private void PerformLock(Accounts acc)
        {
            if (MessageBox.Show("Bạn có chắc muốn khóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bllManage.LockTeacher(acc.IdTeacher))
                {
                    MessageBox.Show("Đã khóa tài khoản.");
                    RefreshGrid();
                }
                else MessageBox.Show("Khóa thất bại.");
            }
        }

        private void PerformUnlock(Accounts acc)
        {
            if (MessageBox.Show("Bạn có chắc muốn mở khóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bllManage.UnlockTeacher(acc.IdTeacher))
                {
                    MessageBox.Show("Đã mở khóa tài khoản.");
                    RefreshGrid();
                }
                else MessageBox.Show("Mở khóa thất bại.");
            }
        }

        private void PerformDelete(Accounts acc)
        {
            if (MessageBox.Show("Xóa tài khoản sẽ xóa thực sự khỏi hệ thống. Tiếp tục?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (bllManage.DeleteTeacher(acc.IdTeacher))
                {
                    MessageBox.Show("Đã xóa tài khoản.");
                    RefreshGrid();
                }
                else MessageBox.Show("Xóa thất bại.");
            }
        }

        private void PerformAssignClass(Accounts acc, Classes cls)
        {
            if (MessageBox.Show($"Gán {acc.FullName} làm chủ nhiệm lớp {cls.name_Class}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bllManage.AssignTeacherToClass(cls.id_Class, acc.IdTeacher))
                {
                    MessageBox.Show("Phân lớp thành công.");
                    RefreshGrid();
                }
                else MessageBox.Show("Phân lớp thất bại.");
            }
        }

        private void AdjustPanelsWidth()
        {
            int width = flowLayoutPanelList.ClientSize.Width - 25;
            foreach (Control c in flowLayoutPanelList.Controls)
            {
                if (c is Guna2Panel pnl)
                {
                    pnl.Width = Math.Max(800, width);
                }
            }
        }

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

        private void AddTeacherCard(int teacherId, string name, string username, string email, string phone, string role)
        {
            Guna2GradientPanel panel = new Guna2GradientPanel();
            panel.Size = new System.Drawing.Size(Math.Max(800, flowLayoutPanelList.ClientSize.Width - 25), 200);
            panel.FillColor = System.Drawing.Color.FromArgb(250, 250, 252);
            panel.FillColor2 = System.Drawing.Color.FromArgb(240, 248, 255);
            panel.BorderColor = System.Drawing.Color.FromArgb(220, 226, 235);
            panel.BorderRadius = 14;
            panel.BorderThickness = 1;
            panel.Margin = new Padding(0, 0, 0, 12);
            panel.Padding = new Padding(12);

            Guna2CirclePictureBox pic = new Guna2CirclePictureBox();
            pic.Size = new Size(64, 64);
            pic.Location = new Point(16, 16);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            try { pic.Image = CreateAvatarFromName(name, Math.Min(pic.Width, pic.Height)); } catch { pic.FillColor = Color.Gray; }
            panel.Controls.Add(pic);

            Guna2HtmlLabel lblName = new Guna2HtmlLabel();
            lblName.Text = name;
            lblName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblName.Location = new Point(96, 16);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            panel.Controls.Add(lblName);

            Guna2HtmlLabel lblInfo = new Guna2HtmlLabel();
            lblInfo.Text = $"{email}    |    {phone}";
            lblInfo.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblInfo.Location = new Point(96, 68);
            lblInfo.ForeColor = Color.DimGray;
            panel.Controls.Add(lblInfo);

            // class name
            string className = string.Empty;
            try { int cid = new SchoolManager.DAL.data_Access_Account().GetAssignedClassId(teacherId); if (cid > 0) { var c = classesList.FirstOrDefault(x => x.id_Class == cid); if (c != null) className = c.name_Class; } } catch { }
            Guna2HtmlLabel lblClass = new Guna2HtmlLabel();
            lblClass.Text = string.IsNullOrEmpty(className) ? "Chưa phân lớp" : "Chủ nhiệm: " + className;
            lblClass.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblClass.Location = new Point(96, 110);
            lblClass.ForeColor = Color.Goldenrod;
            panel.Controls.Add(lblClass);

            // status
            string status = "Unknown";
            try { var dal = new SchoolManager.DAL.data_Access_Account(); int act = dal.GetActivationState(username); if (act == 1) status = "Active"; else if (act == 0) status = "Inactive"; } catch { }
            Guna2HtmlLabel lblStatus = new Guna2HtmlLabel();
            lblStatus.Text = "Trạng thái: " + status;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblStatus.Location = new Point(96, 150);
            lblStatus.ForeColor = Color.FromArgb(100, 100, 100);
            panel.Controls.Add(lblStatus);

            // buttons
            int btnX = panel.Width - 360; // leave more space for assign controls
            Guna2Button btnLock = new Guna2Button() { Text = "Khóa", Size = new Size(100, 50), Location = new Point(btnX, 20), BorderRadius = 8 };
            btnLock.Click += (s, e) => { if (MessageBox.Show("Khóa tài khoản?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { if (bllManage.LockTeacher(teacherId)) { MessageBox.Show("Đã khóa"); RefreshGrid(); } else MessageBox.Show("Thất bại"); } };
            btnLock.FillColor = Color.OrangeRed;
            btnLock.ForeColor = Color.White;
            panel.Controls.Add(btnLock);

            Guna2Button btnUnlock = new Guna2Button() { Text = "Mở khóa", Size = new Size(100, 50), Location = new Point(btnX + 110, 20), BorderRadius = 8 };
            btnUnlock.Click += (s, e) => { if (MessageBox.Show("Mở khóa tài khoản?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) { if (bllManage.UnlockTeacher(teacherId)) { MessageBox.Show("Đã mở khóa"); RefreshGrid(); } else MessageBox.Show("Thất bại"); } };
            btnUnlock.FillColor = Color.SeaGreen;
            btnUnlock.ForeColor = Color.White;
            panel.Controls.Add(btnUnlock);

            Guna2Button btnDelete = new Guna2Button() { Text = "Xóa", Size = new Size(100, 50), Location = new Point(btnX + 220, 20), BorderRadius = 8, FillColor = Color.Crimson };
            btnDelete.Click += (s, e) => { if (MessageBox.Show("Xóa tài khoản?", "Cảnh báo", MessageBoxButtons.YesNo) == DialogResult.Yes) { if (bllManage.DeleteTeacher(teacherId)) { MessageBox.Show("Đã xóa"); RefreshGrid(); } else MessageBox.Show("Thất bại"); } };
            btnDelete.ForeColor = Color.White;
            panel.Controls.Add(btnDelete);

            // Assign dropdown: use classesList as DataSource so SelectedValue gives id_Class
            if (classesList != null && classesList.Count > 0)
            {
                Guna2ComboBox cboAssign = new Guna2ComboBox();
                cboAssign.Location = new Point(btnX, 80);
                cboAssign.Size = new Size(150, 60);
                cboAssign.DisplayMember = "name_Class";
                cboAssign.ValueMember = "id_Class";
                cboAssign.DataSource = classesList;
                cboAssign.SelectedIndexChanged += (s, e) => { /* no-op */ };
                // pre-select current assigned class if any
                try
                {
                    int currentClassId = new SchoolManager.DAL.data_Access_Account().GetAssignedClassId(teacherId);
                    if (currentClassId > 0)
                    {
                        cboAssign.SelectedValue = currentClassId;
                    }
                }
                catch { }
                // add assign button
                Guna2Button btnAssign = new Guna2Button() { Text = "Phân lớp", Size = new Size(150, 40), Location = new Point(btnX + 170, 75), BorderRadius = 8 };
                btnAssign.Click += (s, e) => {
                    if (cboAssign.SelectedItem == null) { MessageBox.Show("Chọn lớp trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    int selectedClassId = 0;
                    try
                    {
                        selectedClassId = Convert.ToInt32(cboAssign.SelectedValue);
                    }
                    catch
                    {
                        MessageBox.Show("Lỗi lấy id lớp đã chọn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Check if class already assigned to someone else
                    var classObj = classesList.FirstOrDefault(c => c.id_Class == selectedClassId);
                    if (classObj != null && classObj.AssignedTeacherId.HasValue && classObj.AssignedTeacherId.Value != teacherId)
                    {
                        var other = classObj.AssignedTeacherId.Value;
                        if (MessageBox.Show($"Lớp {classObj.name_Class} hiện đang có chủ nhiệm khác (ID={other}). Bạn vẫn muốn gán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                    }

                    if (MessageBox.Show($"Gán {name} làm chủ nhiệm lớp {classObj?.name_Class ?? selectedClassId.ToString()}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (bllManage.AssignTeacherToClass(selectedClassId, teacherId)) { MessageBox.Show("Phân lớp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); RefreshGrid(); } else MessageBox.Show("Phân lớp thất bại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                cboAssign.BackColor = Color.White;
                panel.Controls.Add(cboAssign);
                btnAssign.FillColor = Color.DodgerBlue;
                btnAssign.ForeColor = Color.White;
                panel.Controls.Add(btnAssign);
            }

            // store account id
            panel.Tag = teacherId;
            flowLayoutPanelList.Controls.Add(panel);
        }
    }
 }
