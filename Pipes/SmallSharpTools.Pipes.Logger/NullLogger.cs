using System.Diagnostics;
using SmallSharpTools.Pipes.Model;

namespace SmallSharpTools.Pipes.Logger
{

    public class NullLogger : ILogger
    {

        public void Log(string message, TraceEventType eventType) { }
    
    }

}
