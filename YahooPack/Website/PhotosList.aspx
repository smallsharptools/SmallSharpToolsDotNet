<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PhotosList.aspx.cs" Inherits="PhotosList" Title="Photos List" %>

<%@ Register Src="ServiceView.ascx" TagName="ServiceView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:Panel ID="Panel1" runat="server" Visible="false"></asp:Panel>
    
    <asp:DataList ID="dlPhotos" runat="server" OnItemDataBound="dlPhotos_ItemDataBound" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <ItemTemplate>
            <asp:Image ID="Image1" runat="server" />
        </ItemTemplate>
    </asp:DataList>
    <uc1:ServiceView ID="ServiceView1" runat="server" YahooServiceUrl="http://photos.yahooapis.com/V3.0/listPhotos?showPerms=true" />

</asp:Content>
