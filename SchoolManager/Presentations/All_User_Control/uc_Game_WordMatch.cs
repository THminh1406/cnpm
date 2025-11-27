using Guna.UI2.WinForms;
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
    public partial class uc_Game_WordMatch : UserControl
    {
        // === KHAI BÁO BLL (KHÔNG KHỞI TẠO) ===
        private Business_Logic_Quizzes bll_Quizzes;
        private Business_Logic_Vocabulary bll_Vocabulary;

        // ... (các biến private khác của bạn như gameData, score...)
        private List<VocabularyDTO> gameData;
        private int currentQuizId;
        private int score;
        private int totalPairs;
        private Guna2Panel selectedPanel = null;
        private Guna2Button selectedWord = null;

        public uc_Game_WordMatch()
        {
            InitializeComponent();

            // Thêm hàm này vào Constructor (nếu Designer chưa tự thêm)
            this.Load += new System.EventHandler(this.uc_Game_WordMatch_Load);
        }

        // === THÊM HÀM NÀY VÀO ===
        private void uc_Game_WordMatch_Load(object sender, EventArgs e)
        {
            // 1. KIỂM TRA DESIGN MODE
            if (this.DesignMode)
            {
                return; // Dừng lại, không chạy code CSDL
            }

            // 2. KHỞI TẠO BLL (An toàn)
            bll_Quizzes = new Business_Logic_Quizzes();
            bll_Vocabulary = new Business_Logic_Vocabulary();
        }
        // ===================================

        // === HÀM CHÍNH ĐỂ BẮT ĐẦU GAME ===
        // (Đã dọn dẹp, bỏ code khởi tạo BLL thừa)
        public void LoadGame(int quizId)
        {

            this.currentQuizId = quizId;
            this.score = 0;
            lbl_Feedback.Text = "Hãy chọn 1 hình và 1 chữ tương ứng!";
            lbl_Feedback.ForeColor = Color.Black;
            ClearSelection();

            // 1. Lấy dữ liệu
            try
            {
                // A. Lấy danh sách ID nội dung từ BLL
                List<int> vocabIds = bll_Quizzes.GetQuizContentIds(quizId);
                if (vocabIds == null || vocabIds.Count == 0)
                {
                    lbl_Feedback.Text = "Lỗi: Game này không có nội dung.";
                    return;
                }

                // B. Lấy dữ liệu Hình/Chữ từ các ID đó
                this.gameData = bll_Vocabulary.GetVocabularyByIds(vocabIds);
                this.totalPairs = gameData.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu game: " + ex.Message);
                return;
            }

            // 2. Cập nhật điểm
            UpdateScore();

            // 3. Vẽ game lên màn hình
            PopulateGameboard();
        }

        /// <summary>
        /// Tạo và vẽ các control Hình/Chữ lên 2 FlowLayoutPanel
        /// </summary>
        private void PopulateGameboard()
        {
            // 1. Xóa sạch các control cũ
            flp_Images.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();

            // 2. Trộn ngẫu nhiên danh sách
            var random = new Random();
            var shuffledImages = gameData.OrderBy(x => random.Next()).ToList();
            var shuffledWords = gameData.OrderBy(x => random.Next()).ToList();

            // 3. VẼ CỘT HÌNH ẢNH (GIỮ NGUYÊN LOGIC, CHỈNH LẠI SIZE CHO CHUẨN)
            foreach (var vocab in shuffledImages)
            {
                // Tạo Ảnh (Bên trong)
                Guna2PictureBox pic = new Guna2PictureBox();
                pic.Image = ConvertByteArrayToImage(vocab.WordImage);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.FillColor = Color.Transparent;
                pic.BorderRadius = 10;
                pic.Dock = DockStyle.Fill;
                pic.Cursor = Cursors.Hand;
                pic.Click += Image_Click_Passthrough; // Sự kiện click xuyên qua

                // Tạo Panel Khung (Bên ngoài)
                Guna2Panel panel = new Guna2Panel();
                panel.Size = new Size(200, 200); // Kích thước vuông vức
                panel.BorderRadius = 10;
                panel.Margin = new Padding(10);  // Khoảng cách giữa các ô
                panel.Cursor = Cursors.Hand;
                panel.Tag = vocab.id_vocabulary; // Lưu ID

                // Màu sắc viền
                panel.FillColor = Color.Black;   // <--- ĐỔI THÀNH MÀU ĐEN (Viền ban đầu)
                panel.Padding = new Padding(1);  // Độ dày viền (4px)

                // Gắn control vào nhau
                panel.Controls.Add(pic);
                flp_Images.Controls.Add(panel);

                // Gắn sự kiện
                panel.Click += Image_Click;
            }

            // 4. VẼ CỘT CHỮ (DÙNG GUNA2BUTTON THAY CHO CHIP)
            foreach (var vocab in shuffledWords)
            {
                Guna2Button btnWord = new Guna2Button();

                // Nội dung & Font chữ
                btnWord.Text = vocab.WordText;
                // Dùng font size 10 hoặc 11 để chữ dài không bị mất
                btnWord.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

                // Màu sắc mặc định
                btnWord.FillColor = Color.Gainsboro; // Màu xám nhạt
                btnWord.ForeColor = Color.Black;     // Chữ đen
                btnWord.BorderRadius = 10;           // Bo góc giống hình

                // Kích thước: Rộng bằng hình (150), Cao đủ chứa 2 dòng chữ (60)
                btnWord.Size = new Size(200, 100);
                btnWord.Margin = new Padding(10);    // Khoảng cách đều nhau

                // Lưu dữ liệu & Sự kiện
                btnWord.Cursor = Cursors.Hand;
                btnWord.Tag = vocab.id_vocabulary;   // Lưu ID để so sánh
                btnWord.Click += Word_Click;         // Gắn sự kiện Click

                // Thêm vào panel chữ
                flowLayoutPanel1.Controls.Add(btnWord);
            }
        }

        // === THÊM HÀM MỚI NÀY ===
        // Khi click vào Ảnh, nó sẽ "báo" cho Panel cha
        private void Image_Click_Passthrough(object sender, EventArgs e)
        {
            // 'sender' là PictureBox, 'sender.Parent' là Panel
            Image_Click(((Control)sender).Parent, e);
        }

        private void Image_Click(object sender, EventArgs e)
        {
            // Bỏ chọn panel cũ (trả khung về màu trắng)
            if (selectedPanel != null)
            {
                selectedPanel.FillColor = Color.Black;
            }

            // Chọn panel mới (sender là Guna2Panel)
            selectedPanel = (Guna2Panel)sender;
            selectedPanel.FillColor = Color.DodgerBlue; // Đổi màu khung thành xanh

            lbl_Feedback.Text = "";
        }

        private void Word_Click(object sender, EventArgs e)
        {
            // 1. Reset màu ô cũ (nếu đang chọn ô nào đó trước đây)
            if (selectedWord != null)
            {
                selectedWord.FillColor = Color.Gainsboro; // Trả về màu xám
                selectedWord.ForeColor = Color.Black;
            }

            // 2. Gán ô mới được chọn (Ép kiểu sang Guna2Button)
            selectedWord = (Guna2Button)sender;

            // 3. Đổi màu ô mới để đánh dấu
            selectedWord.FillColor = Color.DodgerBlue; // Nền xanh dương
            selectedWord.ForeColor = Color.White;      // Chữ trắng

            // Xóa thông báo cũ
            lbl_Feedback.Text = "";
        }

        // (Trong file uc_Game_WordMatch.cs)

        // (Bạn phải gán sự kiện này cho btn_CheckAnswer trong Designer)
        private void btn_CheckAnswer_Click(object sender, EventArgs e)
        {
            // Sửa ở đây
            if (selectedPanel == null || selectedWord == null)
            {
                lbl_Feedback.Text = "Bạn phải chọn 1 hình VÀ 1 chữ!";
                lbl_Feedback.ForeColor = Color.Red;
                return;
            }

            int imageId = (int)selectedPanel.Tag; // Sửa ở đây
            int wordId = (int)selectedWord.Tag;

            if (imageId == wordId)
            {
                AudioHelper.PlayCorrect();
                lbl_Feedback.Text = "Chính xác!";
                lbl_Feedback.ForeColor = Color.Green;

                // Sửa ở đây
                selectedPanel.Visible = false; // Làm biến mất Panel
                selectedWord.Visible = false;

                score++;
                UpdateScore();

                if (score == this.totalPairs)
                {
                    lbl_Feedback.Text = "Chúc mừng! Bạn đã hoàn thành!";
                    lbl_Feedback.ForeColor = Color.Blue;
                }
            }
            else
            {
                AudioHelper.PlayWrong();
                lbl_Feedback.Text = "Sai rồi! Hãy thử lại.";
                lbl_Feedback.ForeColor = Color.Red;
            }

            ClearSelection();
        }

        // (Lưu ý: Bạn phải gán sự kiện này cho btn_ResetGame trong Designer)
        private void btn_ResetGame_Click(object sender, EventArgs e)
        {
            // Tải lại game với id cũ
            if (this.currentQuizId > 0)
            {
                LoadGame(this.currentQuizId);
            }
        }


        // Hủy chọn (reset viền/màu)
        private void ClearSelection()
        {
            // 1. Reset ô HÌNH (về viền trắng)
            if (selectedPanel != null)
            {
                selectedPanel.FillColor = Color.Black;
            }

            // 2. Reset ô CHỮ (về màu xám)
            if (selectedWord != null)
            {
                selectedWord.FillColor = Color.Gainsboro;
                selectedWord.ForeColor = Color.Black;
            }

            // 3. Xóa biến lưu trữ
            selectedPanel = null;
            selectedWord = null;
        }

        // Cập nhật Label điểm
        private void UpdateScore()
        {
            lbl_Score.Text = $"Đã ghép: {score} / {this.totalPairs}";
        }

        // Chuyển byte[] (từ CSDL) sang Image
        private Image ConvertByteArrayToImage(byte[] data)
        {
            if (data == null || data.Length == 0) return null;
            try
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    return Image.FromStream(ms);
                }
            }
            catch
            {
                return null; // Trả về null nếu dữ liệu ảnh bị lỗi
            }
        }
    }
}