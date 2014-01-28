using System;
using System.Collections.Generic;
using System.Text;
using SmallSharpTools.Tidy;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MarkupCleaner cleaner = new MarkupCleaner();
            cleaner.ErrorOccured += new EventHandler(cleaner_ErrorOccured);
            cleaner.OptionFile = "foo.tidy";
            //cleaner.ErrorFile = "errors.txt";
            cleaner.CleanFile("foo.html", "foo.out.html");
            //Console.WriteLine("Press enter to end.");
            //Console.ReadLine();
        }

        static void cleaner_ErrorOccured(object sender, EventArgs e)
        {
            MarkupCleaner cleaner = (MarkupCleaner)sender;
            Console.WriteLine("An error has occured: " + cleaner.GetStatusMessage());
        }
    }
}
