using ClientApplication.Core;
using ClientApplication.Data;
using ClientApplication.Exception;
using DoctorApplication;
using RHApplicationLib.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientApplication
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

    }
}
