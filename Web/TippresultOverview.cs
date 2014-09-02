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
using System.Collections.Generic;
using BusinessLayerLogic.Typemethods;

/// <summary>
/// Summary description for TippresultOverview
/// </summary>
public class TippresultOverview : MemberInfomationBasePage, ITippresultOverview 
{
    private readonly string _season = null;
    private readonly string _spieltag = null;
    private readonly string _userGroup = null;

    private const string cssTippOverview = "TippOverview";
    private static SeasonBL _seasonB = null;
    private static MemberBL _memberB = null;
    
    public TippresultOverview(string season, string spieltag, string userGroup)
    {
        _season = season;
        _spieltag = spieltag;
        _userGroup = userGroup;
    }

    #region Properties

    public MemberBL MemberB
    {
        get
        {
            if (_memberB == null)
                return new MemberBL(TGANConfiguration.DBACCESS);
            else
                return _memberB;
        }
    }

    public RoundBL RoundB
    {
        get
        {
            if (SeasonOfImportance == null)
                return null;
            return new RoundBL(SeasonOfImportance, TGANConfiguration.DBACCESS);
        }
    }

    private SeasonBL SeasonB
    {
        get
        {
            if (_seasonB == null)
                return new SeasonBL(TGANConfiguration.DBACCESS);
            else
                return _seasonB;
        }
    }

    private UserGroup UserGroupOfImportance
    {
        get
        {
            return MemberB.GetUserGroupByName(_userGroup);
        }
    }

    private Season SeasonOfImportance
    {
        get
        {
            return SeasonB.GetSeasonBySeasonName(_season);
        }
    }

    private Round RoundOfImportance
    {
        get
        {
            return RoundB.GetRoundByRoundNo(Int32.Parse(_spieltag.Split('.').GetValue(0).ToString()));
        }
    }

    private List<RoundGame> GamesOfImportance
    {
        get
        {
            if ((RoundOfImportance == null) || (SeasonOfImportance == null))
                return null;
            return new RoundBL(RoundOfImportance, SeasonOfImportance, TGANConfiguration.DBACCESS).GetGames();
        }
    }

    #endregion


    #region ITippresultOverview Members

    Table ITippresultOverview.CreateTippedOverview()
    {
        return CreateTippResultsOfAllMembers();
    }

    Table ITippresultOverview.CreateGesamtstand()
    {
        return CreateGesamtstand();
    }

    #endregion

    private Table CreateGesamtstand()
    {
        int punkteInsgesamt, richtigeTipps, falscheTipps, echteBank, unechteBank, neunerTipp, nichtGetippt, position = 0;

        TippBL tippB = null;

        Table tbl = new Table();
        tbl.CssClass = cssTippOverview;
        TableHeaderRow thr = new TableHeaderRow();
        TableHeaderCell th = null;
        TableRow tr = null;
        TableCell td = null;

        th = new TableHeaderCell(); th.Text = "Position"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.Text = "Name"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.Text = "Punkte insgesamt"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.Text = "Richtige Tipps"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.Text = "Falsche Tipps"; thr.Cells.Add(th);
        thr.Cells.Add(th); th = new TableHeaderCell(); th.Text = "Echte Bank"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.Text = "Unechte Bank"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.Text = "Neuner-Tipp"; thr.Cells.Add(th);
        thr.Cells.Add(th); th = new TableHeaderCell(); th.Text = "Nicht getippt"; thr.Cells.Add(th);

        tbl.Rows.Add(thr);

        List<Member> members = TGANConfiguration.DBACCESS.GetAllUsers(UserGroupOfImportance);

        foreach (Member m in members)
        {
            position++;
            tr = new TableRow();
            tippB = new TippBL(RoundOfImportance, SeasonOfImportance, m, GamesOfImportance, UserGroupOfImportance, TGANConfiguration.DBACCESS);
            tippB.GetGesamtStandPerUser(out punkteInsgesamt, out richtigeTipps, out falscheTipps, out echteBank, out unechteBank, out nichtGetippt, out neunerTipp);

            td = new TableCell(); td.Text = position.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = String.Format("{0}", m.UserName); tr.Cells.Add(td);
            td = new TableCell(); td.Text = punkteInsgesamt.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = richtigeTipps.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = falscheTipps.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = echteBank.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = unechteBank.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = neunerTipp.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = nichtGetippt.ToString(); tr.Cells.Add(td);

            tbl.Rows.Add(tr);
        }

        return tbl;
    }

    private Table CreateTippResultsOfAllMembers()
    {
        List<Member> users = TGANConfiguration.DBACCESS.GetAllUsers(UserGroupOfImportance);
        TippBL tippB = null;
        KeyValuePair<Tipp, List<TippState>> result;
        TippValue[] tippvals = null;
        TippState[] tippStates = null;

        Table tbl = new Table();
        tbl.CssClass = cssTippOverview;
        tbl.CellPadding = 0;
        tbl.CellSpacing = 0;

        TableRow tr = new TableRow();
        TableHeaderCell th = new TableHeaderCell();
        TableCell td = null;

        tr.Cells.Add(th);

        for (int i = 0; i < GamesOfImportance.Count; i++)
        {
            th = new TableHeaderCell();
            th.Text = String.Format("{0}<br/>{1}<br/>{2}", RoundB.SelectTeamName(GamesOfImportance[i].HomeTeam), GamesOfImportance[i].Result, RoundB.SelectTeamName(GamesOfImportance[i].AwayTeam));
            tr.Cells.Add(th);
        }

        tbl.Rows.Add(tr);

        foreach (Member m in users)
        {
            tippB = new TippBL(RoundOfImportance, SeasonOfImportance, m, GamesOfImportance, UserGroupOfImportance, TGANConfiguration.DBACCESS);
            result = tippB.GetTipp();
            
            tippvals = new TippValue[result.Key.GivenTipps.Values.Count];
            result.Key.GivenTipps.Values.CopyTo(tippvals, 0);
            tippStates = result.Value.ToArray();

            tr = new TableRow();

            //UserName wird in die Tabelle eingefügt
            td = new TableCell();
            td.Text = m.UserName;
            tr.Cells.Add(td);

            // Tipps mit einer farblichen Markierung bzgl. Tippevaluierung werden in die Tabelle eingefügt
            for (int i = 0; i < result.Key.GivenTipps.Values.Count; i++)
            {
                td = new TableCell();
                
                //nur wenn das Spiel schon vorbei ist werden die Tipps angezeigt
                if(DateTime.Compare(GamesOfImportance[i].StartTime,DateTime.Now) == -1)
                    td.Text = tippB.GetStringTippValue(tippvals[i]);
                td.CssClass = tippStates[i].ToString();
                tr.Cells.Add(td);
            }

            tbl.Rows.Add(tr);
        }

        return tbl;
    }
}


