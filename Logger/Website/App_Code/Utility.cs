using System;
using log4net;
using SmallSharpTools;
using SmallSharpTools.Logging;

/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{  
    
    public static ILogger GetLogger(Type type)
    {
        return LoggingProvider.Instance.GetLogger(type);
    }

}
