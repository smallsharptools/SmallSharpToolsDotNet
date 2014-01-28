using System;
using System.Collections.Generic;
using System.Text;

namespace SmallSharpTools.Packer.Utilities
{

    public class ConsoleLogger : ILogger
    {

        #region ILogger Members

        public void WriteMessage(string message, MessageType messageType)
        {
            if (messageType == MessageType.Normal)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (messageType == MessageType.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (messageType == MessageType.Error)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(message);
        }

        #endregion

    }

}
