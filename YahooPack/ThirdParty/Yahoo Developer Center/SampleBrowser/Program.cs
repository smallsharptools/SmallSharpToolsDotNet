using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yahoo.Samples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			// Check if we need to upgrade the user settings files after a program upgrade
			if (Properties.Settings.Default.UpgradeSettings == true)
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.UpgradeSettings = false;
			}
			
			Application.Run(new SampleBrowser());
        }
    }
}