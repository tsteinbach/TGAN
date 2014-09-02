using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessLayerLogic.Typemethods;
using DataLayerLogic.Types;
using System.Collections.Generic;
using System.Text;

public partial class ExportToExcel : BasePage
{
    #region properties

    public RoundBL RoundB
    {
        get
        {
            return new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS);
        }
    }

    private Round RoundOfImportance
    {
        get
        {
            return RoundB.GetRoundByRoundNo(Int32.Parse(dropSpieltag.SelectedItem.Text.Split('.').GetValue(0).ToString()));
        }
    }

    private List<RoundGame> GamesOfImportance
    {
        get
        {
            if (RoundOfImportance == null) 
                return null;
            return new RoundBL(RoundOfImportance, ACTUALSEASON, TGANConfiguration.DBACCESS).GetGames();
        }
    }

    private UserGroup UserGroupOfImportance
    {
        get
        {
            return new MemberBL(TGANConfiguration.DBACCESS).GetUserGroupByName(dropUserGroup.SelectedItem.Text);
        }
    }
    #endregion

    #region Handlers
    protected void Page_Init(object sender, EventArgs e)
    {
        if (ACTIVEMEMBER != null)
        {
            if (!ACTIVEMEMBER.IsAdministrator)
                Response.Redirect("./Error.aspx");
        }
        else
            Response.Redirect("./Error.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RoundBL rb = new RoundBL();

            dropSpieltag.DataSource = rb.Create34SpieltagArray();
            dropSpieltag.DataBind();
            dropSpieltag.SelectedIndex = ACTUALROUND.RoundNo - 1;

            dropUserGroup.DataSource = new MemberBL(TGANConfiguration.DBACCESS).GetAllNamesOfUserGroups();
            dropUserGroup.DataBind();
            dropUserGroup.Text = ACTIVEUSERGROUP.Name;
        }

        CreateOverview();
    }
    protected void dropSpieltag_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void dropUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        lblCopyinfo.Text = String.Format("In der Zwischenablage befindet sich der Tipp von {0}!",((Button)sender).ID);
    }
    
    #endregion

    private void CreateOverview()
    {
        try
        {
            List<Member> users = new MemberBL(TGANConfiguration.DBACCESS).GetAllMembers(UserGroupOfImportance,true,ACTUALSEASON);
            
            //Aufbau des Außengerüsts
            Table frame = new Table();
           
            frame.Caption = UserGroupOfImportance.Name;
            TableRow trFrame = new TableRow();
            frame.Rows.Add(trFrame);
            
            //der Spieltag wird abgerufen  
            TableCell tdGames = new TableCell();
            
            trFrame.Cells.Add(tdGames);
            Table tblGames = new Table();
            tblGames.CssClass = "excelOverviewGames"; 
            tdGames.Controls.Add(tblGames);
            setRoundData(tblGames);
            setTippData(users, trFrame);
            
            placeExportToExcel.Controls.Add(frame);
        }
        catch (Exception ex)
        { throw ex; }
    }

    private void setTippData(List<Member> users, TableRow trFrame)
    {
        //die Tipps der einzelnen Mitglieder werden ausgegeben
        foreach (Member m in users)
        {
            TippValue[] tippLine = null;
            TableCell tdTipps = new TableCell();
            trFrame.Cells.Add(tdTipps);
            Table tblTipp = new Table();
            tblTipp.CssClass = "excelOverViewTipps"; 
            tdTipps.Controls.Add(tblTipp);

            Tipp t = TGANConfiguration.DBACCESS.GetTippedValues(RoundOfImportance, ACTUALSEASON, m, GamesOfImportance);
            tippLine = new TippValue[t.GivenTipps.Values.Count];
            t.GivenTipps.Values.CopyTo(tippLine, 0);

            TableRow trTipp = new TableRow();
            
            tblTipp.Rows.Add(trTipp);
            TableHeaderCell thTipp = new TableHeaderCell();
            thTipp.CssClass = "excelOverViewTippsTH"; 
            thTipp.Text = t.TGANMember.UserName;
            trTipp.Cells.Add(thTipp);

            //Table für die Zwischenablage
            StringBuilder clipboard = new StringBuilder("<table>");

            for (int i = 0; i < 9; i++)
            {
                clipboard.Append("<tr>");

                trTipp = new TableRow();
                trTipp.CssClass = "excelOverViewTippsTR"; 
                tblTipp.Rows.Add(trTipp);

                clipboard.Append("<td>");

                TableCell tdTipp = new TableCell();
                if (tippLine.Length != 9)
                    tdTipp.Text = TippBL.GetStringTippValue(TippValue.NotSet);
                else
                {
                    string sTipp = TippBL.GetStringTippValue(tippLine[i]); 
                    if(tippLine[i] == TippValue.NotSet)
                        clipboard.Append("-");
                    else
                        clipboard.Append(sTipp);
                    tdTipp.Text = sTipp;
                }

                clipboard.Append("</td>");
                clipboard.Append("</tr>");

                trTipp.Cells.Add(tdTipp);
            }

            clipboard.Append("</table>");

            trTipp = new TableRow();
            trTipp.CssClass = "excelOverViewTippsTR";
            TableCell btnTipp = new TableCell();
            trTipp.Cells.Add(btnTipp);
            tblTipp.Rows.Add(trTipp);
            
            Button btn = new Button();
            btn.ID = t.TGANMember.UserName; 
            btn.Text = "Tipp kopieren";
            btn.Click += new EventHandler(btn_Click);
            btn.Attributes.Add("onClick", "window.clipboardData.setData(\"Text\",\""+clipboard.ToString()+"\");");
            btnTipp.Controls.Add(btn);
        }
    }


    Unit height = new Unit(100, UnitType.Pixel);

    private void setRoundData(Table tblGames)
    {
        TableCell tdGames;
        TableRow trGames = new TableRow();
        
        tblGames.Rows.Add(trGames);
        TableHeaderCell thGames = new TableHeaderCell();
        thGames.Text = "Spiel";
        trGames.Cells.Add(thGames);
        thGames = new TableHeaderCell();
        thGames.Text = "Ergebnis";
        trGames.Cells.Add(thGames);
        thGames = new TableHeaderCell();
        thGames.Text = "Sollwert";
        trGames.Cells.Add(thGames);

        for (int i = 0; i < GamesOfImportance.Count; i++)
        {
            trGames = new TableRow();
            
            //Spielpaarung
            tdGames = new TableCell();
            tdGames.Text = String.Format("{0} - {1}"
                                    , RoundB.SelectTeamName(GamesOfImportance[i].HomeTeam)
                                    , RoundB.SelectTeamName(GamesOfImportance[i].AwayTeam));
            trGames.Cells.Add(tdGames);
            //Ergebnis
            tdGames = new TableCell();
            tdGames.Text = GamesOfImportance[i].Result;
            trGames.Cells.Add(tdGames);
            //Sollwert
            tdGames = new TableCell();
            tdGames.Text = TippBL.GetStringTippValue(TippBL.GetTendenz(GamesOfImportance[i].Result));
            trGames.Cells.Add(tdGames);

            tblGames.Rows.Add(trGames);
        }

        trGames = new TableRow();
        tdGames = new TableCell();
        tdGames.ColumnSpan = 3;
        trGames.Cells.Add(tdGames);
        tblGames.Rows.Add(trGames);

    }
    
}
