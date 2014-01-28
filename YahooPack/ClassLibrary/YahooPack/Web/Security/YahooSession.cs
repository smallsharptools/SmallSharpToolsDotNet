using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using Yahoo;

namespace SmallSharpTools.YahooPack.Web.Security
{
    public class YahooSession
    {
        
        #region "  Events  "

        public event EventHandler StatusChanged;

        #endregion

        #region  "  Methods  "
        
        internal YahooSession()
        {
            _authentication = new Authentication(
                YahooConfiguration.ApplicationID,
                YahooConfiguration.SharedSecret);
        }

        public string GetLoginUrl(string applicationData)
        {
            if (!String.IsNullOrEmpty(applicationData))
            {
                return Authentication.GetUserLogOnAddress(applicationData).ToString();
            }
            else
            {
                return Authentication.GetUserLogOnAddress().ToString();
            }
        }

        public DataSet GetService(Uri uri)
        {
             if (!Authentication.IsCredentialed)
             {
                 Authentication.UpdateCredentials();
             }
            return Authentication.GetAuthenticatedServiceDataSet(uri);
        }

        public void ClearAuthentication()
        {
            Token = String.Empty;
            LoginDate = DateTime.MinValue;
            ValidUntil = DateTime.MinValue;
            OnStatusChanged(EventArgs.Empty);
        }

        public void UpdateCredentials()
        {
            Authentication.UpdateCredentials();
            OnStatusChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raise the LoggingOut event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStatusChanged(EventArgs e)
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, e);
            }
        }
        
        #endregion

        #region "  Properties  "
        
        public bool IsAuthenticated
        {
            get
            {
                return TokenExpiration > DateTime.Now;
            }
        }

        private Authentication _authentication;
        internal Authentication Authentication
        {
            get
            {
                return _authentication;
            }
        }
        
        public String ApplicationId
        {
            get
            {
                return Authentication.ApplicationId;
            }
            set
            {
                Authentication.ApplicationId = value;
            }

        }

        public String Token
        {
            get
            {
                return Authentication.Token;
            }
            set
            {
                Authentication.Token = value;
            }

        }
        
        private String _applicationData = String.Empty;
        public String ApplicationData
        {
            get
            {
                return _applicationData;
            }
            set
            {
                _applicationData = value;
            }
        }

        private DateTime _loginDate = DateTime.MinValue;
        public DateTime LoginDate
        {
            get
            {
                return _loginDate;
            }
            set
            {
                _loginDate = value;
            }
        }
        
        public DateTime TokenExpiration
        {
            get
            {
                return LoginDate.AddDays(14);
            }
        }
        
        public DateTime ValidUntil
        {
            get
            {
                return Authentication.ValidUntil;
            }
            set
            {
                Authentication.ValidUntil = value;
            }
        }
        
        #endregion
        
    }
}
