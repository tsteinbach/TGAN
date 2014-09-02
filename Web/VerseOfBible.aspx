<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="VerseOfBible.aspx.cs" Inherits="VerseOfBible" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <div class="whiteBackground">
        <asp:Label CssClass="headline" ID="BibleHeadline" Text="Der heutige Vers aus der Bibel" runat="server"></asp:Label>
        <span class="lh4"><br /></span>
        <table class="centerControl">
            <tr>
                <td>
                    <script type="text/javascript" src="http://www.gmodules.com/ig/ifr?url=http://plom.be/gadgetfactory.de/losung/gadget.xml&amp;up_fontisize=10px&amp;synd=open&amp;w=320&amp;h=190&amp;title=Losung+des+Tages&amp;border=http%3A%2F%2Fwww.gmodules.com%2Fig%2Fimages%2F&amp;output=js"></script>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

