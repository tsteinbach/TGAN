using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DataLayerLogic;
using System.Diagnostics;
using System.Data.SqlClient;

/// <summary>
/// Summary description for TGANConfiguration
/// </summary>
public class TGANConfiguration
{
    private static Buisinesses _dbAccess = null;
    
    public static Buisinesses DBACCESS
    {
        [DebuggerStepThrough]
        get
        {
            if (_dbAccess == null || String.IsNullOrEmpty(_dbAccess.conn.ConnectionString))
            {
                _dbAccess = new Buisinesses(new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString)
                                            {
                                                UserID = ConfigurationManager.AppSettings["dbUser"],
                                                Password = ConfigurationManager.AppSettings["pw"]
                                            }.ToString());
            }    
            return _dbAccess;
        }
    }

    public static int ShowForumForumHistory
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings["ShowForumHistory"]); }
    }

    public static double CheckResultsAfterGameStart
    {
        get { return Double.Parse(ConfigurationManager.AppSettings["CheckResultsAfterGameStart"]); }
    }

    public static double DaysHowLongResultsAreChecked
    {
        get { return Double.Parse(ConfigurationManager.AppSettings["DaysHowLongResultsAreChecked"]); }
    }

    public static string VereinslogosFolder
    {
        get { return ConfigurationManager.AppSettings["VereinslogosFolder"].ToString(); }
    }

    public static int GeburtstagsHistory
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings["GeburtstagsHistorie"]); }
    }
}
