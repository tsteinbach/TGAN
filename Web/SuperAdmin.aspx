<%@ Page Language="C#" MasterPageFile="~/TGANMaster.master" AutoEventWireup="true" CodeFile="SuperAdmin.aspx.cs" Inherits="SuperAdmin" Title="Untitled Page" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" Runat="Server">
    <div id="newSchedule">
            <h4><asp:Label ID="lblHeadline" runat="server" Text="Anlegen eines neuen Spielplans"></asp:Label></h4>
            <asp:Label ID="lblNewSeason" runat="server" Text="Saison:"></asp:Label>
            <asp:TextBox ID="txtNewSeason" runat="server"></asp:TextBox>
            <asp:Button ID="cmdInsertSchedule0" runat="server" Text="Neue Saison anlegen" 
              OnClick="cmdInsertSchedule0_Click" />
            <p><asp:Label ID="lblRound" runat="server" Text="Spieltag von:"></asp:Label>
              <asp:DropDownList ID="dropFrom" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>26</asp:ListItem>
                <asp:ListItem>27</asp:ListItem>
                <asp:ListItem>28</asp:ListItem>
                <asp:ListItem>29</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>31</asp:ListItem>
                <asp:ListItem>32</asp:ListItem>
                <asp:ListItem>33</asp:ListItem>
                <asp:ListItem>34</asp:ListItem>
              </asp:DropDownList>
            </p>
            <p><asp:Label ID="Label1" runat="server" Text="Spieltag bis:"></asp:Label>
              <asp:DropDownList ID="dropTo" runat="server">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
                <asp:ListItem>9</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>26</asp:ListItem>
                <asp:ListItem>27</asp:ListItem>
                <asp:ListItem>28</asp:ListItem>
                <asp:ListItem>29</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>31</asp:ListItem>
                <asp:ListItem>32</asp:ListItem>
                <asp:ListItem>33</asp:ListItem>
                <asp:ListItem>34</asp:ListItem>
              </asp:DropDownList>
            </p>
            <p><asp:Button ID="cmdInsertSchedule" runat="server" Text="Spielplan erstellen" OnClick="cmdInsertSchedule_Click" /></p>
            <p><asp:TextBox ID="txtResultOfNewSchedule" runat="server" TextMode="MultiLine" Text="Fehler!" Width="499px" Height="198px"></asp:TextBox> </p>
    </div>
    <div id="newUserGroup">
            <h4><asp:Label ID="lblHeadNewUserGroup" runat="server" Text="Anlegen einer neuen Benutzergruppe"></asp:Label></h4>
            <asp:Label ID="lblCaptionNewUserGroup" runat="server" Text="Benutzergruppe:"></asp:Label>
            <asp:TextBox ID="txtNewUserGroup" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtErrorUserGroup" runat="server" Text="Error!" TextMode = "MultiLine" ></asp:TextBox>
            <p><asp:Button ID="cmdNewUserGroup" runat="server" Text="Benutzergruppe erstellen" OnClick="cmdNewUserGroup_Click"/></p>
    </div>
    <div id="setResults">
        <h4><asp:Label ID="lblHeadSetRes" runat="server" Text="Spielergebnisse setzen"></asp:Label></h4>
        <asp:Label ID="lblRoundNo" runat="server" Text="Spieltag:"></asp:Label>
        <asp:TextBox ID="txtRoundNo" runat="server"></asp:TextBox>
        <asp:Button ID="cmdSetRes" runat="server" Text="set results" OnClick="cmdSetRes_Click"/>
    </div>
    <div id="updateRound">
            <h4><asp:Label ID="lblHeadUpdateStartTime" runat="server" Text="Startzeit aktualisieren"></asp:Label></h4>
            <p><asp:Label ID="lblRoundStartTime" runat="server" Text="Spieltag:"></asp:Label>
            <asp:TextBox ID="txtRoundStartTime" runat="server"></asp:TextBox></p>
            <p><asp:Button ID="cmdUpdateStartTime" runat="server" Text="Startzeit aktualisieren" OnClick="cmdUpdateStartTime_Click"/></p>
    </div>
    <div id="sqlPart">
            <h4><asp:Label ID="lblSQL" runat="server" Text="Hier können beliebige SQL-Statements abgefeuert werden"></asp:Label></h4>
            <p><asp:Button ID="cmdDoQuery" runat="server" Text="TSQL abfeuern" OnClick="cmdDoQuery_Click"/></p>
            <asp:TextBox ID="txtTsql" runat="server" TextMode="MultiLine" Height="409px" Width="506px"></asp:TextBox><br />
            <asp:DataGrid ID="tsqlResult" runat="server"></asp:DataGrid> 
    </div>
</asp:Content>