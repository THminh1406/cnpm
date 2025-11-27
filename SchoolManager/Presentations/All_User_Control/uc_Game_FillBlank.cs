// === BẮT ĐẦU COPY TỪ ĐÂY ===
using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions; // Dùng cho Regex
using System.Threading.Tasks;       // Dùng cho async/await
using System.Windows.Forms;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Game_FillBlank : UserControl
    {
        // === 1. KHAI BÁO BIẾN VÀ BLL ===
        private Business_Logic_Quizzes bll_Quizzes;
        private Business_Logic_Vocabulary bll_Vocabulary;

        // DÙNG DANH SÁCH "SẠCH" (ĐÃ LỌC)
        private List<ValidQuestion> processedQuestions;

        private int currentQuestionIndex = 0; // Đang ở câu thứ mấy
        private string currentCorrectAnswer = ""; // Đáp án đúng cho câu hiện tại
        private int currentQuizId = 0;

        public uc_Game_FillBlank()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.uc_Game_FillBlank_Load);
        }

        // === 2. HÀM LOAD (KHỞI TẠO) ===
        private void uc_Game_FillBlank_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            bll_Quizzes = new Business_Logic_Quizzes();
            bll_Vocabulary = new Business_Logic_Vocabulary();

            // *** LƯU Ý QUAN TRỌNG ***
            // Hãy đảm bảo bạn đã kết nối các nút này TRONG DESIGNER
            // (Không gán sự kiện .Click += ở đây nữa để tránh lỗi lặp)
            // btn_CheckAnswer.Click += btn_CheckAnswer_Click;
            // btn_ResetGame.Click += btn_ResetGame_Click;

            // Thêm sự kiện KeyDown cho TextBox để bấm Enter
            txt_Answer.KeyDown += txt_Answer_KeyDown;
        }

        // === 3. HÀM LOADGAME (ĐÃ "LÀM SẠCH") ===
        public void LoadGame(int quizId)
        {
            this.currentQuizId = quizId;
            lbl_Feedback.Text = "";
            btn_CheckAnswer.Enabled = true;

            // 1. Lấy dữ liệu DTO thô từ CSDL
            List<VocabularyDTO> rawGameQuestions;
            try
            {
                List<int> vocabIds = bll_Quizzes.GetQuizContentIds(quizId);
                if (vocabIds == null || vocabIds.Count == 0)
                {
                    lbl_Feedback.Text = "Lỗi: Game này không có nội dung.";
                    return;
                }
                rawGameQuestions = bll_Vocabulary.GetVocabularyByIds(vocabIds);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu game: " + ex.Message);
                return;
            }

            // 2. === LOGIC LỌC BỎ CÂU LỖI ===
            // (Đây là bước quan trọng nhất)
            this.processedQuestions = new List<ValidQuestion>();
            foreach (var dto in rawGameQuestions)
            {
                string rawText = dto.WordText;
                Match match = Regex.Match(rawText, @"\[(.*?)\]");

                // Nếu câu DTO hợp lệ (có [ ])
                if (match.Success)
                {
                    // Tách Câu hỏi và Đáp án, rồi thêm vào danh sách "sạch"
                    this.processedQuestions.Add(new ValidQuestion
                    {
                        Answer = match.Groups[1].Value.Trim(),
                        QuestionTemplate = rawText.Replace(match.Value, "_______")
                    });
                }
                // (Nếu câu DTO không hợp lệ (lỗi), chúng ta bỏ qua nó)
            }

            // (Kiểm tra debug)
            // MessageBox.Show($"Debug: Đã lọc. Số câu hợp lệ: {processedQuestions.Count} / {rawGameQuestions.Count}");

            // 3. Tải CÂU HỎI ĐẦU TIÊN
            this.currentQuestionIndex = 0;
            LoadSingleQuestion(this.currentQuestionIndex);
        }

        // === 4. LOGIC TẢI TỪNG CÂU HỎI (ĐÃ SỬA) ===
        // (Hàm này không còn khối 'else' gây lỗi)
        private void LoadSingleQuestion(int index)
        {
            // 1. Kiểm tra xem đã hết câu chưa (dùng danh sách "sạch")
            if (index >= processedQuestions.Count)
            {
                lbl_Feedback.Text = "Chúc mừng! Bạn đã hoàn thành!";
                lbl_Feedback.ForeColor = Color.Green;
                btn_CheckAnswer.Enabled = false;
                lbl_Question.Text = "";
                txt_Answer.Visible = false;
                return;
            }

            // 2. Lấy câu hỏi (đã được làm sạch)
            ValidQuestion q = processedQuestions[index];

            // 3. Gán đáp án và câu hỏi
            this.currentCorrectAnswer = q.Answer;
            lbl_Question.Text = q.QuestionTemplate;

            // 4. Dọn dẹp
            lbl_Feedback.Text = "";
            txt_Answer.Text = "";
            txt_Answer.Visible = true;
            txt_Answer.Focus();
        }

        // === 5. LOGIC KIỂM TRA (async) ===
        private async void btn_CheckAnswer_Click(object sender, EventArgs e)
        {
            string userAnswer = txt_Answer.Text.Trim();

            // 1. Kiểm tra
            if (userAnswer.Equals(this.currentCorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                // === ĐÚNG ===
                AudioHelper.PlayCorrect();
                lbl_Feedback.Text = "Chính xác!";
                lbl_Feedback.ForeColor = Color.Green;
                btn_CheckAnswer.Enabled = false;

                // 2. Chờ 1 giây
                await Task.Delay(2000);

                // 3. Tải câu tiếp theo
                this.currentQuestionIndex++;
                LoadSingleQuestion(this.currentQuestionIndex);
                btn_CheckAnswer.Enabled = true;
            }
            else
            {
                // === SAI ===
                AudioHelper.PlayWrong();
                lbl_Feedback.Text = "Sai rồi! Hãy thử lại.";
                lbl_Feedback.ForeColor = Color.Red;

                // === THÊM DÒNG NÀY VÀO ===
                txt_Answer.Text = ""; // Xóa ô nhập
                txt_Answer.Focus();   // Tự động trỏ chuột vào ô nhập
                                      // ========================
            }
        }

        // === 6. LOGIC CHƠI LẠI ===
        private void btn_ResetGame_Click(object sender, EventArgs e)
        {
            if (this.currentQuizId > 0)
            {
                LoadGame(this.currentQuizId);
            }
        }

        // === 7. HÀM HỖ TRỢ (BẤM ENTER) ===
        private void txt_Answer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_CheckAnswer_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    } // <-- Dấu } kết thúc lớp uc_Game_FillBlank

    // === LỚP HELPER (ĐỂ LỌC CÂU HỎI) ===
    internal class ValidQuestion
    {
        public string QuestionTemplate { get; set; } // Ví dụ: "Tôi ____ ở Việt Nam"
        public string Answer { get; set; }           // Ví dụ: "sống"
    }

} // <-- Dấu } kết thúc namespace
  // === KẾT THÚC COPY TẠI ĐÂY ===