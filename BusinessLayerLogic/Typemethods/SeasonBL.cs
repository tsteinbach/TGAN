using System;
using System.Collections.Generic;
using System.Text;
using DataLayerLogic.Types;
using DataLayerLogic;

namespace BusinessLayerLogic.Typemethods
{
    public class SeasonBL
    {
        private readonly Buisinesses _dbAccess = null;
        public SeasonBL(Buisinesses dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public List<string> GetAllNamesOfSeasons()
        {
            List<Season> seasons = _dbAccess.GetAllSeasons();
            List<string> names = new List<string>();

            foreach (Season s in seasons)
                names.Add(s.Name);

            return names;
        }

        public int getStartYearOfSeason(Season season)
        {
            return Int32.Parse(season.Name.Split('/').GetValue(0).ToString());
        }

        public Season GetActualSeason()
        {
            Season season = _dbAccess.GetActualSeason();
            string actualSeason = String.Empty;

            //if (DateTime.Today.Month <= 6)
            //    actualSeason = Convert.ToString(DateTime.Today.Year - 1) + "/" + DateTime.Today.Year;
            //else
            //    actualSeason = DateTime.Today.Year + "/" + DateTime.Today.AddYears(1).Year;
            
            if (season != null)
            {
                //if (actualSeason == season.Name)
                //    return season;
                //else
                return season;
            }
            else
                throw new NullReferenceException("No actual season found!"); 
        }

        public List<Season> GetAllSeasons()
        {
          return _dbAccess.GetAllSeasons();
        }

        public Season GetSeasonBySeasonName(string season)
        {
            List<Season> seasons = _dbAccess.GetAllSeasons();
            _seasonName = season;
            return seasons.Find(FindSeason);
        }

        public Season GetSeasonBySeasonID(Guid seasonID)
        {
          List<Season> seasons = _dbAccess.GetAllSeasons();
          _seasonID = seasonID;
          return seasons.Find(FindBySeasonId);
        }

        private string _seasonName = null;

        private bool FindSeason(Season s)
        {
            if (s.Name == _seasonName)
                return true;
            else
                return false;
        }

        internal Guid _seasonID;

        internal bool FindBySeasonId(Season s)
        {
          if (s.ID.Equals(_seasonID))
            return true;
          else
            return false;
        }
    }
}
