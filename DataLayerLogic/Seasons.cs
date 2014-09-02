using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using DataLayerLogic.Types;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Seasons.
	/// </summary>
	public class Seasons
	{
		private SqlConnection m_conn;

        private const string GETACTUALSEASON = "Select ID, Season, ActualSeason,[Order] " +
                                                    "FROM Season " +
                                                        "WHERE (ActualSeason = 1)";
		private const string m_selectSeasonID = "Select ID "+
													"FROM Season "+
                                                    //"WHERE (Season = {0})";
														"WHERE (Season = @Season)";
        private const string SELECTALLSEASONS = "Select ID, Season, ActualSeason,[Order] from Season order by actualseason DESC";
		private const string INSERTACTUALSEASON = "INSERT INTO Season "+
												"(ID, Season, ActualSeason) "+
													"VALUES (@ID,@Season,1)";				
		private const string CHANGESTATUSOFACTUALSEASON = "UPDATE Season "+
														"SET ActualSeason = '0' "+
															"WHERE (ActualSeason = '1')";				

		public Seasons(SqlConnection conn)
		{
			m_conn=  conn;
		}

        public Season GetActualSeason()
        {
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(GETACTUALSEASON,c);
                c.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                  Season s = new Season(reader.GetGuid(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3));
                    reader.Close();
                    c.Close();
                    return s;
                }
                else
                {
                    reader.Close();
                    c.Close();
                    return null;
                }
            }
        }

		public List<Season> GetAllSeasons()
		{
            List<Season> seasons = new List<Season>();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand selectAllSeasons = new SqlCommand(SELECTALLSEASONS, c);
                c.Open();
                SqlDataReader reader = selectAllSeasons.ExecuteReader();
                while (reader.Read())
                  seasons.Add(new Season(reader.GetGuid(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3)));
                reader.Close();
                c.Close();
            }
			return seasons;
		}

		public void InsertActualSeason(Season season)
		{
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand sqlInsertActualSeason = new SqlCommand(INSERTACTUALSEASON, c);
                SqlCommand sqlChangeStatusOfActualSeason = new SqlCommand(CHANGESTATUSOFACTUALSEASON, c);

                sqlInsertActualSeason.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = season.ID;
                sqlInsertActualSeason.Parameters.Add("@Season", SqlDbType.Text).Value = season.Name;

                c.Open();
                sqlChangeStatusOfActualSeason.ExecuteNonQuery();
                c.Close();

                c.Open();
                sqlInsertActualSeason.ExecuteNonQuery();
                c.Close();
            }
		}
	}
}
