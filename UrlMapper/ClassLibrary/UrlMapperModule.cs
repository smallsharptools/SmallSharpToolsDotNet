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
using log4net;
using SmallSharpTools.Logging;

namespace SmallSharpTools.UrlMapping
{
    public class UrlMapperModule : IHttpModule
    {
        ///<summary>
        ///Initializes a module and prepares it to handle requests.
        ///</summary>
        ///
        ///<param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.PostMapRequestHandler += new EventHandler(context_PostMapRequestHandler);
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            if (UrlMappingProvider.Instance.IsLoggingEnabled)
            {
                Logger.Info(GetType().Name + ".context_BeginRequest");
            }
            HttpContext context = GetContext(sender);
            string path = HttpContext.Current.Request.Path;
            if (UrlMappingProvider.Instance.IsMappedPath(path))
            {
                string rewriteUrl = UrlMappingProvider.Instance.GetMappedPath(path);
                context.Items["Request.Path"] = HttpContext.Current.Request.Path;
                context.RewritePath(rewriteUrl, true);
            }
        }

        void context_PostMapRequestHandler(object sender, EventArgs e)
        {
            if (UrlMappingProvider.Instance.IsLoggingEnabled)
            {
                Logger.Info(GetType().Name + ".context_PostMapRequestHandler");
            }
            HttpContext context = GetContext(sender);
            RestoreRewritePath(context);
        }
        
        private void RestoreRewritePath(HttpContext context)
        {
            string requestPath = context.Items["Request.Path"] as string;
            if (!String.IsNullOrEmpty(requestPath))
            {
                context.RewritePath(requestPath);
            }
        }

        ///<summary>
        ///Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
        ///</summary>
        ///
        void IHttpModule.Dispose()
        {
            // do nothing
        }

        private HttpContext GetContext(object sender)
        {
            HttpApplication application = sender as HttpApplication;
            if (application != null)
            {
                return application.Context;
            }
            throw new ApplicationException("Unable to cast source to HttpApplication: " + sender.GetType().Name);
        }

        private ILogger _logger;
        public ILogger Logger
        {
            get { 
                if (_logger == null)
                {
                    _logger = LoggingProvider.Instance.GetLogger(GetType());
                }
                return _logger; 
            }
            set { _logger = value; }
        }
	
    }
}
