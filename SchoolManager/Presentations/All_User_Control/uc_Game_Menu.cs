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
            // Bỏ qua header
            if (e.RowIndex < 0) return;

            // Lấy tên cột được click
            string colName = dgv_GameList.Columns[e.ColumnIndex].Name;

            // Lấy ID an toàn (kiểm tra DBNull)
            object idValue = dgv_GameList.Rows[e.RowIndex].Cells["id_quiz"].Value;
            if (idValue == null || idValue == DBNull.Value) return;

            int quizId = Convert.ToInt32(idValue);

            // === LOGIC PHÂN LOẠI NÚT BẤM ===

            if (colName == "playButton")
            {
                // === 1. CHƠI GAME ===
                string quizType = dgv_GameList.Rows[e.RowIndex].Cells["quiz_type"].Value.ToString();
                // Gửi tín hiệu về Form chính (như cũ)
                OnPlayGameClicked?.Invoke(quizId, quizType);
            }
            else if (colName == "deleteButton")
            {
                // === 2. XÓA GAME ===

                // Lấy tên game để hỏi
                string quizTitle = dgv_GameList.Rows[e.RowIndex].Cells["quiz_title"].Value.ToString();

                // 2a. Hỏi xác nhận
                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn XÓA game: '{quizTitle}'?",
                    "Xác nhận Xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        // 2b. Gọi BLL
                        bool success = bll_Quizzes.DeleteQuiz(quizId);

                        if (success)
                        {
                            MessageBox.Show("Đã xóa game thành công.", "Hoàn tất");
                            // 2c. Tải lại lưới (dùng hàm public đã tạo)
                            RefreshGameList();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại.", "Lỗi");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                    }
                }
            }
        }
    }
}