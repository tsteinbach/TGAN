using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerLogic.Types
{
    public class Round
    {
        private int _round = 0;
        private Guid _Id = Guid.Empty;
        private Guid _seasonId = Guid.Empty;
        
        public Round(int round, Guid seasonId)
        {
            _round = round;
            _seasonId = seasonId;
        }

        public int Round
        {
            get {return _round;}
        }

        public Guid ID
        {
            get { _Id;}
            set { _Id = value; }
        }

        public Guid SeasonID
        {
            get { _seasonId;}
        }

        public List<RoundGame> Games
        {
            get
            {
                return GetGames();   
            }
        }

        private List<RoundGame> GetGames()
        { 
            List<RoundGame> games = new List<RoundGame>();
            
            //Code

            return games;
        }
    
    }
}
