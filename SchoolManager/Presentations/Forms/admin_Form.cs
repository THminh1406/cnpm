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
    public partial class admin_Form : Form
    {
        public admin_Form()
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            showUC(uc_Approve_Registration1);
        }

        private void AccessTeacherManagement(object sender, EventArgs e)
        {
            showUC(uc_Teacher_Management1);
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
