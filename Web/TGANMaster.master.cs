using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataLayerLogic.Types;
using BusinessLayerLogic.Typemethods;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Web.Services;

public partial class TGANMaster : System.Web.UI.MasterPage
{
    
    private const string IDOFCONTENTPLACEHOLDER = "mainContent";

    private Member ActiveMember
    {
        get 
        {
            if (Session["ActiveUser"] == null)
                return null;
            return (Member)Session["ActiveUser"]; 
        }
    }

    public string ANGEMELDET
    {
        get
        {
            const string NICHTANGEMELDET = "Nicht angemeldet";

            if (ActiveMember != null)
                return String.Format("{0}, Du bist angemeldet", ActiveMember.UserName);
            else
                return NICHTANGEMELDET;
        }
    }

    protected Dictionary<Guid, string> ACTIVEUSERLIST
    {
        get
        {
            if (Application["ActiveUserList"] == null)
                Application["ActiveUserList"] = new Dictionary<Guid, string>();

            return (Dictionary<Guid, string>)Application["ActiveUserList"];
        }
    }

        
    #region Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        SETActiveUserList();
        if (!IsPostBack)
        {
            SETLoginInfo();
        }
    }

    protected void cmdLogOut_Click(object sender, EventArgs e)
    {
        //Member actUser = null;

        //if (ActiveMember != null)
        //    actUser = ActiveMember;
               
        //for (int i = 0; i < Session.Contents.Count; i++)
        //    Session.Contents[i] = null;
        
        //if (actUser != null)
        //{
        //    ACTIVEUSERLIST.Remove(actUser.ID);
        //    SETActiveUserList();
        //}


        Session.Abandon();

        Response.Redirect("./Default.aspx");
    }

    #endregion

    #region WebMethod
    
    [WebMethod]
    public static void AbandonSession()
    {
        throw new Exception("test"); 
    }

    #endregion

    //it is called from content page
    public void SETLoginInfo()
    {
        lblLogininfo.Text = ANGEMELDET;
    }

    public void SETActiveUserList()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"Angemeldete Benutzer:");
  
        foreach (KeyValuePair<Guid, string> user in ACTIVEUSERLIST)
            sb.Append(String.Format(" {0}", user.Value));

        
        litActUserList.Text = sb.ToString();
    }

    
}
