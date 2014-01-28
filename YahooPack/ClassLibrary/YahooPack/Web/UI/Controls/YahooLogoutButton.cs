using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
using SmallSharpTools.Logging;
using SmallSharpTools.YahooPack.Providers;

namespace SmallSharpTools.YahooPack.Web.UI.Controls
{


    /// <summary>
    /// SmallSharpTools.YahooPack: Logout Button
    /// </summary>
    [
        AspNetHostingPermission(SecurityAction.Demand,
        Level = AspNetHostingPermissionLevel.Minimal),
        AspNetHostingPermission(SecurityAction.InheritanceDemand,
        Level = AspNetHostingPermissionLevel.Minimal),
        ToolboxData("<{0}:YahooLogoutButton runat=server></{0}:YahooLogoutButton>"),
       Designer(typeof(YahooLogoutButtonDesigner))
    ]
    public class YahooLogoutButton : CompositeControl
    {

        #region "  Events  "

        public event CancelEventHandler LoggingOut;

        #endregion

        #region "  Variables  "

        private LinkButton logoutButton;

        #endregion

        #region "  Control Events  "

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            CancelEventArgs args = new CancelEventArgs(false);
            OnLoggingOut(args);

            if (!args.Cancel)
            {
                YahooAuthenticationProvider.Instance.ClearSession();
                Logger.Debug("Logged out");
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
            if (logoutButton == null)
            {
                logoutButton = new LinkButton();
                logoutButton.Text = "Log Out";
                logoutButton.Click += new EventHandler(logoutButton_Click);
            }

            Controls.Add(logoutButton);
        }

        #endregion

        #region "  Control Properties  "

        /// <summary>
        /// Property
        /// </summary>
        [Browsable(true), Category("Display"), Description("Link Text"), DefaultValue("Log Out")]
        public string Text
        {
            get
            {
                EnsureChildControls();
                return logoutButton.Text;
            }
            set
            {
                EnsureChildControls();
                logoutButton.Text = value;
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
                return logoutButton.CssClass;
            }
            set
            {
                EnsureChildControls();
                logoutButton.CssClass = value;
            }
        }

        #endregion

        /// <summary>
        /// Raise the LoggingOut event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLoggingOut(CancelEventArgs e)
        {
            if (LoggingOut != null)
            {
                LoggingOut(this, e);
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

    internal class YahooLogoutButtonDesigner : CompositeControlDesigner
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