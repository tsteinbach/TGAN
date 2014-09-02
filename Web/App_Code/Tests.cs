using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BusinessLayerLogic;
using DataLayerLogic;
using DataLayerLogic.Types;
using BusinessLayerLogic.Typemethods;
using System.Collections.Generic;

/// <summary>
/// Summary description for Tests
/// </summary>
public class Tests
{
    public Tests()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    
    public static void LoadRoundResultTest()
    {
        Season s = new SeasonBL(TGANConfiguration.DBACCESS).GetActualSeason();
        
        for (int i = 4; i <= 30; i++)
        {
            Round r = new RoundBL(s, TGANConfiguration.DBACCESS).GetRoundByRoundNo(i);

            KickerManager k = new KickerManager(r, s, TGANConfiguration.DBACCESS);
            k.UpdateResults(TGANConfiguration.CheckResultsAfterGameStart, TGANConfiguration.DaysHowLongResultsAreChecked);
        }
    }

}
