<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<p class="errorLink">Dir fehlen die notwendigen Rechte oder bis Du noch nicht angemeldet?</p>
<asp:HyperLink ID="hypNotLggedIn" CssClass="errorLink" NavigateUrl="~/Default.aspx" Text="Hier geht es zurück zur Startseite" runat="server"></asp:HyperLink> 
</asp:Content>

