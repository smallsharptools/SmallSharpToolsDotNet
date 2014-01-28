using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using SmallSharpTools.Logging;
using SmallSharpTools.YahooPack.Providers;
using SmallSharpTools.YahooPack.Web.Security;

namespace SmallSharpTools.YahooPack.Web.UI.Controls
{

    /// <summary>
    /// SmallSharpTools.YahooPack: Login Button
    /// </summary>
    [
        AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
        AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
        ToolboxData("<{0}:YahooLoginButton runat=server></{0}:YahooLoginButton>"),
       Designer(typeof(YahooLoginButtonDesigner))
    ]
    public class YahooLoginButton : CompositeControl
    {
        
        #region "  Events  "

        public event CancelEventHandler LoggingIn;

        #endregion

        #region "  Variables  "

        private LinkButton loginButton;

        #endregion

        #region "  Control Events  "

        protected void loginButton_Click(object sender, EventArgs e)
        {
            CancelEventArgs args = new CancelEventArgs(false);
            OnLoggingIn(args);

            if (!args.Cancel)
            {
                YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();
                StringBuilder appData = new StringBuilder();
                if (ApplicationData.Count > 0)
                {
                    foreach (string key in ApplicationData.Keys)
                    {
                        if (key.Contains("_") || key.Contains("."))
                        {
                            throw new InvalidDataException(
                                "Application Data Key cannot contain periods or underscores: " + 
                                key);
                        }
                        if (ApplicationData[key].Contains("_") || 
                                ApplicationData[key].Contains("."))
                        {
                            throw new InvalidDataException(
                                "Application Data Value cannot contain periods or underscores: " + 
                                ApplicationData[key]);
                        }
                        appData.Append(key);
                        appData.Append("_");
                        appData.Append(ApplicationData[key]);
                        appData.Append(".");
                    }
                    Logger.Debug("appData: " + appData.ToString());
                }
                string loginUrl = yahooSession.GetLoginUrl(appData.ToString());
                HttpContext.Current.Response.Redirect(loginUrl);
            }
        }
        
        #endregion

        #region "  Control Methods  "

        /// <summary>
        /// Created child controls
        /// </summary>
        protected override void CreateChildControls()
        {
            // Clear child controls
            Controls.Clear();

            // Build the control tree
            CreateControlHierarchy();

            // Clear the viewstate of child controls
            ClearChildViewState();
        }
        
        /// <summary>
        /// Creates control hierarchy
        /// </summary>
        protected void CreateControlHierarchy()
        {
            if (loginButton == null)
            {
                loginButton = new LinkButton();
                loginButton.Text = "Yahoo Login";
                loginButton.Click += new EventHandler(loginButton_Click);
            }
            
            Controls.Add(loginButton);
        }
        
        #endregion

        #region "  Control Properties  "

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Display"), Description("Link Text"), DefaultValue("Yahoo Login")]
        public string Text
        {
            get
            {
                EnsureChildControls();
                return loginButton.Text;
            }
            set
            {
                EnsureChildControls();
                loginButton.Text = value;
            }
        }

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Display"), Description("CSS Class"), DefaultValue("")]
        public new string CssClass
        {
            get
            {
                EnsureChildControls();
                return loginButton.CssClass;
            }
            set
            {
                EnsureChildControls();
                loginButton.CssClass = value;
            }
        }

        private Dictionary<String, String> _applicationData = new Dictionary<String, String>();
        [Browsable(true), Category("Behavior"), Description("Application Data"), DefaultValue("")]
        public Dictionary<String, String> ApplicationData
        {
            get
            {
                EnsureChildControls();
                return _applicationData;
            }
        }
        
        #endregion
        
        /// <summary>
        /// Raise the LoggingIn event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoggingIn(CancelEventArgs e)
        {
            if (LoggingIn != null)
            {
                LoggingIn(this, e);
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

    #region "  Helper Classes  "

    internal class YahooLoginButtonDesigner : CompositeControlDesigner
    {
        
        #region "  Designer Methods  "

        /// <summary>
        /// Override method
        /// </summary>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }
        
        /// <summary>
        /// Override method
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            string markup;
            
            try
            {
                markup = base.GetDesignTimeHtml();
            }
            catch (Exception ex)
            {
                markup = "<p style='background: #ccc; color: #900; font-weight: bold;'>Error: " +
                 ex.Message + "</p>\n<div>" +
                 ex.StackTrace + "</div>";
            }
            
            return markup;
        }

        #endregion
        
    }

    #endregion

}