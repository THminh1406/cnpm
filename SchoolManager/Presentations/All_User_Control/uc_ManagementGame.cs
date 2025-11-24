using SchoolManager.Presentations.Forms;
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
    public partial class uc_ManagementGame : UserControl
    {
        public uc_ManagementGame()
        {
            InitializeComponent();
        }

        private void btn_Vocab_Click(object sender, EventArgs e)
        {
            index mainForm = this.FindForm() as index;
            if (mainForm != null) mainForm.showUC(mainForm.uc_Manage_Vocabulary1);
        }

        private void btn_CreateGame_Click(object sender, EventArgs e)
        {
            index mainForm = this.FindForm() as index;
            if (mainForm != null) mainForm.showUC(mainForm.uc_Create_Quiz1);
        }

        private void btn_ListGame_Click(object sender, EventArgs e)
        {
            index mainForm = this.FindForm() as index;
            if (mainForm != null) mainForm.showUC(mainForm.uc_Game_Menu1);
        }
    }
}
