using System;
using System.Collections.Generic;
using System.IO;
using SmallSharpTools.Packer.Utilities;

namespace ConsoleApplication
{
    class Program
    {
        private const string CommonName = "SmallSharpTools.Packer";

        static void Main(string[] args)
        {
            if (args.Length == 0 || "-h".Equals(args[0]) || "-?".Equals(args[0]))
            {
                ShowUsage();
                return;
            }

            int startIndex = 0;
            string outputFile = "output.js";
            if ("-o".Equals(args[startIndex]))
            {
                if (args.Length > startIndex+1)
                {
                    outputFile = args[startIndex+1];
                    startIndex += 2;
                }
                else
                {
                    ShowUsage();
                    return;
                }
            }

            PackMode mode = PackMode.Packer;

            if ("-m".Equals(args[startIndex]))
            {
                if (args.Length > startIndex+1)
                {
                    string str = args[startIndex+1];
                    if ("packer".Equals(str, StringComparison.InvariantCultureIgnoreCase))
                    {
                        mode = PackMode.Packer;
                    }
                    else if ("jsmin".Equals(str, StringComparison.InvariantCultureIgnoreCase))
                    {
                        mode = PackMode.JSMin;
                    }
                    else if ("cssmin".Equals(str, StringComparison.InvariantCultureIgnoreCase))
                    {
                        mode = PackMode.CSSMin;
                    }
                    else if ("combine".Equals(str, StringComparison.InvariantCultureIgnoreCase))
                    {
                        mode = PackMode.Combine;
                    }
                    else
                    {
                        FileProcessor.Logger.WriteMessage("Mode is not recognized: " + str, MessageType.Error);
                        ShowUsage();
                        return;
                    }
                    startIndex += 2;
                }
                else
                {
                    ShowUsage();
                    return;
                }
            }

            List<string> inputFiles = new List<string>();
            
            for (int i=startIndex;i<args.Length;i++)
            {
                string filename = args[i];
                // expand wildcard
                if (filename.Contains("*"))
                {
                    inputFiles.AddRange(ExpandWildcard(filename));
                }
                else if (File.Exists(filename))
                {
                    inputFiles.Add(filename);
                }
            }

            FileProcessor.Run(outputFile, mode, inputFiles.ToArray(), true);
        }

        private static List<String> ExpandWildcard(string filename)
        {
            List<String> results = new List<String>();

            string dir = Path.GetDirectoryName(filename);
            string part = Path.GetFileName(filename);

            if (dir.Length == 0)
            {
                dir = Directory.GetCurrentDirectory();
            }
            foreach (string file in Directory.GetFiles(dir, part))
            {
                //string path = Path.Combine(dir, file);
                if (File.Exists(file))
                {
                    results.Add(file);
                }
            }

            return results;
        }

        private static void ShowUsage()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(CommonName);
            Console.WriteLine("");
            Console.WriteLine("Usage: Packer [-?] [-o <filename>] [-m <packer | jsmin | cssmin | combine>] <script1> <script2> ...");
            Console.WriteLine("");
            Console.WriteLine(" Options:");
            Console.WriteLine("  -h or -?       Help");
            Console.WriteLine("  -o <filename>  Output Filename");
            Console.WriteLine("  -m <mode>      Mode");
        }

    }
}
