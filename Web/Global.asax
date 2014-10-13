<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
 
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        bool isremoved = false;
        if (Session["ActiveUser"] == null)
            return;
        else
            isremoved = ((System.Collections.Generic.Dictionary<Guid, string>)Application["ActiveUserList"]).Remove(((DataLayerLogic.Types.Member)Session["ActiveUser"]).ID);

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    protected System.Collections.Generic.Dictionary<Guid, string> ACTIVEUSERLIST
    {
        get
        {
            if (Application["ActiveUserList"] == null)
                Application["ActiveUserList"] = new System.Collections.Generic.Dictionary<Guid, string>();

            return (System.Collections.Generic.Dictionary<Guid, string>)Application["ActiveUserList"];
        }
    }

    private DataLayerLogic.Types.Member ActiveMember
    {
        get
        {
            if (Session["ActiveUser"] == null)
                return null;
            return (DataLayerLogic.Types.Member)Session["ActiveUser"];
        }
    }

    void Session_Start(object sender, EventArgs e) 
    {
        

    }
   

    void Session_End(object sender, EventArgs e) 
    {
        if (ActiveMember == null)
            return;

        ACTIVEUSERLIST.Remove(ActiveMember.ID);
    }
       
</script>
