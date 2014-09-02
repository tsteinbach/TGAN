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
using DataLayerLogic.Types;

public partial class MemberAdmin : MemberInfomationBasePage
{
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
        this.Title = "TGAN-Memberadministration";

        if (!IsPostBack)
        {
            lstUserGroup.DataSource = MEMBERBUSINESS.GetAllNamesOfUserGroups();
            lstUserGroup.DataBind();
            lstUserGroup.Text = ACTIVEUSERGROUP.Name;

            loadUserList(ACTIVEUSERGROUP);

            ddlTitle.DataSource = MEMBERBUSINESS.GetAllTitles();
            ddlTitle.DataBind();

            List<Season> seasons = base.GetAllSeasons();
            dropMemberSince.DataSource = seasons;
            dropMemberSince.DataTextField = "Name";
            dropMemberSince.DataValueField = "ID";
            dropMemberSince.DataBind();

            dropMemberTo.DataSource = seasons;
            dropMemberTo.DataTextField = "Name";
            dropMemberTo.DataValueField = "ID";
            dropMemberTo.DataBind();

            dropMemberSince.SelectedItem.Selected = false;
            dropMemberSince.SelectedItem.Value = Guid.Empty.ToString();
            dropMemberTo.SelectedItem.Selected = false;
            dropMemberTo.SelectedItem.Value = Guid.Empty.ToString();
        }
    }

    protected void cmdChangeUserInfo_Click(object sender, EventArgs e)
    {
        InsertNewUser();
    }

    protected void lstUserGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUserList(MEMBERBUSINESS.GetUserGroupByName(lstUserGroup.SelectedItem.Text));
    }

    private void loadUserList(UserGroup userGroup)
    {   
        List<string> users = base.GetAllUsersOfUserGroup(userGroup);
        
        if(users.Count != 0)
        {
            lstUsers.DataSource = users; 
            lstUsers.DataBind();
        }
    }

    private void InsertNewUser()
    {
        List<UserGroup> lstgroups = TGANConfiguration.DBACCESS.GetAllUserGroups();
        _groupName = lstUserGroup.SelectedItem.Text;
        UserGroup g = lstgroups.Find(FindGroup);

        DateTime birthday = DateTime.MinValue;

        if (!base.DoesUserExist(txtUserName.Text, g) && base.IsDateTimeValid(txtGeburtstag.Text, out birthday))
        {
            TGANConfiguration.DBACCESS.InsertUsers(g.ID, txtUserName.Text, txtFirstName.Text, txtLastName.Text,
                                txtAdresse.Text, txtPlz.Text, txtOrt.Text, ddlTitle.SelectedItem.Text, txtEMail.Text,
                                FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1"),
                                birthday, isAdministrator.Checked, txtPhone.Text,
                                new Guid(dropMemberSince.SelectedValue), new Guid(dropMemberTo.SelectedValue));
        }
        else
            txtErrorMsg.Text = "Entweder existiert der Benutzername bereits oder das Datumsformat wurde nicht eingehalten!";
    }

    private string _groupName = String.Empty;
    private string _userName = String.Empty;

    private bool FindGroup(UserGroup group)
    {
        if (String.Compare(group.Name,_groupName,true) == 0)
            return true;
        else
            return false;
    }

    private bool FindUserName(string user)
    {
        if (String.Compare(_userName, user,true) == 0)
            return true;
        else
            return false;
    }

    protected void cmdChangePW_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtNewPW.Text))
            MEMBERBUSINESS.ChangePassword(txtNewPW.Text,
                MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP, lstUsers.SelectedItem.Text,false,null), ACTIVEUSERGROUP);
        else
            throw new Exception("Ein neues Passwort bitte!");
    }
    
    //protected void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (lstUsers.SelectedItem.Text == ACTIVEMEMBER.UserName)
    //    {
    //        cmdChangeUserInfo.Enabled = true;
    //        cmdChangePW.Enabled = true;
    //    }
    //    else
    //    {
    //        cmdChangeUserInfo.Enabled = false;
    //        cmdChangePW.Enabled = false;
    //    }

    //    FillMemberInformation(MEMBERBUSINESS.GetMemberByUserName(ACTIVEUSERGROUP, lstUsers.SelectedItem.Text));
    //}

    //private void FillMemberInformation(Member m)
    //{
    //    txtAdresse.Text = m.Adresse;
    //    txtEMail.Text = m.EMAIL;
    //    txtFirstName.Text = m.FirstName;
    //    txtGeburtstag.Text = m.Birthday.Value.ToShortDateString();
    //    txtLastName.Text = m.LastName;
    //    txtOrt.Text = m.City;
    //    txtPassword.Text = m.Password;
    //    txtPlz.Text = m.PLZ;
    //    ddlTitle.Text = m.Title.Value.ToString();
    //    isAdministrator.Checked = m.IsAdministrator;
    //    txtUserName.Text = m.UserName;
    //    lstUserGroup.Text = TGANConfiguration.DBACCESS.GetUserGroup(m.UserGroupID).Name;
    //}
}
