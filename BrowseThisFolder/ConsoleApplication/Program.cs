using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security;

namespace ConsoleApplication
{

    [ComVisible(false), SuppressUnmanagedCodeSecurity]
    internal class Win32Native
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Win32Native.FreeConsole();
                string path = string.Empty;
                string command = ConfigurationManager.AppSettings["WebServerCommand"];
                if (!File.Exists(command))
                {
                    LogMessage("Web server does not exist: " + command);
                    return;
                }
                string commandArgs = string.Empty;
                int r = new System.Random().Next(1024, 9000);
                string port = r.ToString();
                if ((args.Length == 1) && !args[0].Contains("vshost.exe"))
                {
                    path = args[0];
                }
                else
                {
                    path = ConfigurationManager.AppSettings["DefaultDirectory"];
                    if (!Directory.Exists(path))
                    {
                        LogMessage("Path does not exist: " + path);
                        return;
                    }
                }
                commandArgs = commandArgs + " /path:\"" + path + "\"";
                commandArgs = commandArgs + " /port:";
                commandArgs = commandArgs + port;
                commandArgs = commandArgs + " /vpath: \" / ";
                commandArgs = commandArgs + path.Substring(path.LastIndexOf(@"\\") + 1);
                commandArgs = commandArgs + "\\\"";
                LogMessage("path: " + path);
                LogMessage("commandArgs: " + commandArgs);
                ProcessStartInfo info = new ProcessStartInfo();
                info.Arguments = commandArgs;
                info.CreateNoWindow = true;
                info.FileName = command;
                info.UseShellExecute = false;
                info.WorkingDirectory = command.Substring(0, command.LastIndexOf(Path.DirectorySeparatorChar));
                Process.Start(info);
                string starupDelayStr = string.Empty;
                int startupDelay = 1000;
                int.TryParse(ConfigurationManager.AppSettings["StartupDelay"], out startupDelay);
                Thread.Sleep(startupDelay);
                Process.Start("http://localhost:" + port + "/");
            }
            catch (Exception ex)
            {
                LogMessage(ex.Message);
            }
        }

        private static void LogMessage(string errorStr)
        {
            string source = "Browse This Folder";
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, "Application");
            }
            EventLog.WriteEntry(source, errorStr);
        }
    }
}
