<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportToExcel.aspx.cs" Inherits="ExportToExcel" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TGAN_Export to Excel</title>
    <link href="main.css" rel="Stylesheet" type="text/css" />
    
    <script type="text/javascript" language="javascript">
        function clpSet()
        {
            window.clipboardData.setData("Text","TEST1");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" />
        <div class="menueItems">
            <ul>
                    <li><a class="menue" id="lnkHome" href="Default.aspx">Startseite</a><br /></li>
                    <li><a class="menue" id="lnkMemberAdmin" href="MemberAdmin.aspx">Neue Mitglieder</a><br /></li>
                    <li><a class="menue" id="lnkTippAdmin" href="TippAdmin.aspx">Tipps nachtragen</a></li>
            </ul>
        </div>
        <asp:UpdatePanel ID="updateHeader" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblCopyinfo" runat="server" CssClass="headline"></asp:Label>
                <div>
                    <asp:DropDownList ID="dropUserGroup" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropUserGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropSpieltag" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropSpieltag_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:PlaceHolder ID="placeExportToExcel" runat="server">
                    </asp:PlaceHolder>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>


