<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editor</title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div id="Main">
            <sst:Editor ID="editor1" runat="server" 
                Content="<div class='regular'>Hello Editor!</div>"
                ContentSaved="editor1_ContentSaved"
                ContentSaving="editor1_ContentSaving" />

            <table border="1">
                <tr>
                    <th>Unclean</th>
                    <th>Clean</th>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <div class="SavedContent">
                            <asp:PlaceHolder ID="phSavedContent" runat="server"></asp:PlaceHolder>
                        </div>
                    </td>
                    <td style="vertical-align: top;">
                        <div class="SavedContent">
                            <asp:PlaceHolder ID="phSavedContent2" runat="server"></asp:PlaceHolder>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
