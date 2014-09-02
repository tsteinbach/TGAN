using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class Round
    {
        private readonly int _roundNo = 0;
        private readonly Guid _Id = Guid.Empty;
        private readonly Guid _seasonId = Guid.Empty;
        private readonly Season _season = null;
        
        public Round(int roundNo)
        {
            _roundNo = roundNo;
        }
        
        public Round(Season season, int roundNo) : this(roundNo)
        {
            _season = season;
        }

        public Round(Guid id, Guid seasonId,int roundNo) : this(roundNo)
        {
            _season = new Season(seasonId,String.Empty,true,0);
            _Id = id;
            _seasonId = seasonId;
        }

        public int RoundNo
        {
            get {return _roundNo;}
        }

        public Guid ID
        {
            get { return _Id;}
        }

        public Guid SeasonID
        {
            get {return _seasonId;}
        }

        
        
    
    }
}
