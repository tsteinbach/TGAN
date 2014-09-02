using System;
using System.Data.SqlClient;
using System.Data;
using DataLayerLogic.Types;
using DataLayerLogic.DataSets;
using System.Collections.Generic;
using System.Data.Common;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Forums.
	/// </summary>
	public class Foren
	{
		private SqlConnection m_conn;
		private const string m_InsertThema = "Insert Into Forum_Themen (ID, Titel, MemberID, DateTime) "+
			"Values(@ID,@Titel,@MemberID,@DateTime)";
		private const string m_InsertInhalt = "Insert Into Forum_Inhalt (ID, MemberID, Forum_ThemenID, Inhalt,DateTime) "+
			"Values(@ID,@MemberID,@ThemenID,@Inhalt,@DateTime)";
		private const string m_SelectThemen = "Select ID,Titel,MemberID,DateTime from Forum_Themen order by DateTime DESC";
        private const string m_SelectInhalt = "Select ID,MemberID,Forum_ThemenID,Inhalt, DateTime from Forum_Inhalt " +
                        "Where Forum_ThemenID = @Titel order By DateTime DESC";
		private const string m_SelectActualContent = "SELECT ft.ID,ft.Titel, COUNT(ft.Titel) AS Anzahl "+
													"FROM (SELECT * "+
															"FROM forum_inhalt fi "+
                                                            "WHERE Dateadd(day, {0}, fi.[DateTime]) >= getdate()) fi " +
														"INNER JOIN Forum_Themen ft ON ft.ID = fi.Forum_ThemenID "+
                                                    "GROUP BY ft.ID,ft.Titel order by Max(fi.DateTime) DESC";					

		public Foren(SqlConnection conn)
		{
			m_conn = conn;
		}

		public int InsertThema(Forum_Themen theme)
		{
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand insertThema = new SqlCommand(m_InsertThema, c);

                insertThema.Parameters.Add("@MemberID", SqlDbType.UniqueIdentifier).Value = theme.MemberID;
                insertThema.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = theme.ID;
                insertThema.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = theme.DateOfInsert;
                insertThema.Parameters.Add("@Titel", SqlDbType.NVarChar, 200).Value = theme.Title;

                c.Open();
                int result = insertThema.ExecuteNonQuery();
                c.Close();

                return result;
            }
		}

		public int InsertInhalt(Forum_Inhalt content)
		{
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand insertInhalt = new SqlCommand(m_InsertInhalt, c);

                insertInhalt.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = content.ID;
                insertInhalt.Parameters.Add("@MemberID", SqlDbType.UniqueIdentifier).Value = content.MemberId;
                insertInhalt.Parameters.Add("@ThemenID", SqlDbType.UniqueIdentifier).Value = content.ThemenID;
                insertInhalt.Parameters.Add("@DateTime", SqlDbType.DateTime).Value = content.DateOfInsert;
                insertInhalt.Parameters.Add("@Inhalt", SqlDbType.Text).Value = content.Content;

                c.Open();
                int result = insertInhalt.ExecuteNonQuery();
                c.Close();

                return result;
            }
		}

		public List<Forum_Themen> SelectThema()
		{
            ForumData f_thema = new ForumData();
            List<Forum_Themen> result = new List<Forum_Themen>();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand selectThema = new SqlCommand(m_SelectThemen, c);
                SqlDataAdapter daThema = new SqlDataAdapter(selectThema);
                daThema.TableMappings.AddRange(new DataTableMapping[] { new DataTableMapping("Table", "Forum_Themen") });

                c.Open();
                daThema.Fill(f_thema);
                c.Close();

                foreach (ForumData.Forum_ThemenRow r in f_thema.Forum_Themen.Rows)
                    result.Add(new Forum_Themen(r.ID, r.MemberID, r.Titel, r.DateTime));
            }
            return result;
        }

		public List<Forum_Inhalt> SelectInhalt(Forum_Themen theme)
		{
            ForumData f_inhalt = new ForumData();
            List<Forum_Inhalt> result = new List<Forum_Inhalt>();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand selectInhalt = new SqlCommand(m_SelectInhalt, c);
                selectInhalt.Parameters.Add("@Titel", SqlDbType.UniqueIdentifier).Value = theme.ID;
                SqlDataAdapter daInhalt = new SqlDataAdapter(selectInhalt);
                daInhalt.TableMappings.AddRange(new DataTableMapping[] { new DataTableMapping("Table", "Forum_Inhalt") });

                c.Open();
                daInhalt.Fill(f_inhalt);
                c.Close();

                foreach (ForumData.Forum_InhaltRow row in f_inhalt.Forum_Inhalt.Rows)
                    result.Add(new Forum_Inhalt(row.ID, row.MemberID, row.Forum_ThemenID, row.Inhalt, row.DateTime));
            }
            return result;
		}

		public List<Forum_ActualContent> SelectActualContent(int daysToAdd)
		{
            string tsql = "";
            try
            {
                    List<Forum_ActualContent> result = new List<Forum_ActualContent>();

                    using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                    {
                        SqlCommand select = new SqlCommand(string.Format(m_SelectActualContent,daysToAdd), c);
                        //select.Parameters.Add("@DaysToAdd", SqlDbType.Int).Value = daysToAdd;
                        c.Open();
                        tsql = select.CommandText; 
                        tsql +=" PARAMS: " + daysToAdd;
                        SqlDataReader reader = select.ExecuteReader();
                        while (reader.Read())
                            result.Add(new Forum_ActualContent(reader.GetString(1), reader.GetGuid(0), reader.GetInt32(2)));
                        reader.Close();
                        c.Close();
                    }
			        return result;
            }
            catch(Exception ex)
            {
                throw new Exception(tsql,ex);
            }
		}
	}
}
