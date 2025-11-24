using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO;
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
    public partial class uc_Create_Quiz : UserControl
    {

        private Business_Logic_Categories bll_Categories;
        private Business_Logic_Vocabulary bll_Vocabulary;
        private Business_Logic_Quizzes bll_Quizzes;

        public uc_Create_Quiz()
        {
            InitializeComponent();
        }

        private void uc_Create_Quiz_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            // Khởi tạo BLL
            bll_Categories = new Business_Logic_Categories();
            bll_Vocabulary = new Business_Logic_Vocabulary();
            bll_Quizzes = new Business_Logic_Quizzes();

            LoadCategories();
            LoadQuizTypes();

            // === THÊM DÒNG NÀY ===
            // Luôn tắt AutoGenerateColumns để dùng cột tùy chỉnh
            dgv_Word.AutoGenerateColumns = false;
        }

        private void LoadCategories()
        {
            // Gọi BLL để lấy danh sách mới nhất từ CSDL
            var categories = bll_Categories.GetAllCategories(true);

            // Thêm mục mặc định
            categories.Insert(0, new CategoryDTO { id_category = 0, category_name = "-- Chọn chủ đề --" });

            // Gán lại DataSource (Gán null trước để nó refresh)
            cbo_FilterBank.DataSource = null;
            cbo_FilterBank.DataSource = categories;
            cbo_FilterBank.DisplayMember = "category_name";
            cbo_FilterBank.ValueMember = "id_category";

            if (cbo_FilterBank.Items.Count > 0) cbo_FilterBank.SelectedIndex = 0;
        }

        public void Reload()
        {
            // A. Tải lại danh sách chủ đề (Quan trọng nhất)
            // Để khi bạn vừa thêm chủ đề bên kia, qua đây nó hiện ra ngay
            LoadCategories();

            // B. Làm sạch các ô nhập liệu (Reset Form)
            txt_QuizTitle.Clear();

            // Reset loại game về mặc định
            if (cbo_QuizType.Items.Count > 0) cbo_QuizType.SelectedIndex = 0;

            // Xóa lưới hiển thị từ vựng cũ
            dgv_Word.DataSource = null;

            // Reset các thông báo khác
            lbl_PairCount.Text = "";
            if (rad_LevelEasy != null) rad_LevelEasy.Checked = true;
        }

        // Tải các loại game
        private void LoadQuizTypes()
        {
            cbo_QuizType.Items.Clear();
            cbo_QuizType.Items.Add("Nối hình");
            cbo_QuizType.Items.Add("Sắp xếp câu");
            cbo_QuizType.Items.Add("Điền từ");
            cbo_QuizType.Items.Add("Lật thẻ");
            cbo_QuizType.SelectedIndex = 0;
        }

        private void cbo_FilterBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_category = 0;
            if (cbo_FilterBank.SelectedItem != null && cbo_FilterBank.SelectedItem is CategoryDTO)
            {
                id_category = ((CategoryDTO)cbo_FilterBank.SelectedItem).id_category;
            }

            if (id_category == 0)
            {
                dgv_Word.DataSource = null;
                return;
            }

            string gameType = cbo_QuizType.SelectedItem.ToString();
            string vocabTypeToLoad = "";

            if (gameType == "Nối hình")
                vocabTypeToLoad = "Word";
            else if (gameType == "Lật thẻ")
                vocabTypeToLoad = "Word";
            else if (gameType == "Sắp xếp câu")
                vocabTypeToLoad = "Sentence";
            else if (gameType == "Điền từ")
                vocabTypeToLoad = "FillBlank";
            else
            {
                dgv_Word.DataSource = null;
                return;
            }

            // Gọi BLL
            var vocabList = bll_Vocabulary.GetVocabularyByCategory(id_category, vocabTypeToLoad);
            dgv_Word.DataSource = vocabList;

            // Cấu hình cột
            dgv_Word.RowTemplate.Height = 80;

            // === PHẦN SỬA LỖI (CẬP NHẬT CỘT) ===

            // 1. Ẩn/Hiện cột Ảnh (Giữ nguyên)
            if (dgv_Word.Columns.Contains("WordImage"))
            {
                // Ép kiểu cột về dạng ImageColumn để chỉnh thuộc tính Layout
                DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dgv_Word.Columns["WordImage"];

                // Chế độ Zoom: Ảnh tự co nhỏ để nằm trọn trong ô (giữ tỷ lệ, không bị méo)
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

                // Ẩn/Hiện tùy theo loại game
                imgCol.Visible = (vocabTypeToLoad == "Word");

                // (Tùy chọn) Chỉnh độ rộng cột ảnh cho đẹp
                imgCol.Width = 120;
            }
            // ===================================
        }

        private void btn_AddToQuiz_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_QuizTitle.Text) || cbo_QuizType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập Tên Game và chọn Loại Game.", "Thiếu thông tin");
                return;
            }
            string quizType = cbo_QuizType.SelectedItem.ToString();

            List<int> vocabularyIds = new List<int>();
            int selectedCount = 0;

            // === LOGIC THÔNG MINH (ĐÃ SỬA) ===
            if (quizType == "Lật thẻ")
            {
                // LOẠI 1: Nếu là "Lật thẻ", CHỈ lấy hàng được check [ ]
                foreach (DataGridViewRow row in dgv_Word.Rows)
                {
                    // Kiểm tra checkbox
                    if (row.Cells["col_Select"].Value as bool? == true)
                    {
                        vocabularyIds.Add(Convert.ToInt32(row.Cells["id_vocabulary"].Value));
                    }
                }
                selectedCount = vocabularyIds.Count;
            }
            else
            {
                // LOẠI 2: Nếu là game khác, lấy TẤT CẢ hàng
                foreach (DataGridViewRow row in dgv_Word.Rows)
                {
                    if (row.IsNewRow) continue;
                    vocabularyIds.Add(Convert.ToInt32(row.Cells["id_vocabulary"].Value));
                }
                selectedCount = vocabularyIds.Count;
            }
            // ========================

            if (selectedCount == 0)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 1 nguyên liệu (hoặc chủ đề không có nguyên liệu).", "Thiếu nội dung");
                return;
            }

            // Kiểm tra riêng cho "Lật thẻ"
            if (quizType == "Lật thẻ")
            {
                int requiredPairs = 0;
                if (rad_LevelEasy.Checked) requiredPairs = 6;
                else if (rad_LevelMedium.Checked) requiredPairs = 8;
                else if (rad_LevelHard.Checked) requiredPairs = 10;

                if (selectedCount != requiredPairs)
                {
                    MessageBox.Show(
                        $"Lỗi: Mức độ bạn chọn yêu cầu ĐÚNG {requiredPairs} cặp.\n" +
                        $"Bạn đang chọn {selectedCount} cặp.",
                        "Sai số lượng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            // (Code Lưu Game - Giữ nguyên)
            QuizDTO newQuiz = new QuizDTO
            {
                quiz_title = txt_QuizTitle.Text,
                quiz_type = quizType,
                id_teacher = 1,
                id_class = 1,
                quiz_config_json = "{}"
            };

            try
            {
                bool success = bll_Quizzes.CreateQuizWithContent(newQuiz, vocabularyIds);
                if (success)
                {
                    MessageBox.Show("Tạo game thành công!", "Thành công");
                    txt_QuizTitle.Text = "";
                    cbo_FilterBank.SelectedIndex = 0;
                    dgv_Word.DataSource = null;
                }
                else { MessageBox.Show("Lỗi khi tạo game.", "Lỗi"); }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nghiêm trọng: " + ex.Message); }
        }

        private void cbo_QuizType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string gameType = cbo_QuizType.SelectedItem.ToString();

            // 1. Ẩn/Hiện Panel Mức độ (Giữ nguyên)
            pnl_MemoryLevels.Visible = (gameType == "Lật thẻ");

            // 2. === LOGIC MỚI ===
            // Ẩn/Hiện cột Chọn (kể cả khi lưới rỗng)
            if (dgv_Word.Columns.Contains("col_Select"))
            {
                dgv_Word.Columns["col_Select"].Visible = (gameType == "Lật thẻ");
            }
            // ==================

            // 3. Tự động tải lại lưới (Giữ nguyên)
            if (cbo_FilterBank.SelectedIndex > 0)
            {
                cbo_FilterBank_SelectedIndexChanged(sender, e);
            }
            else
            {
                dgv_Word.DataSource = null;
            }

            // 4. Tự động cập nhật bộ đếm (Giữ nguyên)
            UpdatePairCount();
        }


        private void UpdatePairCount(object sender = null, EventArgs e = null)
        {
            if (cbo_QuizType.SelectedItem == null) return;

            // Chỉ chạy nếu là game "Lật thẻ"
            if (cbo_QuizType.SelectedItem.ToString() == "Lật thẻ")
            {
                // 1. Xác định số cặp YÊU CẦU
                int requiredPairs = 0;
                if (rad_LevelEasy.Checked) requiredPairs = 6;
                else if (rad_LevelMedium.Checked) requiredPairs = 8;
                else if (rad_LevelHard.Checked) requiredPairs = 10;

                // 2. Lấy số lượng ĐÃ CHỌN (Đếm checkbox)
                int selectedCount = 0;
                foreach (DataGridViewRow row in dgv_Word.Rows)
                {
                    if (row.Cells["col_Select"].Value as bool? == true)
                    {
                        selectedCount++;
                    }
                }

                // 3. Cập nhật Label
                lbl_PairCount.Text = $"Đã chọn: {selectedCount} / {requiredPairs} cặp";

                // 4. Đổi màu
                lbl_PairCount.ForeColor = (selectedCount == requiredPairs) ? Color.Green : Color.Red;
            }
            else
            {
                lbl_PairCount.Text = ""; // Ẩn đi
            }
        }

        private void dgv_Word_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // === KIỂM TRA AN TOÀN (PHIÊN BẢN MỚI) ===
            // Thử lấy cột "col_Select"
            var col = dgv_Word.Columns["col_Select"];

            // Nếu cột đó không tồn tại (null) tại thời điểm này, hãy thoát ra
            if (col == null)
            {
                return;
            }
            // ===================================

            // Nếu code chạy đến đây, 'col' chắc chắn không null
            if (e.ColumnIndex == col.Index && e.RowIndex >= 0)
            {
                UpdatePairCount(); // Gọi hàm đếm
            }
        }

        private void dgv_Word_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // === KIỂM TRA AN TOÀN (PHIÊN BẢN MỚI) ===
            // Thử lấy cột "col_Select"
            var col = dgv_Word.Columns["col_Select"];

            // Nếu cột đó không tồn tại (null) tại thời điểm này, hãy thoát ra
            if (col == null)
            {
                return;
            }
            // ===================================

            // Nếu code chạy đến đây, 'col' chắc chắn không null
            if (e.ColumnIndex == col.Index && e.RowIndex >= 0)
            {
                dgv_Word.EndEdit();
            }
        }
    }
}
