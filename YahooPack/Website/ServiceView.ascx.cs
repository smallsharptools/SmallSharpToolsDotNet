using System;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SmallSharpTools;
using SmallSharpTools.Logging;
using SmallSharpTools.YahooPack.Providers;
using SmallSharpTools.YahooPack.Web.Security;

public partial class ServiceView : UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        YahooSession yahooSession = YahooAuthenticationProvider.Instance.LoadSession();

        if (yahooSession.IsAuthenticated)
        {

            try
            {
                DataSet dsServices;
                dsServices = yahooSession.GetService(new Uri(YahooServiceUrl));

                foreach (DataTable dt in dsServices.Tables)
                {
                    Label label = new Label();
                    label.Text = dt.TableName;
                    GridView gv = new GridView();
                    gv.ID = dt.TableName + "GridView";
                    gv.DataSource = dt;
                    gv.DataBind();
                    Panel1.Controls.Add(label);
                    Panel1.Controls.Add(gv);
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Failure with photos url: " + YahooServiceUrl, ex);
            }
        }

    }

    private String _serviceUrl;

    [Category("Behavior"),Browsable(true),DefaultValue("")]
    public String YahooServiceUrl
    {
        get
        {
            return _serviceUrl;
        }
        set
        {
            Label1.Text = value;
            _serviceUrl = value;
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
