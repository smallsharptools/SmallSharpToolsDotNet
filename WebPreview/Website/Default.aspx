<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Web Preview</title>
<style type="text/css">
    div.box
    {
    	width: 210px;
    	height: 160px;
    	float: left;
    }
    
    img.thumbnail
    {
        width: 200px;
        height: 150px;
        background: #ddd;
        border: 1px solid #ccc;
    }

</style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <asp:TextBox ID="TextBox1" runat="server" Width="300px" Text="http://www.digg.com/"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create Web Preview" />
            <br /><br />
            <asp:Image ID="Image1" runat="server" Visible="False" BorderWidth="1px" CssClass="thumbnail" />
            <hr />
            
            <asp:Repeater ID="rptImages" runat="server" 
                onitemdatabound="rptImages_ItemDataBound">
                <ItemTemplate>
                    <div class="box">
                        <asp:HyperLink ID="HyperLink1" runat="server">
                            <asp:Image ID="Image1" runat="server" BorderWidth="1px" AlternateText="" CssClass="thumbnail" />
                        </asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
        </div>
    </form>
</body>
</html>
