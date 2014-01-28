using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;
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
        ToolboxData("<{0}:YahooLoginStatus runat=server></{0}:YahooLoginStatus>"),
       Designer(typeof(YahooLoginStatusDesigner))
    ]
    public class YahooLoginStatus : CompositeControl
    {
        
        #region "  Variables  "

        private YahooLoginButton loginButton;
        private YahooLogoutButton logoutButton;

        #endregion
        
        public YahooLoginStatus()
        {
            Load += new EventHandler(YahooLoginStatus_Load);
        }

        void YahooLoginStatus_Load(object sender, EventArgs e)
        {
            YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();
            yahooSession.StatusChanged += new EventHandler(yahooSession_StatusChanged);
            UpdateButtons();
        }

        private void yahooSession_StatusChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();

            if (yahooSession.IsAuthenticated)
            {
                loginButton.Visible = false;
                logoutButton.Visible = true;
            }
            else
            {
                loginButton.Visible = true;
                logoutButton.Visible = false;
            }
        }
        
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
                loginButton = new YahooLoginButton();
                loginButton.ID = "loginButton";
            }
            if (logoutButton == null)
            {
                logoutButton = new YahooLogoutButton();
                logoutButton.ID = "logoutButton";
            }

            Controls.Add(loginButton);
            Controls.Add(logoutButton);
        }

        #endregion

        #region "  Control Properties  "

        [Browsable(true), Category("Display"), Description("Logout Button"), DefaultValue("")]
        public YahooLoginButton LoginButton
        {
            get
            {
                EnsureChildControls();
                return loginButton;
            }
        }

        [Browsable(true), Category("Display"), Description("Logout Button"), DefaultValue("")]
        public YahooLogoutButton LogoutButton
        {
            get
            {
                EnsureChildControls();
                return logoutButton;
            }
        }

        #endregion

    }

    #region "  Helper Classes  "

    internal class YahooLoginStatusDesigner : CompositeControlDesigner
    {

        private YahooLoginStatus yls = null;
        
        #region "  Designer Methods  "

        /// <summary>
        /// Override method
        /// </summary>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            yls = component as YahooLoginStatus;
        }

        /// <summary>
        /// Override method
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            string markup;
            bool loginVisible = yls.LoginButton.Visible;
            bool logoutVisible = yls.LogoutButton.Visible;

            try
            {
                yls.LoginButton.Visible = true;
                yls.LogoutButton.Visible = false;
                
                markup = base.GetDesignTimeHtml();

                yls.LoginButton.Visible = loginVisible;
                yls.LogoutButton.Visible = logoutVisible;
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
