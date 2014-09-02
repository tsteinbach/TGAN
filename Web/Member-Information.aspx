<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Member-Information.aspx.cs" Inherits="Member_Information" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<%--<asp:UpdatePanel runat="server" ID="updateMember">
<ContentTemplate>--%>
<DIV ID="MemberInfoOverview"><TABLE><TBODY><TR><TD>Benutzerinformationen:</TD><TD><asp:DropDownList id="lstUsers" runat="server" OnSelectedIndexChanged="lstUsers_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></TD></TR><TR><TD>Benutzername:</TD><TD><asp:Label id="lblUserName" runat="server"></asp:Label></TD></TR><TR><TD>Benutzergruppe:</TD><TD><asp:Label id="lblUserGroup" runat="server"></asp:Label></TD></TR><TR><TD>Vorname:</TD><TD><asp:TextBox id="txtFirstName" runat="server"></asp:TextBox></TD></TR><TR><TD>Nachname:</TD><TD><asp:TextBox id="txtLastName" runat="server"></asp:TextBox></TD></TR><TR><TD>Adresse:</TD><TD><asp:TextBox id="txtAdresse" runat="server"></asp:TextBox></TD></TR><TR><TD>PLZ:</TD><TD><asp:TextBox id="txtPlz" runat="server"></asp:TextBox></TD></TR><TR><TD>Ort:</TD><TD><asp:TextBox id="txtOrt" runat="server"></asp:TextBox></TD></TR><TR><TD>Titel:</TD><TD><asp:Label id="lblTitle" runat="server"></asp:Label></TD></TR><TR><TD>EMail:</TD><TD><asp:TextBox id="txtEMail" runat="server"></asp:TextBox></TD></TR><TR><TD>Telefon:</TD><TD><asp:TextBox id="txtPhone" runat="server"></asp:TextBox></TD></TR><TR><TD>Passwort:</TD><TD><asp:TextBox id="txtPassword" runat="server" TextMode="Password"></asp:TextBox></TD></TR>
<TR><TD>Geburtstag:</TD><TD><asp:TextBox id="txtGeburtstag" runat="server"></asp:TextBox></TD></TR>
<TR><TD>Dabei seit:</TD><TD><asp:DropDownList id="dropMemberSince" runat="server" 
    Enabled="False"></asp:DropDownList></TD></TR>
<TR><TD>Dabei bis:</TD><TD><asp:DropDownList id="dropMemberTo" runat="server" 
    Enabled="False"></asp:DropDownList></TD></TR>
<TR><TD colSpan=2><asp:CheckBox id="isAdministrator" runat="server" Text="Administrator?" Enabled="false"></asp:CheckBox></TD></TR><TR><TD colSpan=2><asp:Button id="cmdChangeUserInfo" onclick="cmdChangeUserInfo_Click" runat="server" Text="Änderungen bestätigen"></asp:Button></TD></TR></TBODY></TABLE>
<br />
<asp:Panel id="panelChangePW" runat="server" GroupingText="Passwort abändern?">
    <p>Neues Passwort: <asp:TextBox id="txtNewPW" runat="server"></asp:TextBox></p>
    <br />
    <p><asp:Button id="cmdChangePW" onclick="cmdChangePW_Click" runat="server" Text="Passwort ändern"></asp:Button></p>
</asp:Panel> </DIV>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
     <%-- <act:UpdatePanelAnimationExtender ID="animateMember" runat="server" BehaviorID="animatenOfMember" TargetControlID="updateMember">
                <Animations>
                 <OnUpdating>
                    <Sequence>
                        <Parallel duration="0">
                            <EnableAction AnimationTarget="cmdChangePW" Enabled="false"/>
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
                        </Parallel>
                    </Sequence>
                 </OnUpdated>
                 </Animations>
      </act:UpdatePanelAnimationExtender>--%>
</asp:Content>

