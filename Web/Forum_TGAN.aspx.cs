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
using System.Collections.Generic;
using BusinessLayerLogic.Typemethods;
using System.Text;

public partial class Forum_TGAN : BasePage
{
    List<Forum_Themen> _themes = null;
    private Guid _actualThemeID = Guid.Empty;
    private Guid ActualThemeID
    {
        get { return _actualThemeID; }
        set { _actualThemeID = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "TGAN-Forum";
        if (ACTIVEMEMBER == null)
            Response.Redirect("./Error.aspx");
                
        if (!IsPostBack)
        {
            FillThemeList();

            bool hasThemes;

            if (lstThemes.Items.Count > 0) hasThemes = true;
            else hasThemes = false;

            if (Page.Request.QueryString["titleID"] == null)
            {
                if (hasThemes)
                    ActualThemeID = new Guid(lstThemes.SelectedValue);
            }
            else
            {
                ActualThemeID = new Guid(Page.Request.QueryString["titleID"]);

                lstThemes.ClearSelection();
                foreach (ListItem i in lstThemes.Items)
                {
                    if (new Guid(i.Value) == ActualThemeID)
                    {
                        i.Selected = true;
                        break;
                    }
                }

                hasThemes = true;
            }

            if(hasThemes)
                CreateContent();
        }

    }
    
    protected void lstThemes_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActualThemeID = new Guid(lstThemes.SelectedValue);
        CreateContent();
    }

    protected void cmdNewTheme_Click(object sender, EventArgs e)
    {
        string popupScript;

        if (ACTIVEMEMBER != null)
        {
            if (String.IsNullOrEmpty(txtNewTheme.Text))
            {
                popupScript = "<script language='javascript'>alert('Ein neues Thema muss eingegeben werden!')</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", popupScript);
                return;
            }
            
            Forum_Themen newTheme = new Forum_Themen(Guid.NewGuid(), ACTIVEMEMBER.ID, txtNewTheme.Text, DateTime.Now);
            if (TGANConfiguration.DBACCESS.InsertThema(newTheme) == 0)
                throw new Exception("Could not insert new theme");
            FillThemeList();

            lstThemes.ClearSelection();

            foreach (ListItem i in lstThemes.Items)
                if (Guid.Equals(new Guid(i.Value), newTheme.ID))
                {
                    i.Selected = true;
                    break;
                }
        }
        else
        {
            popupScript = "<script language='javascript'>alert('Du musst angemeldet sein, um ein neues Thema hinzuzufügen!')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", popupScript);
        }
    }

    protected void cmdNewContent_Click(object sender, EventArgs e)
    {
        string popupScript;
        if (ACTIVEMEMBER != null)
        {
            if (String.IsNullOrEmpty(txtNewContent.Text))
            {
                popupScript = "<script language='javascript'>alert('Ein Kommentar muss eingegeben werden!')</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", popupScript);
                return;
            }

            TGANConfiguration.DBACCESS.InsertInhalt(new Forum_Inhalt(Guid.NewGuid(), ACTIVEMEMBER.ID, new Guid(lstThemes.SelectedValue), txtNewContent.Text, DateTime.Now));
            CreateContent();
        }
        else
        {
            popupScript = "<script language='javascript'>alert('Du musst angemeldet sein, um neuen Inhalt hinzuzufügen!')</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", popupScript);
        }
    }



    private void FillThemeList()
    {
        lstThemes.Items.Clear();
        _themes = TGANConfiguration.DBACCESS.SelectThemen();

        foreach (Forum_Themen t in _themes)
            lstThemes.Items.Add(new ListItem(t.Title, t.ID.ToString()));
        lstThemes.SelectedIndex = 0;
    }

    

    private void CreateContent()
    {
        ActualThemeID = new Guid(lstThemes.SelectedValue);
        Forum_Themen theme = TGANConfiguration.DBACCESS.SelectThemen().Find(FindTheme);
        List<Forum_Inhalt> contentList = null;
        StringBuilder sb = new StringBuilder();
        
        if (theme != null)
        {
            contentList = TGANConfiguration.DBACCESS.SelectInhalt(theme);
        
            foreach (Forum_Inhalt c in contentList)
            {
                sb.Append(String.Format("<span class=\"ForumComment\"><i><u>{0} am {1} um {2}:</u></i> <br/> {3}<br/>---------------------</span><br/>",
                    TGANConfiguration.DBACCESS.GetUserByID(c.MemberId).UserName,
                    c.DateOfInsert.ToShortDateString(),
                    c.DateOfInsert.ToShortTimeString(),c.Content));
            }

            if (contentList.Count != 0)
                placeContent.Controls.Add(new LiteralControl(sb.ToString()));
        }
        else
        {
            throw new Exception("Thema not found in DB");
        }
    }

    
    private bool FindTheme(Forum_Themen theme)
    {
        if (theme.ID == ActualThemeID)
            return true;
        else return false;
    }
    
}
