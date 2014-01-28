<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SmallSharpTools.YahooPack</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <sst:YahooLoginStatus ID="yahooLoginStatus" runat="server" />
        <br />
        <br />
        
        <table>
            <tr>
                <td style="text-align: right;">
                    Application ID:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblApplicationID" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Token:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblToken" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Applicaton Data:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblApplicationData" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Login:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblLoginDateTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Token Expiration:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblTokenExpiration" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Valid Until:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblValidUntil" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Current Date and Time:</td>
                <td style="text-align: left;">
                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        
        <h3>Photos</h3>
        <ul>
        <li>
            <asp:HyperLink ID="hlServicesList" runat="server" NavigateUrl="~/PhotoServices.aspx">Services List</asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="hlAlbumsList" runat="server" NavigateUrl="~/AlbumList.aspx">Albums List</asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="hlPhotosList" runat="server" NavigateUrl="~/PhotosList.aspx">Photos List</asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="hlPhotosTags" runat="server" NavigateUrl="~/PhotosTags.aspx">Photos Tags</asp:HyperLink></li>
        </ul>
    </div>
    </form>
</body>
</html>
