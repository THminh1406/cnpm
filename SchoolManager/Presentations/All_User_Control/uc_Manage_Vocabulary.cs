using Guna.UI2.WinForms;
using Microsoft.VisualBasic;
using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Manage_Vocabulary : UserControl
    {

        // === KHAI BÁO BLL ===
        private Business_Logic_Categories bll_Categories;
        private Business_Logic_Vocabulary bll_Vocabulary;

        private byte[] currentImageData = null; // Biến tạm giữ ảnh
        private int selectedVocabId = 0;
        public uc_Manage_Vocabulary()
        {
            InitializeComponent();
        }

        private void uc_Manage_Vocabulary_Load(object sender, EventArgs e)
        {

            if (this.DesignMode)
            {
                return; // Dừng lại! Không chạy code CSDL khi đang ở chế độ thiết kế.
            }
            bll_Categories = new Business_Logic_Categories();
            bll_Vocabulary = new Business_Logic_Vocabulary();
            // Tải danh sách chủ đề cho cả 2 ComboBox
            LoadCategories(this.cbo_Category, false); // ComboBox để thêm (không cần "Tất cả")
            LoadCategories(this.cbo_FilterCategory, true); // ComboBox để lọc (cần "Tất cả")

            // Tải danh sách từ vựng
            LoadVocabularyGrid();
        }

        // Tải danh sách chủ đề vào ComboBox
        private void LoadCategories(Guna2ComboBox cbo, bool addDefaultOption)
        {
            // === SỬA DÒNG NÀY ===
            var categories = bll_Categories.GetAllCategories(false); // Lấy tất cả
                                                                     // ==================

            if (addDefaultOption)
            {
                categories.Insert(0, new CategoryDTO { id_category = 0, category_name = "-- Tất cả chủ đề --" });
            }
            cbo.DisplayMember = "category_name";
            cbo.ValueMember = "id_category";
            cbo.DataSource = categories;
        }

        private void ReloadCategoriesAfterAdd(int newId)
        {
            LoadCategories(this.cbo_Category, false);
            // Dùng SelectedValue ở đây là OK, vì chúng ta TỰ GÁN 1 SỐ NGUYÊN
            cbo_Category.SelectedValue = newId;
        }

        // Tải tất cả từ vựng lên DataGridView (sẽ là 'guna2DataGridView1')
        private void LoadVocabularyGrid()
        {
            int filterCategoryId = 0;
            var selectedItem = cbo_FilterCategory.SelectedItem;
            if (selectedItem != null && selectedItem is CategoryDTO)
            {
                filterCategoryId = ((CategoryDTO)selectedItem).id_category;
            }
            string searchTerm = txt_SearchVocab.Text;

            guna2DataGridView1.DataSource = bll_Vocabulary.GetVocabulary(filterCategoryId, searchTerm);

            // Cấu hình cột
            if (guna2DataGridView1.Columns.Contains("WordImage"))
            {
                (guna2DataGridView1.Columns["WordImage"] as DataGridViewImageColumn).ImageLayout = DataGridViewImageCellLayout.Zoom;
                guna2DataGridView1.Columns["WordImage"].Width = 100;
            }
            guna2DataGridView1.RowTemplate.Height = 100;
            guna2DataGridView1.Columns["id_vocabulary"].Visible = false;

            // === THÊM DÒNG NÀY ===
            // (Ẩn cột VocabType, chúng ta chỉ cần nó cho code-behind)
            if (guna2DataGridView1.Columns.Contains("VocabType"))
            {
                guna2DataGridView1.Columns["VocabType"].Visible = false;
            }
        }

        // Nút Thêm Chủ đề (btn_AddNewCategory)
        private void btn_AddNewCategory_Click(object sender, EventArgs e)
        {
            string newCategoryName = Interaction.InputBox("Nhập tên chủ đề mới:", "Tạo Chủ Đề Mới", "");
            if (string.IsNullOrWhiteSpace(newCategoryName)) return;

            try
            {
                int newId = bll_Categories.SaveCategory(newCategoryName);
                if (newId > 0)
                {
                    ReloadCategoriesAfterAdd(newId);
                    LoadCategories(cbo_FilterCategory, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo chủ đề: " + ex.Message);
            }
        }

        // Nút Chọn ảnh (btn_SelectImage)
        private void btn_SelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pic_Preview.Image = new Bitmap(openFile.FileName);
                currentImageData = File.ReadAllBytes(openFile.FileName);
            }
        }

        // Nút Làm mới (btn_ClearForm)
        private void btn_ClearForm_Click(object sender, EventArgs e)
        {
            txt_WordText.Text = "";
            pic_Preview.Image = null;
            cbo_Category.SelectedIndex = -1;
            currentImageData = null;
            selectedVocabId = 0;
            btn_SaveVocab.Text = "Lưu vào Kho";
        }

        // Nút Lưu (btn_SaveVocab)
        private void btn_SaveVocab_Click(object sender, EventArgs e)
        {
            int selectedCategoryId = 0;
            var selectedItem = cbo_Category.SelectedItem;
            if (selectedItem != null && selectedItem is CategoryDTO)
            {
                selectedCategoryId = ((CategoryDTO)selectedItem).id_category;
            }

            // === LOGIC LẤY VocabType MỚI ===
            string vocabType = "Word"; // Mặc định
            if (cbo_VocabType.SelectedIndex == 1)
                vocabType = "Sentence";
            else if (cbo_VocabType.SelectedIndex == 2)
                vocabType = "FillBlank";
            // =================================

            // Kiểm tra chung (UI)
            if (string.IsNullOrWhiteSpace(txt_WordText.Text) || selectedCategoryId == 0)
            {
                MessageBox.Show("Vui lòng nhập Text và chọn Chủ đề.", "Thiếu thông tin");
                return;
            }

            if (vocabType == "Word" && currentImageData == null && selectedVocabId == 0)
            {
                MessageBox.Show("Loại 'Từ vựng' bắt buộc phải có ảnh.", "Thiếu ảnh");
                return;
            }
            else if (vocabType == "Sentence" || vocabType == "FillBlank")
            {
                currentImageData = null; // Đảm bảo không có ảnh
            }

            // Tạo DTO
            VocabularyDTO vocab = new VocabularyDTO
            {
                id_vocabulary = this.selectedVocabId,
                WordText = txt_WordText.Text,
                WordImage = currentImageData,
                id_category = selectedCategoryId,
                VocabType = vocabType
            };

            // Gọi BLL (đã bọc try...catch)
            try
            {
                bool success = bll_Vocabulary.SaveVocabulary(vocab);

                if (success)
                {
                    MessageBox.Show(selectedVocabId == 0 ? "Lưu thành công!" : "Cập nhật thành công!");

                    // (Code dọn dẹp form của bạn)
                    int currentCategoryId = selectedCategoryId;
                    txt_WordText.Text = "";
                    pic_Preview.Image = null;
                    currentImageData = null;
                    selectedVocabId = 0;
                    btn_SaveVocab.Text = "Lưu vào Kho";
                    LoadVocabularyGrid();
                    cbo_Category.SelectedValue = currentCategoryId;
                    // (Không reset cbo_VocabType)
                }
                else
                {
                    MessageBox.Show("Thao tác thất bại.");
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi validation từ BLL (ví dụ: lỗi thiếu "[ ]")
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Lọc theo chủ đề
        private void cbo_FilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadVocabularyGrid();
        }

        // Lọc theo text
        private void txt_SearchVocab_TextChanged(object sender, EventArgs e)
        {
            LoadVocabularyGrid();
        }

        // Click vào lưới (để Sửa)
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra nếu click vào tiêu đề hoặc dòng lỗi
            if (e.RowIndex < 0) return;

            // 2. Lấy ID của từ vựng tại dòng đó
            DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
            object idValue = row.Cells["id_vocabulary"].Value;

            if (idValue == null || idValue == DBNull.Value) return;
            int vocabId = Convert.ToInt32(idValue);

            // 3. Kiểm tra đúng cột thao tác chưa 
            // (Lưu ý: Giữ nguyên tên "edit" nếu trong Design bạn đặt tên cột là "edit", 
            // dù bây giờ chức năng là xóa)
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                // === PHẦN XỬ LÝ XÓA ===

                // Hỏi xác nhận trước khi xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa từ vựng: " + row.Cells["Word"].Value.ToString() + " không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Gọi BLL để xóa (Bạn cần đảm bảo BLL đã có hàm DeleteVocabulary)
                        bool isDeleted = bll_Vocabulary.DeleteVocabulary(vocabId);

                        if (isDeleted)
                        {
                            MessageBox.Show("Đã xóa thành công!");

                            // Tải lại lưới để cập nhật danh sách
                            LoadVocabularyGrid();

                            // Nếu đang chọn dòng đó để sửa thì clear form đi
                            if (selectedVocabId == vocabId)
                            {
                                btn_ClearForm_Click(null, null);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại. Vui lòng thử lại.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                    }
                }
            }
        }

        private void cbo_VocabType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chọn "Từ vựng (Hình + Chữ)" (index 0)
            if (cbo_VocabType.SelectedIndex == 0)
            {
                // Hiện ô ảnh và nút
                pic_Preview.Visible = true;
                btn_SelectImage.Visible = true;
            }
            // Nếu chọn "Câu (Sắp xếp)" (index 1) HOẶC "Điền từ" (index 2)
            else
            {
                // Ẩn ô ảnh và nút
                pic_Preview.Visible = false;
                btn_SelectImage.Visible = false;

                // Xóa ảnh (nếu lỡ chọn)
                pic_Preview.Image = null;
                currentImageData = null;
            }
        }

        private void guna2DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Bỏ qua Header
            if (e.RowIndex < 0) return;

            // Kiểm tra đúng cột "delete" (hoặc "edit" nếu bạn chưa đổi tên)
            if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                // 1. Vẽ nền mặc định của ô (trừ nội dung)
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // 2. Tạo khung hình chữ nhật cho nút (giữ nguyên vị trí cũ)
                var btnRect = new Rectangle(
                    e.CellBounds.X + 15,
                    e.CellBounds.Y + 35,
                    e.CellBounds.Width - 30,
                    50
                );

                // 3. Vẽ nền nút màu ĐỎ (Bo tròn)
                // Nếu bạn chỉ muốn hiện mỗi cái ảnh không cần nền đỏ, hãy xóa đoạn 'using' này đi
                using (var brush = new SolidBrush(Color.FromArgb(220, 53, 69)))
                using (var path = GetRoundedRectPath(btnRect, 15))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // 4. VẼ HÌNH ẢNH (Thay thế cho vẽ chữ)
                Image img = Properties.Resources.delete; // Lấy ảnh từ Resource

                if (img != null)
                {
                    // Thiết lập kích thước icon muốn vẽ (ví dụ 24x24 hoặc 30x30)
                    int iconSize = 24;

                    // Tính toán vị trí để icon nằm CHÍNH GIỮA nút
                    int x = btnRect.X + (btnRect.Width - iconSize) / 2;
                    int y = btnRect.Y + (btnRect.Height - iconSize) / 2;

                    // Vẽ ảnh
                    e.Graphics.DrawImage(img, new Rectangle(x, y, iconSize, iconSize));
                }

                // 5. Báo hiệu đã vẽ xong
                e.Handled = true;
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            if (radius == 0) { path.AddRectangle(bounds); return path; }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}