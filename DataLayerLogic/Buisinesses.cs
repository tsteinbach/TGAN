using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

using DataLayerLogic.DataSets;
using DataLayerLogic.Types;
using System.Diagnostics;
using System.Text;

namespace DataLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Buisinesses.
	/// </summary>
	public class Buisinesses
	{
		private string m_myConnectionString = null; 
		SqlConnection m_myConnection = null;

		public Buisinesses(string connectionString)
		{
            m_myConnectionString = connectionString;
			m_myConnection = new SqlConnection(m_myConnectionString);
		}

		public SqlConnection conn
		{
		    get{return m_myConnection;}
		}

        public bool IsGroupInCurrentTotalWriterList(Guid userGroupId)
        {
            return new GesamtstandWriterOverview(m_myConnection).IsGroupInCurrentTotalWriterList(userGroupId);
		}

        public void RemoveGroupFromCurrentTotalWriterList(Guid userGroupId)
        {
            new GesamtstandWriterOverview(m_myConnection).RemoveGroupFromCurrentTotalWriterList(userGroupId);
        }

        public void InsertGroupInCurrentTotalWriterList(Guid userGroupId)
        {
            new GesamtstandWriterOverview(m_myConnection).InsertGroupInCurrentTotalWriterList(userGroupId);
        }

		public DataSet GetQueryResult(string query)
		{
            using (SqlConnection c = new SqlConnection(conn.ConnectionString))
            {
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand command = new SqlCommand(query, c);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    c.Open();
                    da.Fill(ds);
                    c.Close();
                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    else
                        return ds;
                }
                catch (Exception ex)
                {
                    c.Close();
                    throw ex;
                }
            }
        }

        #region Gesamtstands
        public void insertGesamtstandsRow(Guid memberID, Guid roundId, int echteBank, int falscheTipps, int NeunerTipp
            , int nichtgetippt, int punkteInsgesamt, int richtigeTipps, int unechteBank)
        {
            Gesamtstands g = new Gesamtstands(m_myConnection, roundId, memberID);
            
            g.echteBank = echteBank;
            g.falscheTipps = falscheTipps;
            g.NeunerTipp = NeunerTipp;
            g.nichtgetippt = nichtgetippt;
            g.PunkteInsgesamt = punkteInsgesamt;
            g.richtigeTipps = richtigeTipps;
            g.unechteBank = unechteBank;
    
            g.insertGesamtstandRow();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="roundID"></param>
        /// <returns> are records in DB</returns>
        public bool selectGesamtstandsRow(Guid memberID, Guid roundID, out Gesamtstands g)
        {
            g = new Gesamtstands(m_myConnection,roundID,memberID);
            return g.selectGesamtstandRound();
        }

        public void deleteGesamtStandOfRound(Guid roundID)
        {
            new Gesamtstands(m_myConnection,roundID,Guid.Empty).deleteGesamtstandOfRound();
        }


        #endregion

        public int InsertUserGroup(Guid id, string name)
        {
            return new UserGroups(m_myConnection).InsertUserGroup(name, id);
        }

        #region update Data
		
        public void UpdateVisitors(int count)
		{
            using (SqlConnection c = new SqlConnection(conn.ConnectionString))
            {
                string update = "Update Visitors set Count = @Count where Count = @CountOld";
                SqlCommand command = new SqlCommand(update, c);
                command.Parameters.Add("@CountOld", SqlDbType.Int).Value = count;
                command.Parameters.Add("@Count", SqlDbType.Int).Value = count + 1;
                c.Open();
                int result = command.ExecuteNonQuery();
                c.Close();
            }
		}
		
		public List<BundesligaTeam> GetAllTeams()
        {
            return new Teams(m_myConnection).GetAllTeams();
        }

        public List<Member> GetSpecialUsers(string userName)
        {
            return new Members(m_myConnection).GetSpecialUser(userName);
        }

        public Member GetUserByID(Guid userID)
        {
            return new Members(m_myConnection).GetUserByID(userID);
        }

		public void UpdateUserPassword(Member user, string pwNew,UserGroup group)
		{
			new Members(m_myConnection).UpdatePassword(pwNew,user.UserName,group.ID);
		}

		public void UpdateUser(Guid userGroupId,string userName_new, string userName_old, string vorname, string nachname,string adresse, string plz,
            string ort, string titel, string mail, DateTime geburtstag, bool isAdmin, string telefon,
      Guid memberFromSeason, Guid memberToSeason)
		{
			new Members(m_myConnection).UpdateUsers(userGroupId, userName_new,userName_old,vorname,nachname, adresse, plz, ort, titel, mail, geburtstag,isAdmin,telefon,memberFromSeason,memberToSeason);
		}

		public int UpdateTipp(Tipp tipp)
		{
            return new Tipps(m_myConnection).UpdateTipp(tipp);
		}

		public int UpdateRoundGame(Round round, RoundGame game)
		{
			return new Ansetzungen(m_myConnection).UpdateRound(round,game);
		}

		#endregion
		
		#region insert Data

		// allready refactored
		public void FillCheckRounds(Season season)
		{
			new CheckRounds(m_myConnection).FillCheckRounds(season);
		}
		
		public int FillTipp(Tipp tipp)
		{
            return new Tipps(m_myConnection).FillTipp(tipp);
		}

		public void InsertUsers(Guid userGroupID,string user, string vorname, string nachname,string adresse, string plz,
			string ort, string titel, string mail, string pw, DateTime geburtstag, bool isAdmin,string telefon,
      Guid memberFromSeason, Guid memberToSeason)
		{
			new Members(m_myConnection).InsertUsers(userGroupID,user,vorname,nachname,adresse,plz,
				ort,titel,mail,pw,geburtstag,isAdmin, telefon,memberFromSeason,memberToSeason);
		}
        public int InsertNewSeasonRounds(Season season)
        {
            return new Spieltage(m_myConnection, season).InsertNewSpieltage(season); 
        }

        public void InsertNewPlayingSchedule(Season season, List<RoundGame> games)
        {
            new Ansetzungen(m_myConnection).InsertNewAnsetzung(season, games);
        }

		#endregion

        #region UserGroup
        public List<UserGroup> GetAllUserGroups()
        {
            return new UserGroups(m_myConnection).GetAllUserGroups();
        }

        public UserGroup GetUserGroup(Guid id)
        {
            return new UserGroups(m_myConnection).GetUserGroup(id);
        }

        #endregion

        #region select Data

        public int CountVisitors()
		{
            using (SqlConnection c = new SqlConnection(conn.ConnectionString))
            {
                string select = "select * from Visitors";
                SqlCommand command = new SqlCommand(select, c);
                c.Open();
                object count = command.ExecuteScalar();
                c.Close();
                if (count == null)
                    return 0;
                else
                    return (int)count;
            }
		}

		public List<Season> GetAllSeasons()
		{
			return new Seasons(m_myConnection).GetAllSeasons();
		}
		
		public List<Member> GetAllUsers(UserGroup userGroup)
		{
			return new Members(m_myConnection).GetAllUsers(userGroup);
		}

		public Member GetSpecialUserInformation(string userName, Guid usergroupID)
		{
			return new Members(m_myConnection).GetSpecialUser(userName,usergroupID);
		}

		public List<Round> GetCheckableRounds(Season season)
		{
			return new Ansetzungen(m_myConnection).GetCheckableRounds(season);
		}

        public int DetermineActualRoundNo(Season season, bool includeHiddenGames)
        {
            return new Ansetzungen(m_myConnection).DetermineActualRoundNo(season, includeHiddenGames);
        }
		
		public bool TodayCheck(Season season)
		{
			return new CheckRounds(m_myConnection).DateTimeCheck(season);
		}

		public List<Tipp> GetAllTippsOfAllMembersPerRound(Round spieltag,List<RoundGame> games,Season saison, List<Member> users)
		{
            return new Tipps(m_myConnection).GetAllTippsOfAllMembersPerRound(spieltag,games,saison,users);
		}

		public Tipp GetTippedValues(Round spieltag,Season saison, Member user, List<RoundGame> games)
		{
            return new Tipps(m_myConnection).GetTipps(user,spieltag,games,saison);
		}

        public void GetGuidFromAnsetzung(ArrayList result, Season saison, Round spieltag)
        {
            //new Ansetzungen(m_myConnection).GetGuidfromAnsetzung(result, spieltag, saison);
            throw new NotImplementedException("GetGuidFromAnsetzung is not implemented");
        }

        public List<RoundGame> GetSingleRound(Round round)
		{
			return new Ansetzungen(m_myConnection).SelectSingleRound(round);			
		}

        public StringBuilder GetRescheduledGames()
        {
            return new Ansetzungen(m_myConnection).getRescheduledGames();
        }

		#endregion

        #region Season
        public Season GetActualSeason()
        {
            return new Seasons(m_myConnection).GetActualSeason();
        }

        public void InsertActualSeason(Season actualSeason)
        {
            new Seasons(m_myConnection).InsertActualSeason(actualSeason);
        }
        #endregion

        #region Round
        public Round GetRoundByRoundNo(Season season, int roundNo)
        {
            return new Spieltage(m_myConnection, season).GetRoundByRoundNo(roundNo);
        }
        #endregion

        #region Forum

        public int InsertThema(Forum_Themen theme)
		{
            return new Foren(m_myConnection).InsertThema(theme);
		}

        public int InsertInhalt(Forum_Inhalt content)
		{
            return new Foren(m_myConnection).InsertInhalt(content);
		}

		public List<Forum_Themen> SelectThemen()
		{
			return new Foren(m_myConnection).SelectThema();
		}

		public List<Forum_Inhalt> SelectInhalt(Forum_Themen theme)
		{
			return new Foren(m_myConnection).SelectInhalt(theme);
		}

		public List<Forum_ActualContent> SelectActualContent(int daysToadd)
		{
			return new Foren(m_myConnection).SelectActualContent(daysToadd);
		}

		#endregion
		
		public static DataSet SourceXML_DestDataSet(string path)
		{
			return FillDataSet(new DataSet(),path);
		}

		static DataSet FillDataSet(DataSet ds, string path)
		{
			ds.ReadXml(path);
			return ds;
		}

        public static bool ValidateDBValue(object val, Type t)
        {
            if (val.GetType() == t)
                return true;
            else
                return false;
        }
	}
}
