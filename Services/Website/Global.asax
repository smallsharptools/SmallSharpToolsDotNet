<%@ Import namespace="SmallSharpTools.Services.Provider"%>
<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        ServicesAdmin.Instance.Startup();
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        ServicesAdmin.Instance.Shutdown();
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
    }

    void Session_Start(object sender, EventArgs e) 
    {
    }

    void Session_End(object sender, EventArgs e) 
    {
    }
       
</script>
