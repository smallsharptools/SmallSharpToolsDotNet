using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web;
using SmallSharpTools.YahooPack.Web.Security;

namespace SmallSharpTools.YahooPack.Providers
{
    public class XmlYahooAuthenticationProvider : YahooAuthenticationProvider
    {
        
        public static readonly String YahooSessionsTable = "YahooSessions";

        static object padLock = new object();

        /// <summary>
        /// XML Implementation for Yahoo Authentication
        /// </summary>
        public override void Initialize(string name, NameValueCollection configValue)
        {
            if (!String.IsNullOrEmpty(configValue["stateFileName"]))
            {
                AuthenticationStateFileName = configValue["stateFileName"];
            }
            else
            {
                AuthenticationStateFileName = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, @"App_Data\YahooSessions.xml");
            }
        }
        
        public override YahooSession LoadSession()
        {
            YahooSession yahooSession = null;
            if (HttpContext.Current.Session != null &&
                HttpContext.Current.Session["YahooSession"] != null)
            {
                yahooSession = HttpContext.Current.Session["YahooSession"] as YahooSession;
            }

            // Load from XML when not found in the Session
            if (yahooSession == null)
            {
                yahooSession = new YahooSession();
                DataSet authenticationState = LoadAuthenticationState();
                if (authenticationState != null)
                {
                    DataRow row = GetYahooSessionRow(authenticationState);
                    if (row != null)
                    {
                        yahooSession.ApplicationId = row["ApplicationId"] as String;
                        yahooSession.Token = row["Token"] as String;
                        yahooSession.ApplicationData = row["ApplicationData"] as String;
                        yahooSession.LoginDate = (DateTime) row["LoginDate"];
                        yahooSession.ValidUntil = (DateTime)row["ValidUntil"];
                        if (yahooSession.ValidUntil > DateTime.Now &&
                            !String.IsNullOrEmpty(yahooSession.Token))
                        {
                            yahooSession.UpdateCredentials();
                        }
                        if (HttpContext.Current.Session != null)
                        {
                            HttpContext.Current.Session["YahooSession"] = yahooSession;
                        }
                    }
                }
            }
            
            return yahooSession;
        }

        public override void SaveSession(YahooSession yahooSession)
        {
            if (HttpContext.Current.Session != null &&
               HttpContext.Current.Session["YahooSession"] == null)
            {
                HttpContext.Current.Session["YahooSession"] = yahooSession;
            }
            
            // protected against concurrency
            lock (padLock)
            {
                DataSet authenticationState = LoadAuthenticationState();
                if (authenticationState == null)
                {
                    authenticationState = CreateAuthenticationState();
                }

                DataRow row = GetYahooSessionRow(authenticationState);
                if (row == null)
                {
                    DataRow dRow = authenticationState.Tables[YahooSessionsTable].NewRow();
                    dRow["UserID"] = UserID;
                    dRow["ApplicationID"] = yahooSession.ApplicationId;
                    dRow["Token"] = yahooSession.Token;
                    dRow["ApplicationData"] = yahooSession.ApplicationData;
                    dRow["LoginDate"] = yahooSession.LoginDate;
                    dRow["ValidUntil"] = yahooSession.ValidUntil;
                    dRow["CreationDate"] = DateTime.Now;
                    dRow["ModifiedDate"] = DateTime.Now;
                    authenticationState.Tables[YahooSessionsTable].Rows.Add(dRow);
                }
                else
                {
                    row["ApplicationID"] = yahooSession.ApplicationId;
                    row["Token"] = yahooSession.Token;
                    row["ApplicationData"] = yahooSession.ApplicationData;
                    row["LoginDate"] = yahooSession.LoginDate;
                    row["ValidUntil"] = yahooSession.ValidUntil;
                    row["ModifiedDate"] = DateTime.Now;
                }

                // write out changes
                StreamWriter xmlSW = new StreamWriter(AuthenticationStateFileName);
                authenticationState.WriteXml(xmlSW, XmlWriteMode.WriteSchema);
                xmlSW.Close();
            }
        }

        private DataRow GetYahooSessionRow(DataSet ds)
        {
            return ds.Tables[YahooSessionsTable].Rows.Find(UserID);
        }
        
        private DataSet CreateAuthenticationState()
        {
            DataSet ds = new DataSet();
            ds.DataSetName = YahooSessionsTable + "DataSet";
            DataTable dt = new DataTable(YahooSessionsTable);
            DataColumn userIdColumn = new DataColumn("UserID", typeof(Guid));
            dt.Columns.Add(userIdColumn);
            dt.Columns.Add("ApplicationID", typeof(String));
            dt.Columns.Add("Token", typeof(String));
            dt.Columns.Add("ApplicationData", typeof(String));
            dt.Columns.Add("LoginDate", typeof(DateTime));
            dt.Columns.Add("ValidUntil", typeof(DateTime));
            dt.Columns.Add("CreationDate", typeof(DateTime));
            dt.Columns.Add("ModifiedDate", typeof(DateTime));
            dt.PrimaryKey = new DataColumn[] { userIdColumn };
            ds.Tables.Add(dt);

            DataRow dRow = dt.NewRow();
            dRow["UserID"] = Guid.NewGuid();
            dRow["ApplicationID"] = "";
            dRow["Token"] = "";
            dRow["ApplicationData"] = "";
            dRow["LoginDate"] = DateTime.MinValue;
            dRow["ValidUntil"] = DateTime.MinValue;
            dRow["CreationDate"] = DateTime.Now;
            dRow["ModifiedDate"] = DateTime.Now;
            dt.Rows.Add(dRow);
            
            return ds;
        }

        private string _authenticationStateFileName = String.Empty;
        public String AuthenticationStateFileName
        {
            get
            {
                return _authenticationStateFileName;
            }
            set
            {
                _authenticationStateFileName = value;
            }
        }

        public override bool IsLoggingEnabled
        {
            get {
                return false;
            }
        }
        
        private DataSet LoadAuthenticationState()
        {
            DataSet dataSet = null;
            if (File.Exists(AuthenticationStateFileName))
            {
                dataSet = new DataSet();
                dataSet.ReadXml(AuthenticationStateFileName, XmlReadMode.ReadSchema);
                return dataSet;
            }
            return dataSet;
        }
        
    }
}
