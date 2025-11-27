using Guna.UI2.WinForms;
using SchoolManager.BLL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Game_MemoryMatch : UserControl
    {
        private Business_Logic_Quizzes bll_Quizzes;
        private Business_Logic_Vocabulary bll_Vocabulary;

        private List<MemoryCard> gameCards;
        private MemoryCard firstCard = null;
        private MemoryCard secondCard = null;

        private int currentQuizId = 0;
        private int pairsFound = 0;
        private int totalPairs = 0;

        private TableLayoutPanel tbl_GameBoard;
        private static Random rng = new Random();

        public uc_Game_MemoryMatch()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.uc_Game_MemoryMatch_Load);
        }

        private void uc_Game_MemoryMatch_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            bll_Quizzes = new Business_Logic_Quizzes();
            bll_Vocabulary = new Business_Logic_Vocabulary();

            btn_ResetGame.Click += btn_ResetGame_Click;
        }

        public void LoadGame(int quizId)
        {
            this.currentQuizId = quizId;
            this.pairsFound = 0;
            lbl_Feedback.Text = "";

            firstCard = null;
            secondCard = null;

            // 1. Lấy dữ liệu
            List<VocabularyDTO> gameData;
            try
            {
                List<int> vocabIds = bll_Quizzes.GetQuizContentIds(quizId);
                gameData = bll_Vocabulary.GetVocabularyByIds(vocabIds);
                this.totalPairs = gameData.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu game: " + ex.Message);
                return;
            }

            UpdateScore();

            // 2. Tạo danh sách thẻ
            this.gameCards = new List<MemoryCard>();
            foreach (var vocab in gameData)
            {
                gameCards.Add(new MemoryCard { VocabId = vocab.id_vocabulary, IsImage = true, Data = vocab });
                gameCards.Add(new MemoryCard { VocabId = vocab.id_vocabulary, IsImage = false, Data = vocab });
            }

            Shuffle(gameCards);

            int rows, cols;
            DetermineGridSize(this.totalPairs, out rows, out cols);

            CreateGameBoard(rows, cols);
            PopulateGrid();
        }

        // === TẠO GIAO DIỆN ===
        private void DetermineGridSize(int pairCount, out int rows, out int cols)
        {
            if (pairCount <= 6) { rows = 3; cols = 4; }
            else if (pairCount <= 8) { rows = 4; cols = 4; }
            else { rows = 4; cols = 5; }
        }

        private void CreateGameBoard(int rows, int cols)
        {
            if (tbl_GameBoard != null)
            {
                pnl_GameArea.Controls.Remove(tbl_GameBoard);
                tbl_GameBoard.Dispose();
            }

            tbl_GameBoard = new TableLayoutPanel();
            tbl_GameBoard.Dock = DockStyle.Fill;
            tbl_GameBoard.BackColor = Color.Transparent;
            tbl_GameBoard.RowCount = rows;
            tbl_GameBoard.ColumnCount = cols;

            for (int i = 0; i < rows; i++) tbl_GameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            for (int i = 0; i < cols; i++) tbl_GameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));

            pnl_GameArea.Controls.Add(tbl_GameBoard);
        }

        private void PopulateGrid()
        {
            int cardIndex = 0;
            for (int r = 0; r < tbl_GameBoard.RowCount; r++)
            {
                for (int c = 0; c < tbl_GameBoard.ColumnCount; c++)
                {
                    if (cardIndex < gameCards.Count)
                    {
                        var card = gameCards[cardIndex];

                        Guna2Button btn = new Guna2Button();
                        btn.Dock = DockStyle.Fill;
                        btn.BorderRadius = 12;
                        btn.Margin = new Padding(6);

                        // Cấu hình mặc định (Mặt úp)
                        btn.FillColor = Color.DodgerBlue;
                        btn.Text = "?";
                        btn.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
                        btn.ForeColor = Color.White;

                        // Quan trọng: Zoom để ảnh không bị méo
                        btn.BackgroundImageLayout = ImageLayout.Zoom;

                        // Quan trọng: Viền để khi khớp đúng sẽ đổi màu viền
                        btn.BorderThickness = 3;
                        btn.BorderColor = Color.DodgerBlue;

                        btn.Tag = card;
                        btn.Click += Card_Click;

                        card.CardButton = btn;
                        tbl_GameBoard.Controls.Add(btn, c, r);
                        cardIndex++;
                    }
                }
            }
        }

        // === LOGIC CLICK & XỬ LÝ TRẠNG THÁI ===
        private async void Card_Click(object sender, EventArgs e)
        {
            if (secondCard != null) return;

            var clickedButton = (Guna2Button)sender;
            var clickedCard = (MemoryCard)clickedButton.Tag;

            if (clickedCard.IsMatched || clickedCard.IsRevealed) return;

            // --- BƯỚC 1: LẬT THẺ ---
            clickedCard.IsRevealed = true;
            FlipCardUI(clickedCard, true); // Hiện nội dung

            if (firstCard == null)
            {
                firstCard = clickedCard;
            }
            else
            {
                secondCard = clickedCard;

                // --- BƯỚC 2: SO SÁNH ---
                if (firstCard.VocabId == secondCard.VocabId)
                {
                    // === ĐÚNG ===
                    AudioHelper.PlayCorrect();

                    firstCard.IsMatched = true;
                    secondCard.IsMatched = true;

                    // === SỬA LỖI MẤT HÌNH KHI ĐÚNG ===
                    // Thay vì đổi màu nền (FillColor), ta đổi màu VIỀN (BorderColor)
                    // Để giữ nguyên ảnh đang hiển thị
                    SetMatchedStyle(firstCard);
                    SetMatchedStyle(secondCard);

                    pairsFound++;
                    UpdateScore();

                    firstCard = null;
                    secondCard = null;

                    if (pairsFound == totalPairs)
                    {
                        await Task.Delay(500);
                        MessageBox.Show("Chúc mừng! Bạn đã chiến thắng!", "Thông báo");
                    }
                }
                else
                {
                    // === SAI ===

                    // Báo sai bằng viền đỏ (Tùy chọn)
                    firstCard.CardButton.BorderColor = Color.Salmon;
                    secondCard.CardButton.BorderColor = Color.Salmon;

                    await Task.Delay(1000); // Chờ 1 giây

                    // Úp lại
                    firstCard.IsRevealed = false;
                    secondCard.IsRevealed = false;

                    FlipCardUI(firstCard, false); // Úp xuống
                    FlipCardUI(secondCard, false); // Úp xuống

                    firstCard = null;
                    secondCard = null;
                }
            }
        }

        // === HÀM LẬT THẺ QUAN TRỌNG (ĐÃ SỬA LỖI HIỂN THỊ) ===
        private void FlipCardUI(MemoryCard card, bool showContent)
        {
            if (showContent) // TRẠNG THÁI NGỬA (Hiện nội dung)
            {
                card.CardButton.Text = "";
                card.CardButton.BorderColor = Color.Gold; // Viền vàng khi đang chọn

                if (card.IsImage)
                {
                    // --- THẺ HÌNH ---
                    // 1. Nền PHẢI LÀ Transparent để không che ảnh
                    card.CardButton.FillColor = Color.Transparent;
                    // 2. Gán ảnh vào BackgroundImage
                    card.CardButton.BackgroundImage = ConvertByteArrayToImage(card.Data.WordImage);
                }
                else
                {
                    // --- THẺ CHỮ ---
                    // 1. Nền màu Cam (hoặc màu sáng tùy ý)
                    card.CardButton.FillColor = Color.Orange;
                    card.CardButton.BackgroundImage = null;
                    // 2. Hiện chữ
                    card.CardButton.Text = card.Data.WordText;
                    card.CardButton.ForeColor = Color.Black; // Chữ đen cho dễ đọc
                    card.CardButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                }
            }
            else // TRẠNG THÁI ÚP (Mặt sau)
            {
                card.CardButton.BackgroundImage = null;
                card.CardButton.FillColor = Color.DodgerBlue; // Màu xanh úp thẻ
                card.CardButton.BorderColor = Color.DodgerBlue; // Viền cùng màu
                card.CardButton.Text = "?";
                card.CardButton.ForeColor = Color.White;
                card.CardButton.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            }
        }

        // Hàm tạo hiệu ứng khi đã ghép đúng (Giữ ảnh, đổi viền)
        private void SetMatchedStyle(MemoryCard card)
        {
            // Đổi màu viền thành Xanh Lá để báo hiệu đúng
            card.CardButton.BorderColor = Color.LimeGreen;
            card.CardButton.BorderThickness = 5;

            if (!card.IsImage)
            {
                // Nếu là thẻ chữ, ta có thể đổi màu nền cho đẹp
                card.CardButton.FillColor = Color.LightGreen;
            }
            // Nếu là thẻ hình, GIỮ NGUYÊN FillColor = Transparent để không mất ảnh
        }

        private void UpdateScore()
        {
            if (lbl_Score != null)
                lbl_Score.Text = $"Đã tìm thấy: {pairsFound} / {totalPairs}";
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Hàm chuyển đổi ảnh AN TOÀN (Fix lỗi mất hình)
        private Image ConvertByteArrayToImage(byte[] data)
        {
            if (data == null || data.Length == 0) return null;
            try
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    // Tạo bản sao Bitmap để ngắt kết nối stream
                    return new Bitmap(Image.FromStream(ms));
                }
            }
            catch { return null; }
        }

        private void btn_ResetGame_Click(object sender, EventArgs e)
        {
            if (this.currentQuizId > 0)
            {
                LoadGame(this.currentQuizId);
            }
        }
    }

    internal class MemoryCard
    {
        public int VocabId { get; set; }
        public bool IsImage { get; set; }
        public VocabularyDTO Data { get; set; }
        public Guna2Button CardButton { get; set; }
        public bool IsRevealed { get; set; } = false;
        public bool IsMatched { get; set; } = false;
    }
}