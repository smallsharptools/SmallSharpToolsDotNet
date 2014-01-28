<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JQuery.aspx.cs" Inherits="JQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>jQuery: Color Change</title>
    <style type="text/css">
    body
    {
        font-size: 12px;
        font-family: arial, verdana, san-serif;
    }
    ul#thelist li 
    {
        font-size: 20px;
        color: blue;
    }
    ul#thelist li.red 
    {
        color: red;
        font-size: 22px;
    }
    ul#thelist li.gray 
    {
        color: gray;
        font-size: 16px;
    }
    </style>
    <script type="text/javascript">
    function changeList() {
        var liSelector = "#thelist > li";
        var listItems = $(liSelector);
        var buttonSelector = "#button";
        var btn = $(buttonSelector);
        if (listItems && btn) {
            if (listItems.attr("class") == null) {
                listItems.removeClass("blue");
                listItems.addClass("red");
                btn.attr("value", "Change to Gray");
            }
            else if (listItems.attr("class") == "red") {
                listItems.removeClass("red");
                listItems.addClass("gray");
                btn.attr("value", "Change to Blue");
            }
            else {
                listItems.removeClass("gray");
                btn.attr("value", "Change to Red");
            }
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>jQuery: Color Change</h3>
        <p>Click the button and to change the color.  Click again to change it again.</p>
        <input id="button" type="button" value="Change to Red" onclick="changeList();return false;" /><br />
        <ul id="thelist">
            <li>One</li>
            <li>Two</li>
            <li>Three</li>
            <li>Four</li>
        </ul>
    </div>
    <sst:EmbeddedScriptsManager ID="esm1" runat="server" UseJQueryContextMenuScript="True" UseJQueryInterfaceScript="True" />
    </form>
</body>
</html>
