using System;
using System.Web;
using System.Web.Security;

namespace SmallSharpTools.EventsBanner.Website
{

    /// <summary>
    /// Summary description for Global
    /// </summary>
    public class Global : HttpApplication
    {
        
        private ILogger _logger = null;
        
        public Global()
        {
            Logger.Info("Web Application Initialized");
        }

        public void Application_Start(object sender, EventArgs e)
        {
            
            if (Roles.Enabled)
            {
                String[] requiredRoles = { "Admin", "Users", "Editors" };
                foreach (String role in requiredRoles)
                {
                    if (!Roles.RoleExists(role))
                    {
                        Roles.CreateRole(role);
                    }
                }
                string[] users = Roles.GetUsersInRole("Admin");
                if (users.Length == 0)
                {
                    // create admin user
                    MembershipCreateStatus status;
                    Membership.CreateUser("admin", "abc123!", "admin@offwhite.net",
                                          "Favorite developer?", "Scott Guthrie", true, out status);
                    if (MembershipCreateStatus.Success.Equals(status))
                    {
                        Roles.AddUserToRole("admin", "Admin");
                    }
                    else
                    {
                        Logger.Warn(("Unable to create admin user: " + status.ToString()));
                    }
                }
            }
        }

        public void Application_Error(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Url.ToString().Contains(SiteConfiguration.ErrorUrl))
            {
                Exception lastError = Server.GetLastError();
                Utility.GetLogger(typeof(Global)).Error(lastError.Message, lastError);
                Session.Add("LastError", lastError);

                HttpContext.Current.Response.Redirect(SiteConfiguration.ErrorUrl);
            }
        }

        private ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = Utility.GetLogger(typeof(Global));
                }
                return _logger;
            }
        }
    }
}
