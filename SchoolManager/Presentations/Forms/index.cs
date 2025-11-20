using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.Forms
{
    public partial class index : Form
    {
        public index()
        {
            InitializeComponent();
        }

        public void showUC(UserControl uc)
        {
            // Ẩn tất cả UC khác
            foreach (Control c in panelContainer.Controls)
                if (c is UserControl) c.Visible = false;

            uc.Visible = true;
            uc.BringToFront();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            showUC(uc_Manual_Roll_Call);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            showUC(qR_Roll_Call);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            showUC(uc_Show_Tools);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            showUC(uc_Manage_Vocabulary);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            showUC(uc_Show_Study_Management1);
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            showUC(uc_Create_Quiz);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            uc_Game_Menu1.RefreshGameList();
            showUC(uc_Game_Menu1);
        }

        private void uc_Game_Menu1_OnPlayGameClicked(int quizId, string quizType)
        {
            uc_Game_Menu1.SendToBack();
            uc_Game_Menu1.Visible = false;

            // (Giả sử bạn có 4 UC game: uc_Game_WordMatch1, uc_Game_MemoryFlip1, ...)

            // Dựa vào loại game, gọi UC tương ứng
            switch (quizType)
            {
                case "Nối hình":
                    //1.Hiển thị UC "Nối hình"
                    uc_Game_WordMatch1.Visible = true;
                    uc_Game_WordMatch1.BringToFront();

                    // 2. GỌI HÀM LOADGAME
                    uc_Game_WordMatch1.LoadGame(quizId);
                    break;

                case "Lật thẻ":
                    uc_Game_MemoryMatch1.Visible = true;
                    uc_Game_MemoryMatch1.BringToFront();
                    uc_Game_MemoryMatch1.LoadGame(quizId);
                    break;

                case "Điền từ":
                    uc_Game_FillBlank1.Visible = true;
                    uc_Game_FillBlank1.BringToFront();
                    uc_Game_FillBlank1.LoadGame(quizId);
                    break;

                case "Sắp xếp câu":
                    uc_Game_SentenceScramble1.Visible = true;
                    uc_Game_SentenceScramble1.BringToFront();
                    uc_Game_SentenceScramble1.LoadGame(quizId);
                    break;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            showUC(uC_Student_Management1);
        }
    }
}
