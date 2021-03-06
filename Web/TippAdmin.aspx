<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="TippAdmin.aspx.cs" Inherits="TippAdmin" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
<p>Willkommen im Administrationsbereich</p>
<asp:Literal id="litError" runat = "Server" visible="false"></asp:Literal>

<asp:DropDownList ID="lstMembers" runat="server" OnSelectedIndexChanged="changeActiveMember" AutoPostBack="true">
</asp:DropDownList>  
<asp:DropDownList ID="lstRoundList" runat="server" OnSelectedIndexChanged="loadRound" AutoPostBack="true">
</asp:DropDownList>
<asp:Label ID="lblInfo" runat="server"></asp:Label>

<table id="tblOverview" class="w3-table w3-border w3-hoverable">
    <tr>
        <th class="w3-border">Anspiel</th>
        <th class="w3-border">Heim</th>
        <th class="w3-border">Gast</th>
        <th class="w3-border">Tipp</th>
        <th class="w3-border">Spiel wird verschoben</th>
        <th class="w3-border">Ergebnis</th>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date1"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check1" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result1" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date2"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check2" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result2" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date3"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check3" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result3" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date4"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check4" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result4" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date5"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check5" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result5" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date6"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check6" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result6" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date7"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check7" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result7" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date8"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check8" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result8" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border"><asp:TextBox runat="server" ID="date9"></asp:TextBox></td>
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
        <td class="w3-border">
            <asp:CheckBox ID="check9" runat="server" Text="Spiel wird verschoben?" Checked="false"/>
        </td>
        <td class="w3-border">
            <asp:TextBox ID="result9" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="w3-border">
            <asp:Label runat="server" ID="roundNo"></asp:Label>
        </td>
        <td id="tipp"></td>
        <td colspan="2">
            <asp:Button runat="server" ID="cmdGetBet" Text="Tipp bestštigen" OnClick="cmdGetBet_Click" />
        </td>
        <td colspan="2">
            <asp:Button runat="server" ID="cmdChangeRoundData" Text="Spieldaten anpassen" OnClick="cmdChangeRoundData_Click"/>
        </td>
    </tr>
</table>
</asp:Content>
