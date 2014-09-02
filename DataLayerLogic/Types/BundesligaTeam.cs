using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class BundesligaTeam
    {
        private readonly Guid _ID = Guid.Empty;
        private readonly string _kickerName = null;
        private readonly string _nameToShow = null;
        private readonly string _Logo = null;
        
        public BundesligaTeam(Guid id, string kickerName,string nameToShow, string logo)
        {
            _ID = id;
            _kickerName = kickerName;
            _Logo = logo;
            _nameToShow = nameToShow;
        }

        public Guid ID
        {
            get { return _ID; }
        }

        public string KickerName
        {
            get { return _kickerName; }
        }

        public string NameToShow
        {
            get { return _nameToShow; }
        }

        public string Logo
        {
            get { return _Logo; }
        }
    }
}
