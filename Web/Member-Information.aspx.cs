using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataLayerLogic.Types;
using BusinessLayerLogic.Typemethods;

public partial class Member_Information : MemberInfomationBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "TGAN-Member";
        if (!IsPostBack)
        {
            if (ACTIVEMEMBER == null)
                Response.Redirect("./Error.aspx");
            
            lstUsers.DataSource = base.GetAllUsers();
            lstUsers.DataBind();
            lstUsers.Text = ACTIVEMEMBER.UserName;

            List<Season> seasons = base.GetAllSeasons();
            dropMemberSince.DataSource = seasons;
            dropMemberSince.DataTextField = "Name";
            dropMemberSince.DataValueField = "ID";
            dropMemberSince.DataBind();

            dropMemberTo.DataSource = seasons;
            dropMemberTo.DataTextField = "Name";
            dropMemberTo.DataValueField = "ID";
            dropMemberTo.DataBind();

            FillMemberInformation(ACTIVEMEMBER,seasons);

            if (lstUsers.SelectedItem.Text == ACTIVEMEMBER.UserName)
            {
                cmdChangeUserInfo.Enabled = true;
                cmdChangePW.Enabled = true;
            }
        }   
    }

    protected void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstUsers.SelectedItem.Text == ACTIVEMEMBER.UserName)
        {
            cmdChangeUserInfo.Enabled = true;
            cmdChangePW.Enabled = true;
        }
        else
        {
            cmdChangeUserInfo.Enabled = false;
            cmdChangePW.Enabled = false;
        }

        FillMemberInformation(MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP,lstUsers.SelectedItem.Text,false,null), base.GetAllSeasons());
    }

    protected void cmdChangePW_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtNewPW.Text))
            MEMBERBUSINESS.ChangePassword(txtNewPW.Text,
               MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP, lstUsers.SelectedItem.Text,false,null),
                ACTIVEUSERGROUP);
        else
            throw new Exception("Bitte neues Passwort eingeben");
           
    }

    protected void cmdChangeUserInfo_Click(object sender, EventArgs e)
    {
        DateTime birthday  = DateTime.MinValue;
        if (base.IsDateTimeValid(txtGeburtstag.Text, out birthday))
            TGANConfiguration.DBACCESS.UpdateUser(ACTIVEUSERGROUP.ID, lblUserName.Text, lstUsers.SelectedItem.Text, txtFirstName.Text,
                txtLastName.Text, txtAdresse.Text, txtPlz.Text, txtOrt.Text, lblTitle.Text,
                txtEMail.Text, birthday, isAdministrator.Checked,txtPhone.Text,
                new Guid(dropMemberSince.SelectedValue), new Guid(dropMemberTo.SelectedValue));
        else
            throw new Exception("Datumseingabe ist nicht valide");
    }

    private void FillMemberInformation(Member m, List<Season> seasons)
    {
        txtAdresse.Text = m.Adresse;
        txtEMail.Text = m.EMAIL;
        txtFirstName.Text = m.FirstName;
        txtGeburtstag.Text = m.Birthday.Value.ToShortDateString();
        txtLastName.Text = m.LastName;
        txtOrt.Text = m.City;
        txtPassword.Text = m.Password;
        txtPlz.Text = m.PLZ;
        lblTitle.Text = m.Title.Value.ToString();
        isAdministrator.Checked = m.IsAdministrator;
        lblUserName.Text = m.UserName;
        txtPhone.Text = m.Telefon;
        lblUserGroup.Text = TGANConfiguration.DBACCESS.GetUserGroup(m.UserGroupID).Name;
        dropMemberSince.SelectedItem.Selected = false;
        dropMemberSince.SelectedValue = m.MemberFromSeasonID.ToString();
        dropMemberTo.SelectedItem.Selected = false;
        dropMemberTo.SelectedValue = m.MemberToSeasonID.ToString();
    }
    
}
