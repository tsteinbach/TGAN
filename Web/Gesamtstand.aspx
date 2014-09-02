<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="Gesamtstand.aspx.cs" Inherits="Login" Title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <asp:UpdatePanel ID="updateGesamt" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:UpdateProgress ID="progUpdate" runat="server" AssociatedUpdatePanelID="updateGesamt">
                <ProgressTemplate>
                    <div class="progress">
                        <div id="progressBackgroundFilter"></div>
                        <div id="processMessage">Die Übersicht wird aktualisiert...<br /><br />
                        <img alt="Spinner" src="images/fussball00007.gif"/>
                        </div>
                    </div>

                </ProgressTemplate>
            </asp:UpdateProgress>
            <div>
                <p id="infoText" class="headline">Die Auswahl des aktuellen Spieltags richtet sich nach den Spielstartzeitpunkten größer als HEUTE. Die resultierende Auswahl wird AUFSTEIGEND nach Spieltag sortiert!</p>
            </div>
            
                <table class="gesamtStandConfig">
                    <tr>
                        <td colspan="3">
                            Auswahl der Saison, des Spieltags und der Tippgemeinschaft.
                        </td>
                    </tr>
                    <tr>
                        <td><asp:DropDownList ID = "lstSpieltag" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                        <td><asp:DropDownList ID = "lstSeason" AutoPostBack="true" runat="server"></asp:DropDownList></td>
                        <td><asp:DropDownList ID = "lstUserGroup" AutoPostBack="true" runat="server"></asp:DropDownList></td>
     
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID = "cmdTippOverview" runat="server" Text="Tipps pro Spieltag" onclick="cmdTippOverview_Click"/>
                            <asp:Button ID = "cmdTotalOverview" runat="server" Text="Gesamtstand" onclick="cmdTotalOverview_Click"/>
                            <div><p></p></div>
                            <div>Die Berechnung kann einen Augenblick dauern!</div>
                        </td>
                    </tr>    
                </table>
            <asp:PlaceHolder ID="overview" runat="server" EnableViewState="true"></asp:PlaceHolder>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="placeLegend" runat="server" EnableViewState="true"></asp:PlaceHolder>
 </asp:Content>

