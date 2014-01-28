using System;
using System.ServiceModel;
using SmallSharpTools.Services.Generic;
using SmallSharpTools.Services.Provider;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicesAdmin.Instance.Startup();

            //ServiceHost serviceHost = new ServiceHost(typeof(GenericService), new Uri("http://localhost:8001/GenericService/"));
            //serviceHost.Open();

            Console.WriteLine("Waiting for you...");
            Console.ReadLine();

            //serviceHost.Close();
            ServicesAdmin.Instance.Shutdown();
        }
    }
}
