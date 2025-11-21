using SchoolManager.Presentations.Forms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login_Form());
            //Application.Run(new register_Form());
            //Application.Run(new forget_Password_Form());
            //Application.Run(new admin_Form());
            ////Application.Run(new teacher_AccountManager_Form());
            //Application.Run(new main_Form());
            //Application.Run(new index());

        }
    }
}
