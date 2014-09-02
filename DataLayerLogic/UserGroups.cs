using System;
using System.Collections.Generic;
using System.Text;
using DataLayerLogic.Types;
using System.Data.SqlClient;
using System.Data;  

namespace DataLayerLogic
{
    public class UserGroups
    {
        private SqlConnection m_conn = null;
        public UserGroups(SqlConnection conn)
        {
            m_conn = conn;
        }

        private const string GETALLUSERGROUPS = "SELECT Id, Name FROM UserGroup";
        //private const string GETUSERGROUP = "SELECT Id, Name FROM UserGroup WHERE ID = @ID";
        private const string GETUSERGROUP_ByFunc = "SELECT Id, Name FROM GetUserGoup(@ID)";
        
        private const string INSERTUSERGROUP = "INSERT INTO [UserGroup] VALUES (@ID,@Name)";

        public int InsertUserGroup(string name, Guid id)
        {
            int anz = 0;

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand insert = new SqlCommand(INSERTUSERGROUP, c);

                insert.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;
                insert.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = name;

                c.Open();
                anz = insert.ExecuteNonQuery();

                c.Close();
            }
            if (anz == 0)
                throw new Exception("UserGroup could not be inserted");

            return anz;
        }

        public List<UserGroup> GetAllUserGroups()
        {
            List<UserGroup> groups = new List<UserGroup>();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand select = new SqlCommand(GETALLUSERGROUPS, c);

                c.Open();
                SqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    groups.Add(new UserGroup(reader.GetGuid(0), reader.GetString(1)));
                }

                reader.Close();
                c.Close();
            }
            return groups;
        }

        public UserGroup GetUserGroup(Guid id)
        {
            UserGroup group = null;

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {

                SqlCommand select = new SqlCommand(GETUSERGROUP_ByFunc, c);

                select.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = id;

                c.Open();
                SqlDataReader reader = select.ExecuteReader();

                while (reader.Read())
                {
                    group = new UserGroup(reader.GetGuid(0), reader.GetString(1));
                }

                reader.Close();
                c.Close();
            }
            return group;
        }
    }
}
