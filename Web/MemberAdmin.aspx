<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="MemberAdmin.aspx.cs" Inherits="MemberAdmin" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
 
 <%--<asp:UpdatePanel ID="updateMemberAdmin" runat="server">
 <ContentTemplate>--%>
<DIV id="MemberInfoOverview">
    <table>
        <tr>
            <td>
                Benutzer der ausgewählten Benutzergruppe:
            </td>
            <td><asp:DropDownList id="lstUsers" runat="server"></asp:DropDownList>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>Benutzergruppe:</td>
            <td><asp:DropDownList id="lstUserGroup" runat="server" OnSelectedIndexChanged="lstUserGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            <td></td>
        </tr>
        <tr>
            <td>Benutzername:</td>
            <td><asp:TextBox id="txtUserName" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator id="requUserName" runat="server" ErrorMessage="Bitte Benutzername eingeben" ControlToValidate="txtUserName"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>Vorname:</td>
            <td><asp:TextBox id="txtFirstName" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator id="valFirstName" runat="server" ErrorMessage="Ein Vorname ist notwendig" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>Nachname:</td>
            <td><asp:TextBox id="txtLastName" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator id="valNachname" runat="server" ErrorMessage="Ein Nachname ist notwendig" ControlToValidate="txtLastName"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>Adresse:</td>
            <td><asp:TextBox id="txtAdresse" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>PLZ:</td>
            <td><asp:TextBox id="txtPlz" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Ort:</td>
            <td><asp:TextBox id="txtOrt" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Titel:</td>
            <td><asp:DropDownList id="ddlTitle" runat="server" EnableViewState="true"></asp:DropDownList></td>
            <td></td>
        </tr>
        <tr>
            <td>EMail:</td>
            <td><asp:TextBox id="txtEMail" runat="server"></asp:TextBox></td></tr>
            <td></td>
        <tr>
            <td>Telefon:</td>
            <td><asp:TextBox id="txtPhone" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td>Passwort:</td>
            <td><asp:TextBox id="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator id="valPassword" runat="server" ErrorMessage="Ein Passwort ist notwendig" ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>Geburtstag:</td>
            <td><asp:TextBox id="txtGeburtstag" runat="server"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator id="valBirthday" runat="server" ErrorMessage="Ein Geburtsdatum ist notwendig" ControlToValidate="txtGeburtstag"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>Dabei seit:</td>
            <td colspan="2"><asp:DropDownList id="dropMemberSince" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Dabei bis:</td>
            <td colspan="2"><asp:DropDownList id="dropMemberTo" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan=2><asp:CheckBox id="isAdministrator" runat="server" Text="Administrator?"></asp:CheckBox></td>
            <td></td>
        </tr>
        <tr>
            <td colspan=2><asp:Button id="cmdChangeUserInfo" onclick="cmdChangeUserInfo_Click" runat="server" Text="Neuer Benutzer anlegen"></asp:Button></td>
            <td></td>
    </tr>
</table>
    <asp:TextBox id="txtErrorMsg" cssclass="errorMsg" runat="server" textmode="MultiLine" text="mögl. Fehler"></asp:TextBox>
    <asp:Panel id="panelChangePW" runat="server" GroupingText="Passwort abändern?">
        <p>Neues Passwort: <asp:TextBox id="txtNewPW" runat="server"></asp:TextBox></p>
        <br />
        <p><asp:Button id="cmdChangePW" onclick="cmdChangePW_Click" runat="server" Text="Passwort ändern"></asp:Button></p>
</asp:Panel> </DIV>
<%--</ContentTemplate>   
</asp:UpdatePanel>--%>
<%--<act:UpdatePanelAnimationExtender ID="animateMemberAdmin" runat="server" BehaviorID="animatenOfMemberAdmin" TargetControlID="updateMemberAdmin">
                <Animations>
                 <OnUpdating>
                    <Sequence>
                        <Parallel duration="0">
                            <EnableAction AnimationTarget="cmdChangePW" Enabled="false"/>
                            <EnableAction AnimationTarget="cmdChangeUserInfo" Enabled="false"/>
                        </Parallel>
                        <Parallel duration=".25" Fps="30">
                            <FadeOut AnimationTarget="up_container" minimumOpacity=".2"/>
                        </Parallel>
                    </Sequence>
                 </OnUpdating>
                 <OnUpdated>
                    <Sequence>
                        <Parallel duration=".25" Fps="30">
                            <FadeIn AnimationTarget="up_container" minimumOpacity=".2"/>
                        </Parallel>
                        <Parallel duration="0">
                            <EnableAction AnimationTarget="cmdChangePW" Enabled="true"/>
                            <EnableAction AnimationTarget="cmdChangeUserInfo" Enabled="true"/>
                        </Parallel>
                    </Sequence>
                 </OnUpdated>
                 </Animations>
      </act:UpdatePanelAnimationExtender>--%>
</asp:Content>

