using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace DevenvSetupCustomAction
{
    [RunInstaller(true)]
    public partial class PackageInstaller : Installer
    {
        public PackageInstaller()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            DoReset();
        }

        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);
            DoReset();
        }

        void DoReset()
        {
            if (DevenvIsRunning())
            {
                throw new System.Configuration.Install.InstallException(Properties.Resources.AlreadyRunning);
            }
            else
            {
                DevenvSetup();
            }
        }

        private static bool DevenvIsRunning()
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process != null)
                {
                    try
                    {
                        foreach (ProcessModule module in process.Modules)
                        {
                            if (module != null && module.FileName != null)
                            {
                                if (string.Compare(Path.GetFileName(module.FileName), "devenv.exe", StringComparison.InvariantCultureIgnoreCase) == 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        // For some reason we're unable to enumerate the modules of some processes.
                        // Just ignore the error in that case.
                    }
                }
            }

            return false;
        }

        private static void DevenvSetup()
        {
            RegistryKey vsKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\VisualStudio\\9.0");

            if (vsKey != null)
            {
                string vsPath = ((string)vsKey.GetValue("InstallDir")) + "devenv.exe";

                if (File.Exists(vsPath))
                {
                    Process vsProcess = Process.Start(vsPath, "/setup");
                    vsProcess.WaitForExit();
                }
            }
        }
    }
}