<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="loginContent" ContentPlaceHolderID="mainContent" Runat="Server">
            
    <div class="w3-container w3-section">        
        <div id="login" class="w3-card-12 w3-dark-grey" style="width:25%">
                    <label>Benutzername:</label>
                    <div class="lh4"><br /></div>
                    <asp:TextBox ID="txtUser" CssClass="w3-input" runat="server"></asp:TextBox><br />
                    <div class="lh4"><br /></div>
                    <label>Passwort:</label>
                    <div class="lh4"><br /></div>
                    <asp:TextBox ID="txtPassword" CssClass="w3-input" runat="server" TextMode="Password"></asp:TextBox>
                    <div class="lh4"><br /></div>
                   
                    <div class="w3-container w3-section">
                        <asp:Button CssClass="w3-btn w3-round-large w3-orange w3-small" ID="group1" runat="server" onclick="cmdLoginGroup1_Click"/>
                        <asp:Button CssClass="w3-btn w3-round-large w3-orange w3-small" ID="group2" runat="server" onclick="cmdLoginGroup2_Click"/>
                        <asp:Button CssClass="w3-btn w3-round-large w3-orange w3-small" ID="group3" runat="server" onclick="cmdLoginGroup3_Click"/>
                    </div>
                    
                    <p><asp:Label ID="lblLoginnotValid" runat="server" Visible = "false" Text="Login ungültig"/></p>
                    <asp:Literal ID="litError" runat="server" Visible="false"></asp:Literal>
                    <div class="usercount"><asp:Literal ID="lbluserCount" runat="server"></asp:Literal></div>
            </div>
        </div>
            <div class="w3-container w3-section">
                <asp:PlaceHolder ID="placerescheduledGames" runat="server">
                
                </asp:PlaceHolder>
            </div>
            
            <div id="BirthdayChildren" class="w3-container w3-section">
                <asp:Label CssClass="headline" ID="geburtstagskinder" Text="" runat="server"></asp:Label>
                <span class="lh4"><br /></span>
                <asp:PlaceHolder ID="placebirthdayChildren" runat="server"></asp:PlaceHolder>
            </div>
            <div id="forumItems" class="w3-container w3-section">
                    <asp:Label CssClass="headline" ID="Forum" Text="Beiträge im Forum" runat="server"></asp:Label>
                    <span class="lh4"><br /></span>
                    <asp:PlaceHolder ID="placeRecentForum" runat="server"></asp:PlaceHolder>
            </div>
            
        <div class="w3-container w3-section">
            <div class="w3-card-12" style="width:25%">    
                    <ul class="w3-ul w3-border w3-tiny">
                        <li><h5>Administration</h5></li>
                        <li><a class="w3-hover-none w3-text-black w3-hover-text-white"  href="MemberAdmin.aspx">Neue Mitglieder</a></li>
                        <li><a class="w3-hover-none w3-text-black w3-hover-text-white" href="TippAdmin.aspx">Tipps nachtragen</a></li>
                        <li><a class="w3-hover-none w3-text-black w3-hover-text-white" href="exportToExcel.aspx">Excel-Export</a></li>
                        <li><asp:HyperLink id="SuperAdminLink" CssClass="w3-hover-none w3-text-red w3-hover-text-white" Text = "Superadministration" NavigateUrl="~/superAdmin.aspx" Visible="false" runat="server"></asp:HyperLink></li>
                    </ul> 
            </div>
        </div>
 </asp:Content>

