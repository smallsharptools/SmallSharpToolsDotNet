<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SmallSharpTools.EmbeddedScripts</title>
    <script type="text/javascript" src="ui.js"></script>
    <style type="text/css">
    body
    {
        font-size: 12px;
        font-family: arial, verdana, san-serif;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>SmallSharpTools.EmbeddedScripts</h1>
    <ul>
    <li><a href="AutoCompleteDemo.aspx">Scriptaculous: Auto-Complete Demo</a></li>
    <li><a href="DragAndDropDemo.aspx">Scriptaculous: Drag and Drop Demo</a></li>
    <li><a href="JQuery.aspx">jQuery: Change Color Demo</a></li>
    <li><a href="TooltipDemo.aspx">jTip: Tooltip Demo (jQuery plugin)</a></li>
    </ul>
    <div id="links">
    </div>
    <script type="text/javascript">
    showLinks('links');
    </script>
    <br />

    <sst:EmbeddedScriptsManager ID="esm1" runat="server" UseJQueryScript="True" UseJQueryInterfaceScript="True" UseJQueryJsonScript="True" UseJQueryContextMenuScript="True" UseJQueryDimensionsScript="True" UseJQueryTooltipScript="True" />
    </div>
    </form>
</body>
</html>