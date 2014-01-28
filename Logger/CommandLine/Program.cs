using System;
using System.Collections.Generic;
using System.Text;
using SmallSharpTools;
using SmallSharpTools.Logging;

namespace CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = GetLogger(typeof(Program));
            logger.Info("Init");
        }
        
        private static ILogger GetLogger(Type type)
        {
            return LoggingProvider.Instance.GetLogger(type);
        }
    }
}
