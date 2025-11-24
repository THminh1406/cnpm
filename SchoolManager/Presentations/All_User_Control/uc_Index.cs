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
    public partial class uc_Index : UserControl
    {
        public uc_Index()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            // Cập nhật vào Label theo định dạng: 00:00:00 | 00/00/00
            // HH: Giờ 24h, mm: Phút, ss: Giây
            // dd: Ngày, MM: Tháng, yyyy: Năm
            label_RealTime.Text = now.ToString("HH:mm:ss | dd/MM/yyyy");
        }

        private void uc_Index_Load(object sender, EventArgs e)
        {
            timer1.Start();

            // 2. Cập nhật ngay lập tức để không bị text mặc định (ví dụ "label1")
            label_RealTime.Text = DateTime.Now.ToString("HH:mm:ss | dd/MM/yyyy");
        }
    }
}
