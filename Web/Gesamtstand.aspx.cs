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
using System.Text;
using System.Net;
using System.IO;
//using FlickrNet;
using System.Collections.Generic;
using BusinessLayerLogic;

public partial class Login : BasePage
{
    private ITippresultOverview _tippResultOverview = null;

    #region Properties
    
    public string RoundString
    {
        get { return Session["RoundString"].ToString(); }
        set { Session["RoundString"] = value; }
    }

    public string SeasonString
    {
        get { return Session["SeasonString"].ToString(); }
        set { Session["SeasonString"] = value; }
    }

    public string UserGroupString
    {
        get { return Session["UserGroupString"].ToString(); }
        set { Session["UserGroupString"] = value; }
    }

    private int ActiveTabIndex
    {
        get { return (int)Session["ActiveTab"]; }
        set { Session["ActiveTab"] = value; }
    }

	
    #endregion
    

    #region Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "TGAN-Tippoverview";
        placeLegend.Controls.Add(base.CreateTippLegend());
        
        if(ACTIVEMEMBER == null)
            Response.Redirect("./Error.aspx");

        try
        {
            if (!IsPostBack)
            {
                RoundBL rb = new RoundBL();
                
                lstSeason.DataSource = new SeasonBL(TGANConfiguration.DBACCESS).GetAllNamesOfSeasons();
                lstSeason.DataBind();
                lstUserGroup.DataSource = new MemberBL(TGANConfiguration.DBACCESS).GetAllNamesOfUserGroups();
                lstUserGroup.DataBind();
                lstUserGroup.Text = ACTIVEUSERGROUP.Name;
                lstSpieltag.DataSource = rb.Create34SpieltagArray();
                lstSpieltag.DataBind();
                lstSpieltag.SelectedIndex = ACTUALROUND.RoundNo - 1;

                ActiveTabIndex = 0;
                //bigTippOverview.ActiveTab = bigTippOverview.Tabs[ActiveTabIndex];

                CheckOfForgottenTipp();
               
                try
                {
                    BuLiDataWebService b = new BuLiDataWebService(new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetActualRound(false), ACTUALSEASON, TGANConfiguration.DBACCESS);
                    b.setResults();
                }
                catch
                {
                    Response.Write(base.NOTLOADABLERESULTS);
                }
            }
        }
        catch (Exception ex)
        {
            overview.Controls.Add(new LiteralControl(ex.ToString()));
        }
    }

    protected void cmdTippOverview_Click(object sender, EventArgs e)
    {
        CreateTippResultInstance();
        overview.Controls.Add(this._tippResultOverview.CreateTippedOverview());
    }

    protected void cmdTotalOverview_Click(object sender, EventArgs e)
    {
        CreateTippResultInstance();
        overview.Controls.Add(this._tippResultOverview.CreateGesamtstand());
    }
  
    #endregion

    /// <summary>
    /// prüft, ob jemand komplett nicht getippt hat
    /// </summary>
    private void CheckOfForgottenTipp()
    {
        try
        {
            MemberBL mB = new MemberBL(TGANConfiguration.DBACCESS);
            List<UserGroup> uGroups = TGANConfiguration.DBACCESS.GetAllUserGroups();
            List<RoundGame> games = new RoundBL(ACTUALROUND, ACTUALSEASON, TGANConfiguration.DBACCESS).GetGames();

            if (DateTime.Compare(DateTime.Now, games[0].StartTime) < 0)
                return;

            foreach (UserGroup ug in uGroups)
            {
                List<Member> members = mB.GetAllMembers(ug,true,ACTUALSEASON);
                
                foreach (Member m in members)
                {
                    TippBL tB = new TippBL(ACTUALROUND, ACTUALSEASON, m, games, ug, TGANConfiguration.DBACCESS);
                    Tipp tipp = tB.GetTippWithoutTippEvaluation();

                    if (tipp.GivenTipps.Count == 0)
                    {
                        Dictionary<RoundGame, TippValue> tipps = new Dictionary<RoundGame, TippValue>();

                        foreach (RoundGame g in games)
                            tipps.Add(g, TippValue.NotSet);

                        tB.SetTipp(new Tipp(tipps, m, m, DateTime.Now), true);
                    }
                }
            
            }
        }
        catch (Exception ex)
        {
            overview.Controls.Add(new LiteralControl(ex.ToString()));
            //bigTippOverview.Tabs[0].Controls.Add(new LiteralControl(ex.ToString()));
        }
    }

    //private void GetOverview()
    //{
    //    CreateTippResultInstance();
    //    //bigTippOverview.Tabs[ActiveTabIndex].Controls.Clear();
        
    //    if (_tippResultOverview != null)
    //    {
    //        if (lstView.SelectedValue == "Overview")
    //            overview.Controls.Add(this._tippResultOverview.CreateGesamtstand());
    //        else if (lstView.SelectedValue == "Bets")
    //            overview.Controls.Add(this._tippResultOverview.CreateTippedOverview());
    //    }
    //    else
    //        overview.Controls.Add(new LiteralControl("Eine der Listen Saison, Spieltag oder Benutzergruppe sind nicht gefüllt!"));
    //}
    
    private void CreateTippResultInstance()
    {
        
        
        if ((lstSeason.SelectedItem == null) ||
            (lstSpieltag.SelectedItem == null) ||
            (lstUserGroup.SelectedItem == null))
        {
            RoundString = null;
            SeasonString = null;
            UserGroupString = null;
            return;
        }
        else
        {
            RoundString = lstSpieltag.SelectedItem.Text;
            SeasonString = lstSeason.SelectedItem.Text;
            UserGroupString = lstUserGroup.SelectedItem.Text;
            
            TippresultOverview tippResult = new TippresultOverview(SeasonString,
                RoundString.Split('.').GetValue(0).ToString(), UserGroupString);
            _tippResultOverview = tippResult;
        }
    }
}
