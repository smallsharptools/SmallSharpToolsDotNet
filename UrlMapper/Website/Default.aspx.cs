using System;
using System.Web.UI;

public partial class _Default : Page 
{
    
    protected void Page_Init(object sender, EventArgs e)
    {
        Utility.GetLogger(GetType()).Info(GetType().Name + ".Page_Init");
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.GetLogger(GetType()).Info(GetType().Name + ".Page_Load");
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Utility.GetLogger(GetType()).Info(GetType().Name + ".Page_PreRender");
    }
}
