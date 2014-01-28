using System;
using System.Web.UI;
using SmallSharpTools;
using SmallSharpTools.Logging;

public partial class AlbumList : Page
{
    protected void Page_Load(object sender, EventArgs e)
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
