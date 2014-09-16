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
using BusinessLayerLogic.Typemethods;
using System.Collections.Generic;
using DataLayerLogic.Types;
using System.Text;

public partial class TippAdmin : BasePage
{
    private Member _User = null;

    #region Properties

    private Member USER
    {
        get { return _User; }
        set { _User = value; }
    }

    private static MemberBL _memberB = null;
    
    private MemberBL MEMBERBUSINESS
    {
        get 
        {
            if (_memberB == null)
                _memberB = new MemberBL(TGANConfiguration.DBACCESS);
            return _memberB;
        }
    }

    #endregion

    #region EventHandlers

    protected void Page_Init(object sender, EventArgs e)
    {
        if (ACTIVEMEMBER != null)
        {
            if (!ACTIVEMEMBER.IsAdministrator)
                Response.Redirect("./Error.aspx");
        }
        else
            Response.Redirect("./Error.aspx");
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.Title = "TGAN-Betadministration";
            if (!ACTIVEMEMBER.IsSuperAdmin)
                cmdChangeRoundData.Enabled = false;

            if (sender != null)
            {
                if (!IsPostBack)
                {
                    this.lstRoundList.DataSource = new RoundBL(ACTUALROUND, ACTUALSEASON, TGANConfiguration.DBACCESS).Create34SpieltagArray();
                    this.lstRoundList.DataBind();
                    
                    this.lstRoundList.SelectedIndex = ACTUALROUND.RoundNo - 1;
                    
                    List<string> userList = MEMBERBUSINESS.GetAllUserNames(ACTIVEUSERGROUP,true,ACTUALSEASON);
                    userList.Remove(ACTIVEMEMBER.UserName);
                    lstMembers.DataSource = userList;
                    lstMembers.DataBind();
                    USER = MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP, lstMembers.SelectedValue.ToString(),true,ACTUALSEASON);          
                    base.FillRoundTable(Page.Controls[0].FindControl(IDOFCONTENTPLACEHOLDER),USER,true,ACTUALROUND);
                    this.roundNo.Text = String.Format("{0}", lstRoundList.SelectedItem.Text);
                }
                else
                    USER = MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP, lstMembers.SelectedValue.ToString(),true,ACTUALSEASON);          
            }
            else
                throw new NullReferenceException();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void changeActiveMember(object sender, EventArgs e)
    {
        USER = MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP, lstMembers.SelectedValue.ToString(),true,ACTUALSEASON);
        base.FillRoundTable(((Control)sender).Parent, USER, true, new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetRoundByRoundNo(base.getRoundNo(lstRoundList.SelectedItem.Text)));
    }

    protected override void loadRound(object sender, EventArgs e)
    {
        try
        {
            if (sender != null)
            {
                base.FillRoundTable(((Control)sender).Parent,USER,true, new RoundBL(ACTUALSEASON,TGANConfiguration.DBACCESS).GetRoundByRoundNo(base.getRoundNo(lstRoundList.SelectedItem.Text)));
            }
            else
                throw new Exception();
        }
        catch (Exception ex)
        {
            litError.Text = (ex.ToString());
            litError.Visible = true;
        }

        this.roundNo.Text = String.Format("{0}", lstRoundList.SelectedItem.Text);
    }

    private Round SelectedRound
    {
        get 
        { 
            return new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetRoundByRoundNo(base.getRoundNo(lstRoundList.SelectedItem.Text));
        }
    }

    private List<RoundGame> SelectedRoundGames
    {
        get 
        {
            return new RoundBL(SelectedRound, ACTUALSEASON, TGANConfiguration.DBACCESS).GetGames(); 
        }
    }

    protected override void cmdGetBet_Click(object sender, EventArgs e)
    {
        string cssClass = String.Empty;
        string infoText = String.Empty;
        Dictionary<RoundGame, TippValue> tipps = new Dictionary<RoundGame, TippValue>();
        TippBL tippB = new TippBL(TGANConfiguration.DBACCESS);
        
        foreach (RoundGame g in SelectedRoundGames)
            tipps.Add(g, TippBL.GetEnumTippValue(((DropDownList)GetTippControl("tipp" + g.GameNo)).Text));

        if (SelectedRound != null && USER != null)
        {
            base.GetTippInfo(tippB.SetTipp(new Tipp(tipps, USER, ACTIVEMEMBER, DateTime.Now),true),out cssClass,out infoText);

            TGANConfiguration.DBACCESS.deleteGesamtStandOfRound(this.SelectedRound.ID);

            lblInfo.CssClass = cssClass;
            lblInfo.Text = infoText;

        }
        else
            throw new NullReferenceException();
    }

    protected void cmdChangeRoundData_Click(object sender, EventArgs e)
    {
        foreach (RoundGame game in SelectedRoundGames)
        {
            game.StartTime = DateTime.Parse(((TextBox)((Control)sender).Parent.FindControl("date" + game.GameNo)).Text);
            game.Result = ((TextBox)((Control)sender).Parent.FindControl("result" + game.GameNo)).Text;
            game.IsHidden = ((CheckBox)((Control)sender).Parent.FindControl("check" + game.GameNo)).Checked;
            TGANConfiguration.DBACCESS.UpdateRoundGame(SelectedRound,game);    
        }

        TGANConfiguration.DBACCESS.deleteGesamtStandOfRound(this.SelectedRound.ID);
    }

    #endregion

    #region Helpers

    private DropDownList GetTippControl(string id)
    {
        switch (id)
        {
            case "tipp1": return this.tipp1;
            case "tipp2": return this.tipp2;
            case "tipp3": return this.tipp3;
            case "tipp4": return this.tipp4;
            case "tipp5": return this.tipp5;
            case "tipp6": return this.tipp6;
            case "tipp7": return this.tipp7;
            case "tipp8": return this.tipp8;
            case "tipp9": return this.tipp9;
            default: throw new Exception(String.Format("Control with ID {0} does not exis", id));
        }
    }

    #endregion
    
}
