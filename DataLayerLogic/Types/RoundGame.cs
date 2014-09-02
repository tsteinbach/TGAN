using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public class RoundGame
    {
        private readonly Guid _Id = Guid.Empty;
        private readonly Guid _roundId = Guid.Empty;
        private DateTime _playDate = DateTime.MinValue;
        private DateTime _startTime = DateTime.MinValue;
        private readonly Guid _homeTeam = Guid.Empty;
        private readonly Guid _awayTeam = Guid.Empty;
        private string _result = string.Empty;
        private readonly int _gameNo = 0;
        private bool _hidden;

        public RoundGame(Guid id, Guid roundId,/* DateTime playDate,*/ DateTime startTime,
            Guid homeTeam, Guid awayTeam, string result, int gameNo, bool hidden)
        {
            _Id = id;
            _roundId = roundId;
            //_playDate = playDate;
            _startTime = startTime;
            _homeTeam = homeTeam;
            _awayTeam = awayTeam;
            _result = result;
            _gameNo = gameNo;
            _hidden = hidden;
        }

        public Guid ID
        {
            get {return  _Id;}
        }
        public Guid RoundID
        {
            get {return _roundId;}
        }
        //public DateTime PlayDate
        //{
        //    get {return _playDate;}
        //    set { _playDate = value; }
        //}
        public DateTime StartTime
        {
            get {return _startTime;}
            set { _startTime = value; }
        }
        public Guid HomeTeam
        {
            get { return _homeTeam;}
        }
        public Guid AwayTeam
        {
            get { return _awayTeam;}
        }
        public string Result
        {
            get { return _result;}
            set { _result = value; }
        }
        public int GameNo
        {
            get { return _gameNo;}
        }

        //wenn die Tipps eines Spiels wegen Verlegung nicht angezeigt werden sollen 
        public bool IsHidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }
    }
}
