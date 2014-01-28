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
using System.Web;

namespace SmallSharpTools.Logging
{
    public class LoggingModule : IHttpModule
    {
        
        ///<summary>
        ///Initializes a module and prepares it to handle requests.
        ///</summary>
        ///
        ///<param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        public void Init(HttpApplication context)
        {
            ILogger logger = GetLogger(GetType());
            logger.Info(GetType().Name + ".Init");
        }

        ///<summary>
        ///Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
        ///</summary>
        ///
        public void Dispose()
        {
            // do nothing
        }

        public static ILogger GetLogger(Type type)
        {
            return LoggingProvider.Instance.GetLogger(type);
        }
    }
}
