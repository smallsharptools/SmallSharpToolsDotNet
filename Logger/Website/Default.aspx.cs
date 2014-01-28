using System;
using System.Web.UI;
using SmallSharpTools;

public partial class _Default : Page 
{
    
    protected void Page_Init(object sender, EventArgs e)
    {
        Logger.Info(GetType().Name + ".Page_Init");
        Logger.Info("Info message");
        Logger.Warn("Warn message");
        Logger.Error("Error message");
        Logger.Fatal("Fatal message");
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Logger.Info(GetType().Name + ".Page_Load");
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Logger.Info(GetType().Name + ".Page_PreRender");
    }
    
    public ILogger Logger
    {
        get
        {
            return Utility.GetLogger(GetType());
        }
    }
}
