<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="loginContent" ContentPlaceHolderID="mainContent" Runat="Server">
       
    <div class="w3-row-padding w3-padding-small"> 
         
        <div class="w3-container w3-third w3-border w3-padding-large" id="loginForm">
            <h3>Login</h3>
            <div class="w3-section">
                <label><b>Benutzername</b></label>
                <asp:TextBox runat="server" ID="txtUser" CssClass="w3-input w3-border w3-margin-bottom" ToolTip="Benutzername"></asp:TextBox>

                <label><b>Passwort</b></label>
                <asp:TextBox ID="txtPassword" CssClass="w3-input w3-border w3-margin-bottom" runat="server" TextMode="Password"></asp:TextBox>
                    
                <div class="w3-bar">
                    <div class="w3-bar-item w3-padding-small w3-orange w3-third w3-block">
                        <asp:Button CssClass="w3-block w3-button" ID="group1" runat="server" onclick="cmdLoginGroup1_Click"/>
                    </div>
                    <div class="w3-bar-item w3-padding-small w3-orange w3-third w3-block">
                        <asp:Button CssClass="w3-block w3-button" ID="group2" runat="server" onclick="cmdLoginGroup2_Click"/>
                    </div>
                    <div class="w3-bar-item w3-padding-small w3-orange w3-third">
                        <asp:Button CssClass="w3-block w3-button" ID="group3" runat="server" onclick="cmdLoginGroup3_Click"/>
                    </div>
                </div>
            
                <asp:Literal ID="litError" runat="server" Visible="false"></asp:Literal>
              
            </div>
            <div class="w3-section">
                <label><b>Bisher angemeldete Benutzer:</b></label>
                <asp:Label ID="lbluserCount" runat="server"></asp:Label>
            </div>
            <div class="w3-section">
                <asp:Label ID="lblLoginnotValid" CssClass="w3-leftbar w3-border-red" runat="server" Visible = "false" Text="Login ungültig"/>
            </div>
        </div>
        
        <div id="BirthdayChildren" class="w3-container w3-third w3-border w3-padding-large">
                <div class="w3-section">
                    <asp:Label CssClass="headline" ID="geburtstagskinder" Text="" runat="server"></asp:Label>
                    <span class="lh4"><br /></span>
                    <asp:PlaceHolder ID="placebirthdayChildren" runat="server"></asp:PlaceHolder>
            </div>
        </div> 

        <div id="forumItems" class="w3-container w3-third w3-border w3-padding-large">
                 <div class="w3-section">
                    <h3>Beiträge im Forum</h3>
                    <asp:PlaceHolder ID="placeRecentForum" runat="server"></asp:PlaceHolder>
                </div>
            </div>
    </div>

    <div class="w3-row-padding w3-padding-small"> 
    
        <div class="w3-container w3-third w3-border w3-padding-small" id ="AdminMenu">
            <h3>Administration</h3>    
            <div class="w3-bar-block w3-light-blue">
                <a class="w3-button w3-bar-item"  href="MemberAdmin.aspx">Neue Mitglieder</a>
                <a class="w3-button w3-bar-item" href="TippAdmin.aspx">Tipps nachtragen</a>
                <a class="w3-button w3-bar-item" href="exportToExcel.aspx">Excel-Export</a>
                <asp:HyperLink id="SuperAdminLink" CssClass="w3-bar-item w3-text-red" Text = "Superadministration" NavigateUrl="~/superAdmin.aspx" Visible="false" runat="server"></asp:HyperLink>
            </div>
        </div>

        <div class="w3-container w3-third" id="rescheduled games">
                <asp:PlaceHolder ID="placerescheduledGames" runat="server">
                
                </asp:PlaceHolder>
        </div>

    </div>
         
    
      
        
            
        

            
        
 </asp:Content>

