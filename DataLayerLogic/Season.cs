using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerLogic.Types
{
    public class Season
    {
        private string _name = String.Empty;
        private Guid _ID = Guid.Empty;
        
        public Guid ID
        {
            get { _ID; }        
            set { _ID = value; }        
        }
        
        public string Name
        {
            get { _name; }
            set { _name = value; }        
        }
    }
}
