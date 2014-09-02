using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class Forum_Inhalt
    {
        private Guid _ID = Guid.Empty;
        private Guid _MemberId = Guid.Empty;
        private Guid _Forum_ThemenID = Guid.Empty;
        private string _Inhalt = String.Empty;
        private DateTime _DateOfInsert = DateTime.MinValue;

        public Forum_Inhalt(Guid id, Guid mId, Guid tId, string content, DateTime dateOfInsert)
        {
            _ID = id;
            _MemberId = mId;
            _Forum_ThemenID = tId;
            _Inhalt = content;
            _DateOfInsert = dateOfInsert;
        }

        public Guid ID
        {
            get { return _ID; }
        }

        public Guid MemberId
        {
            get { return _MemberId; }
        }

        public Guid ThemenID
        {
            get { return _Forum_ThemenID; }
        }

        public string Content
        {
            get { return _Inhalt; }
        }

        public DateTime DateOfInsert
        {
            get { return _DateOfInsert; }
        }
    
    }
}
