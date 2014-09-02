using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Threading;

using DataLayerLogic.DataSets;
using DataLayerLogic.Types;
using System.Diagnostics;
using System.Collections.Generic;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Tipps.
	/// </summary>
    
    
    public class Tipps
	{
		private SqlConnection m_conn;
        
        //private const string INSERTTIPP = "INSERT INTO Tipp (Id, AnsetzungID, Tipp, MemberID,Time, Eingeber) "+
        //                                    "VALUES (newID(),@AnsetzungID,@Tipp,@MemberID,@Time,@Member)";
        //private const string UPDATETIPP = "UPDATE Tipp "+
        //                                    "SET Tipp = @Tipp, Time = @Time, Eingeber = @Member "+
        //                                    "WHERE ((AnsetzungID=@AnsetzungID)and(MemberID=@MemberID))";
//        private const string SElECTTIPPSOFMEMBER = @"SELECT Tipp.Tipp, Ansetzung.Spiel 
//FROM Tipp INNER JOIN Ansetzung ON Tipp.AnsetzungID = Ansetzung.ID INNER JOIN Spieltag ON Ansetzung.SpieltagID = Spieltag.ID INNER JOIN Member ON Tipp.MemberID = Member.ID 
//WHERE (Spieltag.Spieltag = @Spieltag) AND (Member.ID = @Mitglied) AND (Spieltag.SeasonID = @Saison) ORDER BY Ansetzung.Spiel";

        private const string SELECTTIPPOFMEMBER_ByFunc = @"SELECT Tipp, Spiel from GetTipp(@Spieltag, @Mitglied, @Saison ) order by Spiel";

        public Tipps(SqlConnection conn)
		{
			m_conn = conn;
		}

        private TippValue[] _tipps = null;
        private RoundGame[] _games = null;
        
        public int FillTipp(Tipp tipp)
		{
            try
            {
                int rows = 0;
                SqlCommand insertTipp;

                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    int length = tipp.GivenTipps.Count;

                    _tipps = new TippValue[length];
                    _games = new RoundGame[length];
                    tipp.GivenTipps.Values.CopyTo(_tipps, 0);
                    tipp.GivenTipps.Keys.CopyTo(_games, 0);

                    c.Open();
                    for (int i = 0; i < tipp.GivenTipps.Count; i++)
                    {
                        insertTipp = new SqlCommand("InsertTipp", c);
                        insertTipp.CommandType = CommandType.StoredProcedure;

                        insertTipp.Parameters.Add("@AnsetzungID", SqlDbType.UniqueIdentifier).Value = _games[i].ID;
                        insertTipp.Parameters.Add("@Tipp", SqlDbType.VarChar).Value = _tipps[i];
                        insertTipp.Parameters.Add("@MemberID", SqlDbType.UniqueIdentifier).Value = tipp.TGANMember.ID;
                        insertTipp.Parameters.Add("@Time", SqlDbType.DateTime).Value = DateTime.Now;
                        insertTipp.Parameters.Add("@Member", SqlDbType.VarChar, 50).Value = tipp.TippSetter.UserName;

                        insertTipp.ExecuteNonQuery();

                        rows++;
                    }
                    c.Close();
                }
                return rows;
            }
            catch (Exception e)
            {
                m_conn.Close();
                throw e;
            }
		}

		public int UpdateTipp(Tipp tipp)
		{
            try
            {
                int rows = 0;
                SqlCommand updateTipp;

                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    int length = tipp.GivenTipps.Count;

                    _tipps = new TippValue[length];
                    _games = new RoundGame[length];
                    tipp.GivenTipps.Values.CopyTo(_tipps, 0);
                    tipp.GivenTipps.Keys.CopyTo(_games, 0);

                    c.Open();

                    for (int i = 0; i < length; i++)
                    {
                        updateTipp = new SqlCommand("UpdateTipp", c);
                        updateTipp.CommandType = CommandType.StoredProcedure;

                        updateTipp.Parameters.Add("@AnsetzungID", SqlDbType.UniqueIdentifier).Value = _games[i].ID;
                        updateTipp.Parameters.Add("@Tipp", SqlDbType.VarChar).Value = _tipps[i];
                        updateTipp.Parameters.Add("@MemberID", SqlDbType.UniqueIdentifier).Value = tipp.TGANMember.ID;
                        updateTipp.Parameters.Add("@Time", SqlDbType.DateTime).Value = DateTime.Now;
                        updateTipp.Parameters.Add("@Member", SqlDbType.VarChar, 50).Value = tipp.TippSetter.UserName;
                        SqlParameter returnParam = updateTipp.Parameters.Add("@RowCount", SqlDbType.Int);
                        returnParam.Direction = ParameterDirection.ReturnValue;


                        updateTipp.ExecuteNonQuery();
                        rows += (Int32)updateTipp.Parameters["@RowCount"].Value;
                    }

                    c.Close();
                }
                return rows;
            }
            catch (Exception e)
            {
                m_conn.Close();
                throw e;
            }
		}

        public List<Tipp> GetAllTippsOfAllMembersPerRound(Round spieltag,List<RoundGame> games, Season saison, List<Member> members)
		{
            try
            {
                List<Tipp> allTippsOfRound = new List<Tipp>();
                
                foreach (Member m in members)
                    allTippsOfRound.Add(GetTipps(m, spieltag, games, saison));
                return allTippsOfRound;
            }
            catch (Exception e)
            {
                m_conn.Close();
                throw e;
            }
		}

        public Tipp GetTipps(Member user, Round spieltag,List<RoundGame> games, Season saison)
		{
            try
            {
                RoundGame game = null;
                Dictionary<RoundGame, TippValue> tipps = new Dictionary<RoundGame, TippValue>();

                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    //SqlCommand sqlSelectTipps = new SqlCommand(SElECTTIPPSOFMEMBER, c);
                    SqlCommand sqlSelectTipps = new SqlCommand(SELECTTIPPOFMEMBER_ByFunc, c);

                    sqlSelectTipps.Parameters.Add("@Saison", SqlDbType.UniqueIdentifier).Value = saison.ID;
                    sqlSelectTipps.Parameters.Add("@Spieltag", SqlDbType.Int).Value = spieltag.RoundNo;
                    sqlSelectTipps.Parameters.Add("@Mitglied", SqlDbType.UniqueIdentifier).Value = user.ID;

                    c.Open();
                    SqlDataReader reader = sqlSelectTipps.ExecuteReader();
                    object tipp = null;
                    while (reader.Read())
                    {
                        _gameNo = reader.GetInt32(1);
                        game = games.Find(FindGame);

                        tipp = reader.GetValue(0);
                        if (Buisinesses.ValidateDBValue(tipp, typeof(string)))
                            tipps.Add(game, (TippValue)Enum.Parse(typeof(TippValue), tipp.ToString()));
                        else
                            tipps.Add(game, TippValue.NotSet);
                    }

                    reader.Close();
                    c.Close();
                }
                //Eingabezeitpunkt und Eingabeperson ist nicht interessant
                return new Tipp(tipps, user, null, DateTime.MinValue);
            }
            catch (Exception e)
            {
                throw e;
            }
		}

        private int _gameNo = 0;
        private int GameNo
        {
            get { return _gameNo; }
            set { _gameNo = value; }
        }

        private bool FindGame(RoundGame g)
        {
            if (g.GameNo == GameNo)
                return true;
            else
                return false;
        }
	}
}
