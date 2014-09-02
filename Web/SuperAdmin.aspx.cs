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
using DataLayerLogic.Types;
using BusinessLayerLogic.Typemethods;
using BusinessLayerLogic;
using System.Collections.Generic;
using System.Text;

public partial class SuperAdmin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "TGAN-Administration";
        if(!IsPostBack)
        {
            if (ACTIVEMEMBER != null)
            {
                if (!ACTIVEMEMBER.IsSuperAdmin)
                    Response.Redirect("./Error.aspx");
            }
            else
                Response.Redirect("./Error.aspx");
        }
    }

    protected void cmdInsertSchedule0_Click(object sender, EventArgs e)
    {
      try
      {
        if (String.IsNullOrEmpty(txtNewSeason.Text))
        {
          txtResultOfNewSchedule.Text = "Der Name der neuen Saison muss gesetzt werden!";
          return;
        }

        Season season = new Season(Guid.NewGuid(), txtNewSeason.Text, true,0);
        TGANConfiguration.DBACCESS.InsertActualSeason(season);
        TGANConfiguration.DBACCESS.InsertNewSeasonRounds(season);

        txtResultOfNewSchedule.Text = "Die Saison wurde aktualisiert!";
      }
      catch (Exception ex)
      {
        txtResultOfNewSchedule.Text = ex.ToString();
      }



    }

    protected void cmdInsertSchedule_Click(object sender, EventArgs e)
    {
      StringBuilder log = new StringBuilder();

      try
      {
        Season season = new SeasonBL(TGANConfiguration.DBACCESS).GetActualSeason();

        int resultFrom = 0;
        Int32.TryParse(dropFrom.Text, out resultFrom);
        int resultTo = 0;
        Int32.TryParse(dropTo.Text, out resultTo);
       
        
        for (int i = resultFrom; i <= resultTo; i++)
        {
          Round round = new RoundBL(season, TGANConfiguration.DBACCESS).GetRoundByRoundNo(i);
          List<RoundGame> games = new RoundBL(round,season, TGANConfiguration.DBACCESS).GetGames();
          if (games.Count > 0)
          {
            log.AppendLine(String.Format("Der {0}. Spieltag existiert bereits!", i));
            continue;
          }
          
          new BuLiDataWebService(round, season, TGANConfiguration.DBACCESS).setGamesInitially();

          log.AppendLine(String.Format("Der {0}. Spieltag wurde erfolgreich eingetragen!", i));
        }
      }
      catch (Exception ex)
      {
        log.AppendLine(ex.ToString());
      }

      txtResultOfNewSchedule.Text = log.ToString();

    }
    protected void cmdNewUserGroup_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtNewUserGroup.Text))
            TGANConfiguration.DBACCESS.InsertUserGroup(Guid.NewGuid(), txtNewUserGroup.Text);
        else
            txtErrorUserGroup.Text = "Es wurde keine Benutzergruppe angegeben!";
    }
    protected void cmdDoQuery_Click(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(txtTsql.Text))
            {
                tsqlResult.DataSource = TGANConfiguration.DBACCESS.GetQueryResult(txtTsql.Text);
                tsqlResult.BackColor = System.Drawing.Color.White;
                tsqlResult.DataBind();
            }
            else
                txtTsql.Text = "Es wurde kein TSql angegeben!";
        }
        catch (Exception ex)
        {
            txtResultOfNewSchedule.Text = ex.ToString();
        }
    }
    protected void cmdUpdateStartTime_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            Int32.TryParse(txtRoundStartTime.Text, out result);
            if (result > 0 && result < 35)
            {
                Season season = new SeasonBL(TGANConfiguration.DBACCESS).GetActualSeason();
                Round round = new RoundBL(season, TGANConfiguration.DBACCESS).GetRoundByRoundNo(result);
                new BuLiDataWebService(round, season, TGANConfiguration.DBACCESS).setStartTime(false);
                //new KickerManager(round, season, TGANConfiguration.DBACCESS).UpdateStartDateTime(false);
                txtResultOfNewSchedule.Text = String.Format("Der {0}. Spieltag wurde erfolgreich aktualisiert!",result);
            }
            else
                throw new Exception("Spieltag nur als Nummer eingeben!");
        }
        catch (Exception ex)
        {
            txtResultOfNewSchedule.Text = ex.ToString();
        }
    }
    protected void cmdSetRes_Click(object sender, EventArgs e)
    {
        try
        {
            SeasonBL s = new SeasonBL(TGANConfiguration.DBACCESS);
            Season aS = s.GetActualSeason();
            RoundBL r = new RoundBL(aS, TGANConfiguration.DBACCESS);
            BuLiDataWebService bldws = new BuLiDataWebService(r.GetRoundByRoundNo(Int32.Parse(txtRoundNo.Text)), aS, TGANConfiguration.DBACCESS);
            bldws.setResults();
            txtResultOfNewSchedule.Text = "Resultate erfolgreich geladen!";
        }
        catch(Exception ex)
        {
            txtResultOfNewSchedule.Text = ex.ToString();
        }
    }
    
}
