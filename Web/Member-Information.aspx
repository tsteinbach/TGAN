<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Member-Information.aspx.cs" Inherits="Member_Information" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <div class="w3-container w3-border w3-padding-16" id="MemberInfoOverview">
        <div class="w3-section">
            <label><b>Benutzerinformationen von:</b></label>
            <p><asp:DropDownList id="lstUsers" runat="server" OnSelectedIndexChanged="lstUsers_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></p>

            <label><b>Benutzername</b></label>
            <asp:TextBox id="lblUserName" CssClass="w3-input w3-border w3-margin-bottom" Enabled ="false" runat="server"></asp:TextBox>
             
            <label><b>Benutzergruppe</b></label>
            <asp:TextBox id="lblUserGroup" CssClass="w3-input w3-border w3-margin-bottom" Enabled ="false" runat="server"></asp:TextBox>
             
            <label><b>Vorname</b></label>
            <asp:TextBox id="txtFirstName" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>Nachname</b></label>
            <asp:TextBox id="txtLastName" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>Adresse</b></label>
            <asp:TextBox id="txtAdresse" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>PLZ</b></label>
            <asp:TextBox id="txtPlz" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>Ort</b></label>
            <asp:TextBox id="txtOrt" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>Titel</b></label>
            <asp:Label id="lblTitle" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:Label>

            <label><b>E-Mail</b></label>
            <asp:TextBox id="txtEMail" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>Telefon</b></label>
            <asp:TextBox id="txtPhone" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <label><b>Passwort</b></label>
            <asp:TextBox id="txtPassword" CssClass="w3-input w3-border w3-margin-bottom" runat="server" TextMode="Password"></asp:TextBox>

            <label><b>Geburtstag</b></label>
            <asp:TextBox id="txtGeburtstag" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>

            <div class="w3-section" style="width:15%">
                <label>
                <b>Dabei seit:</b>
               </label>
                <span class="w3-right">
                    <asp:DropDownList id="dropMemberSince" CssClass="w3-border w3-margin-bottom" runat="server" Enabled="False">
                    </asp:DropDownList>
                </span>
            </div>

            <div class="w3-section" style="width:15%">
                <label>
                <b>Dabei bis:</b>
               </label>
                <span class="w3-right">
                    <asp:DropDownList id="dropMemberTo" CssClass="w3-border w3-margin-bottom" runat="server" Enabled="False">
                    </asp:DropDownList>
                </span>
            </div>

            <div class="w3-section" style="width:15%">
                <label>
                <b>Ist Administrator:</b>
               </label>
                <span class="w3-right">
                    <asp:CheckBox id="isAdministrator" CssClass="w3-check w3-margin-bottom" runat="server" Enabled="false"></asp:CheckBox>
                </span>
            </div>
            
            <asp:Button id="cmdChangeUserInfo" CssClass="w3-btn w3-round-large w3-orange w3-small" onclick="cmdChangeUserInfo_Click" runat="server" Text="Änderungen bestätigen"></asp:Button>

            <asp:Panel id="panelChangePW" runat="server" GroupingText="Passwort abändern?">
                <label><b>Neues Passwort</b></label> 
                <asp:TextBox id="txtNewPW" CssClass="w3-input w3-border w3-margin-bottom" runat="server"></asp:TextBox>
                <asp:Button id="cmdChangePW" CssClass="w3-btn  w3-round-large w3-orange w3-small" onclick="cmdChangePW_Click" runat="server" Text="Passwort ändern"></asp:Button>
            </asp:Panel> 

        </div>    
    </div>
</asp:Content>

