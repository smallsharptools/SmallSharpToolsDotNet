<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TooltipDemo.aspx.cs" Inherits="TooltipDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>jTip</title>
    <link href="jTip.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <sst:EmbeddedScriptsManager ID="EmbeddedScriptsManager1" runat="server" UseJQueryScript="True" UseJQueryTooltipScript="True" />
        <br />
        <a href="TooltipContent.aspx?name=Tooltip%20Content" class="jTip" id="link" name="This is a tooltip">scroll over for tooltip</a> 
        <br />
        <p>Using <a href="http://www.codylindley.com/blogstuff/js/jtip/">jTip</a></p>
    </div>
    </form>
</body>
</html>
