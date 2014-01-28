using System;
using System.Collections.Generic;
using System.Text;

namespace SmallSharpTools.Packer.Utilities
{

    public enum MessageType
    {
        Normal,
        Warning,
        Error
    }

    public interface ILogger
    {

        void WriteMessage(string message, MessageType messageType);

    }

}
