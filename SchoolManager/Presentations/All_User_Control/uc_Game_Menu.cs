using SchoolManager.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Game_Menu : UserControl
    {
        public event Action<int, string> OnPlayGameClicked;
        private Business_Logic_Quizzes bll_Quizzes;

        public uc_Game_Menu()
        {
            InitializeComponent();
        }

        private void uc_Game_Menu_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            // (Không cần code ở đây nữa, chuyển vào Refresh)
            RefreshGameList();
        }

        // === SỬA LẠI HÀM NÀY ===
        // 1. Đổi 'private' thành 'public' (để Form chính gọi được)
        // 2. Đổi tên thành 'RefreshGameList' (cho rõ nghĩa)
        // 3. Thêm kiểm tra 'null' cho BLL
        public void RefreshGameList()
        {
            if (this.DesignMode) return;

            // Nếu BLL chưa được tạo (lần chạy đầu tiên), thì tạo nó
            if (bll_Quizzes == null)
            {
                bll_Quizzes = new Business_Logic_Quizzes();
            }

            dgv_GameList.AutoGenerateColumns = false;
            dgv_GameList.DataSource = bll_Quizzes.GetAllQuizzes();
        }
        // ======================

        private void dgv_GameList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra hợp lệ: Bỏ qua nếu click vào Header (RowIndex < 0)
            if (e.RowIndex < 0) return;

            // 2. Lấy tên cột vừa được click
            string colName = dgv_GameList.Columns[e.ColumnIndex].Name;

            // 3. Lấy ID Quiz (Quan trọng: Kiểm tra null để tránh lỗi)
            var cellValue = dgv_GameList.Rows[e.RowIndex].Cells["id_quiz"].Value;
            if (cellValue == null || cellValue == DBNull.Value) return;

            int quizId = Convert.ToInt32(cellValue);

            // === TRƯỜNG HỢP 1: NÚT CHƠI (playButton) ===
            if (colName == "playButton")
            {
                // Lấy loại game để biết đường mở Form tương ứng
                var typeValue = dgv_GameList.Rows[e.RowIndex].Cells["quiz_type"].Value;
                string quizType = typeValue != null ? typeValue.ToString() : "";

                // Kích hoạt sự kiện để Form chính (MainForm) biết và chuyển trang
                if (OnPlayGameClicked != null)
                {
                    OnPlayGameClicked.Invoke(quizId, quizType);
                }
                else
                {
                    MessageBox.Show("Sự kiện chuyển trang chưa được gắn kết!", "Lỗi Hệ Thống");
                }
            }
            // === TRƯỜNG HỢP 2: NÚT XÓA (deleteButton) ===
            else if (colName == "deleteButton")
            {
                // Lấy tên bài để hỏi xác nhận cho thân thiện
                var titleValue = dgv_GameList.Rows[e.RowIndex].Cells["quiz_title"].Value;
                string quizTitle = titleValue != null ? titleValue.ToString() : "Bài này";

                // Hỏi xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn XÓA bài thi:\n'{quizTitle}'\n\nHành động này không thể hoàn tác!",
                    "Xác nhận Xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Gọi BLL để xóa trong CSDL
                        bool success = bll_Quizzes.DeleteQuiz(quizId);

                        if (success)
                        {
                            MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Tải lại danh sách để cập nhật giao diện ngay lập tức
                            // (Hàm này là hàm Load danh sách bạn đã viết trong uc_GameMenu)
                            RefreshGameList();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại. Có thể bài thi không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hệ thống khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgv_GameList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Chỉ xử lý nếu là dòng dữ liệu (không phải header) và là cột nút bấm
            if (e.RowIndex < 0) return;

            // 1. Xử lý cột "Chơi" (playButton)
            if (e.ColumnIndex == dgv_GameList.Columns["playButton"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // Tạo vùng vẽ cho nút (nhỏ hơn ô một chút để có padding)
                var buttonRect = new Rectangle(e.CellBounds.X + 20, e.CellBounds.Y + 20, e.CellBounds.Width - 40, e.CellBounds.Height - 40);

                // Vẽ nền nút (Bo tròn) - Màu Xanh Dương
                using (var brush = new SolidBrush(Color.FromArgb(0, 122, 204))) // Màu xanh đẹp
                using (var path = GetRoundedRectPath(buttonRect, 15)) // Bo góc 15px
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Vẽ chữ "Chơi" (Hoặc vẽ Icon nếu có)
                TextRenderer.DrawText(e.Graphics, "Chơi Ngay",
                    new Font("Segoe UI", 10, FontStyle.Bold),
                    buttonRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // (Tùy chọn) Nếu muốn vẽ Icon thay vì chữ:
                // if (Properties.Resources.play_icon != null)
                //    e.Graphics.DrawImage(Properties.Resources.play_icon, new Rectangle(buttonRect.X + 15, buttonRect.Y + 10, 32, 32));

                e.Handled = true; // Báo cho hệ thống biết mình đã tự vẽ xong
            }

            // 2. Xử lý cột "Xóa" (deleteButton)
            if (e.ColumnIndex == dgv_GameList.Columns["deleteButton"].Index)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                var buttonRect = new Rectangle(e.CellBounds.X + 30, e.CellBounds.Y + 20, e.CellBounds.Width - 60, e.CellBounds.Height - 40);

                // Vẽ nền nút - Màu Đỏ Cam
                using (var brush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                using (var path = GetRoundedRectPath(buttonRect, 15))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Vẽ chữ "Xóa"
                TextRenderer.DrawText(e.Graphics, "Xóa",
                    new Font("Segoe UI", 10, FontStyle.Bold),
                    buttonRect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                // (Tùy chọn) Vẽ Icon thùng rác
                // if (Properties.Resources.delete_icon != null) ...

                e.Handled = true;
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Góc trên trái
            path.AddArc(arc, 180, 90);

            // Góc trên phải
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Góc dưới phải
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Góc dưới trái
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}