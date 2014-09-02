<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Forum_TGAN.aspx.cs" Inherits="Forum_TGAN" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
        <asp:Label CssClass="headline" ID="lblForumHeader" Text="Unser Forum" runat="server"></asp:Label>
        <div class="lh4"><br /></div>
        <div id = "Forum">
            <asp:Panel ID="newTheme" runat="server" CssClass="forumPanel">
                <h4>Neue Diskussionsthemen hinzufügen</h4>
                <asp:TextBox ID="txtNewTheme" CssClass="forumControl" runat="server"></asp:TextBox>
                <div class="lh4"><br /></div>
                <asp:Button ID="cmdNewTheme" runat="server" Text="Neues Thema anlegen" OnClick="cmdNewTheme_Click" />
            </asp:Panel>
            <div class="lh4"><br /></div>
            <asp:Panel ID="chooseTheme" runat="server" CssClass="forumPanel">
                <h4>Thema auswählen</h4>
                <div class="lh4"><br /></div>
                <asp:DropDownList ID="lstThemes" CssClass="forumControl" EnableViewState="true" AutoPostBack="true" runat="server" OnSelectedIndexChanged="lstThemes_SelectedIndexChanged" ></asp:DropDownList><br />
            </asp:Panel>
            <div class="lh4"><br /></div>
            <asp:Panel ID="addComment" runat="server" CssClass="forumPanel">
                <h4>Neue Kommentare hinzufügen</h4>
                <asp:TextBox ID="txtNewContent" CssClass="forumControl" runat="server" TextMode="MultiLine"></asp:TextBox> 
                <div class="lh4"><br /></div>
                <asp:Button ID="cmdNewContent" runat="server" Text="Kommentar hinzufügen" OnClick="cmdNewContent_Click" />    
            </asp:Panel>
            <h4>Kommentare zum aktuellen Thema</h4>
            <div class="lh4"><br /></div>
            <asp:PlaceHolder ID="placeContent" runat="server" ></asp:PlaceHolder>
        </div>
</asp:Content>

