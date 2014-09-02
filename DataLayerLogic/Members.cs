using System;
using System.Data;
using System.Data.SqlClient;

using DataLayerLogic.DataSets;
using System.Collections.Generic;
using DataLayerLogic.Types;
using System.Data.Common;

namespace DataLayerLogic
{
    /// <summary>
    /// Zusammenfassung für Members.
    /// </summary>
    public class Members
    {
        private SqlConnection m_conn;
        private const string INSERTNEWUSER = "INSERT INTO Member " +
            "(ID,UserGroupID, [User], Vorname, Nachname, Adresse, PLZ, Ort, Titel, EMail, Password, Geburtstag, IsAdministrator,Telefon, IsSuperAdmin,MemberFromSeason, MemberToSeason) " +
            "VALUES (newID(),@UserGroupID,@User,@Vorname,@Nachname, @Adresse, @PLZ, @Ort, @Titel, @EMail, @Password, @Geburtstag,@IsAdministrator, @Telefon, 0,@MemberFromSeason,@MemberToSeason)";
        //private const string UPDATEUSER = "Update Member " +
        //    "SET UserGroupID = @UserGroupID,[User] =  @User_new, Vorname = @Vorname, Nachname = @Nachname, Adresse = @Adresse, PLZ = @PLZ,Ort = @Ort, Titel = @Titel, " +
        //        "EMail = @EMail, Geburtstag = @Geburtstag, IsAdministrator = @IsAdministrator, Telefon = @Telefon " +
        //        "MemberFromSeason = @MemberFromSeason, MemberToSeason = @MemberToSeason " +
        //    "WHERE [User] = @User_old AND @UserGroupID = @UserGroupID";
        //private const string SELECTUSER = "SELECT * FROM Member WHERE [User] = @User AND [UserGroupID] = @UserGroupID";
        private const string SELECTUSER_ByFunc = "select * from GetUserByGroupAndUserName(@User, @UserGroupID)";

        private const string SELECTUSERBYID_ByFunc = "SELECT * FROM GetUserById(@ID)";
        

        //private const string SELECTUSERWITHINALLGROUPS = "SELECT * FROM Member WHERE [User] = @User";
        private const string SELECTUSERWITHINALLGROUPS_ByFunc = "select * from GetUser(@User)";
        
        //private const string SELECTALLUSERS = "SELECT * FROM Member where UserGroupID = @userGroup Order By [User]";
        private const string SELECTALLUSERS_ByFunc = "SELECT * FROM GetUsersByUserGroup(@userGroup) Order By [User]";
        
        //private const string UPDADEPASSWORD = "Update Member Set Password = @PW WHERE [User] = @User AND [UserGroupID] = @UserGroupID";

        public Members(SqlConnection conn)
        {
            m_conn = conn;
        }

        public List<Member> GetAllUsers(UserGroup userGroup)
        {
            try
            {
                List<Member> members = new List<Member>();
                MemberData memberData = new MemberData();

                using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
                {
                    SqlCommand selectAllUsers = new SqlCommand(SELECTALLUSERS_ByFunc, c);
                    selectAllUsers.Parameters.Add("@userGroup", SqlDbType.UniqueIdentifier).Value = userGroup.ID;

                    SqlDataAdapter daUsers = new SqlDataAdapter(selectAllUsers);
                    daUsers.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] { new System.Data.Common.DataTableMapping("Table", "MemberTable") });
                    c.Open();
                    daUsers.Fill(memberData);
                    c.Close();

                    foreach (MemberData.MemberTableRow row in memberData.MemberTable.Rows)
                    {
                        members.Add(new Member(row.ID, row.UserGroupID, row.User, row.Vorname, row.Nachname, row.Adresse, row.PLZ,
                            row.Ort, (MemberTitle)Enum.Parse(typeof(MemberTitle), row.Titel), row.EMail,
                            row.Password, row.Geburtstag, row.IsAdministrator, row.IsSuperAdmin, row.Telefon
                            ,row.MemberFromSeason,row.MemberToSeason));
                    }
                }
                return members;
            }
            catch (Exception ex)
            { throw ex; }
        }


        public void InsertUsers(Guid usergroup, string user, string vorname, string nachname, string adresse, string plz,
                                string ort, string titel, string mail, string pw, DateTime geburtstag, bool isAdmin, string telefon, Guid memberFromSeason, Guid memberToSeason)
        {
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand insertNewUser = new SqlCommand(INSERTNEWUSER, c);

                insertNewUser.Parameters.Add("@UserGroupID", SqlDbType.UniqueIdentifier).Value = usergroup;
                insertNewUser.Parameters.Add("@User", SqlDbType.NVarChar, 500).Value = user;
                insertNewUser.Parameters.Add("@Vorname", SqlDbType.NVarChar, 500).Value = vorname;
                insertNewUser.Parameters.Add("@Nachname", SqlDbType.NVarChar, 500).Value = nachname;
                insertNewUser.Parameters.Add("@Adresse", SqlDbType.NVarChar, 500).Value = adresse;
                insertNewUser.Parameters.Add("@PLZ", SqlDbType.NVarChar, 500).Value = plz;
                insertNewUser.Parameters.Add("@Ort", SqlDbType.NVarChar, 500).Value = ort;
                insertNewUser.Parameters.Add("@Titel", SqlDbType.NVarChar, 500).Value = titel;
                insertNewUser.Parameters.Add("@EMail", SqlDbType.NVarChar, 500).Value = mail;
                insertNewUser.Parameters.Add("@Password", SqlDbType.NVarChar, 500).Value = pw;
                insertNewUser.Parameters.Add("@Geburtstag", SqlDbType.DateTime).Value = geburtstag;
                insertNewUser.Parameters.Add("@IsAdministrator", SqlDbType.Bit).Value = isAdmin;
              insertNewUser.Parameters.Add("@Telefon", SqlDbType.NVarChar, 500).Value = String.IsNullOrEmpty(telefon) ? String.Empty : telefon;
                insertNewUser.Parameters.Add("@MemberFromSeason", SqlDbType.UniqueIdentifier).Value = memberFromSeason;
                insertNewUser.Parameters.Add("@MemberToSeason", SqlDbType.UniqueIdentifier).Value = memberToSeason;
              
                c.Open();
                insertNewUser.ExecuteNonQuery();
                c.Close();
            }
        }

        public void UpdateUsers(Guid userGroup, string userName_new, string userName_Old, string vorname, string nachname, string adresse, string plz,
            string ort, string titel, string mail, DateTime geburtstag, bool isAdmin, string telefon,
            Guid memberFromSeason, Guid memberToSeason)
        {
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand updateUser = new SqlCommand("UpdateMember", c);
                updateUser.CommandType = CommandType.StoredProcedure;

                updateUser.Parameters.Add("@UserGroupID", SqlDbType.UniqueIdentifier).Value = userGroup;
                updateUser.Parameters.Add("@User_old", SqlDbType.NVarChar, 500).Value = userName_Old;
                updateUser.Parameters.Add("@User_new", SqlDbType.NVarChar, 500).Value = userName_new;
                updateUser.Parameters.Add("@Vorname", SqlDbType.NVarChar, 500).Value = vorname;
                updateUser.Parameters.Add("@Nachname", SqlDbType.NVarChar, 500).Value = nachname;
                updateUser.Parameters.Add("@Adresse", SqlDbType.NVarChar, 500).Value = adresse;
                updateUser.Parameters.Add("@PLZ", SqlDbType.NVarChar, 500).Value = plz;
                updateUser.Parameters.Add("@Ort", SqlDbType.NVarChar, 500).Value = ort;
                updateUser.Parameters.Add("@Titel", SqlDbType.NVarChar, 500).Value = titel;
                updateUser.Parameters.Add("@EMail", SqlDbType.NVarChar, 500).Value = mail;
                updateUser.Parameters.Add("@Geburtstag", SqlDbType.DateTime).Value = geburtstag;
                updateUser.Parameters.Add("@IsAdministrator", SqlDbType.Bit).Value = isAdmin;
                updateUser.Parameters.Add("@Telefon", SqlDbType.NVarChar, 500).Value = String.IsNullOrEmpty(telefon) ? String.Empty : telefon; 
                updateUser.Parameters.Add("@MemberFromSeason", SqlDbType.UniqueIdentifier).Value = memberFromSeason;
                updateUser.Parameters.Add("@MemberToSeason", SqlDbType.UniqueIdentifier).Value = memberToSeason;

                c.Open();
                updateUser.ExecuteNonQuery();
                c.Close();
            }
        }

        public Member GetUserByID(Guid uID)
        {
            MemberData memberData = new MemberData();

            using (SqlConnection c = m_conn)
            {
                SqlCommand selectUsers = new SqlCommand(SELECTUSERBYID_ByFunc, c);
                selectUsers.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = uID;

                SqlDataAdapter daUsers = new SqlDataAdapter(selectUsers);
                daUsers.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] { new DataTableMapping("Table", "MemberTable") });
                c.Open();
                daUsers.Fill(memberData);
                c.Close();

                MemberData.MemberTableRow r = (MemberData.MemberTableRow)memberData.MemberTable.Rows[0];
                return new Member(r.ID, r.UserGroupID, r.User, r.Vorname, r.Nachname, r.Adresse, r.PLZ, r.Ort,
                    (MemberTitle)Enum.Parse(typeof(MemberTitle), r.Titel), r.EMail, r.Password, r.Geburtstag, r.IsAdministrator, r.IsSuperAdmin, r.Telefon
                    , r.MemberFromSeason, r.MemberToSeason);
            }

        }

        public Member GetSpecialUser(string userName, Guid userGroup)
		{
			MemberData memberData = new MemberData();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand selectUsers = new SqlCommand(SELECTUSER_ByFunc, c);

                selectUsers.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = userName;
                selectUsers.Parameters.Add("@UserGroupID", SqlDbType.UniqueIdentifier).Value = userGroup;

                SqlDataAdapter daUsers = new SqlDataAdapter(selectUsers);
                daUsers.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] { new System.Data.Common.DataTableMapping("Table", "MemberTable") });
                c.Open();
                daUsers.Fill(memberData);
                c.Close();
            }

            if (memberData.MemberTable.Rows.Count == 1)
            {
                MemberData.MemberTableRow r = (MemberData.MemberTableRow)memberData.MemberTable.Rows[0];
                return new Member(r.ID,r.UserGroupID,r.User, r.Vorname,r.Nachname,r.Adresse,r.PLZ,r.Ort,
                    (MemberTitle)Enum.Parse(typeof(MemberTitle),r.Titel),r.EMail,r.Password,r.Geburtstag,r.IsAdministrator,r.IsSuperAdmin,r.Telefon
                    , r.MemberFromSeason, r.MemberToSeason);
            }
            else if (memberData.MemberTable.Rows.Count == 0)
            {
                return null;
            }
            else
                throw new Exception("More than 1 Member-Record with the same UserName und UserGroupID was found!");
		}

        public List<Member> GetSpecialUser(string userName)
        {
            List<Member> result = new List<Member>();
            MemberData memberData = new MemberData();

            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand selectUsers = new SqlCommand(SELECTUSERWITHINALLGROUPS_ByFunc, c);

                selectUsers.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = userName;

                SqlDataAdapter daUsers = new SqlDataAdapter(selectUsers);
                daUsers.TableMappings.AddRange(new DataTableMapping[] { new DataTableMapping("Table", "MemberTable") });
                c.Open();
                daUsers.Fill(memberData);
                c.Close();

                foreach (MemberData.MemberTableRow r in ((MemberData.MemberTableDataTable)memberData.MemberTable).Rows)
                {
                    result.Add(new Member(r.ID, r.UserGroupID, r.User, r.Vorname, r.Nachname, r.Adresse, r.PLZ, r.Ort,
                        (MemberTitle)Enum.Parse(typeof(MemberTitle), r.Titel), r.EMail, r.Password, r.Geburtstag, r.IsAdministrator,r.IsSuperAdmin, r.Telefon
                        , r.MemberFromSeason, r.MemberToSeason));
                }
            }
            return result;
        }

        public void UpdatePassword(string password, string userName, Guid userGroupID)
        {
            using (SqlConnection c = new SqlConnection(m_conn.ConnectionString))
            {
                SqlCommand updatePassword = new SqlCommand("UpdatePassword", c);
                updatePassword.CommandType = CommandType.StoredProcedure;

                updatePassword.Parameters.Add("@PW", SqlDbType.VarChar, 50).Value = password;
                updatePassword.Parameters.Add("@User", SqlDbType.VarChar, 50).Value = userName;
                updatePassword.Parameters.Add("@UserGroupID", SqlDbType.UniqueIdentifier).Value = userGroupID;

                c.Open();
                updatePassword.ExecuteNonQuery();
                c.Close();
            }
        }
    }
}
