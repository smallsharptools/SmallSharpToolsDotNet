using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SmallSharpTools.Pipes.Model
{
    public interface ILogger
    {

        void Log(string message, TraceEventType eventType);

    }
}
