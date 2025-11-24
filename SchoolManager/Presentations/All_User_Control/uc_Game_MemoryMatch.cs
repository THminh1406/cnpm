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

        private List<MemoryCard> gameCards; // Danh sách các thẻ
        private MemoryCard firstCard = null; // Thẻ đầu tiên lật
        private MemoryCard secondCard = null; // Thẻ thứ hai lật

        private int currentQuizId = 0;
        private int pairsFound = 0;
        private int totalPairs = 0;

        private TableLayoutPanel tbl_GameBoard; // Bảng game (sẽ tạo bằng code)
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

            // Gán sự kiện Reset (Hãy đảm bảo tên 'btn_ResetGame' là đúng)
            btn_ResetGame.Click += btn_ResetGame_Click;
        }


        // === 3. HÀM LOADGAME (HÀM CHÍNH) ===
        public void LoadGame(int quizId)
        {
            this.currentQuizId = quizId;
            this.pairsFound = 0;
            lbl_Feedback.Text = "";
            // XÓA DÒNG GÂY LỖI: tbl_GameBoard.Enabled = true;

            // 1. Lấy dữ liệu (DTO) (PHẢI LÀM VIỆC NÀY TRƯỚC)
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

            // 2. Tạo danh sách 2N thẻ (N Hình, N Chữ)
            this.gameCards = new List<MemoryCard>();
            foreach (var vocab in gameData)
            {
                gameCards.Add(new MemoryCard { VocabId = vocab.id_vocabulary, IsImage = true, Data = vocab });
                gameCards.Add(new MemoryCard { VocabId = vocab.id_vocabulary, IsImage = false, Data = vocab });
            }

            // 3. Xáo trộn danh sách 2N thẻ
            Shuffle(gameCards);

            // 4. Quyết định kích thước lưới
            int rows, cols;
            DetermineGridSize(this.totalPairs, out rows, out cols);

            // 5. TẠO BẢNG (Bây giờ tbl_GameBoard mới tồn tại)
            CreateGameBoard(rows, cols);

            // 6. (Bây giờ dòng này đã an toàn)
            tbl_GameBoard.Enabled = true;

            // 7. Đặt các thẻ vào Bảng
            PopulateGrid();
        }

        // === 4. LOGIC XÂY DỰNG LƯỚI ===

        // Quyết định kích thước (6 cặp -> 3x4)
        private void DetermineGridSize(int pairCount, out int rows, out int cols)
        {
            if (pairCount <= 6) { rows = 3; cols = 4; } // 12 thẻ (Dễ)
            else if (pairCount <= 8) { rows = 4; cols = 4; } // 16 thẻ (Trung bình)
            else { rows = 4; cols = 5; } // 20 thẻ (Khó - 10 cặp)
            // (Bạn có thể thêm mức 4x6 = 24 thẻ = 12 cặp nếu muốn)
        }

        // Tạo TableLayoutPanel bằng code
        private void CreateGameBoard(int rows, int cols)
        {
            // Xóa bảng cũ (nếu có)
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

            // Đặt kích thước % cho các hàng/cột
            for (int i = 0; i < rows; i++)
            {
                tbl_GameBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));
            }
            for (int i = 0; i < cols; i++)
            {
                tbl_GameBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));
            }

            pnl_GameArea.Controls.Add(tbl_GameBoard);
        }

        // Đặt thẻ vào các ô của Bảng
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
                        btn.BorderRadius = 10;
                        btn.Margin = new Padding(5);
                        btn.FillColor = Color.DodgerBlue;
                        btn.Text = "?"; // Úp
                        btn.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
                        btn.Tag = card; // Lưu đối tượng Thẻ vào Tag
                        btn.Click += Card_Click;

                        card.CardButton = btn; // Lưu Nút vào đối tượng Thẻ
                        tbl_GameBoard.Controls.Add(btn, c, r); // Đặt vào Bảng
                        cardIndex++;
                    }
                }
            }
        }

        // === 5. LOGIC CHƠI GAME ===

        private async void Card_Click(object sender, EventArgs e)
        {
            // 1. Guard Clause: Nếu 2 thẻ đang lật, không cho click thẻ thứ 3
            if (secondCard != null) return;

            var clickedButton = (Guna2Button)sender;
            var clickedCard = (MemoryCard)clickedButton.Tag;

            // 2. Nếu click thẻ đã lật/đã ghép, bỏ qua
            if (!clickedButton.Enabled || clickedButton.FillColor != Color.DodgerBlue)
                return;

            // 3. Lật thẻ (Hiện Hình/Chữ)
            FlipCard(clickedCard, true);

            if (firstCard == null)
            {
                // 4. Đây là thẻ đầu tiên
                firstCard = clickedCard;
            }
            else
            {
                // 5. Đây là thẻ thứ hai
                secondCard = clickedCard;
                // === XÓA DÒNG NÀY: tbl_GameBoard.Enabled = false; ===

                // 6. So sánh
                if (firstCard.VocabId == secondCard.VocabId)
                {
                    // === ĐÚNG ===
                    firstCard.CardButton.Enabled = false;
                    secondCard.CardButton.Enabled = false;

                    pairsFound++;
                    UpdateScore();

                    firstCard = null;
                    secondCard = null;
                    // === XÓA DÒNG NÀY: tbl_GameBoard.Enabled = true; ===

                    if (pairsFound == totalPairs)
                    {
                        lbl_Feedback.Text = "Chúc mừng! Bạn đã thắng!";
                        lbl_Feedback.ForeColor = Color.Green;
                    }
                }
                else
                {
                    // === SAI ===
                    await Task.Delay(1000); // Chờ 1 giây

                    FlipCard(firstCard, false); // Lật úp
                    FlipCard(secondCard, false); // Lật úp

                    firstCard = null;
                    secondCard = null;
                    // === XÓA DÒNG NÀY: tbl_GameBoard.Enabled = true; ===
                }
            }
        }

        // Hàm lật thẻ (lật/úp)
        private void FlipCard(MemoryCard card, bool show)
        {
            if (show)
            {
                // 1. Khi lật ngửa (Hiện nội dung)

                // Đặt màu nền thành Transparent để thấy được BackgroundImage
                card.CardButton.FillColor = Color.Transparent;
                card.CardButton.Text = "";

                if (card.IsImage) // Lật thẻ Hình
                {
                    // Sử dụng BackgroundImage để ảnh lấp đầy khung
                    card.CardButton.BackgroundImage = ConvertByteArrayToImage(card.Data.WordImage);

                    // Chọn Zoom (giữ tỉ lệ ảnh) hoặc Stretch (kéo dãn hết khung)
                    // Khuyên dùng Zoom để ảnh không bị méo
                    card.CardButton.BackgroundImageLayout = ImageLayout.Zoom;

                    // Xóa Image (icon) để tránh bị chồng chéo
                    card.CardButton.Image = null;
                }
                else // Lật thẻ Chữ
                {
                    // Thẻ chữ thì không cần BackgroundImage
                    card.CardButton.BackgroundImage = null;
                    card.CardButton.FillColor = Color.DarkGray; // Màu nền cho thẻ chữ

                    card.CardButton.Text = card.Data.WordText;
                    card.CardButton.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                }
            }
            else
            {
                // 2. Khi lật úp (Về trạng thái ban đầu)
                card.CardButton.BackgroundImage = null; // Xóa ảnh nền
                card.CardButton.FillColor = Color.DodgerBlue; // Khôi phục màu xanh
                card.CardButton.Text = "?";
                card.CardButton.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
                card.CardButton.Image = null;
            }
        }

        // === 6. HÀM HỖ TRỢ ===
        private void UpdateScore()
        {
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
        public int VocabId { get; set; } // ID để so sánh (ví dụ: 10)
        public bool IsImage { get; set; } // Là thẻ Hình hay thẻ Chữ?
        public VocabularyDTO Data { get; set; } // Dữ liệu (chứa Ảnh và Chữ)
        public Guna2Button CardButton { get; set; } // Nút bấm UI
    }
}
