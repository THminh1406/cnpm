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
using System.Text.RegularExpressions;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Game_SentenceScramble : UserControl
    {

        // === 1. KHAI BÁO BIẾN VÀ BLL ===
        private Business_Logic_Quizzes bll_Quizzes;
        private Business_Logic_Vocabulary bll_Vocabulary;

        // Dữ liệu game
        private List<VocabularyDTO> gameSentences; // Danh sách các CÂU
        private int currentSentenceIndex = 0; // Đang ở câu thứ mấy
        private string currentCorrectAnswer = ""; // Câu trả lời đúng cho câu hiện tại
        private int currentQuizId = 0; // Để chơi lại

        // Dùng để xáo trộn
        private static Random rng = new Random();
        public uc_Game_SentenceScramble()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.uc_Game_SentenceScramble_Load);
        }

        private void uc_Game_SentenceScramble_Load(object sender, EventArgs e)
        {
            // Kiểm tra DesignMode (ngăn crash)
            if (this.DesignMode) return;

            // Khởi tạo BLL
            bll_Quizzes = new Business_Logic_Quizzes();
            bll_Vocabulary = new Business_Logic_Vocabulary();

            // Gán sự kiện click cho các nút (Bạn cũng có thể gán trong Designer)
            //btn_CheckAnswer.Click += btn_CheckAnswer_Click;
            //btn_ResetGame.Click += btn_ResetGame_Click;
        }

        public void LoadGame(int quizId)
        {
            this.currentQuizId = quizId;
            lbl_Feedback.Text = "";

            // 1. Lấy dữ liệu (Tất cả các CÂU của game này)
            try
            {
                List<int> vocabIds = bll_Quizzes.GetQuizContentIds(quizId);
                if (vocabIds == null || vocabIds.Count == 0)
                {
                    lbl_Feedback.Text = "Lỗi: Game này không có nội dung.";
                    return;
                }
                // (BLL đã được sửa ở Bước 3 để chỉ trả về 'Sentence')
                this.gameSentences = bll_Vocabulary.GetVocabularyByIds(vocabIds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu game: " + ex.Message);
                return;
            }

            // 2. Tải CÂU ĐẦU TIÊN
            this.currentSentenceIndex = 0;
            LoadSingleSentence(this.currentSentenceIndex);
        }

        // === 4. LOGIC TẢI TỪNG CÂU (HÀM MỚI) ===
        private void LoadSingleSentence(int index)
        {
            // 1. Xóa sạch 2 panel
            flp_Answer.Controls.Clear();
            flp_Words.Controls.Clear();
            lbl_Feedback.Text = "";

            // 2. Kiểm tra xem đã hết câu chưa
            if (index >= gameSentences.Count)
            {
                lbl_Feedback.Text = "Chúc mừng! Bạn đã hoàn thành tất cả các câu!";
                lbl_Feedback.ForeColor = Color.Green;
                btn_CheckAnswer.Enabled = false;
                return;
            }

            // 3. Lấy câu gốc (VÍ DỤ: "  This is  an apple ")
            string rawText = gameSentences[index].WordText;

            // 4. === SỬA LỖI TẠI ĐÂY: "LÀM SẠCH" CÂU ===

            // a. Bỏ tất cả dấu câu (giữ lại chữ, số, và dấu cách)
            string cleanedText = Regex.Replace(rawText, @"[^\p{L}\p{N}\s]", "");

            // b. Bỏ dấu cách thừa ở đầu/cuối
            cleanedText = cleanedText.Trim();

            // c. Thay thế 2+ dấu cách ở giữa bằng 1 dấu cách
            // (BIẾN: "This is  an apple" THÀNH: "This is an apple")
            cleanedText = Regex.Replace(cleanedText, @"\s+", " ");

            // d. Gán câu trả lời đúng (đã được làm sạch)
            this.currentCorrectAnswer = cleanedText;
            // ============================================

            // 5. Tách câu đã làm sạch
            string[] words = this.currentCorrectAnswer.Split(' ');

            // 6. Xáo trộn các từ
            Shuffle(words);

            // 7. Tạo Chip cho mỗi từ
            foreach (string word in words)
            {
                Guna2Chip chip = new Guna2Chip();
                chip.Text = word;
                chip.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                chip.FillColor = Color.Gainsboro;
                chip.ForeColor = Color.Black;
                chip.AutoSize = true;
                chip.Margin = new Padding(5);
                chip.Cursor = Cursors.Hand;

                chip.Click += Chip_Click_ToAnswer;

                flp_Words.Controls.Add(chip);
            }
        }

        // === 5. LOGIC DI CHUYỂN CHIP ===

        // Khi click chip ở "Ngân hàng" (flp_Words)
        private void Chip_Click_ToAnswer(object sender, EventArgs e)
        {
            var chip = (Guna2Chip)sender;

            // 1. Xóa khỏi Ngân hàng
            flp_Words.Controls.Remove(chip);
            // 2. Thêm vào ô Trả lời
            flp_Answer.Controls.Add(chip);

            // 3. Đổi màu + Đổi sự kiện
            chip.FillColor = Color.DodgerBlue;
            chip.ForeColor = Color.White;
            chip.Click -= Chip_Click_ToAnswer; // Xóa sự kiện cũ
            chip.Click += Chip_Click_ToBank;   // Thêm sự kiện mới (trả về)
        }

        // Khi click chip ở ô "Trả lời" (flp_Answer)
        private void Chip_Click_ToBank(object sender, EventArgs e)
        {
            var chip = (Guna2Chip)sender;

            // 1. Xóa khỏi ô Trả lời
            flp_Answer.Controls.Remove(chip);
            // 2. Thêm vào Ngân hàng
            flp_Words.Controls.Add(chip);

            // 3. Đổi màu + Đổi sự kiện
            chip.FillColor = Color.Gainsboro;
            chip.ForeColor = Color.Black;
            chip.Click -= Chip_Click_ToBank;   // Xóa sự kiện cũ
            chip.Click += Chip_Click_ToAnswer; // Thêm sự kiện mới (chuyển lên)
        }

        private void ReturnAllChipsToBank()
        {
            // Dùng .ToList() để tạo 1 bản sao,
            // vì không thể sửa 1 collection (xóa) khi đang lặp (foreach)
            var chipsToReturn = flp_Answer.Controls.OfType<Guna2Chip>().ToList();

            foreach (var chip in chipsToReturn)
            {
                // 1. Xóa khỏi ô Trả lời
                flp_Answer.Controls.Remove(chip);
                // 2. Thêm vào Ngân hàng
                flp_Words.Controls.Add(chip);

                // 3. Đổi màu + Đổi sự kiện
                chip.FillColor = Color.Gainsboro;
                chip.ForeColor = Color.Black;
                chip.Click -= Chip_Click_ToBank;   // Xóa sự kiện cũ
                chip.Click += Chip_Click_ToAnswer; // Thêm sự kiện mới (chuyển lên)
            }
        }

        private void btn_CheckAnswer_Click(object sender, EventArgs e)
        {
            // 1. Lấy tất cả Text từ các chip trong ô Trả lời
            List<string> answerWords = flp_Answer.Controls.OfType<Guna2Chip>()
                                                    .Select(c => c.Text)
                                                    .ToList();

            // 2. Ghép chúng lại thành 1 câu (sạch)
            string userAnswer = string.Join(" ", answerWords).Trim();

            // 3. So sánh (không phân biệt hoa/thường)
            if (userAnswer.Equals(this.currentCorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                // === ĐÚNG ===
                lbl_Feedback.Text = "Chính xác!";
                lbl_Feedback.ForeColor = Color.Green;

                // Tải câu tiếp theo
                this.currentSentenceIndex++;
                LoadSingleSentence(this.currentSentenceIndex);
            }
            else
            {
                // === SAI ===

                lbl_Feedback.Text = "Sai rồi! Hãy sắp xếp lại."; // Giữ feedback ngắn

                lbl_Feedback.ForeColor = Color.Red;
                ReturnAllChipsToBank(); // Gọi hàm trả các chip về
            }
        }

        private void btn_ResetGame_Click(object sender, EventArgs e)
        {
            // Chơi lại từ đầu (tải lại toàn bộ game)
            if (this.currentQuizId > 0)
            {
                btn_CheckAnswer.Enabled = true;
                LoadGame(this.currentQuizId);
            }
        }

        private void Shuffle(string[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }
    }
}
