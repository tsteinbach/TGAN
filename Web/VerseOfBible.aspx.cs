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
using System.Text;
using System.IO;
using System.Net;

public partial class VerseOfBible : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Vers aus der Bibel";
        //LoadBibleVerse();
    }

    private void LoadBibleVerse()
    {
        try
        {
            StringBuilder sUrl = new StringBuilder();
            sUrl.Append("http://www.gnpcb.org/esv/share/get/");
            sUrl.Append("?key=IP");
            sUrl.Append("&action=getDailyVerse");
            sUrl.Append("&include-headings=true");
            sUrl.Append("&correct-quotes=true");

            WebRequest oReq = WebRequest.Create(sUrl.ToString());
            StreamReader sStream = new StreamReader(oReq.GetResponse().GetResponseStream());

            StringBuilder sOut = new StringBuilder();
            sOut.Append(sStream.ReadToEnd());
            sStream.Close();

            //litBibleVerse.Text = manipulateBibleVerse(sOut.ToString());
        }
        catch
        {
            ;
        }
    }

    private string manipulateBibleVerse(string verse)
    {
        string resul = null;
        string startString = "<small class=\"audio\">(<a href=\"http://www.gnpcb.org/esv/share/audio";
        string endString = "</small>";

        int start = verse.IndexOf(startString);
        int end = verse.IndexOf(endString);

        if ((start != -1) && (end != -1))
            resul = verse.Remove(start, end + endString.Length - start);
        else
            resul = verse;

        return resul;
    }
}
