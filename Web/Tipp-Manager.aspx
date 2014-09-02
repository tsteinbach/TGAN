<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Tipp-Manager.aspx.cs" Inherits="Tipp_Manager" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
<%--<asp:UpdatePanel ID="updateTippManager" runat="server">
 <ContentTemplate>--%>
 <asp:Literal id="litError" runat = "Server" visible="false"></asp:Literal>
<table id="tblOverview" class="TippTable" cellpadding="0" cellspacing="10" runat="server">
    <tr id="showMember">
        <td colspan="4">
            <asp:Label ID="lblInfo" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <p>Die Auswahl des aktuellen Spieltags richtet sich nach den Spielstartzeitpunkten größer als HEUTE. Die resultierende Auswahl wird AUFSTEIGEND nach Spieltag sortiert! Vom ersten Spieltag in der Liste wird der vorhergehende angezeigt!</p>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:DropDownList ID="lstRoundList" runat="server" OnSelectedIndexChanged="loadRound" AutoPostBack="true">
            </asp:DropDownList>
        </td>
        <td colspan="2">
            <p><%=ACTIVEUSER %></p>
        </td>
    </tr>
    <tr class="tableHeaderRow">
        <th>Anspiel</th>
        <th>Heim</th>
        <th>Gast</th>
        <th>Tipp</th>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date1"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim1"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast1"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp1" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date2"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim2"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast2"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp2" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date3"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim3"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast3"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp3" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date4"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim4"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast4"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp4" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date5"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim5"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast5"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp5" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date6"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim6"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast6"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp6" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date7"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim7"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast7"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp7" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date8"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim8"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast8"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp8" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><asp:Label runat="server" ID="date9"></asp:Label></td>
        <td><asp:Label runat="server" ID="heim9"></asp:Label></td>
        <td><asp:Label runat="server" ID="gast9"></asp:Label></td>
        <td>
            <asp:DropDownList id="tipp9" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="roundNo"></asp:Label>
        </td>
        <td id="tipp"></td>
        <td colspan="2">
            &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cmd_TippAbgabe.gif"
                OnClick="ImageButton1_Click" /></td>
    </tr>
</table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>
<%--<act:UpdatePanelAnimationExtender ID="animatetippMng" runat="server" BehaviorID="animatenOfTippMng" TargetControlID="updateTippManager">
                <Animations>
                 <OnUpdating>
                    <Sequence>
                        <Parallel duration="0">
                            <EnableAction AnimationTarget="ImageButton1" Enabled="false"/>
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
                            <EnableAction AnimationTarget="ImageButton1" Enabled="true"/>
                        </Parallel>
                    </Sequence>
                 </OnUpdated>
                 </Animations>
      </act:UpdatePanelAnimationExtender>--%>
      <asp:PlaceHolder ID="placeLegend" runat="server" EnableViewState="true"></asp:PlaceHolder>
</asp:Content>

