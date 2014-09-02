using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataLayerLogic.Types;
using System.Collections.Generic;
using BusinessLayerLogic.Typemethods;
using System.Globalization;

/// <summary>
/// Summary description for MemberInfomationBasePage
/// </summary>
public class MemberInfomationBasePage : BasePage
{
    public MemberInfomationBasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static MemberBL _memberB = null;
    private static SeasonBL _seasonB = null;

    protected MemberBL MEMBERBUSINESS
    {
        get
        {
            if (_memberB == null)
                _memberB = new MemberBL(TGANConfiguration.DBACCESS);
            return _memberB;
        }
    }

    protected SeasonBL SEASONBUSINESS
    {
      get
      {
        if (_seasonB == null)
          _seasonB = new SeasonBL(TGANConfiguration.DBACCESS);
        return _seasonB;
      }
    }

    protected Dictionary<Guid, string> ACTIVEUSERLIST
    {
        get
        {
            if (Application["ActiveUserList"] == null)
                Application["ActiveUserList"] = new System.Collections.Generic.Dictionary<Guid, string>();

            return (Dictionary<Guid, string>)Application["ActiveUserList"];
        }
    }

    protected List<string> GetAllUsers()
    {
        return MEMBERBUSINESS.GetAllUserNames(ACTIVEUSERGROUP,false,null);
    }

    protected List<Season> GetAllSeasons()
    {
      List<Season> seasons = SEASONBUSINESS.GetAllSeasons();
      seasons.Insert(0, new Season(Guid.Empty, "Unbegrenzt", false,0));
      return seasons;
    }

    protected List<string> GetAllUsersOfUserGroup(UserGroup group)
    {
        return MEMBERBUSINESS.GetAllUserNames(group,false,null);
    }

    protected bool DoesUserExist(string userName, UserGroup group)
    {
        _userName = userName;
        List<string> userNames = MEMBERBUSINESS.GetAllUserNames(group,false,null);

        if (userNames.Find(FindUserName) == null)
            return false;
        else
            return true;
    }

    protected bool IsDateTimeValid(string birthday, out DateTime date)
    {
        date = DateTime.MinValue;
        return DateTime.TryParseExact(birthday,"dd.MM.yyyy",null,DateTimeStyles.AdjustToUniversal, out date);
    }

    private string _userName = String.Empty;

    private bool FindUserName(string user)
    {
        if (String.Compare(_userName, user, true) == 0)
            return true;
        else
            return false;
    }
}
