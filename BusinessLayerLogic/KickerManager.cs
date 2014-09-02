using System;
using System.Collections.Generic;
using System.Text;
using DataLayerLogic.Types;
using System.Net;
using BusinessLayerLogic.Typemethods;
using DataLayerLogic;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace BusinessLayerLogic
{
    public class KickerManager
    {
        private Round _round = null;
        private Season _season = null;
        private string _kickerStream = null;
        private List<RoundGame> _games = null;
        private Buisinesses _dbAccess = null;
        private List<BundesligaTeam> _teams = null;
        private List<string> _kickerTeamOrder = null;
        private const string idOfStartDIV = "ctl00_PlaceHolderContent_begegnungenCtrl";
        private const string tagOfEndElement = "</table>";

        /// <summary>
        /// used to Create the initial Playing Schedule
        /// </summary>
        public KickerManager(Buisinesses dbAccess)
        {
            _dbAccess = dbAccess;
        }
        
        public KickerManager(Round round, Season season, Buisinesses dbAccess) : this(dbAccess)
        {
            try
            {
                InitialObject(round, season);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitialObject(Round round, Season season)
        {
            _round = round;
            _season = season;
            _kickerStream = DataToDownload;
            _teams = _dbAccess.GetAllTeams();
            _kickerTeamOrder = LoadTeams();
            _games = new RoundBL(round, season, _dbAccess).GetGames();
        }

        private string DataToDownload
        {
            get
            {
                try
                {
                    WebClient wc = new WebClient();
                    string[] splitSeason = _season.Name.Split('/');
                    string hinrunde = splitSeason.GetValue(0).ToString();
                    string rückrunde = splitSeason.GetValue(1).ToString().Substring(2, 2);
                      string downloadURL = String.Format("http://www.kicker.de/news/fussball/bundesliga/spieltag/1-bundesliga/{0}-{1}/{2}/0/spieltag.html",
                    //string downloadURL = String.Format("http://www.kicker.de/fussball/bundesliga/spieltag/tabelle/liga/1/tabelle/1/saison/{0}-{1}/spieltag/{2}",
                        hinrunde, rückrunde, _round.RoundNo);

                    string html = System.Text.Encoding.Default.GetString(wc.DownloadData(downloadURL));
                    int startindex = html.IndexOf(idOfStartDIV);
                    return html.Substring(startindex, html.IndexOf(tagOfEndElement, startindex) - startindex);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// used by kicker interface
        /// </summary>
        /// <param name="checkAfterGameStarted"></param>
        /// <param name="daysHowLongTheResultIsUpdateable"></param>
        public void UpdateResults(double checkAfterGameStarted, double daysHowLongTheResultIsUpdateable)
        {
            try
            {
                if (_kickerTeamOrder.Count < 18)
                    throw new Exception("not all teams where found");
                
                //MatchCollection resultMatches = Regex.Matches(_kickerStream,
                //    @"\d{1,2}:\d{1,2}&nbsp;\x28\d{1,2}:\d{1,2}\x29", RegexOptions.IgnoreCase);
                MatchCollection resultMatches = Regex.Matches(_kickerStream,
                    @"(\d{1,2}|(\x2D)):(\d{1,2}|(\x2D))&nbsp;\x28(\d{1,2}|(\x2D)):(\d{1,2}|(\x2D))\x29", RegexOptions.IgnoreCase);

                if (resultMatches.Count > 9)
                    throw new Exception("more than 9 results returned");

                RoundGame game = null;
                _gameNoToFind = 0;
                int teamIndex = 0;
                
                for (int i = 0; i < resultMatches.Count; i++)
                {
                    _gameNoToFind++;
                    _teamKickerName = _kickerTeamOrder[teamIndex];
                    _teamHome = _teams.Find(FindTeam).ID;
                    _teamKickerName = _kickerTeamOrder[teamIndex+1];
                    _teamAway = _teams.Find(FindTeam).ID;

                    teamIndex = teamIndex +  2;
         
                    game = _games.Find(FindGameByTeamAndGameNo);
                    
                    if(game != null)
                        game.Result = resultMatches[i].Value;
                }

                UpdateDB(); 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStartDateTime(bool insertSchedule)
        {
            try
            {
                LoadGameStartDateAndTime(Regex.Matches(_kickerStream,
                    @"(<td>(([0-9\.]{8}&nbsp;[0-9:]{5})|(Mo|Di|Mi|Do|Fr|Sa|So))</td>)|(<TD>&nbsp;-&nbsp;</TD>)",
                    RegexOptions.IgnoreCase));

                if (!insertSchedule) UpdateDB();
                else _dbAccess.InsertNewPlayingSchedule(_season, _games);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string CreateInitialPlayingSchedule(string nameOfSeason, int roundNo, bool allRounds)
        {
            StringBuilder sbResult = new StringBuilder();
            Season season = null;

            if (allRounds && ((roundNo != 0) || (roundNo > 34)))
                return "Die Eingabe war nicht richtig. Entweder alle Runden oder eine bestimmte Runden auswählen.";

            if (allRounds && String.IsNullOrEmpty(nameOfSeason))
                return "Wenn alle Spieltage ausgewählt sind muß eine Saison eingegeben werden";

            if (allRounds)
            {
                season = new Season(Guid.NewGuid(), nameOfSeason, true,0);
                _dbAccess.InsertActualSeason(season);
                _dbAccess.InsertNewSeasonRounds(season);
                _season = season;

                for (int i = 1; i <= 34; i++)
                    sbResult.Append(SetInitialSchedule(i));
            }
            else
            {
                _season = new SeasonBL(_dbAccess).GetActualSeason();
                if (String.IsNullOrEmpty(nameOfSeason) || (nameOfSeason == _season.Name))
                    sbResult.Append(SetInitialSchedule(roundNo));
                else
                    return "Actual Season ist nicht korrekt => erst muß eine neue Saison angelegt werden!";
                
            }

            

            return sbResult.ToString();
        }

        private string SetInitialSchedule(int roundNo)
        {
            InitialObject(_round = new RoundBL(_season, _dbAccess).GetRoundByRoundNo(roundNo), _season);
            if (_kickerTeamOrder.Count == 18)
            {
                try
                {
                    SetGamesForInitialSchedule();
                    UpdateStartDateTime(true);

                    return "---------------------<br/> Spieltag " + roundNo + " erfolgreich";
                }
                catch (Exception ex)
                {
                    return "---------------------<br/> Spieltag " + roundNo + " konnte nicht eingefügt werden, da <br/>" + ex.Message; 
                }
            }
            else
                return "---------------------<br/> Spieltag " + roundNo + " konnte nicht eingefügt werden, da nicht alle Teams gefunden wurden <br/>";
        }


        private void UpdateDB()
        {
            if (_games.Count > 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    _gameNoToFind = i;
                    _dbAccess.UpdateRoundGame(_round, _games.Find(FindGameByGameNo));
                }
            }
            else
                throw new Exception("no games found for round " + _round.RoundNo);
        }

        #region Helper

        /// <summary>
        /// initial müssen die Spiele auf maximales StartDatum gesetzt werden, damit man die Tipps bei Spieltagen nicht sehen kann, 
        /// bei denen das genaue Datum noch nicht bekannt ist
        /// </summary>
        private void SetGamesForInitialSchedule()
        {
            if (_kickerTeamOrder.Count < 18)
                throw new Exception("not all teams where found!");

            _games = new List<RoundGame>(9);
            int spielNo = 0;
            
            for (int i = 0; i < 18; i = i + 2)
            {
                spielNo++;
                _teamKickerName = _kickerTeamOrder[i];
                _teamHome = _teams.Find(FindTeam).ID;
                _teamKickerName = _kickerTeamOrder[i + 1];
                _teamAway = _teams.Find(FindTeam).ID;

                //_games.Add(new RoundGame(Guid.NewGuid(), _round.ID, DateTime.MaxValue, DateTime.MaxValue, _teamHome, _teamAway, "-:-&nbsp;(-:-)", spielNo));
                _games.Add(new RoundGame(Guid.NewGuid(), _round.ID, DateTime.MaxValue, _teamHome, _teamAway, "-:-&nbsp;(-:-)", spielNo,false));
            }
        }

        private List<string> LoadTeams()
        {
            try
            {
                List<string> result = new List<string>();
                StringBuilder teamString = new StringBuilder();

                //_teams = _dbAccess.GetAllTeams();
                const string teamPrefix = "(?<=\"link\" Style=\"\">).*(";
                teamString.Append(teamPrefix);
                foreach (BundesligaTeam t in _teams)
                    teamString.Append(String.Format("{0}|",t.KickerName));
                
                teamString.Remove(teamString.Length - 1, 1);
                teamString.Append(")");

                MatchCollection teamsMatches = Regex.Matches(_kickerStream, teamString.ToString(),
                    RegexOptions.IgnoreCase);

                //html hat sich geändert
                //hin und rückspiel werden abgebildet, d.h. spiele werden doppelt aufgezeigt 
                int index = 0;
                for (int i = 0; i < teamsMatches.Count; i++)
                {
                    index++;
                    
                    if (index == 4)
                        index = 0; 
                    if((index == 1) || (index == 2))
                        result.Add(teamsMatches[i].Value);
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadGameStartDateAndTime(MatchCollection startDates)
        {
            bool dayOfWeek = false;
            string expression = null;
            string tag1 = "<td>";
            string tag2 = "</td>";
            string nbsp = "&nbsp;";
            string dateField = "&nbsp;-&nbsp;";
            DateTime date = DateTime.MaxValue;
            DateTime time = DateTime.MaxValue;
            RoundGame game = null;
            _gameNoToFind = 0;
            int teamindex = 0;
            bool isParsable;

            try
            {
                if (_kickerTeamOrder.Count < 18)
                    throw new Exception("not all teams where found");

                for (int i = 0; i < startDates.Count; i++)
                {
                    expression = startDates[i].Value.ToLower();
                    expression = expression.Replace(tag1, String.Empty);
                    expression = expression.Replace(tag2, String.Empty);

                    if ((expression == "mo") || (expression == "di") || (expression == "mi") || (expression == "do")
                        || (expression == "fr") || (expression == "sa") || (expression == "so"))
                    {
                        dayOfWeek = true;
                    }
                    else if (dateField.ToLower() == expression)
                    {
                        _gameNoToFind++;
                        _teamKickerName = _kickerTeamOrder[teamindex];
                        _teamHome = _teams.Find(FindTeam).ID;
                        _teamKickerName = _kickerTeamOrder[teamindex + 1];
                        _teamAway = _teams.Find(FindTeam).ID;
                        teamindex = teamindex + 2; 
                                                
                        game = _games.Find(FindGameByTeamAndGameNo);
                        if (game != null)
                        {
                            //game.PlayDate = date;
                            game.StartTime = new DateTime(date.Year,date.Month,date.Day, time.Hour,time.Minute,time.Second);
                        }
                    }
                    else if (dayOfWeek)
                    {
                        expression = expression.Replace(nbsp, "|");
                        isParsable = DateTime.TryParse(expression.Split('|').GetValue(0).ToString(), out date);
                        if (!isParsable)
                            date = DateTime.MaxValue;

                        isParsable = DateTime.TryParse(expression.Split('|').GetValue(1).ToString(), out time);
                        if (!isParsable)
                            time = DateTime.MaxValue;

                        dayOfWeek = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        #endregion
    }
}
