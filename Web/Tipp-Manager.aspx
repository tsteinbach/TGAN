<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Tipp-Manager.aspx.cs" Inherits="Tipp_Manager" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">

 <asp:Literal id="litError" runat = "Server" visible="false"></asp:Literal>
 <asp:Label ID="lblInfo" runat="server"></asp:Label>
 <p>Die Auswahl des aktuellen Spieltags richtet sich nach den Spielstartzeitpunkten größer als HEUTE. Die resultierende Auswahl wird AUFSTEIGEND nach Spieltag sortiert! Vom ersten Spieltag in der Liste wird der vorhergehende angezeigt!</p>
<asp:DropDownList ID="lstRoundList" runat="server" OnSelectedIndexChanged="loadRound" AutoPostBack="true">
</asp:DropDownList>
<p><%=ACTIVEUSER %></p>         

<table id="tblOverview" class="w3-table w3-border w3-hoverable" runat="server">
    <tr>
        <th class="w3-border">Anspiel</th>
        <th class="w3-border">Heim</th>
        <th class="w3-border">Gast</th>
        <th class="w3-border">Tipp</th>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date1"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim1"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast1"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp1" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date2"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim2"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast2"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp2" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date3"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim3"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast3"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp3" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date4"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim4"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast4"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp4" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date5"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim5"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast5"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp5" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date6"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim6"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast6"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp6" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date7"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim7"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast7"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp7" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date8"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim8"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast8"></asp:Label></td>
        <td class="w3-border">
            <asp:DropDownList id="tipp8" runat="server">
                <asp:ListItem Selected="True">0</asp:ListItem>
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>~</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:Label runat="server" ID="date9"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="heim9"></asp:Label></td>
        <td class="w3-border"><asp:Label runat="server" ID="gast9"></asp:Label></td>
        <td class="w3-border">
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

      <asp:PlaceHolder ID="placeLegend" runat="server" EnableViewState="true"></asp:PlaceHolder>
</asp:Content>

