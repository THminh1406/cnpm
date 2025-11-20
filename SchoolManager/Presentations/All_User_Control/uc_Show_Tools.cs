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
    public partial class uc_Show_Tools : UserControl
    {
        public uc_Show_Tools()
        {
            InitializeComponent();
        }

        public void showUC(UserControl uc)
        {
            // Ẩn tất cả UC khác
            foreach (Control c in panel_Container_Tools.Controls)
                if (c is UserControl) c.Visible = false;

            uc.Visible = true;
            uc.BringToFront();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            showUC(uc_Time_Management_Tool1);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            showUC(uc_Drawing_Board1);
        }
    }
}


