using System;
using System.Collections.Generic;
using System.Text;
using DataLayerLogic.Types;
using System.Data.SqlClient;

namespace DataLayerLogic
{
    public class Teams
    {
        SqlConnection m_conn = null;

        public Teams(SqlConnection conn)
		{
			m_conn = conn;
		}

        public const string SELECTALLTEAMS = "Select * from Teams";

        public List<BundesligaTeam> GetAllTeams()
        {
            List<BundesligaTeam> result = new List<BundesligaTeam>();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand select = new SqlCommand(SELECTALLTEAMS, c);
                c.Open();
                SqlDataReader reader = select.ExecuteReader();
                Guid id = Guid.Empty;
                string kickerName = null;
                string logo = null;
                string nameToShow = null;

                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        id = Guid.Empty;
                    else
                        id = reader.GetGuid(0);

                    if (reader.IsDBNull(1))
                        kickerName = string.Empty;
                    else
                        kickerName = reader.GetString(1);

                    if (reader.IsDBNull(2))
                        logo = string.Empty;
                    else
                        logo = reader.GetString(2);

                    if (reader.IsDBNull(3))
                        nameToShow = string.Empty;
                    else
                        nameToShow = reader.GetString(3);

                    result.Add(new BundesligaTeam(id, kickerName, nameToShow, logo));
                }
                reader.Close();
                c.Close();
            }
            return result;
        }
    
    }
}
