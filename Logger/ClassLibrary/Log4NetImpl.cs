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

namespace SmallSharpTools
{
    public class Log4NetImpl : ILogger
    {
        private log4net.ILog _logger;
        
        public Log4NetImpl(log4net.ILog logger)
        {
            _logger = logger;
        }

        #region ILog Members

        public void Debug(object message, System.Exception exception)
        {
            _logger.Debug(message, exception);
        }

        public void Debug(object message)
        {
            _logger.Debug(message);        }

        public void Error(object message, System.Exception exception)
        {
            _logger.Error(message, exception);
        }

        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Fatal(object message, System.Exception exception)
        {
            _logger.Fatal(message, exception);
        }

        public void Fatal(object message)
        {
            _logger.Fatal(message);
        }

        public void Info(object message, System.Exception exception)
        {
            _logger.Info(message, exception);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }

        public void Warn(object message, System.Exception exception)
        {
            _logger.Warn(message, exception);
        }

        public void Warn(object message)
        {
            _logger.Warn(message);
        }

        public bool IsDebugEnabled
        {
            get
            {
                return _logger.IsDebugEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return _logger.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return _logger.IsFatalEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return _logger.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return _logger.IsWarnEnabled;
            }
        }

        #endregion

    }
}
