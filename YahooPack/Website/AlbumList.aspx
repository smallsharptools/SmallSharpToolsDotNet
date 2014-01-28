<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AlbumList.aspx.cs" Inherits="AlbumList" Title="Untitled Page" %>

<%@ Register Src="ServiceView.ascx" TagName="ServiceView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:ServiceView ID="ServiceView1" runat="server" YahooServiceUrl="http://photos.yahooapis.com/V3.0/listAlbums" />

        
</asp:Content>
