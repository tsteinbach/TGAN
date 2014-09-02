using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class Forum_ActualContent
    {
        private string _title = String.Empty;
        private Guid _titleId = Guid.Empty;
        private int _amount = 0;

        public Forum_ActualContent(string Title, Guid titleID, int amount)
        {
            _title = Title;
            _titleId = titleID;
            _amount = amount;
        }

        public string Title
        {
            get { return _title; }
        }

        public Guid TitleID
        {
            get { return _titleId; }
        }

        public int Amount
        {
            get { return _amount; }
        }
    }
}
