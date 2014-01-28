/*=======================================================================
  Copyright (C) SmallSharpTools.com.  All rights reserved.
 
  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  
  Brennan Stehling
  brennan@smallsharptools.com
  http://www.smallsharptools.com/
=======================================================================*/

using System;
using System.Diagnostics;

namespace SmallSharpTools.Logging
{
    public class EventsImpl : ILogger
    {

        public string EventSource
        {
            get
            {
                return EventsLoggingProvider.EventSource;
            }
        }
        
        #region ILogger Members

        public void Debug(object message, Exception exception)
        {
            WriteEntry(IsDebugEnabled, message as string, exception, EventLogEntryType.Information);
        }

        public void Debug(object message)
        {
            WriteEntry(IsDebugEnabled, message as string, null, EventLogEntryType.Information);
        }

        public void Error(object message, Exception exception)
        {
            WriteEntry(IsErrorEnabled, message as string, exception, EventLogEntryType.Error);
        }

        public void Error(object message)
        {
            WriteEntry(IsErrorEnabled, message as string, null, EventLogEntryType.Error);
        }

        public void Fatal(object message, Exception exception)
        {
            WriteEntry(IsFatalEnabled, message as string, exception, EventLogEntryType.FailureAudit);
        }

        public void Fatal(object message)
        {
            WriteEntry(IsFatalEnabled, message as string, null, EventLogEntryType.FailureAudit);
        }

        public void Info(object message, Exception exception)
        {
            WriteEntry(IsInfoEnabled, message as string, exception, EventLogEntryType.Information);
        }

        public void Info(object message)
        {
            WriteEntry(IsInfoEnabled, message as string, null, EventLogEntryType.Information);
        }

        public void Warn(object message, Exception exception)
        {
            WriteEntry(IsWarnEnabled, message as string, exception, EventLogEntryType.Warning);
        }

        public void Warn(object message)
        {
            WriteEntry(IsWarnEnabled, message as string, null, EventLogEntryType.Warning);
        }

        public bool IsDebugEnabled
        {
            get
            {
                return EventsLoggingProvider.IsDebugEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return EventsLoggingProvider.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return EventsLoggingProvider.IsFatalEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get {
                return EventsLoggingProvider.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get {
                return EventsLoggingProvider.IsWarnEnabled;
            }
        }

        #endregion
        
        private void WriteEntry( bool isEnabled, string message, Exception exception, EventLogEntryType entryType)
        {
            if (isEnabled)
            {
                if (exception == null)
                {
                    EventLog.WriteEntry(EventSource, message, entryType);
                }
                else
                {
                    EventLog.WriteEntry(EventSource, GetFullMessage(message, exception), entryType);
                }
            }
        }
        
        private string GetFullMessage(object message, Exception exception)
        {
            return (message as string) + "\n" + exception.Message + "\n" + exception.StackTrace;
        }

    }
}
