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
using System.Threading;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;

public partial class _Default : MemberInfomationBasePage
{
    private const string _UserCountText = "";
    private void reloadUserCount()
    {
        lbluserCount.Text = String.Format("{0}", TGANConfiguration.DBACCESS.CountVisitors().ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CreateRecentForumItems();
        ShowGeburtstagskinder();
        ShowRescheduledGames();
        this.Title = "TGAN-Start";
        reloadUserCount();

        if (ACTIVEMEMBER != null)
        {
            if (ACTIVEMEMBER.IsSuperAdmin)
                SuperAdminLink.Visible = true;
        }

        if (!IsPostBack)
        {
            List<string> groups = new MemberBL(TGANConfiguration.DBACCESS).GetAllNamesOfUserGroups();

            if (groups.Count != 3)
                throw new Exception("Amount of groups is not equal to 3!");


            group1.Text = groups[0];
            group2.Text = groups[1];
            group3.Text = groups[2];

            try
            {
                //Thread updateRounds = new Thread(DoUpdateRound);
                
                //updateRounds.Start();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
    
    protected void cmdLoginGroup1_Click(object sender, EventArgs e)
    {
        if (sender.GetType() != typeof(Button))
            throw new Exception("sender is not a Button");
        login(((Button)sender).Text);    
    }

    protected void cmdLoginGroup2_Click(object sender, EventArgs e)
    {
        if (sender.GetType() != typeof(Button))
            throw new Exception("sender is not a Button");
        login(((Button)sender).Text);
    }

    protected void cmdLoginGroup3_Click(object sender, EventArgs e)
    {
        if (sender.GetType() != typeof(Button))
            throw new Exception("sender is not a Button");
        login(((Button)sender).Text);
    }

    private void login(string userGroup)
    {
        Member user = null;
        UserGroup group = null;

        if (base.ACTIVEMEMBER != null)
            return;

        if (new MemberBL(TGANConfiguration.DBACCESS).IsLoginValid(userGroup, 
            txtPassword.Text, txtUser.Text, out user, out group))
        {
            ACTIVEMEMBER = user;
            ACTIVEUSERGROUP = group;

            if (ACTIVEMEMBER.IsSuperAdmin)
                SuperAdminLink.Visible = true;

            try
            {
                TGANConfiguration.DBACCESS.UpdateVisitors(Int32.Parse(TGANConfiguration.DBACCESS.CountVisitors().ToString().Trim()));
                reloadUserCount();
            }
            catch (Exception ex)
            {
                litError.Text = ex.ToString();
                litError.Visible = true;
            }

            FormsAuthentication.SetAuthCookie(txtUser.Text, false);
            lblLoginnotValid.Visible = false;

           ((TGANMaster)this.Master).SETLoginInfo();

           if (ACTIVEMEMBER == null)
               return;

           string member;
           if (!ACTIVEUSERLIST.TryGetValue(ACTIVEMEMBER.ID, out member))
           {
               ACTIVEUSERLIST.Add(ACTIVEMEMBER.ID, ACTIVEMEMBER.UserName);
               ((TGANMaster)this.Master).SETActiveUserList();
           }
        }
        else
            lblLoginnotValid.Visible = true;

        //updateLogin.Update();
    }

    private void ShowGeburtstagskinder()
    {
        bool hasBirthday = false;
        try
        {
            List<UserGroup> groups = TGANConfiguration.DBACCESS.GetAllUserGroups();
            List<Member> users = null;
            StringBuilder sb = null;
            int index;
            Table t = null;
            TableRow tr = null;
            TableCell td = null;
            
            foreach (UserGroup g in groups)
            {
                index = 0;
                hasBirthday = false;
                users = new MemberBL(TGANConfiguration.DBACCESS).GetAllMembers(g,true,ACTUALSEASON);
                sb = new StringBuilder(String.Format("<h4>{0}</h4>", g.Name));
                t = new Table();

                foreach (Member m in users)
                {
                    
                    if (m.Birthday.HasValue)
                    {
                        if ((DateTime.Today.DayOfYear <= m.Birthday.Value.DayOfYear) &&
                            (DateTime.Today.AddDays(TGANConfiguration.GeburtstagsHistory).DayOfYear >= m.Birthday.Value.DayOfYear))
                        {
                            index++;
                            hasBirthday = true;
                            if (index % 2 != 0)
                            {
                                tr = new TableRow();
                                t.Rows.Add(tr);
                            }

                            td = new TableCell();
                            td.Text = String.Format("<img src=\"images/bday.gif\" alt=\"\"  /> {0}", m.FirstName);
                            tr.Cells.Add(td);
                        }
                    }
                }
          
                if (hasBirthday)
                {
                    geburtstagskinder.Text = String.Format("Unsere Geburtstagskinder in den kommenden {0} Tagen!", TGANConfiguration.GeburtstagsHistory);
                    placebirthdayChildren.Controls.Add(new LiteralControl(sb.ToString()));
                    placebirthdayChildren.Controls.Add(t);
                }
            }
        }
        catch (Exception ex)
        {
            placebirthdayChildren.Controls.Add(new LiteralControl(ex.ToString()));
        }
    }

    private void ShowRescheduledGames()
    {
        try
        {
            StringBuilder sb = TGANConfiguration.DBACCESS.GetRescheduledGames();
            if (sb != null)
            {
                sb.Insert(0, "<u>Auflistung der verschobenen Spiele</u>");
                Label lblResch = new Label();
                lblResch.Text = sb.ToString();
                lblResch.CssClass = "rescheduledGames";
                placerescheduledGames.Controls.Add(lblResch);
            }
        }
        catch(Exception ex)
        {
            placerescheduledGames.Controls.Add(new LiteralControl(ex.ToString()));
        }
    }


    private void CreateRecentForumItems()
    {
        try
        {
            StringBuilder sb = new StringBuilder("<ul>");

            List<Forum_ActualContent> recentThemes = TGANConfiguration.DBACCESS.SelectActualContent(TGANConfiguration.ShowForumForumHistory);

            foreach (Forum_ActualContent t in recentThemes)
                sb.Append(String.Format(@"<li><a class=""menue"" href=""Forum_TGAN.aspx?titleID={0}"">{1} {2}</a> </li>",
                    t.TitleID, t.Title, t.Amount.ToString()));

            if (recentThemes.Count != 0)
            {
                sb.Append("</ul>");
                placeRecentForum.Controls.Add(
                    new LiteralControl(String.Format("<h4>Die Beiträge der letzten {0} Tage!</h4> {1}",
                    TGANConfiguration.ShowForumForumHistory, sb.ToString())));
            }

        }
        catch (Exception ex)
        {
            placeRecentForum.Controls.Add(
                    new LiteralControl(ex.ToString()));
        }
    }

    /// <summary>
    /// DateTime is updated
    /// </summary>
    private void DoUpdateRound()
    {
        Season actualSeason = new SeasonBL(TGANConfiguration.DBACCESS).GetActualSeason();
        RoundBL roundB = new RoundBL(actualSeason, TGANConfiguration.DBACCESS);
        roundB.UpdateCheckableRounds();
    }

    
}
