using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für UpdateData.
	/// </summary>
	public class UpdateData
	{
		public UpdateData(SqlConnection conn)
		{
			m_conn = conn;
			conn.Open();
		}

		private SqlConnection m_conn;
		private SqlCommand myCommand  = null;

		public void UpdateTipp(ArrayList ansetzungIDs, string user, ArrayList tipp)
		{
			for(int i=0;i<ansetzungIDs.Count;i++)
			{
				myCommand = new SqlCommand();

				myCommand.Parameters.Add("@AnsetzungID", SqlDbType.UniqueIdentifier).Value = ansetzungIDs[i];
				myCommand.Parameters.Add("@Tipp", SqlDbType.VarChar).Value = tipp[i];
				myCommand.Parameters.Add("@Mitglied", SqlDbType.VarChar,40).Value = user;
								
				myCommand.Connection = m_conn;
				string updateTipp = "Update Tipp Set Tipp = @Tipp where ((AnsetzungID=@AnsetzungID)and(Mitglied=@Mitglied))";				

				myCommand.CommandText = updateTipp;
				myCommand.ExecuteNonQuery();
			}
				
			m_conn.Close();
		}

		
	}
}
