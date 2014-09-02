using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class Forum_Themen
    {
        private Guid _ID = Guid.Empty;
        private string _Title = String.Empty;
        private Guid _MemberID = Guid.Empty;
        private DateTime _DateTime = DateTime.MinValue;

        public Forum_Themen(Guid id, Guid mId, string title, DateTime date)
        {
            _DateTime = date;
            _ID = id;
            _MemberID = mId;
            _Title = title;
        }

        public DateTime DateOfInsert
        {
            get { return _DateTime; }
        }

        public string Title
        {
            get { return _Title; }
        }

        public Guid ID
        {
            get { return _ID; }
        }

        public Guid MemberID
        {
            get { return _MemberID; }
        }
    }
}
