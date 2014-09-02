using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public enum TippValue
    {
        NotSet = 0,
        Home = 1,
        Away = 2,
        Draw = 3
    }

    public class Tipp
    {
        private Dictionary<RoundGame,TippValue> _tipps = null;
        private DateTime _inputTimeStamp = DateTime.MinValue;
        private Member _tganMember = null;
        private Member _tippSetter = null;
        
        /// <summary>
        /// Constructor set after data is reat from the db
        /// </summary>
        /// <param name="tipps"></param>
        /// <param name="tganMember"></param>
        /// <param name="tippSetter"></param>
        /// <param name="inputTimeStamp"></param>
        public Tipp(Dictionary<RoundGame,TippValue> tipps, Member tganMember, Member tippSetter, DateTime inputTimeStamp)
        {
            _tipps = tipps;
            _tganMember = tganMember;
            _tippSetter = tippSetter;
        }

        public DateTime InputTimeStamp
        { 
            get{return _inputTimeStamp;}
        }

        public Dictionary<RoundGame, TippValue> GivenTipps
        {
            get { return _tipps; }
        }

        /// <summary>
        /// Member who the bet belongs to
        /// </summary>
        public Member TGANMember
        {
            get { return _tganMember; }
        }

        /// <summary>
        /// Member who did the input 
        ///     can be an administrator or a regular member
        /// </summary>
        public Member TippSetter
        {
            get { return _tippSetter; }
        }

        
    }
}
