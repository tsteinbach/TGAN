using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessLayerLogic.Typemethods;
using DataLayerLogic.Types;
using BusinessLayerLogic.openligadb;
using DataLayerLogic;
using System.Collections.Generic;

namespace BusinessLayerLogic
{

  

    /// <summary>
    /// Summary description for BuLiDataWebService
    /// </summary>
    public class BuLiDataWebService
    {
      private const string EMTYPRESULTTEMPLATE = "-:-&nbsp;({0}:{1})";
        
      private Season _season;
        private Round _actRound;
        private Buisinesses _dbAccess;
        private List<RoundGame> _games = null;
        private List<BundesligaTeam> _teams = null;
                
      /// <summary>
      /// used to update schedule
      /// </summary>
      /// <param name="actRound"></param>
      /// <param name="season"></param>
      /// <param name="dbAccess"></param>
        public BuLiDataWebService(Round actRound,Season season, Buisinesses dbAccess)
        {
            _season = season;
            _actRound = actRound;
            _dbAccess = dbAccess;

            _teams = _dbAccess.GetAllTeams();
            _games = new RoundBL(actRound, season, _dbAccess).GetGames();
        }

        public void setGamesInitially()
        {
          Sportsdata s = new Sportsdata();
          Fussballdaten[] fd = s.GetFusballdaten(_actRound.RoundNo, "bl1", new SeasonBL(_dbAccess).getStartYearOfSeason(_season), "steinbacht");
          Matchdata md = null;
          List<RoundGame> games = new List<RoundGame>();

          int gameNo = 1;
          foreach (Fussballdaten f in fd)
          {
            md = s.GetMatchByMatchID(f.SpielID);

            getTeam(f, true);
            getTeam(f,false);
            var game = new RoundGame(Guid.NewGuid(), _actRound.ID, DateTime.MinValue, _teamHome, _teamAway, 
              String.Format(EMTYPRESULTTEMPLATE,"-", "-"), gameNo, false);
            setStarttime(md, ref game);
            games.Add(game);
            gameNo++;
          }

          _dbAccess.InsertNewPlayingSchedule(_season, games);
          
        }

        public void setStartTime(bool insertSchedule)
        {
            try
            {
                Sportsdata s = new Sportsdata();

                Fussballdaten[] fd = s.GetFusballdaten(_actRound.RoundNo, "bl1", new SeasonBL(_dbAccess).getStartYearOfSeason(_season), "steinbacht");
                Matchdata md = null;
                RoundGame game = null;

                foreach (Fussballdaten f in fd)
                {
                    md = s.GetMatchByMatchID(f.SpielID);
                    
                    game = getGame(f);
                    setStarttime(md,ref game);
                }

                if (!insertSchedule) updateDB();
                else _dbAccess.InsertNewPlayingSchedule(_season, _games);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void setStarttime(Matchdata md,ref RoundGame game)
        {
          game.StartTime = new DateTime(md.matchDateTime.Year, md.matchDateTime.Month, md.matchDateTime.Day
              , md.matchDateTime.Hour, md.matchDateTime.Minute, md.matchDateTime.Second);
        }

      

        public void setResults()
        {
            try
            {
                Sportsdata s = new Sportsdata();
                
                Fussballdaten[] fd = s.GetFusballdaten(_actRound.RoundNo, "bl1", new SeasonBL(_dbAccess).getStartYearOfSeason(_season), "steinbacht");
                Matchdata md = null;
                RoundGame game = null;
              
                foreach (Fussballdaten f in fd)
                {
                    md = s.GetMatchByMatchID(f.SpielID);
                    matchResult endergebnis;
                    matchResult halbzeitergebnis;
                    getResultTypes(md, out endergebnis, out halbzeitergebnis);

                    game = getGame(f);

                    if ((game != null) && (md.matchIsFinished))
                    {
                        game.Result = String.Format("{0}:{1}&nbsp;({2}:{3})",
                            endergebnis.pointsTeam1, endergebnis.pointsTeam2,
                            halbzeitergebnis.pointsTeam1, halbzeitergebnis.pointsTeam2);
                    }
                    else if (halbzeitergebnis != null)
                    {
                        game.Result = String.Format(EMTYPRESULTTEMPLATE,
                                halbzeitergebnis.pointsTeam1, halbzeitergebnis.pointsTeam2);
                    }

                }
                
                this.updateDB();
                  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private RoundGame getGame(Fussballdaten f)
        {
            getTeam(f, true);
            getTeam(f, false);

            RoundGame game = _games.Find(FindGameByTeamAndGameNo);
            return game;
        }

        private void getTeam(Fussballdaten f, bool isHome)
        {
            if(isHome)
                _teamKickerName = f.Team1;
            if (!isHome)
                _teamKickerName = f.Team2;

            BundesligaTeam team = _teams.Find(FindTeam);
            if (team == null)
                throw new Exception(String.Format("Team '{0}' could not be found",_teamKickerName));
            
            if (isHome)
                _teamHome = team.ID;
            if (!isHome)
                _teamAway = team.ID;
            
        }

        private bool getResultTypes(Matchdata input,out matchResult endergebnis, out matchResult halbzeitergebnis)
        { 
            string endString = "Endergebnis";
            string halbString = "Halbzeitergebnis";
            endergebnis = null;
            halbzeitergebnis = null;

            if (input.matchIsFinished)
            {
                if (input.matchResults.Length == 2)
                {
                    endergebnis = new matchResult();
                    halbzeitergebnis = new matchResult();

                    if (input.matchResults[0].resultName == endString)
                    {
                        endergebnis.pointsTeam1 = input.matchResults[0].pointsTeam1;
                        endergebnis.pointsTeam2 = input.matchResults[0].pointsTeam2;
                        halbzeitergebnis.pointsTeam1 = input.matchResults[1].pointsTeam1;
                        halbzeitergebnis.pointsTeam2 = input.matchResults[1].pointsTeam2;
                    }
                    else
                    {
                        endergebnis.pointsTeam1 = input.matchResults[1].pointsTeam1;
                        endergebnis.pointsTeam2 = input.matchResults[1].pointsTeam2;
                        halbzeitergebnis.pointsTeam1 = input.matchResults[0].pointsTeam1;
                        halbzeitergebnis.pointsTeam2 = input.matchResults[0].pointsTeam2;
                    }
                }
                else
                    throw new Exception("Halbzeitstand geht nicht!");
                return true;
            }
            else if ((input.matchResults.Length == 1) && (input.matchResults[0].resultName == halbString))
            {
                if ((input.matchResults[0].pointsTeam1 != -1) && (input.matchResults[0].pointsTeam2 != -1))
                {
                    halbzeitergebnis = new matchResult();
                    halbzeitergebnis.pointsTeam1 = input.matchResults[0].pointsTeam1;
                    halbzeitergebnis.pointsTeam2 = input.matchResults[0].pointsTeam2;
                }
                return false;
            }
            return false;

        }
        
        private int _gameNoToFind = 0;
        private Guid _teamHome = Guid.Empty;
        private Guid _teamAway = Guid.Empty;

        private bool FindGameByTeamAndGameNo(RoundGame game)
        {
            if (/*(game.GameNo == _gameNoToFind) &&*/
                (game.HomeTeam == _teamHome) &&
                (game.AwayTeam == _teamAway))
                return true;
            else
                return false;
        }
                
        private bool FindGameByGameNo(RoundGame game)
        {
            if ((game.GameNo == _gameNoToFind))
                return true;
            else
                return false;
        }

        private string _teamKickerName = null;

        private bool FindTeam(BundesligaTeam team)
        {
            //if (team.KickerName == _teamKickerName)
            if (team.NameToShow.Contains(_teamKickerName))
                return true;
            else
                return false;
        }

        private void updateDB()
        {
            if (_games.Count > 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    _gameNoToFind = i;
                    _dbAccess.UpdateRoundGame(_actRound, _games.Find(FindGameByGameNo));
                }
            }
            else
                throw new Exception("no games found for round " + _actRound.RoundNo);
        }
    }
}
