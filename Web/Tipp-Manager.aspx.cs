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
using System.Collections.Generic;

using BusinessLayerLogic;
using BusinessLayerLogic.Typemethods;
using DataLayerLogic;
using DataLayerLogic.Types;
using System.Text;


public partial class Tipp_Manager : BasePage
{
    
    public string ACTIVEUSER = null;
    
    
    #region Handlers
    protected void Page_Init(object sender, EventArgs e)
    {
        this.Title = "TGAN-Betpage";

        if (!IsPostBack)
        {
            placeLegend.Controls.Add(base.CreateTippLegend());

            if (ACTIVEMEMBER != null)
            {
                lblInfo.CssClass = CSSINFOTEXTHIDDEN;
                ACTIVEUSER = String.Format("{0}: Viel Erfolg beim Tippen!", ACTIVEMEMBER.UserName);
                Master.Page.Title = "Tipp-Manager";
            }
            else
                Response.Redirect("./Error.aspx");
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (ACTUALROUND == null)
            {
                lblInfo.CssClass = CSSINFOTEXTWARNING;
                lblInfo.Text = INFOSPIELTAGFEHLT;
                return;
            }

            if (sender != null)
            {
                if (!IsPostBack)
                {
                    try
                    {
                        //KickerManager km = new KickerManager(new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetActualRound(false), ACTUALSEASON, TGANConfiguration.DBACCESS);
                        //km.UpdateResults(TGANConfiguration.CheckResultsAfterGameStart, TGANConfiguration.DaysHowLongResultsAreChecked);
                        BuLiDataWebService b = new BuLiDataWebService(new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetActualRound(false), ACTUALSEASON, TGANConfiguration.DBACCESS);
                        b.setResults();
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = ex.ToString();
                        lblInfo.Visible = true;
                        //Response.Write(base.NOTLOADABLERESULTS);
                    }
                    
                    this.lstRoundList.DataSource = new RoundBL().Create34SpieltagArray();
                    this.lstRoundList.DataBind();
                    //der zuletzt getippte Spieltag wird gesetzt
                    this.lstRoundList.SelectedIndex = ACTUALROUND.RoundNo - 2;
                    base.FillRoundTable(Page.Controls[0].FindControl(IDOFCONTENTPLACEHOLDER), ACTIVEMEMBER, false, 
                        new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetRoundByRoundNo(base.getRoundNo(lstRoundList.SelectedItem.Text)));
                }

                this.roundNo.Text = String.Format("{0}", lstRoundList.SelectedItem.Text);
            }
            else
                throw new NullReferenceException("sender not found");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override void loadRound(object sender, EventArgs e)
    {
        if (ACTUALROUND == null)
        {
            lblInfo.CssClass = CSSINFOTEXTWARNING;
            lblInfo.Text = INFOSPIELTAGFEHLT;
            return;
        }

        try
        {
            if (sender != null)
            {
                lblInfo.Text = "";
                base.FillRoundTable(((Control)sender).Parent, ACTIVEMEMBER, false, new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetRoundByRoundNo(base.getRoundNo(lstRoundList.SelectedItem.Text)));
            }
            else
                throw new Exception();
            
        }
        catch (Exception ex)
        {
            litError.Text = ex.ToString();
            litError.Visible = true;
        }

        this.roundNo.Text = String.Format("{0}", lstRoundList.SelectedItem.Text);
   
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Round actRound = new RoundBL(ACTUALSEASON, TGANConfiguration.DBACCESS).GetRoundByRoundNo(base.getRoundNo(lstRoundList.SelectedItem.Text));

        if (actRound == null)
        {
            lblInfo.CssClass = CSSINFOTEXTWARNING;
            lblInfo.Text = INFOSPIELTAGFEHLT;
            return;
        }

        List<RoundGame> games = new RoundBL(actRound, ACTUALSEASON, TGANConfiguration.DBACCESS).GetGames();
        Dictionary<RoundGame, TippValue> tipps = new Dictionary<RoundGame, TippValue>();
        TippBL tippB = new TippBL(actRound,ACTUALSEASON,ACTIVEMEMBER,games,ACTIVEUSERGROUP,TGANConfiguration.DBACCESS);
        string cssClass = String.Empty;
        string infoText = String.Empty;

        foreach (RoundGame g in games)
            tipps.Add(g, TippBL.GetEnumTippValue(((DropDownList)GetTippControl("tipp" + g.GameNo)).Text));

        if (actRound != null && ACTIVEMEMBER != null)
        {
            base.GetTippInfo(tippB.SetTipp(new Tipp(tipps, ACTIVEMEMBER, ACTIVEMEMBER, DateTime.Now), false), out cssClass, out infoText);

            lblInfo.CssClass = cssClass;
            lblInfo.Text = infoText;
        }
        else
            throw new NullReferenceException();
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
