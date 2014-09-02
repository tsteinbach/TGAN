using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Common;

using DataLayerLogic.DataSets;
using DataLayerLogic.Types;
using System.Collections.Generic;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Spieltag.
	/// </summary>
	public class Spieltage
	{
		Season m_season;
		SqlConnection m_conn;
		SqlCommand m_selectSpielag;
		SqlCommand m_insertSpielag;

        private const string SELECTSPIELTAGEOFSEASON = "SELECT Spieltag.ID,Spieltag.SeasonID,Spieltag.Spieltag  " +
                                                        "FROM Spieltag " +
                                                        //"Where Season.ID = {0}";    
                                                        "Where Season.ID = @SeasonID";
		
		private const string INSERTSPIELTAGEOFSEASON = "INSERT INTO Spieltag (ID, SeasonID, Spieltag) "+
														"VALUES(@ID, @SeasonID, @Spieltag)";

        private const string SELECTROUNDBYROUNDNO = "SELECT ID,SeasonID,Spieltag "+
                                                    "FROM Spieltag "+
                                                    "WHERE SeasonID = '{0}' AND Spieltag = {1}";
        				                            //"WHERE SeasonID = @SeasonID AND Spieltag = @Spieltag";
        				
		public Spieltage(SqlConnection conn, Season season)
		{
			m_season = season;	
			m_conn = conn;
		}

        public Round GetRoundByRoundNo(int roundNo)
        {
            string tsql="";

            try
            {
                Round r = null;
                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    m_selectSpielag = new SqlCommand(string.Format(SELECTROUNDBYROUNDNO, m_season.ID, roundNo), c);
                    //m_selectSpielag.Parameters.Add("@SeasonID", SqlDbType.UniqueIdentifier).Value = m_season.ID;
                    //m_selectSpielag.Parameters.Add("@Spieltag", SqlDbType.Int).Value = roundNo;

                    tsql = m_selectSpielag.CommandText;

                    c.Open();
                    SqlDataReader reader = m_selectSpielag.ExecuteReader();

                    while (reader.Read())
                    {
                        r = new Round(reader.GetGuid(0), reader.GetGuid(1), reader.GetInt32(2));
                    }
                    reader.Close();
                    c.Close();
                }
                return r;
                
            }
            catch(Exception ex)
            {
                throw new Exception(tsql, ex);
            }
        }

		public int InsertNewSpieltage(Season season)
		{
            int anz = 0;
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                c.Open();
                for (int i = 1; i <= 34; i++)
                {
                    m_insertSpielag = new SqlCommand(INSERTSPIELTAGEOFSEASON, c);
                    m_insertSpielag.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    m_insertSpielag.Parameters.Add("@SeasonID", SqlDbType.UniqueIdentifier).Value = season.ID;
                    m_insertSpielag.Parameters.Add("@Spieltag", SqlDbType.Int).Value = i;
                    anz = anz + m_insertSpielag.ExecuteNonQuery();
                }
                c.Close();
            }
            return anz;
		}
	}
}
