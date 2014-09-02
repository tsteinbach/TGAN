using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataLayerLogic.Types;
using BusinessLayerLogic.Typemethods;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    

    #region Variables and Constants

    protected string NOTLOADABLERESULTS = "Was not able to check for results!";
    protected const string EXCEPTIONOFMISSINGGAMESWITHINAROUND = "Der ausgewählte Spieltag hat mehr als 0 und weniger als 9 Spiele. \n Das ist ein inkonsistenter Zustand. \n Bitte wende Dich an Torsten!";
    protected const string IDOFCONTENTPLACEHOLDER = "mainContent";
    private const string TIPPWARNING = "Der Tipp ist nicht gültig. \n Es muß mindestens eine 0 oder 2 getippt werden.";
    private const string TIPPINFO = "Der Tipp wurde eingetragen.";
    protected const string INFOSPIELTAGFEHLT = "Der Spieltag existiert nicht.";
    protected const string CSSINFOTEXTWARNING = "TippIsNotValid";
    private const string CSSINFOTEXTOK = "TippIsValid";
    protected const string CSSINFOTEXTHIDDEN = "TippInfoVisibility";

    protected static Season _actualSeason = null;
    //private static int _roundNo = -1;    

    #endregion

    #region Properties
    //protected static int ActiveRoundNo
    //{
    //    get { return _roundNo; }
    //    set { _roundNo = value; }
    //}

    

    protected static Season ACTUALSEASON
    {
        get
        {
            if (_actualSeason == null)
                _actualSeason = new SeasonBL(TGANConfiguration.DBACCESS).GetActualSeason();
            return _actualSeason;
        }
    }

    private Round _actRound = null;
    protected Round ACTUALROUND
    {
        get
        {
              RoundBL r = new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS);
            if (_actRound == null)
            {
                _actRound = r.GetActualRound(true);
                
                //per Default wird der 1. Spieltag angezeigt
                if (_actRound == null)
                    _actRound = r.GetRoundByRoundNo(1);

//#warning lstActualRound setzen, um Konsistenz zu waren => Tipp über Event PreRender etc.
                
                return _actRound;
            }
            else
                return _actRound;
        }
    }

    protected Member ACTIVEMEMBER
    {
        [DebuggerStepThrough]
        get{return Session["ActiveUser"] as Member;}
        [DebuggerStepThrough]
        set{Session["ActiveUser"] = value;}
    }
    
    protected UserGroup ACTIVEUSERGROUP
    {
        [DebuggerStepThrough]
        get{return Session["UserGroup"] as UserGroup;}
        [DebuggerStepThrough]
        set{Session["UserGroup"] = value;}
    }

    #endregion

    #region EventHandler

    protected virtual void cmdGetBet_Click(object sender, EventArgs e)
    {
    }

    protected virtual void loadRound(object sender, EventArgs e)
    { 
    }
    
    #endregion

    #region Helpers
    /// <summary>
    /// 
    /// </summary>
    /// <param name="round">z.B. 1. Spieltag</param>
    protected int getRoundNo(string round)
    {
        int result;
        string roundStr = round.Split('.').GetValue(0).ToString();
        if (!Int32.TryParse(roundStr, out result))
            throw new Exception("Ausgewählter Spieltag konnte nicht evaluiert werden!");

        return result;
    }

    protected Table CreateTippLegend()
    {
        Table tbl = new Table();
        tbl.CssClass = "tippLegend";
        TableRow tr = null;
        TableCell td = null;

        tr = new TableRow(); td = new TableCell(); td.Text = "Tipp ist falsch"; td.CssClass = TippState.False.ToString(); tr.Cells.Add(td); tbl.Rows.Add(tr);
        tr = new TableRow(); td = new TableCell(); td.Text = "Tipp ist korrekt, <br/> aber keine Bank"; td.CssClass = TippState.True.ToString(); tr.Cells.Add(td); tbl.Rows.Add(tr);
        tr = new TableRow(); td = new TableCell(); td.Text = "Echte Bank"; td.CssClass = TippState.EchteBank.ToString(); tr.Cells.Add(td); tbl.Rows.Add(tr);
        tr = new TableRow(); td = new TableCell(); td.Text = "Unechte Bank"; td.CssClass = TippState.UnechteBank.ToString(); tr.Cells.Add(td); tbl.Rows.Add(tr);
        tr = new TableRow(); td = new TableCell(); td.Text = "Tipp ist noch nicht validierbar"; td.CssClass = TippState.NotReadable.ToString(); tr.Cells.Add(td); tbl.Rows.Add(tr);
        return tbl;
    }


    protected void GetTippInfo(Dictionary<RoundGame, TippValue>.KeyCollection roundBeeingUpdated, out string cssClass, out string infoText)
    {
        cssClass = String.Empty;
        infoText = String.Empty;

        if (roundBeeingUpdated == null)
        {
            cssClass = CSSINFOTEXTWARNING;
            infoText = TIPPWARNING;
        }
        else if (roundBeeingUpdated.Count < 9)
        {
            StringBuilder sb = new StringBuilder("<u>Nur<u/> Die folgenden Spiele  konnten aktualisiert werden: ");
            cssClass = CSSINFOTEXTWARNING;

            foreach (RoundGame g in roundBeeingUpdated)
                sb.Append(String.Format("<br/> {0}. Spiel", g.GameNo));

            infoText = sb.ToString();
        }
        else
        {
            cssClass = CSSINFOTEXTOK;
            infoText = TIPPINFO;
        }
        
    }
    
    protected void FillRoundTable(Control form, Member member, bool alwaysEnableTipps, Round round)
    {
        Control isRescheduled = null;
        Control result = null;
        Control date = null;
        Control heim = null;
        Control gast = null;
        DropDownList tippControl = null;
        TippBL tippB = null;
        RoundBL roundB = null;

        try
        {
            roundB = new RoundBL(round, ACTUALSEASON, TGANConfiguration.DBACCESS);
            int index = 0;
            List<RoundGame> games = roundB.GetGames();
            tippB = new TippBL(round, ACTUALSEASON, member, games, ACTIVEUSERGROUP, TGANConfiguration.DBACCESS);

            KeyValuePair<Tipp, List<TippState>> tipp = tippB.GetTipp();
            TippValue[] tippvals = new TippValue[tipp.Key.GivenTipps.Count];
            tipp.Key.GivenTipps.Values.CopyTo(tippvals, 0);

            if (games.Count == 9)
            {
                foreach (RoundGame game in games)
                {
                    index++;
                    date = form.FindControl("date" + index);
                    heim = form.FindControl("heim" + index);
                    gast = form.FindControl("gast" + index);
                    isRescheduled = form.FindControl("check" + index);
                    result = form.FindControl("result" + index);   
                    tippControl = form.FindControl("tipp" + index) as DropDownList;
                    
                    if (date != null && heim != null && gast != null && tippControl != null)
                    {
                        //im adminbereich
                        if (alwaysEnableTipps)
                        {
                            ((TextBox)date).Text = String.Format("{0} {1}",
                                game.StartTime.ToShortDateString(), game.StartTime.ToShortTimeString());
                            ((TextBox)result).Text = game.Result;
                            ((CheckBox)isRescheduled).Checked = game.IsHidden;
                        }
                        else
                        {
                            ((Label)date).Text = String.Format("{0} <br/> {1}",
                                game.StartTime.ToShortDateString(), game.StartTime.ToShortTimeString());
                        }

                        ((Label)heim).Text = roundB.SelectTeamName(game.HomeTeam);
                        ((Label)gast).Text = roundB.SelectTeamName(game.AwayTeam);

                        tippControl.Enabled = true;
                        if (tippvals.Length == 0)
                            tippControl.Text = TippBL.GetStringTippValue(TippValue.NotSet);
                        else
                            tippControl.Text = TippBL.GetStringTippValue(tippvals[index - 1]);

                        //wenn das Spiel verschoben wurde, dann kann es nochmals getippt werden
                        if ((DateTime.Compare(game.StartTime, DateTime.Now) < 1) && !alwaysEnableTipps && !game.IsHidden)
                            tippControl.Enabled = false;

                        if (tippvals.Length == 0)
                            tippControl.CssClass = TippState.NotReadable.ToString();
                        else
                            tippControl.CssClass = ((List<TippState>)tipp.Value)[index - 1].ToString();
                    }
                    else
                        throw new NullReferenceException();
                }
            }
            else if ((games.Count > 0) && (games.Count < 9))
            {
                throw new Exception(EXCEPTIONOFMISSINGGAMESWITHINAROUND);
            }
            else
            {
                for (int i = 1; i <= 9; i++)
                {
                    date = form.FindControl("date" + i);
                    heim = form.FindControl("heim" + i);
                    gast = form.FindControl("gast" + i);
                    tippControl = form.FindControl("tipp" + index) as DropDownList;
                    if (date != null && heim != null && gast != null && tippControl != null)
                    {
                        //im adminbereich
                        if (alwaysEnableTipps)
                        {
                            ((TextBox)date).Text = String.Format("{0} {1}",
                                DateTime.MaxValue.ToShortDateString(), DateTime.MaxValue.ToShortTimeString());
                            ((TextBox)result).Text = String.Empty;
                            ((CheckBox)isRescheduled).Checked = false;
                        }
                        else
                            ((Label)date).Text = String.Empty;
                        
                        ((Label)heim).Text = String.Empty;
                        ((Label)gast).Text = String.Empty;
                        tippControl.Text = TippBL.GetStringTippValue(TippValue.NotSet);
                    }
                    else
                        throw new NullReferenceException();
                }
            }
        }
        catch (Exception ex)
        { throw ex; }
    }
    
    #endregion
}
