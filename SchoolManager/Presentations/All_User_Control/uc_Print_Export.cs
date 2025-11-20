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
    public partial class uc_Print_Export : UserControl
    {
        public uc_Print_Export()
        {
            InitializeComponent();
        }

        private void uc_Print_Export_Load(object sender, EventArgs e)
        {
            guna2DataGridView2.Rows.Add(false, "Nguyễn Văn A");
            guna2DataGridView2.Rows.Add(false, "Trần Thị B");
            guna2DataGridView2.Rows.Add(false, "Lê Văn C");
            guna2DataGridView2.Rows.Add(false, "Nguyễn Văn A");
            guna2DataGridView2.Rows.Add(false, "Trần Thị B");
            guna2DataGridView2.Rows.Add(false, "Lê Văn C");
            guna2DataGridView2.Rows.Add(false, "Nguyễn Văn A");
            guna2DataGridView2.Rows.Add(false, "Trần Thị B");
            guna2DataGridView2.Rows.Add(false, "Lê Văn C");
            guna2DataGridView2.Rows.Add(false, "Nguyễn Văn A");
            guna2DataGridView2.Rows.Add(false, "Trần Thị B");
            guna2DataGridView2.Rows.Add(false, "Lê Văn C");
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
