using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using DataLayerLogic.DataSets;
using DataLayerLogic.Types;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Ansetzungen.
	/// </summary>
	public class Ansetzungen
	{
		private SqlConnection m_conn;
        //private const string m_InsertAnsetzung  = "INSERT INTO Ansetzung "+
        //                                        "(ID, TeamID_home,TeamID_away,Datum, Zeit, SpieltagID, Spiel,Result) " +
        //                                            "VALUES (@ID,@Heim_Team,@Gast_Team,@Datum,@Zeit,@SpieltagID,@Spiel,@Result)";
        //{0}
        private const string m_InsertAnsetzung = "INSERT INTO Ansetzung " +
                                                "(ID, TeamID_home,TeamID_away, Zeit, SpieltagID, Spiel,Result,IsHidden) " +
                                                    "VALUES (@ID,@Heim_Team,@Gast_Team,@Zeit,@SpieltagID,@Spiel,@Result,0)";



        //private const string SELECTRESCHEDULEDGAMES = "select (select NameToShow from Teams where a.TeamID_home = id) as Heim, "+
        //                                                        "(select NameToShow from Teams where a.TeamID_away = id) as Gast, "+
        //                                                        "s.Spieltag, se.Season "+
        //                                            "from Ansetzung a inner join Spieltag s on a.spieltagid = s.id "+
        //                                            "inner join season se on se.id = s.seasonid "+
        //                                            "where a.ishidden = 1";
        private const string SELECTRESCHEDULEDGAMES_ByFunc = "select * from GetRescheduledGames()";

        //private const string m_SelectAnsetzung = "SELECT Ansetzung.ID "+
        //                                        "FROM Ansetzung INNER JOIN Spieltag "+
        //                                            "ON Ansetzung.SpieltagID = Spieltag.ID "+
        //                                                //"WHERE (Ansetzung.Spiel = {0}) AND (Ansetzung.SpieltagID ='{1}')";
        //                                                "WHERE (Ansetzung.Spiel = @Spiel) AND (Ansetzung.SpieltagID = @SpieltagID)";
        //private const string SELECTAVAILABLEROUNDS = "SELECT Distinct Spieltag.ID,Spieltag.SeasonID,Spieltag.Spieltag " +
        //                       "FROM Ansetzung INNER JOIN "+
        //                        "Spieltag ON Ansetzung.SpieltagID = Spieltag.ID "+
        //                                "WHERE Spieltag.SeasonID = @Saison Order by Spieltag.Spieltag";
        //private const string SELECTCHECKABLEROUNDS = "SELECT DISTINCT Spieltag.ID,Spieltag.SeasonID,Spieltag.Spieltag " +
        //                                    "FROM Ansetzung INNER JOIN Spieltag ON "+
        //                                        "Ansetzung.SpieltagID = Spieltag.ID "+
        //                                    "WHERE (Ansetzung.Zeit > getDate()) AND (Spieltag.SeasonID = @Season) ORDER BY Spieltag.Spieltag ASC";

        private const string SELECTCHECKABLEROUNDS_ByFunc = "select * from GetCheckableRounds(@Season) ORDER BY Spieltag ASC";
		
        //private const string m_SelectAnsetzungID = "SELECT Ansetzung.ID, Ansetzung.Spiel "+
        //                                            "FROM Ansetzung INNER JOIN Spieltag INNER JOIN Season "+
        //                                                "ON Spieltag.SeasonID = Season.ID "+
        //                                                "ON Ansetzung.SpieltagID = Spieltag.ID "+
        //                                            "WHERE (Season.Season = @Saison) AND (Spieltag.Spieltag = @Spieltag) "+
        //                                            "ORDER BY Ansetzung.Spiel";
        //private const string SELECTSINGLEROUND = "SELECT Ansetzung.ID,Ansetzung.SpieltagID,Ansetzung.Zeit,Ansetzung.TeamID_home, Ansetzung.TeamID_away, " +
        //                                        "Ansetzung.Result,Ansetzung.Spiel,IsHidden " +
        //                                    "FROM Ansetzung INNER JOIN Spieltag " +
        //                                        "ON Ansetzung.SpieltagID = Spieltag.ID " +
        //                                    "WHERE (Spieltag.SeasonID = @Saison) AND (Spieltag.Spieltag = @Spieltag) " +
        //                                    "ORDER BY Ansetzung.Spiel";

        private const string SELECTSINGLEROUND_ByFunc = "select * from SelectSingleRound(@Spieltag, @Saison) order by Spiel";

        //private const string SELECTACTUALROUNDNO = "SELECT Max(ansetzung.Zeit), Spieltag.Spieltag "+
        //                                            "FROM Ansetzung INNER JOIN Spieltag ON "+
        //                                                "Ansetzung.SpieltagID = Spieltag.ID "+
        //                                            "WHERE (Ansetzung.Zeit > @Time) AND (Spieltag.SeasonID = @Season) "+ 
        //                                            "Group By Spieltag.ID, Spieltag.Spieltag "+
        //                                            "Order by Spieltag.Spieltag ASC";

        private const string SELECTACTUALROUNDNO_ByFunc = "select * from SelectActualRoundNo(@Time, @Season) order by Spieltag asc";

        //private const string SELECTACTUALROUNDNO_HIDDENgames_NOTINCLUDED = "SELECT Max(ansetzung.Zeit), Spieltag.Spieltag " +
        //                                            "FROM Ansetzung INNER JOIN Spieltag ON " +
        //                                                "Ansetzung.SpieltagID = Spieltag.ID " +
        //                                            "WHERE (Ansetzung.Zeit > @Time) AND (Spieltag.SeasonID = @Season) AND ((IsHidden = 0) OR (IsHidden is null) )" +
        //                                            "Group By Spieltag.ID, Spieltag.Spieltag " +
        //                                            "Order by Spieltag.Spieltag ASC";

        private const string SELECTACTUALROUNDNO_HIDDENgames_NOTINCLUDED_ByFunc = "select * from SelectActualRoundNoWithoutHiddenGames(@Time, @Season) order by Spieltag asc";
        
        //private const string m_UpdateRound = "Update Ansetzung " +
        //                                    "SET Zeit = @Zeit, Result = @Result, IsHidden = @IsRescheduled " +
        //                                    "WHERE (TeamID_home = @team_home) AND (TeamID_away = @team_away) AND (SpieltagID = @Spieltag)"; 
		
		public Ansetzungen(SqlConnection conn)
		{
			m_conn = conn;
		}

        
        
		public void InsertNewAnsetzung(Season season, List<RoundGame> games)
		{
            try
            {
                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    SqlCommand insertData = new SqlCommand(m_InsertAnsetzung, c);

                    insertData.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                    insertData.Parameters.Add("@Heim_Team", SqlDbType.UniqueIdentifier);
                    insertData.Parameters.Add("@Gast_Team", SqlDbType.UniqueIdentifier);
                    //insertData.Parameters.Add("@Datum", SqlDbType.DateTime);
                    insertData.Parameters.Add("@Zeit", SqlDbType.DateTime);
                    insertData.Parameters.Add("@SpieltagID", SqlDbType.UniqueIdentifier);
                    insertData.Parameters.Add("@Spiel", SqlDbType.Int);
                    insertData.Parameters.Add("@Result", SqlDbType.NVarChar, 100);

                    foreach (RoundGame game in games)
                    {
                        try
                        {
                            c.Open();

                            insertData.Parameters["@ID"].Value = game.ID;
                            insertData.Parameters["@Heim_Team"].Value = game.HomeTeam;
                            insertData.Parameters["@Gast_Team"].Value = game.AwayTeam;
                            //insertData.Parameters["@Datum"].Value = game.PlayDate;
                            insertData.Parameters["@Zeit"].Value = game.StartTime;
                            insertData.Parameters["@SpieltagID"].Value = game.RoundID;
                            insertData.Parameters["@Spiel"].Value = game.GameNo;
                            insertData.Parameters["@Result"].Value = game.Result;

                            if (insertData.ExecuteNonQuery() != 1)
                                throw new Exception("data of round " + game.RoundID + " was not inserted correctly");

                            c.Close();
                        }
                        catch (Exception ex)
                        {
                            c.Close();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
		}

        private Guid _seasonId = Guid.Empty;
        private int _roundNo = 0;

        private bool FindRound(Round round)
        {
            if ((round.SeasonID == _seasonId) && (round.RoundNo == _roundNo))
                return true;
            else
                return false;
        }

		public void GetDistictAvailableRounds(Season season, List<Round> rounds)
		{
            //using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            //{
            //    SqlCommand sqlDistinctAvailableRounds = new SqlCommand(SELECTAVAILABLEROUNDS, c);
            //    sqlDistinctAvailableRounds.Parameters.Add("@Saison", SqlDbType.UniqueIdentifier).Value = season.ID;

            //    c.Open();
            //    SqlDataReader reader = sqlDistinctAvailableRounds.ExecuteReader();
            //    while (reader.Read())
            //        rounds.Add(new Round(reader.GetGuid(0), reader.GetGuid(1), reader.GetInt32(0)));
            //    c.Close();
            //}
            throw new NotImplementedException("GetDistictAvailableRounds is not implemented");
		}

        public int DetermineActualRoundNo(Season season, bool hiddenGamesincluded)
        {
            string tsql="";

            try
            {
                int roundNo = 1;
                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    SqlCommand sqlSelectCheckableRounds;
                    if(hiddenGamesincluded)
                        sqlSelectCheckableRounds = new SqlCommand(SELECTACTUALROUNDNO_ByFunc, c);
                        //sqlSelectCheckableRounds = new SqlCommand(SELECTACTUALROUNDNO, c);
                    else
                        sqlSelectCheckableRounds = new SqlCommand(SELECTACTUALROUNDNO_HIDDENgames_NOTINCLUDED_ByFunc, c);
                        //sqlSelectCheckableRounds = new SqlCommand(SELECTACTUALROUNDNO_HIDDENgames_NOTINCLUDED, c);

                    sqlSelectCheckableRounds.Parameters.Add("@Season", SqlDbType.UniqueIdentifier).Value = season.ID;
                    sqlSelectCheckableRounds.Parameters.Add("@Time", SqlDbType.DateTime).Value = DateTime.Now.AddDays(-1);

                    tsql = sqlSelectCheckableRounds.CommandText;

                    c.Open();
                    SqlDataReader reader = sqlSelectCheckableRounds.ExecuteReader();
                    while (reader.Read())
                    {
                        roundNo = reader.GetInt32(1);
                        //nur der 1. Spieltag ist interssant
                        break;
                    }
                    reader.Close();
                    c.Close();
                }

                return roundNo;

            }
            catch(Exception ex)
            {
                throw new Exception(tsql,ex);
            }
        }

		public List<Round> GetCheckableRounds(Season season)
		{
            List<Round> rounds = new List<Round>();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand sqlSelectCheckableRounds = new SqlCommand(SELECTCHECKABLEROUNDS_ByFunc, c);

                sqlSelectCheckableRounds.Parameters.Add("@Season", SqlDbType.UniqueIdentifier).Value = season.ID;
                
                c.Open();
                SqlDataReader reader = sqlSelectCheckableRounds.ExecuteReader();
                while (reader.Read())
                {
                    rounds.Add(new Round(reader.GetGuid(0), reader.GetGuid(1), reader.GetInt32(2)));
                }
                reader.Close();
                c.Close();
            }
                return rounds;
       	}

		public void GetGuidfromAnsetzung(ArrayList result, Round spieltag, Season saison)
		{
            //using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            //{
            //    SqlCommand sqlGetGuid = new SqlCommand(m_SelectAnsetzungID, c);

            //    sqlGetGuid.Parameters.Add("@Spieltag", SqlDbType.Int).Value = spieltag.RoundNo;
            //    sqlGetGuid.Parameters.Add("@Saison", SqlDbType.VarChar, 20).Value = saison.Name;

            //    c.Open();
            //    SqlDataReader reader = sqlGetGuid.ExecuteReader();
            //    FillArrayList(reader, result);
            //    c.Close();
            //}

            throw new NotImplementedException("GetGuidfromAnsetzung is not implemented");
		}

        public List<RoundGame> SelectSingleRound(Round round)
		{
            List<RoundGame> games = new List<RoundGame>();
            

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                //SqlCommand sqlGetCertainRound = new SqlCommand(SELECTSINGLEROUND, c);
                SqlCommand sqlGetCertainRound = new SqlCommand(SELECTSINGLEROUND_ByFunc, c);

                sqlGetCertainRound.Parameters.Add("@Spieltag", SqlDbType.Int).Value = round.RoundNo;
                sqlGetCertainRound.Parameters.Add("@Saison", SqlDbType.UniqueIdentifier).Value = round.SeasonID;

                c.Open();
                SqlDataReader reader = sqlGetCertainRound.ExecuteReader();
                while (reader.Read())
                {
                    
                    if (!reader.IsDBNull(0) && !reader.IsDBNull(2) && !reader.IsDBNull(3) && !reader.IsDBNull(4) &&
                        !reader.IsDBNull(5) && !reader.IsDBNull(6))
                    {
                        //IsHidden is checked => it is possible to be null
                        bool isHidden = false;
                        Boolean.TryParse(reader.GetValue(7).ToString(), out isHidden);
                            
                        games.Add(new RoundGame(reader.GetGuid(0), reader.GetGuid(1), reader.GetDateTime(2), reader.GetGuid(3),
                            reader.GetGuid(4), reader.GetString(5), reader.GetInt32(6),isHidden));
                    }
                }
                reader.Close();
                c.Close();
            }
            return games;
		}

        public StringBuilder getRescheduledGames()
        {
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                StringBuilder sb = null;
                SqlCommand sqlgetGames = new SqlCommand(SELECTRESCHEDULEDGAMES_ByFunc, c);
                c.Open();

                try
                {
                    SqlDataReader reader = sqlgetGames.ExecuteReader();
                    if(reader.HasRows)
                        sb = new StringBuilder("<table><tr><th>Heim</th><th>Gast</th><th>Spieltag</th><th>Saison</th></tr>");        

                    while (reader.Read())
                    {
                        sb.Append(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                            ,reader.GetString(0)
                            ,reader.GetString(1)
                            ,reader.GetInt32(2)
                            ,reader.GetString(3)));
                    }

                    if (reader.HasRows)
                        sb.Append("</table>");        
                }
                catch (Exception ex)
                {
                    c.Close();
                    throw ex;
                }
                c.Close();
                return sb;
            }
        }

		public int UpdateRound(Round round, RoundGame game)
		{
            int anz = 0;

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand sqlUpdateRound = new SqlCommand("UpdateAnsetzung", c);
                sqlUpdateRound.CommandType = CommandType.StoredProcedure;

                //private const string m_UpdateRound = "Update Ansetzung " +
                //                                    "SET Zeit = @Zeit, Result = @Result, IsHidden = @IsRescheduled " +
                //                                    "WHERE (TeamID_home = @team_home) AND (TeamID_away = @team_away) AND (SpieltagID = @Spieltag)"; 

                //sqlUpdateRound.Parameters.Add("@Saison", SqlDbType.UniqueIdentifier).Value = round.SeasonID;
                sqlUpdateRound.Parameters.Add("@team_away", SqlDbType.UniqueIdentifier).Value = game.AwayTeam;
                sqlUpdateRound.Parameters.Add("@team_home", SqlDbType.UniqueIdentifier).Value = game.HomeTeam;
                sqlUpdateRound.Parameters.Add("@Zeit", SqlDbType.DateTime).Value = game.StartTime;
                sqlUpdateRound.Parameters.Add("@IsRescheduled", SqlDbType.Bit).Value = game.IsHidden;
                sqlUpdateRound.Parameters.Add("@Result", SqlDbType.NVarChar, 500).Value = game.Result;
                sqlUpdateRound.Parameters.Add("@Spieltag", SqlDbType.UniqueIdentifier).Value = game.RoundID;

                c.Open();
                
                try
                {
                    anz = sqlUpdateRound.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    c.Close();
                    throw ex;
                }
                c.Close();
            }
            return anz;
		}

	    private void FillArrayList(SqlDataReader reader, ArrayList list)
		{
			if(reader.HasRows)
				while(reader.Read())
					list.Add(reader.GetValue(0));
			reader.Close();
		}
	}
}
