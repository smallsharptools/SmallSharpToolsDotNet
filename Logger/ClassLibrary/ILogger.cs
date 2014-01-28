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
using log4net;

namespace SmallSharpTools
{
    public interface ILogger 
    {
        void Debug(object message, Exception exception);

        void Debug(object message);

        void Error(object message, Exception exception);

        void Error(object message);

        void Fatal(object message, Exception exception);

        void Fatal(object message);

        void Info(object message, Exception exception);

        void Info(object message);

        void Warn(object message, Exception exception);

        void Warn(object message);

        bool IsDebugEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsFatalEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsWarnEnabled { get; }

    }
}
