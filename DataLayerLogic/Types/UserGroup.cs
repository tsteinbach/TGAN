using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class UserGroup
    {
        private string _Name = String.Empty;
        private Guid _ID = Guid.Empty;

        public UserGroup(Guid id,string name)
        {
            _Name = name;
            _ID = id;
        }

        public Guid ID
        {
            get { return _ID; }
        }

        public string Name
        {
            get { return _Name; }
        }
    
    }
}
