using System;
using System.Runtime.InteropServices;
using System.Security;
using SmallSharpTools.ImageResizer;

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
            if (args.Length == 0)
            {
                PrintUsage();
            }
            Win32Native.FreeConsole();
            string directory = args[0];
            ImageProcessor.Instance.Run(directory);
        }
        
        private static void PrintUsage()
        {
            Console.WriteLine("usage: ");
        }
    }
}
