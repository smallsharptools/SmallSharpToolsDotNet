using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using SmallSharpTools.Logging;
using SmallSharpTools.YahooPack.Providers;

namespace SmallSharpTools.YahooPack.Web.Security
{
    public class YahooModule : IHttpModule
    {

        ///<summary>
        ///Initializes a module and prepares it to handle requests.
        ///</summary>
        ///
        ///<param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(context_PreRequestHandlerExecute);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            InitializeYahooSession();
        }
        
        private void InitializeYahooSession()
        {
            try
            {
                HttpContext context = HttpContext.Current;
                if (!String.IsNullOrEmpty(context.Request.QueryString["token"]) &&
                    !String.IsNullOrEmpty(context.Request.QueryString["appid"]))
                {
                    YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();

                    string signedUrl = YahooConfiguration.ApplicationEntryPoint + context.Request.Url.Query;
                    if (yahooSession.Authentication.IsValidSignedUrl(
                        new Uri(signedUrl), YahooConfiguration.SharedSecret))
                    {
                        Logger.Debug("Successful Login!");

                        string destinationUrl = YahooConfiguration.ApplicationEntryPoint;
                        string localUrl;

                        Dictionary<String, String> appData = 
                            ParseApplicationData(context.Request.QueryString["appdata"]);

                        if (appData.ContainsKey("vslocal") &&
                                "true".Equals(appData["vslocal"]))
                        {
                            localUrl = String.Format("http://localhost:{0}/{1}/",
                                appData["port"], appData["virtdir"]);
                            if ("localhost".Equals(context.Request.Url.Host))
                            {
                                destinationUrl = localUrl;
                            }
                            else {
                                destinationUrl = localUrl + context.Request.Url.Query;
                            }
                        }
                        
                        yahooSession.Authentication.Token =
                            context.Request.QueryString["token"];
                        yahooSession.Authentication.ApplicationId =
                            context.Request.QueryString["appid"];
                        context.Session["YahooApplicationData"] =
                            context.Request.QueryString["appdata"];

                        yahooSession.Authentication.UpdateCredentials();
                        yahooSession.LoginDate = DateTime.Now;

                        YahooAuthenticationProvider.Instance.SaveSession(yahooSession);

                        Logger.Debug("destinationUrl: " + destinationUrl);
                        context.Response.Redirect(destinationUrl, true);
                    }
                    else
                    {
                        Logger.Warn("Url not valid for Yahoo BBAuth: " + context.Request.Url);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                {
                    Logger.Error(ex.Message, ex);
                }
            }
        }
        
        /// <summary>
        /// Parsed the Application Data using periods and underscores as delimiters
        /// 
        /// Example:
        /// vslocal_true.port_1001.virtdir_Website
        /// </summary>
        /// <param name="appData"></param>
        /// <returns></returns>
        private Dictionary<String, String> ParseApplicationData(string appData)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            string[] pairs = appData.Split(".".ToCharArray()[0]);
            foreach (string pair in pairs)
            {
                string[] parts = pair.Split("_".ToCharArray()[0]);
                if (parts.Length == 2)
                {
                    result[parts[0]] = parts[1];
                }
            }
            return result;
        }

        ///<summary>
        ///Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
        ///</summary>
        ///
        public void Dispose()
        {
        }
        
        private ILogger Logger
        {
            get
            {
                return LoggingProvider.Instance.GetLogger(GetType());
            }
        }
        
    }
}
