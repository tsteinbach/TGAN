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
using System.Xml;
using System.Threading;
using System.Linq;
using DataLayerLogic;

/// <summary>
/// Summary description for TippresultOverview
/// </summary>
public class TippresultOverview : MemberInfomationBasePage, ITippresultOverview 
{
    private readonly string _season = null;
    private readonly string _spieltag = null;
    private readonly string _userGroup = null;

    private const string cssTippOverview = "TippOverview";
    private const string cssTH = "gesamtStand_TH";
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
        int position = 0;

        TippBL tippB = null;

        Table tbl = new Table();
        tbl.CssClass = cssTippOverview;
        
        TableHeaderRow thr = new TableHeaderRow();
        TableHeaderCell th = null;
        TableRow tr = null;
        TableCell td = null;
        
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Position"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Name"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Punkte insgesamt"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Richtige Tipps"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Falsche Tipps"; thr.Cells.Add(th);
        thr.Cells.Add(th); th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Echte Bank"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Unechte Bank"; thr.Cells.Add(th);
        th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Neuner-Tipp"; thr.Cells.Add(th);
        thr.Cells.Add(th); th = new TableHeaderCell(); th.CssClass = cssTH; th.Text = "Nicht getippt"; thr.Cells.Add(th);

        tbl.Rows.Add(thr);

        List<Member> members = new MemberBL(TGANConfiguration.DBACCESS).GetAllMembers(UserGroupOfImportance, true, SeasonOfImportance); //TGANConfiguration.DBACCESS.GetAllUsers(UserGroupOfImportance);
        List<GesamtStand> gesamtStandList = new List<GesamtStand>();
        
        foreach (Member m in members)
        {
            tr = new TableRow();
            tippB = new TippBL(RoundOfImportance, SeasonOfImportance, m, GamesOfImportance, UserGroupOfImportance, TGANConfiguration.DBACCESS);
            GesamtStand g;
            tippB.GetGesamtStandPerUser(out g,TGANConfiguration.CheckResultsAfterGameStart);
            gesamtStandList.Add(g);
        }

        GesamtStandComparer gc = new GesamtStandComparer();
        gesamtStandList.Sort(gc);

        int last_WholePoints = -1;
        int last_pos = -1;
        for (int i = 0; i < gesamtStandList.Count; i++)
        {
            tr = new TableRow();
            if (i % 2 == 0)
                tr.CssClass = "alternateRow";
            else
                tr.CssClass = "nonAlternateRow";

            position++;
            if (gesamtStandList[i].PunkteInsgesamt.CompareTo(last_WholePoints) == 0)
            {
                td = new TableCell(); td.Text = last_pos.ToString(); tr.Cells.Add(td);
            }
            else
            {
                last_pos = position;
                td = new TableCell(); td.Text = position.ToString(); ; tr.Cells.Add(td);
            }

            td = new TableCell(); td.Text = String.Format("{0}",gesamtStandList[i].Member.UserName); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].PunkteInsgesamt.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].RichtigeTipps.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].FalscheTipps.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].EchteBank.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].UnechteBank.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].NeunerTipp.ToString(); tr.Cells.Add(td);
            td = new TableCell(); td.Text = gesamtStandList[i].NichtGetippt.ToString(); tr.Cells.Add(td);

            tbl.Rows.Add(tr);
            last_WholePoints = gesamtStandList[i].PunkteInsgesamt;
        }

        return tbl;
    }

    private Table CreateTippResultsOfAllMembers()
    {
      List<Member> users = new MemberBL(TGANConfiguration.DBACCESS).GetAllMembers(UserGroupOfImportance, true, SeasonOfImportance); //TGANConfiguration.DBACCESS.GetAllUsers(UserGroupOfImportance);
        TippBL tippB = null;
        //KeyValuePair<Tipp, List<TippState>> result;
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

        //for total number
        th = new TableHeaderCell();
        th.CssClass = cssTH;
        th.Text = "Total";
        tr.Cells.Add(th);

        for (int i = 0; i < GamesOfImportance.Count; i++)
        {
            th = new TableHeaderCell();
            th.CssClass = cssTH;
            th.Text = String.Format("{0}<br/>{1}<br/>{2}", RoundB.SelectTeamName(GamesOfImportance[i].HomeTeam), GamesOfImportance[i].Result, RoundB.SelectTeamName(GamesOfImportance[i].AwayTeam));
            tr.Cells.Add(th);
        }

        tbl.Rows.Add(tr);

        //reset of tippResultDictionary
        TippsPerUser tippsPerUser = new TippsPerUser();
        Dictionary<Guid, int> totalList = new Dictionary<Guid, int>();

        foreach (Member m in users)
        {
            tippB = new TippBL(RoundOfImportance, SeasonOfImportance, m, GamesOfImportance, UserGroupOfImportance, TGANConfiguration.DBACCESS);
            tippsPerUser.TippsPerUserPROP.Add(m, tippB.GetTipp());
            tippB.CheckResultAfterGameStart = TGANConfiguration.CheckResultsAfterGameStart;

            var total = tippB.GetTotal();
            totalList.Add(m.ID, total);
        }


        foreach (KeyValuePair<Member, KeyValuePair<Tipp, List<TippState>>> tippResult in tippsPerUser.TippsPerUserPROP)
        {
            tippvals = new TippValue[tippResult.Value.Key.GivenTipps.Values.Count];
            tippResult.Value.Key.GivenTipps.Values.CopyTo(tippvals, 0);
            tippStates = tippResult.Value.Value.ToArray();

            tr = new TableRow();

            //UserName wird in die Tabelle eingefügt
            td = new TableCell();
            td.CssClass = "gesamtStandConfig";
            td.Text = tippResult.Key.UserName;
            tr.Cells.Add(td);

            //Totalanzahl des Spieltags wird angezeigt
            td = new TableCell();
            td.CssClass = "gesamtStandConfig";
            td.Text = totalList.Single(x => x.Key == tippResult.Key.ID).Value.ToString(); 
            tr.Cells.Add(td);

            // Tipps mit einer farblichen Markierung bzgl. Tippevaluierung werden in die Tabelle eingefügt
            for (int i = 0; i < tippResult.Value.Key.GivenTipps.Values.Count; i++)
            {
                td = new TableCell();

                //nur wenn das Spiel schon vorbei ist werden die Tipps angezeigt
                if (GamesOfImportance[i].IsHidden)
                    td.CssClass = TippState.NotReadable.ToString();
                else if (DateTime.Compare(GamesOfImportance[i].StartTime, DateTime.Now) == -1)
                {
                    td.Text = TippBL.GetStringTippValue(tippvals[i]);
                    td.CssClass = tippStates[i].ToString();
                }
                else
                    td.CssClass = TippState.NotReadable.ToString();
                tr.Cells.Add(td);
            }

            tbl.Rows.Add(tr);
        }

        return tbl;
    }

    

    private class GesamtStandComparer : IComparer<GesamtStand>
    {
        public int Compare(GesamtStand x, GesamtStand y)
        {
            if (x == null)
            {
                if (y == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (y == null)
                    return 1;
                else
                    return x.PunkteInsgesamt.CompareTo(y.PunkteInsgesamt) * -1;
            }
        }

    }
}


