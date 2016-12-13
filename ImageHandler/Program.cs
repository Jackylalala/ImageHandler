using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageHandler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //with args(user open file with the program)
            if (args != null && args.Length > 0)
            {
                string fileName = args[0];
                //Check file exists
                if (File.Exists(fileName))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Open file with a new form
                    frmImageHandler MainFrom = new frmImageHandler();
                    MainFrom.openFile(fileName, Screen.PrimaryScreen.Bounds.Width - 63, Screen.PrimaryScreen.Bounds.Height - 133);
                    Application.Run(MainFrom);
                }
                //The file does not exist
                else
                {
                    MessageBox.Show("The file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmImageHandler());
                }
            }
            //without args
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmImageHandler());
            }
        }
    }
}
