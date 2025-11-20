using Guna.UI2.WinForms;
using SchoolManager.Presentations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager
{
    public partial class teacher_AccountManager_Form : Form
    {
        public teacher_AccountManager_Form()
        {
            InitializeComponent();
        }

        private void teacher_AccountManager_Form_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void teacher_AccountManager_Form_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;

            // Tạo LinearGradientBrush với hướng từ trên trái đến dưới phải
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                Color.White, // placeholder, sẽ bị ghi đè bởi ColorBlend
                Color.White,
                LinearGradientMode.ForwardDiagonal))
            {
                // Tạo ColorBlend để kiểm soát tỷ lệ màu
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[]
                {
            ColorTranslator.FromHtml("#f0f9ff"), // Sky 50
            ColorTranslator.FromHtml("#bae6fd"), // Sky 200
            ColorTranslator.FromHtml("#0ea5e9")  // Sky 500
                };

                blend.Positions = new float[]
                {
            0.0f,  // bắt đầu với màu rất nhạt
            0.6f,  // màu trung bình chiếm 60%
            1.0f   // màu đậm ở cuối
                };

                brush.InterpolationColors = blend;

                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void CenterControlByScrolling(Panel panel, Control ctrl)
        {
            // vị trí trung tâm mong muốn trong nội dung (panel coordinates)
            int targetX = ctrl.Left + ctrl.Width / 2 - panel.ClientSize.Width / 2;
            int targetY = ctrl.Top + ctrl.Height / 2 - panel.ClientSize.Height / 2;

            if (targetX < 0) targetX = 0;
            if (targetY < 0) targetY = 0;

            // Gọi AutoScrollPosition để scroll tới vị trí này
            panel.AutoScrollPosition = new Point(targetX, targetY);
        }

        public void showUC(UserControl uc)
        {
            // Ẩn tất cả UC khác
            foreach (Control c in panelContainer.Controls)
                if (c is UserControl) c.Visible = false;

            // Tắt dock để location có hiệu lực
            uc.Dock = DockStyle.None;
            uc.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            uc.Visible = true;
            uc.BringToFront();

            // Force layout cập nhật ngay
            panelContainer.PerformLayout();
            panelContainer.Update();

            // Scroll để UC nằm giữa viewport
            CenterControlByScrolling(panelContainer, uc);
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_RealTime.Text = DateTime.Now.ToString("HH:mm:ss | dd/MM/yyyy");
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btn_StudentManager_Click(object sender, EventArgs e)
        {
            moving_Panel.Visible = true;
            moving_Panel.Left = btn_StudentManager.Left;
            showUC(uC_Student_Management);

        }


        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            showUC(uc_Manual_Roll_Call);    
        }

        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            moving_Panel.Visible = true;
            moving_Panel.Left = btn_StudentManager.Left + 220;
            showUC(uc_Study_Result);
       
        }



        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            moving_Panel.Visible = true;
            moving_Panel.Left = btn_StudentManager.Left + 330;
            
        }

        private void uC_Student_Management1_Load(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label_RealTime_Click(object sender, EventArgs e)
        {

        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton13_Click(object sender, EventArgs e)
        {
            showUC(uC_Student_Management);
        }

        private void guna2GradientButton11_Click(object sender, EventArgs e)
        {
            showUC(uC_Import_FIile_Excel);
        }

        private void guna2GradientButton12_Click(object sender, EventArgs e)
        {
            showUC(uc_Print_Export);
        }

        private void guna2GradientButton14_Click(object sender, EventArgs e)
        {
            showUC(uc_Manual_Roll_Call);
        }

        private void guna2GradientButton15_Click(object sender, EventArgs e)
        {
            showUC(qR_Roll_Call1);
        }

        private void guna2GradientButton26_Click(object sender, EventArgs e)
        {
            showUC(qR_Roll_Call1);
        }

        private void guna2GradientButton16_Click(object sender, EventArgs e)
        {
            showUC(uc_Study_Notes);
        }

        private void guna2GradientButton19_Click(object sender, EventArgs e)
        {
            showUC(uc_Study_Result);
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            showUC(uc_Rp_Statistics);
        }
    }
}
