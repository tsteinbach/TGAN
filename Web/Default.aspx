<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page"%>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="loginContent" ContentPlaceHolderID="mainContent" Runat="Server">
            <div id="login">
                    <label>Benutzername:</label>
                    <div class="lh4"><br /></div>
                    <asp:TextBox ID="txtUser" runat="server" Width="147px"></asp:TextBox><br />
                    <div class="lh4"><br /></div>
                    <label>Passwort:</label>
                    <div class="lh4"><br /></div>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="147px"></asp:TextBox>
                    <div class="lh4"><br /></div>
                    <table id="loginCmdTable">
                        <tr>
                            <td>
                                <asp:Button CssClass="loginCmd" ID="group1" runat="server" onclick="cmdLoginGroup1_Click"/>
                            </td>
                            <td>
                                <asp:Button CssClass="loginCmd" ID="group2" runat="server" onclick="cmdLoginGroup2_Click"/>
                            </td>
                        </tr>
                    </table>
                    <div class="lh4"><br /></div>
                    <div class="lh4"><br /></div>
                    <p><asp:Label ID="lblLoginnotValid" runat="server" Visible = "false" Text="Login ungültig"/></p>
                    <div class="lh4"><br /></div>
                    <asp:Literal ID="litError" runat="server" Visible="false"></asp:Literal>
                    <div class="usercount"><asp:Literal ID="lbluserCount" runat="server"></asp:Literal></div>
            </div>
            <div>
                <asp:PlaceHolder ID="placerescheduledGames" runat="server">
                
                </asp:PlaceHolder>
            </div>
            <div id="untenDefault">
                    <div id="divSuperAdmin">
                        <%--<asp:UpdatePanel id="updatesuperAdmin" runat="server" UpdateMode="Always">
                            <ContentTemplate>--%>        
                                <div>
                                    <asp:HyperLink id="SuperAdminLink" CssClass="menue" Text = "Superadministration" NavigateUrl="~/superAdmin.aspx" Visible="false" runat="server"></asp:HyperLink>
                                </div>
                            <%--</ContentTemplate>   
                        </asp:UpdatePanel>--%>
                    </div>
                    <div id="BirthdayChildren">
                        <asp:Label CssClass="headline" ID="geburtstagskinder" Text="" runat="server"></asp:Label>
                        <span class="lh4"><br /></span>
                        <asp:PlaceHolder ID="placebirthdayChildren" runat="server"></asp:PlaceHolder>
                    </div>
                    <div id="forumItems">
                            <asp:Label CssClass="headline" ID="Forum" Text="Beiträge im Forum" runat="server"></asp:Label>
                            <span class="lh4"><br /></span>
                            <asp:PlaceHolder ID="placeRecentForum" runat="server"></asp:PlaceHolder>
                    </div>
            </div>
 </asp:Content>

