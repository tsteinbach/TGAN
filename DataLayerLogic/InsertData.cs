using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using DataLayerLogic.DataSets;

namespace DataLayerLogic
{
	public class InsertData
	{
		public InsertData(SqlConnection conn)
		{
			conn.Open();
			m_conn = conn;
		}

		private SqlConnection m_conn;	
		private SqlCommand myCommand  = null;

		public void FillTipp(ArrayList ansetzungIDs, string user, ArrayList tipp)
		{
			for(int i=0;i<ansetzungIDs.Count;i++)
			{
				myCommand = new SqlCommand();

				myCommand.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
				myCommand.Parameters.Add("@AnsetzungID", SqlDbType.UniqueIdentifier).Value = ansetzungIDs[i];
				myCommand.Parameters.Add("@Tipp", SqlDbType.VarChar).Value = tipp[i];
				myCommand.Parameters.Add("@Mitglied", SqlDbType.VarChar,40).Value = user;
								
				myCommand.Connection = m_conn;
				string insertTipp = "Insert Into Tipp (Id, AnsetzungID, Tipp, Mitglied) Values (@ID,@AnsetzungID,@Tipp,@Mitglied)";				

				myCommand.CommandText = insertTipp;
				myCommand.ExecuteNonQuery();
			}
				
			m_conn.Close();
		}
	}
}
