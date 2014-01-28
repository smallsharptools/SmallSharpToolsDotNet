using System;
using System.ComponentModel;
using System.Web.UI;
using SmallSharpTools;
using SmallSharpTools.Logging;
using SmallSharpTools.YahooPack.Providers;
using SmallSharpTools.YahooPack.Web.Security;

public partial class _Default : Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        yahooLoginStatus.LoginButton.LoggingIn += new CancelEventHandler(LoginButton_LoggingIn);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();

        if (yahooSession.IsAuthenticated)
        {
            lblApplicationID.Text = yahooSession.ApplicationId;
            lblToken.Text = yahooSession.Token;
            lblLoginDateTime.Text = yahooSession.LoginDate.ToString();
            lblTokenExpiration.Text = yahooSession.TokenExpiration.ToString();
            lblValidUntil.Text = yahooSession.ValidUntil.ToString();
            lblDateTime.Text = DateTime.Now.ToString();
            lblApplicationData.Text = Session["YahooApplicationData"] as String;
            
            foreach (String key in Session.Keys)
            {
                Logger.Info(String.Format("{0}: {1}", key, Session[key].ToString()));
            }
        }
    }

    void LoginButton_LoggingIn(object sender, CancelEventArgs e)
    {
        if ("localhost".Equals(Request.Url.Host.ToString()))
        {
            yahooLoginStatus.LoginButton.ApplicationData.Add("vslocal", "true");
            yahooLoginStatus.LoginButton.ApplicationData.Add("port", Request.Url.Port.ToString());
            yahooLoginStatus.LoginButton.ApplicationData.Add("virtdir", "Website");
        }
    }

    private ILogger Logger
    {
        get
        {
            return LoggingProvider.Instance.GetLogger(GetType());
        }
    }
    
}
