using System;
using System.Collections.Generic;
using System.Text;
using DataLayerLogic.Types;
using DataLayerLogic;
using System.Threading;

namespace BusinessLayerLogic.Typemethods
{
   
    public class TippState
    {
        public enum TippStateEnum{True = 0,UnechteBank = 1,EchteBank =2,False =3 ,NotReadable =4}

        const string cssTrue = "w3-green";
        const string cssFalse = "w3-red";
        const string cssEchte = "w3-blue";
        const string cssUnechte = "w3-yellow";
        const string cssNotReadable = "w3-gray";


        public static KeyValuePair<TippStateEnum, string> trueTipp = new KeyValuePair<TippStateEnum,string>(TippStateEnum.True,cssTrue);
        public static KeyValuePair<TippStateEnum, string> falseTipp = new KeyValuePair<TippStateEnum,string>(TippStateEnum.False,cssFalse);
        public static KeyValuePair<TippStateEnum, string> echteBank = new KeyValuePair<TippStateEnum,string>(TippStateEnum.EchteBank,cssEchte);
        public static KeyValuePair<TippStateEnum, string> unEchteBank = new KeyValuePair<TippStateEnum,string>(TippStateEnum.EchteBank,cssUnechte);
        public static KeyValuePair<TippStateEnum, string> NotReadable = new KeyValuePair<TippStateEnum, string>(TippStateEnum.EchteBank, cssNotReadable);

        public static string GetCssClass(TippStateEnum tippStateVal)
        {
            switch (tippStateVal)
            {
                case TippStateEnum.True:
                    return cssTrue;
                case TippStateEnum.UnechteBank:
                    return cssUnechte;
                case TippStateEnum.EchteBank:
                    return cssEchte;
                case TippStateEnum.False:
                    return cssFalse;
                case TippStateEnum.NotReadable:
                    return cssNotReadable;
                default:
                    throw new Exception("TippState not known");
            }
        }
    }

    public class GesamtStand
    {
        private readonly Member _member;

        public Member Member
        {
            get { return _member; }
        }
        
        private int _punkteInsgesamt = 0;
        public int PunkteInsgesamt
        {
            get { return _punkteInsgesamt; }
            set { _punkteInsgesamt = value; }
        }

        private int _richtigeTipps = 0;
        public int RichtigeTipps
        {
            get { return _richtigeTipps; }
            set { _richtigeTipps = value; }
        }

        private int _falscheTipps = 0;
        public int FalscheTipps
        {
            get { return _falscheTipps; }
            set { _falscheTipps = value; }
        }

        private int _echteBank = 0;
        public int EchteBank
        {
            get { return _echteBank; }
            set { _echteBank = value; }
        }

        private int _unechteBank = 0;
        public int UnechteBank
        {
            get { return _unechteBank; }
            set { _unechteBank = value; }
        }

        private int _nichtGetippt = 0;
        public int NichtGetippt
        {
            get { return _nichtGetippt; }
            set { _nichtGetippt = value; }
        }

        private int _neunerTipp = 0;
        public int NeunerTipp
        {
            get { return _neunerTipp; }
            set { _neunerTipp = value; }
        }

        //used to determined when all threads are finished
        public int RoundIndex = 0;

        public GesamtStand(int pktinsgeamt, int richtigeTipps, int falscheTipps
            , int echteBank, int unechteBank, int nichtgetippt, int neunerTipp, Member m) :this(m)
        {
            _punkteInsgesamt = pktinsgeamt;
            _richtigeTipps = richtigeTipps;
            _falscheTipps = falscheTipps;
            _echteBank = echteBank;
            _unechteBank = unechteBank;
            _nichtGetippt = nichtgetippt;
            _neunerTipp = neunerTipp;
        }

        public GesamtStand(Member m)
        {
            _member = m;
        }

        private readonly Round _round;
        public GesamtStand(Member m, Round r) : this(m)
        {
            _round = r;
        }

        public double CheckResultAfterGameStart
        {
            set;
            private get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbAccess"></param>
        /// <param name="isFromAdminArea"></param>
        /// <param name="toleranceAfterGameFinished"></param>
        /// <param name="result"></param>
        /// <returns>is Round over</returns>
        public bool tryGetGesamtStandFromDB(Buisinesses dbAccess, out Gesamtstands result, out bool isRoundinDB)
        {
            result = null;
            isRoundinDB = dbAccess.selectGesamtstandsRow(_member.ID, _round.ID, out result); 
            return new RoundBL(_round, null, dbAccess).isRoundOver(CheckResultAfterGameStart);
        }

        public void insertGesamtStandInDB(bool isFromAdminPage, Buisinesses dbAccess)
        {
            //that is a necessary validation to prevent any inserts of records which are not valid
            if (this.EchteBank == 0 &&
                this.FalscheTipps == 0 &&
                this.NeunerTipp == 0 &&
                this.NichtGetippt == 0 &&
                this.PunkteInsgesamt == 0 &&
                this.RichtigeTipps == 0 &&
                this.UnechteBank == 0)
                return;

            if (isFromAdminPage)
                throw new NotImplementedException();
            else
                dbAccess.insertGesamtstandsRow(_member.ID,_round.ID,this.EchteBank,this.FalscheTipps
                    ,this.NeunerTipp,this.NichtGetippt,this.PunkteInsgesamt,this.RichtigeTipps,this.UnechteBank);
        }
    }

    public class TippsPerUser
    {
        public TippsPerUser()
        {
            TippsPerUserPROP = new Dictionary<Member, KeyValuePair<Tipp, List<TippState.TippStateEnum>>>();
        }

        public Dictionary<Member, KeyValuePair<Tipp, List<TippState.TippStateEnum>>> TippsPerUserPROP = null;
    }

    public class TippBL
    {
        private readonly Buisinesses _dbAccess = null;
        //public const string TRENNUNG_ENDERGEBNIS_HALBZEITSTAND = "&nbsp;";
        /// <summary>
        /// start von Halbzeitergebnis
        /// </summary>
        public const string STR_TRENNUNG_ENDERGEBNIS_HALBZEITSTAND = "&nbsp;";
        public const char CHR_TRENNUNG_ENDERGEBNIS_HALBZEITSTAND = '(';
        public const string KEINERGEBNIS = "-:-";

        public TippBL()
        {
        }

        public TippBL(Buisinesses dbAccess)
        {
            _dbAccess = dbAccess;
        }

        private Round _Round; 
        private Season _Season; 
        private Member _Member;
        private List<RoundGame> _Games;
        private Tipp _Tipp;
        private UserGroup _UserGroup;
        private List<Tipp> _AllTipps = null;
        private TippValue[] _tippTypes = new TippValue[3] { TippValue.Away,TippValue.Draw,TippValue.Home};
        
        public TippBL(Round round, Season season, Member member, List<RoundGame> games,UserGroup userGroup,Buisinesses dbAccess) : this(dbAccess)
        {
            _Round = round;
            _Season = season;
            _Member = member;
            _Games = games;
            _UserGroup = userGroup;
        }

        public double CheckResultAfterGameStart
        {
            set;
            private get;
        }

        private  Tipp TippSet
        {
            get { return _Tipp; }
            set { _Tipp = value; }
        }

        private List<Tipp> AllTipps
        {
            get{return _AllTipps;}
            set{_AllTipps = value;}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="alwaysInsertTipp">StartTime is not checked</param>
        /// <returns></returns>
        public Dictionary<RoundGame,TippValue>.KeyCollection SetTipp(Tipp t, bool alwaysInsertTipp)
        {
            int rows = 0;
            TippSet = t;

            if (IsValide())
            {
                if (!alwaysInsertTipp)CheckStartTime();

                rows = _dbAccess.UpdateTipp(TippSet);
                if (rows == 0)
                    rows = _dbAccess.FillTipp(TippSet);
                return t.GivenTipps.Keys;
            }
            else
                return null;
        }

        /// <summary>
        /// Tipp wird einfach nur aus der DB geladen
        /// </summary>
        /// <returns></returns>
        public Tipp GetTippWithoutTippEvaluation()
        {
            return _dbAccess.GetTippedValues(_Round, _Season, _Member, _Games);
        }

        /// <summary>
        /// Tipp aus der DB wird gesetzt und es findet ne Pr�fung auf Banken statt
        /// </summary>
        /// <returns></returns>
        public void GetTippUsingAThread(object tippsPerUser)
        {
            KeyValuePair<Tipp, List<TippState.TippStateEnum>> result = GetTipp();
            ((TippsPerUser)tippsPerUser).TippsPerUserPROP.Add(_Member, result);
        }

        /// <summary>
        /// Tipp aus der DB wird gesetzt und es findet ne Pr�fung auf Banken statt
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<Tipp, List<TippState.TippStateEnum>> GetTipp()
        {
            TippSet  = _dbAccess.GetTippedValues(_Round,_Season,_Member,_Games);
            return EvaluateMemberTipp();
        }

        /// <summary>
        /// used for threading
        /// </summary>
        public void GetGesamtStandPerUser(object gesamtStandList)
        {
            GesamtStand gesamtStand;
            //30 Minutes is set to default
            GetGesamtStandPerUser(false,out gesamtStand);

            if(gesamtStand != null)
                ((List<GesamtStand>)gesamtStandList).Add(gesamtStand);
        }

        public int GetTotal()
        {
            GesamtStand helper = new GesamtStand(_Member, _Round) { CheckResultAfterGameStart = CheckResultAfterGameStart };

            Gesamtstands gDB;
            bool isRoundInDB;
            bool isRoundOver = helper.tryGetGesamtStandFromDB(this._dbAccess, out gDB, out isRoundInDB);

            if (!isRoundInDB)
            {
                this.calculateGesamtStand(helper);
                return helper.PunkteInsgesamt;
            }
            else
                return gDB.PunkteInsgesamt;
        }

        public int GetGesamtStandPerUser(bool isAllowedToWriteTotalData ,out GesamtStand gesamtStand)
        {
            int selectedRound = _Round.RoundNo; ;
            int totalCurrentRound = -10;
            gesamtStand = new GesamtStand(_Member);
            RoundBL roundB = new RoundBL(_Season,_dbAccess);//
            List<Round> rounds = new List<Round>();//
            
            // ermittelt alle zu betrachtenden Runden
            for (int i = _Round.RoundNo; i > 0; i--)
                rounds.Add(roundB.GetRoundByRoundNo(i));

            foreach (Round round in rounds)
            {
                _Round = round;

                GesamtStand helper = new GesamtStand(_Member, _Round) { CheckResultAfterGameStart = CheckResultAfterGameStart }; ;

                Gesamtstands gDB;
                bool isRoundInDB;
                bool isRoundOver = helper.tryGetGesamtStandFromDB(this._dbAccess, out gDB, out isRoundInDB);

                if (!isRoundInDB && isRoundOver)
                {
                    this.calculateGesamtStand(helper);
                    
                    gesamtStand.EchteBank += helper.EchteBank;
                    gesamtStand.FalscheTipps += helper.FalscheTipps;
                    gesamtStand.NeunerTipp += helper.NeunerTipp;
                    gesamtStand.NichtGetippt += helper.NichtGetippt;
                    gesamtStand.PunkteInsgesamt += helper.PunkteInsgesamt;
                    gesamtStand.RichtigeTipps += helper.RichtigeTipps;
                    gesamtStand.UnechteBank += helper.UnechteBank;

                    if (round.RoundNo == selectedRound)
                        totalCurrentRound = helper.PunkteInsgesamt;

                    if(isAllowedToWriteTotalData)
                        helper.insertGesamtStandInDB(false, this._dbAccess);
                }
                else if (isRoundInDB)
                {
                    gesamtStand.EchteBank += gDB.echteBank;
                    gesamtStand.FalscheTipps += gDB.falscheTipps;
                    gesamtStand.NeunerTipp += gDB.NeunerTipp;
                    gesamtStand.NichtGetippt += gDB.nichtgetippt;
                    gesamtStand.PunkteInsgesamt += gDB.PunkteInsgesamt;
                    gesamtStand.RichtigeTipps += gDB.richtigeTipps;
                    gesamtStand.UnechteBank += gDB.unechteBank;

                    if (round.RoundNo == selectedRound)
                        totalCurrentRound = gDB.PunkteInsgesamt;
                }
                else if (!isRoundOver)
                {
                    this.calculateGesamtStand(gesamtStand);

                    if (round.RoundNo == selectedRound)
                        totalCurrentRound = gesamtStand.PunkteInsgesamt;
                }
            }

            return totalCurrentRound;
        }

        private void IsTotalByMemberInDb(GesamtStand helper, out Gesamtstands gDB, out bool isRoundInDB, out bool isRoundOver)
        {
            isRoundOver = helper.tryGetGesamtStandFromDB(this._dbAccess,out gDB, out isRoundInDB);
        }

        private void calculateGesamtStand(object gesamtStand)
        {
            bool isNeunerTipp = true;

            //kompletter Spieltag nicht getippt
            bool hasCompletelyForgotten = true;

            _Games = new RoundBL(_Round, _Season, _dbAccess).GetGames();
            TippSet = _dbAccess.GetTippedValues(_Round, _Season, _Member, _Games);
            KeyValuePair<Tipp, List<TippState.TippStateEnum>> tipp = this.EvaluateMemberTipp();

            for (int i = 0; i < tipp.Value.Count; i++)
            {
                TippValue[] tippVals = new TippValue[tipp.Value.Count];
                tipp.Key.GivenTipps.Values.CopyTo(tippVals, 0);

                if (tippVals[i] != TippValue.NotSet)
                    hasCompletelyForgotten = false;

                switch (tipp.Value[i])
                {
                    case TippState.TippStateEnum.True:
                        ((GesamtStand)gesamtStand).RichtigeTipps++;
                        ((GesamtStand)gesamtStand).PunkteInsgesamt++;
                        break;
                    case TippState.TippStateEnum.False:
                        if (tippVals[i] == TippValue.NotSet)
                            ((GesamtStand)gesamtStand).NichtGetippt++;
                        ((GesamtStand)gesamtStand).FalscheTipps++;
                        isNeunerTipp = false;
                        break;
                    case TippState.TippStateEnum.EchteBank:
                        ((GesamtStand)gesamtStand).EchteBank++;
                        ((GesamtStand)gesamtStand).RichtigeTipps++;
                        ((GesamtStand)gesamtStand).PunkteInsgesamt += 3;
                        break;
                    case TippState.TippStateEnum.UnechteBank:
                        ((GesamtStand)gesamtStand).UnechteBank++;
                        ((GesamtStand)gesamtStand).RichtigeTipps++;
                        ((GesamtStand)gesamtStand).PunkteInsgesamt += 2;
                        break;
                    default:
                        isNeunerTipp = false;
                        break;
                }
            }

            //falls jemand komplett nicht getippt hat
            if (hasCompletelyForgotten)
                ((GesamtStand)gesamtStand).PunkteInsgesamt = ((GesamtStand)gesamtStand).PunkteInsgesamt + getMinimumTippPoints(_Member);

            if (isNeunerTipp && (tipp.Value.Count > 0))
            {
                ((GesamtStand)gesamtStand).NeunerTipp++;
                //3 Punkte extra
                ((GesamtStand)gesamtStand).PunkteInsgesamt++;
                ((GesamtStand)gesamtStand).PunkteInsgesamt++;
                ((GesamtStand)gesamtStand).PunkteInsgesamt++;
            }

            ((GesamtStand)gesamtStand).RoundIndex++;
        }

        private int getMinimumTippPoints(Member actUser)
        {
            int minimumTipp = 100; ;
            foreach (Tipp t in AllTipps)
            {
                if (t.TGANMember.ID == actUser.ID)
                    continue;

                int actMinimumTipp = 100;
                foreach (RoundGame game in ((Dictionary<RoundGame, TippValue>)t.GivenTipps).Keys)
                {
                    //TippValue target; 
                    //TippState tippState = GetTippResult(game.Result,t.GivenTipps[game],out target);
                    TippState.TippStateEnum tippState = IsBankTipp(game, t.GivenTipps[game]);

                    //if ((tippState != TippState.False) && (tippState != TippState.NotReadable) && (actMinimumTipp == 100))
                    //if (((tippState == TippState.False) || (tippState == TippState.NotReadable)) && (actMinimumTipp == 100))
                    if ((tippState != TippState.TippStateEnum.NotReadable) && (actMinimumTipp == 100))
                        actMinimumTipp = 0;
                    
                    switch (tippState)
                    { 
                        case TippState.TippStateEnum.EchteBank:
                            actMinimumTipp = actMinimumTipp + 3;
                            break;
                        case TippState.TippStateEnum.UnechteBank:
                            actMinimumTipp = actMinimumTipp + 2;
                            break;
                        case TippState.TippStateEnum.True:
                            actMinimumTipp++;
                            break;
                    }
                }
                if (minimumTipp > actMinimumTipp)
                    minimumTipp = actMinimumTipp;
            }
            //alle haben nicht getippt
            if (minimumTipp == 100 )
                return 0;
            else if (minimumTipp == 0)
                return 0;
            else
                return minimumTipp - 1;
        }

        /// <summary>
        /// handelt es sich bei diese Spieltipps um eine Bank
        /// </summary>
        /// <returns></returns>
        private KeyValuePair<Tipp, List<TippState.TippStateEnum>> EvaluateMemberTipp()
        {
            try
            {
              List<Member> users = new MemberBL(_dbAccess).GetAllMembers(_UserGroup, true, _Season);
                AllTipps = _dbAccess.GetAllTippsOfAllMembersPerRound(_Round, _Games, _Season, users);

                int count = TippSet.GivenTipps.Count;

                List<TippState.TippStateEnum> states = new List<TippState.TippStateEnum>();
                TippValue[] tippvals = new TippValue[count];
                TippSet.GivenTipps.Values.CopyTo(tippvals, 0);

                RoundGame[] rGames = new RoundGame[count];
                TippSet.GivenTipps.Keys.CopyTo(rGames, 0);

                for (int i = 0; i < count; i++)
                    states.Add(IsBankTipp(rGames[i], tippvals[i]));

                return new KeyValuePair<Tipp, List<TippState.TippStateEnum>>(TippSet, states);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        
        public static string GetStringTippValue(TippValue val)
        {
            switch (val)
            { 
                case TippValue.Away:
                    return "2";
                case TippValue.Draw:
                    return "0";
                case TippValue.Home:
                    return "1";
                case TippValue.NotSet:
                default:
                    return "~";
            }
        }

        public static TippValue GetEnumTippValue(string val)
        {
            switch (val)
            {
                case "2":
                    return TippValue.Away;
                case "0":
                    return TippValue.Draw;
                case "1":
                    return TippValue.Home;
                case "~":
                default:
                    return TippValue.NotSet;
            }
        }

        /// <summary>
        /// Test, dass mindestens eine 2 oder 0 gesetzt wurde
        /// </summary>

        /// <param name="tipp"></param>
        /// <returns></returns>
        private bool IsValide()
        {
            foreach (TippValue val in TippSet.GivenTipps.Values)
            {
                if((val == TippValue.Away) || (val == TippValue.Draw) || (val == TippValue.NotSet))
                    return true;
            }

            return false;
        }

        public static TippValue GetTendenz(string result)
        {
            TippValue target;
            GetTippResult(result,TippValue.Away,out target);
            return target;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="userTipp"></param>
        /// <param name="target">Tendenz</param>
        /// <returns></returns>
        private static TippState.TippStateEnum GetTippResult(string result, TippValue userTipp, out TippValue target)
        {
            target = DetermineResultOfGame(result);

            if (target == TippValue.NotSet)
                return TippState.TippStateEnum.NotReadable;
            else if (userTipp == target)
                return TippState.TippStateEnum.True;
             else
                return TippState.TippStateEnum.False;
        }

        public static TippValue DetermineResultOfGame(string resultString)
        {
            string[] endergebins = null;
            int toreHeim = -1;
            int toreAusw = -1;
           
            if (String.IsNullOrEmpty(resultString))
                return TippValue.NotSet;

            endergebins = GetFinalResultArray(resultString);

            if ((!Int32.TryParse(endergebins[0], out toreHeim)) || (!Int32.TryParse(endergebins[1], out toreAusw)))
                return TippValue.NotSet;

            if (toreHeim == toreAusw) return TippValue.Draw;
            else if (toreHeim > toreAusw) return TippValue.Home;
            else if(toreHeim < toreAusw) return TippValue.Away;

            return TippValue.NotSet;
        }

        public static string[] GetFinalResultArray(string resultString)
        {
            int index = resultString.IndexOf(STR_TRENNUNG_ENDERGEBNIS_HALBZEITSTAND);
            if (index == -1)
            {
                index = resultString.IndexOf(CHR_TRENNUNG_ENDERGEBNIS_HALBZEITSTAND);

                if (index == -1)
                    throw new Exception("Result could not be parsed");
                //wegen Leerzeichen
                index--;
            }

            return resultString.Substring(0, index).Split(':');
        }

        private TippState.TippStateEnum IsBankTipp(RoundGame game,TippValue userTipp)
        {
            try
            {
                //it is not used in here
                TippValue val;
                TippState.TippStateEnum currentUserTippResult = GetTippResult(game.Result, userTipp, out val);
                TippValue memberTipp = TippValue.NotSet;
                List<TippValue> tippOfAllMembers = new List<TippValue>();
                // pr�ft, ob alle User den gleichen falschen Tipp get�tigt haben
                bool isFalseTipp1 = false;
                bool isFalseTipp2 = true;
                bool isFalsTipp1Set = false;

                if (currentUserTippResult != TippState.TippStateEnum.True)
                    return currentUserTippResult;

                foreach (Tipp t in AllTipps)
                {
                    t.GivenTipps.TryGetValue(game, out memberTipp);

                    //keine Bank
                    if ((GetTippResult(game.Result, memberTipp,out val) == currentUserTippResult) && (t.TGANMember.UserName != _Member.UserName) && (t.TGANMember.UserGroupID == _Member.UserGroupID))
                        return currentUserTippResult;
                    else if ((memberTipp != TippValue.NotSet) && (t.TGANMember.UserName != _Member.UserName) && (t.TGANMember.UserGroupID == _Member.UserGroupID))
                        tippOfAllMembers.Add(memberTipp);
                }

                //ist ne unechte Bank
                currentUserTippResult = TippState.TippStateEnum.UnechteBank;

                //Pr�fung auf echte Bank
                if (tippOfAllMembers.Count > 0)
                {
                    for (int i = 0; i < _tippTypes.Length; i++)
                    {
                        if (_tippTypes[i] != userTipp)
                        {
                            if (!isFalsTipp1Set) isFalseTipp1 = tippOfAllMembers.Contains(_tippTypes[i]);
                            if (isFalsTipp1Set) isFalseTipp2 = tippOfAllMembers.Contains(_tippTypes[i]);

                            isFalsTipp1Set = true;
                        }
                    }
                }

                if (isFalseTipp1 != isFalseTipp2)
                    currentUserTippResult = TippState.TippStateEnum.EchteBank;

                return currentUserTippResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void CheckStartTime()
        {
            RoundGame[] games = new RoundGame[TippSet.GivenTipps.Count];
            //Tipp, der gerade gesetzt wurde
            TippSet.GivenTipps.Keys.CopyTo(games, 0);
            TippValue tippBeingSaved;
            //check if tipp was set allready => Tipp aus der Datenbank
            Tipp dbTipp = GetTippWithoutTippEvaluation();
            
            foreach (RoundGame  game in games)
            {
                if (game.IsHidden)
                    continue;

                // wenn das Spiel bereits gestartet ist
                if (DateTime.Compare(game.StartTime, DateTime.Now) < 1)
                {
                    //Spiel mit Tipp wird aus dem gerade gesetzten Tipp entfernt 
                    TippSet.GivenTipps.Remove(game);

                    //falls bereits vorher getippt wurde, dann wird der vorher gesetzte Spieltipp in die DB geschrieben
                    if (dbTipp.GivenTipps.TryGetValue(game, out tippBeingSaved))
                        TippSet.GivenTipps.Add(game, tippBeingSaved);
                    //sonst wird der Spieltipp auf nicht getippt gesetzt 
                    else
                        TippSet.GivenTipps.Add(game, TippValue.NotSet);
                }
            }
        }
    }
}
