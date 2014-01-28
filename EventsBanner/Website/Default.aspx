<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Events Banner</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Events Banner</h1>
    <asp:HyperLink ID="AdminHyperLink" runat="server" NavigateUrl="~/Admin/Default.aspx">Administation</asp:HyperLink>
    </div>

        
        <pre>
        Events Banner
        
        - Editor for basic events listing
        - Handler to publish Javascript event calls to remote Javascript API
        - Remote Javascript API to fetch and draw the display
        
        </pre>
    </form>
</body>
</html>
