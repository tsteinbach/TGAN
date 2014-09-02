using System;
using System.Data;
using System.Data.SqlClient;
using DataLayerLogic.Types;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für CheckRounds.
	/// </summary>
	public class CheckRounds
	{
		private SqlConnection m_conn;
		private const string m_selectDateTime = "SELECT Checked "+
													"FROM CheckRounds INNER JOIN Season "+
														"ON CheckRounds.SeasonID  = Season.ID "+
															"WHERE (Season.Season = @Saison) AND (DayOfYear = @DayOfYear)";
        private const string m_FillCheckedRounds = "INSERT INTO CheckRounds " +
                                            "(ID, SeasonID, DayOfYear, Checked) " +
                                                "VALUES (@ID,@SeasonID,@DayOfYear,@Checked)";

		public CheckRounds(SqlConnection conn)
		{
			m_conn = conn;
		}

		public bool DateTimeCheck(Season season)
		{
			object result = false;
			SqlCommand sqlSelectDateTime = null;

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                sqlSelectDateTime = new SqlCommand(m_selectDateTime, c);

                sqlSelectDateTime.Parameters.Add("@Saison", SqlDbType.VarChar, 20).Value = season.Name;
                sqlSelectDateTime.Parameters.Add("@DayOfYear", SqlDbType.Int).Value = DateTime.Today.DayOfYear;

                c.Open();
                result = sqlSelectDateTime.ExecuteScalar();
                c.Close();
            }
			if(result == null)
				return false;
			else
				return (bool)result;
		}

        public void FillCheckRounds(Season season)
        {
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand sqlFillCheckedRounds = new SqlCommand(m_FillCheckedRounds, c);

                sqlFillCheckedRounds.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                sqlFillCheckedRounds.Parameters.Add("@SeasonID", SqlDbType.UniqueIdentifier).Value = season.ID;
                sqlFillCheckedRounds.Parameters.Add("@DayOfYear", SqlDbType.Int).Value = DateTime.Today.DayOfYear;
                sqlFillCheckedRounds.Parameters.Add("@Checked", SqlDbType.Bit).Value = 1;

                c.Open();
                sqlFillCheckedRounds.ExecuteNonQuery();
                c.Close();
            }
        }
	}
}
