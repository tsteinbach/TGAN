using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class Season
    {
        private readonly string _name = String.Empty;
        private readonly Guid _ID = Guid.Empty;
        private readonly bool _IsActual = false;
        private int _Order;

        public Season()
        { }
        
        public Season(Guid id, string name, bool isActual, int order)
        {
            _ID = id;
            _name = name;
            _IsActual = isActual;
            _Order = order;
        }

        public int Order
        {
          get { return _Order; }
        }
        
        public Guid ID
        {
            get { return _ID; }        
        }
        
        public string Name
        {
            get { return _name; }
        }

        public bool IsActual
        {
            get { return _IsActual; }
        }

        
    }
}
