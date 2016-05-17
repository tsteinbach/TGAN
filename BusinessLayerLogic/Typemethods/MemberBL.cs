using System;
using System.Collections.Generic;
using System.Text;
using DataLayerLogic;
using DataLayerLogic.Types;
using System.Web.Security;
using System.Diagnostics;
using System.Linq;

namespace BusinessLayerLogic.Typemethods
{
    public class MemberBL
    {
        private Buisinesses _dbAccess = null;
        
        [DebuggerStepThrough]
        public MemberBL(Buisinesses dbAccess)
        {
            _dbAccess = dbAccess;
        }


        public List<Member> GetAllMembers(UserGroup group, bool useSeasonValidation, Season actualSeason)
        {
          List<Member> users = _dbAccess.GetAllUsers(group);
          if (useSeasonValidation)
          {
            validateUserAppearence(actualSeason,ref users);
          }

          return users;
        }

        private void validateUserAppearence(Season selectedSeason,ref List<Member> users)
        {
          List<Season> seasons = new SeasonBL(_dbAccess).GetAllSeasons();
          SeasonBL sBL = new SeasonBL(_dbAccess);
          Season selSeason = getSeason(selectedSeason.ID, seasons, sBL);
          foreach (Member m in users)
          {
            Season from = getSeason(m.MemberFromSeasonID, seasons, sBL);
            Season to = getSeason(m.MemberToSeasonID, seasons, sBL); 

            if ((selSeason.Order >= from.Order) && (m.MemberToSeasonID.Equals(Guid.Empty) || (selSeason.Order <= to.Order)))
              m.Show = true;
            else
              m.Show = false;
          }

          users.RemoveAll(FindMemberToRemove);
        }

        private static Season getSeason(Guid seasonID, List<Season> seasons, SeasonBL sBL)
        {
          sBL._seasonID = seasonID;
          Season selSeason = seasons.Find(sBL.FindBySeasonId);
          return selSeason;
        }
       
        private List<Member> GetAllMembers(UserGroup group)
        {
            return _dbAccess.GetAllUsers(group);
        }

        public List<string> GetAllUserNames(UserGroup group, bool useSeasonValidation, Season season)
        {
            List<string> result = new List<string>();
            List<Member> members = GetAllMembers(group, useSeasonValidation, season); //_dbAccess.GetAllUsers(group);

            foreach (Member m in members)
                result.Add(m.UserName);

            return result;
        }

        public void ChangePassword(string password, Member user, UserGroup group)
        {
            string pwNew = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            _dbAccess.UpdateUserPassword(user, pwNew, group);
        }

        public Member GetMemberByUserName(UserGroup group,string userName,bool useSeasonValidation, Season season)
        {
            _UserNameToFind = userName;
            return GetAllMembers(group,useSeasonValidation,season).Find(FindMember);
        }

        public UserGroup GetUserGroupByName(string name)
        {
            _UserGroupToFind = name;
            return _dbAccess.GetAllUserGroups().Find(FindUserGroup);
        }

        public string[] GetAllTitles()
        { 
            return Enum.GetNames(typeof(MemberTitle));
        }

        public List<string> GetAllNamesOfUserGroups()
        {
            List<UserGroup> groups = _dbAccess.GetAllUserGroups();
            List<string> names = new List<string>();

            foreach (UserGroup group in groups)
                names.Add(group.Name);

            return names;
        }

        public bool IsLoginValid(string userGroup, string pw, string userName, out Member m, out UserGroup group)
        {
            try
            {
                _UserGroupToFind = userGroup;
                group = _dbAccess.GetAllUserGroups().Find(FindUserGroup);
                m = _dbAccess.GetSpecialUserInformation(userName, group.ID);
                
                if (m != null)
                {
                    if (IsUserActive(m) && (m.Password == FormsAuthentication.HashPasswordForStoringInConfigFile(pw, "SHA1")))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsUserActive(Member m)
        {
            var season = new SeasonBL(_dbAccess).GetActualSeason();
            List<Member>  users = new List<Member>() { m };
            validateUserAppearence(season, ref users);

            if (users.SingleOrDefault(x => x.Show && x.UserName == m.UserName) == null)
                return false;
            else
                return true;
        }

        private string _UserNameToFind = null;

        private bool FindMember(Member m)
        {
            if (m.UserName == _UserNameToFind)
                return true;
            else
                return false;
        }

        private bool FindMemberToRemove(Member m)
        {
          if (!m.Show)
            return true;
          else
            return false;
        }

        private string _UserGroupToFind = null;

        private bool FindUserGroup(UserGroup group)
        {
            if (group.Name == _UserGroupToFind)
                return true;
            else
                return false;
        }
    }
}
