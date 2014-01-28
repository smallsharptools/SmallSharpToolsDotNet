<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditorControl.ascx.cs" Inherits="Controls_EditorControl" %>
<asp:TextBox ID="tbContent" runat="Server" Height="300px" TextMode="MultiLine" Width="500px"></asp:TextBox>&nbsp;<br />
<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
<asp:Button ID="btnCancel" runat="server" Text="Cancel" />
