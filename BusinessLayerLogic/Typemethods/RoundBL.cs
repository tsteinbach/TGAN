using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataLayerLogic.Types;
using DataLayerLogic;
using System.Collections;

namespace BusinessLayerLogic.Typemethods
{
    public class RoundBL
    {
        private Round _round = null;
        private Season _season = null;
        private readonly Buisinesses _dbAccess = null;
        private Guid _teamToFind = Guid.Empty;
        private static List<BundesligaTeam> _teams = null;
        

        private Guid TeamID
        {
            get { return _teamToFind; }
            set { _teamToFind = value; }
        }

        private List<BundesligaTeam> TEAMS
        {
            get 
            {
                if (_teams == null)
                    _teams = _dbAccess.GetAllTeams();
                return _teams;
            }
        }

        private Season Season
        {
            get { return _season; }
        }

        #region Constructors
        public RoundBL()
        { 
            
        }
        
        public RoundBL (Season season,Buisinesses dbAccess)
        {
            _season = season;
            _dbAccess = dbAccess;
        }
        
        public RoundBL(Round round, Season season, Buisinesses dbAccess) : this(season,dbAccess)
        {
            _round = round; 
        }

        #endregion

        public bool isRoundOver(double checkResultAfterGameStart)
        {
            DateTime maxStartTime = GetGames().Max(x => x.StartTime);

            if (DateTime.Compare( maxStartTime,DateTime.MaxValue) == 0)
                return false;
            else if (DateTime.Compare(DateTime.Now, maxStartTime.AddMinutes(checkResultAfterGameStart)) != 1)
                return false;
            else
                return true;
        }

        public Round GetRoundByRoundNo(int roundNo)
        {
            return _dbAccess.GetRoundByRoundNo(_season,roundNo);
        }

        public Round GetActualRound(bool includeHiddenGames)
        {
            int roundNo = _dbAccess.DetermineActualRoundNo(Season, includeHiddenGames);
            return GetRoundByRoundNo(roundNo);
        }

        public void UpdateCheckableRounds()
        {
            if (!_dbAccess.TodayCheck(Season))
            {
                List<Round> availableRounds = _dbAccess.GetCheckableRounds(Season);
                KickerManager kickUpdate = null;
                for (int i = 0; i < availableRounds.Count; i++)
                {
                    kickUpdate = new KickerManager(availableRounds[i], Season, _dbAccess);
                    kickUpdate.UpdateStartDateTime(false);
                }

                _dbAccess.FillCheckRounds(Season);
            }
        }

        public List<RoundGame> GetGames()
        {
            return _dbAccess.GetSingleRound(new Round(_round.ID,_round.SeasonID, _round.RoundNo));
        }

        public string SelectTeamName(Guid idOfTeam)
        {
            TeamID = idOfTeam;
            return TEAMS.Find(FindTeam).NameToShow; 
        }

        public string SelectTeamLogo(Guid idOfTeam)
        {
            TeamID = idOfTeam;
            string logo = TEAMS.Find(FindTeam).Logo;
            return String.Format("./images/Logos/{0}", logo);
        }

        public ArrayList Create34SpieltagArray()
        {
            ArrayList spieltage = new ArrayList();

            for (int i = 1; i <= 34; i++)
                spieltage.Add(i + ". Spieltag");

            return spieltage;
        }

        
        private bool FindTeam(BundesligaTeam team)
        {
            if (team.ID == TeamID)
                return true;
            else
                return false;
        }

        protected void GetRoundGamesToUpdateData()
        {
            List<Round> rounds = _dbAccess.GetCheckableRounds(_season);
        }
    }

    class RoundComparer : IComparer<Round>
    {
        public int Compare(Round x, Round y)
        {
            if (x.RoundNo == null)
            {
                if (y.RoundNo == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (y.RoundNo == null)
                    return 1;
                else
                {
                    if (x.RoundNo > y.RoundNo)
                        return 1;
                    else if (x.RoundNo == y.RoundNo)
                        return 0;
                    else
                        return -1;
                }
            }
        }
    }
}
