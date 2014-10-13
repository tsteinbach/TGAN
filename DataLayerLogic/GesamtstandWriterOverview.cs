using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataLayerLogic
{
    public class GesamtstandWriterOverview
    {
        private SqlConnection m_conn;

        public GesamtstandWriterOverview(SqlConnection conn)
        {
            m_conn = conn;
        }

        internal bool IsGroupInCurrentTotalWriterList(Guid userGroupId)
        {
            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {
                try
                {
                    SqlCommand commSelect = new SqlCommand(String.Format("Select UserGroupID from dbo.[GesamtstandWriterOverview] where UserGroupID = '{0}'", userGroupId), conn);
                    
                    conn.Open();
                    return commSelect.ExecuteScalar() != null;
                }       
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        internal void InsertGroupInCurrentTotalWriterList(Guid userGroupId)
        {
            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {
                try
                {
                    SqlCommand commInsert = new SqlCommand("InsertGesamtstandWriterOverview", conn);
                    commInsert.CommandType = CommandType.StoredProcedure;

                    commInsert.Parameters.Add("@UserGroupID", SqlDbType.UniqueIdentifier).Value = userGroupId;
                    conn.Open();
                    commInsert.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        internal void RemoveGroupFromCurrentTotalWriterList(Guid userGroupId)
        {
            using (SqlConnection conn = new SqlConnection(m_conn.ConnectionString))
            {
                try
                {
                    SqlCommand commRemove = new SqlCommand(String.Format("Delete from dbo.[GesamtstandWriterOverview] where UserGroupID = '{0}'", userGroupId), conn);

                    conn.Open();
                    commRemove.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
