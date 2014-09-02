using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayerLogic.Types
{
    public class RoundGame
    {
        private readonly Guid _Id = Guid.Empty;
        private readonly Guid _roundId = Guid.Empty;
        private readonly DateTime _playDate = DateTime.MinValue;
        private readonly DateTime _startTime = DateTime.MinValue;
        private readonly string _homeTeam = string.Empty;
        private readonly string _awayTeam = string.Empty;
        private readonly string _result = string.Empty;
        private readonly int _gameNo = 0;

        public RoundGame(Guid id, Guid roundId, DateTime playDate, DateTime startTime,
            string homeTeam, string awayTeam, string result, int gameNo)
        {
            _Id = id;
            _roundId = roundId;
            _playDate = playDate;
            _startTime = startTime;
            _homeTeam = homeTeam;
            _awayTeam = awayTeam;
            _result = result;
            _gameNo = gameNo;
        }

        public Guid ID
        {
            get { _Id;}
        }
        public Guid RoundID
        {
            get { _roundId;}
        }
        public DateTime PlayDate
        {
            get { _playDate;}
        }
        public DateTime StartTime
        {
            get { _startTime;}
        }
        public string HomeTeam
        {
            get { _homeTeam;}
        }
        public string AwayTeam
        {
            get { _awayTeam;}
        }
        public string Result
        {
            get { _result;}
        }
        public int GameNo
        {
            get { _gameNo;}
        }
    }
}
