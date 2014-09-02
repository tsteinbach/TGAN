using System;
using System.Collections;
using System.Data;
using DataLayerLogic;
using DataLayerLogic.DataSets;
using DataLayerLogic.Types;
using System.Collections.Generic;
using System.Text;
using BusinessLayerLogic.Typemethods;

namespace BusinessLayerLogic
{
	/// <summary>
	/// Zusammenfassung für Forum.
	/// </summary>
	public class Forum
	{
        private UserGroup _UserGroup;
        private Buisinesses _dbAccess;
        private Season _season;
        public Forum(Buisinesses dbAccess, UserGroup userGroup, Season season) 
		{
            _dbAccess = dbAccess;
            _UserGroup = userGroup;
            _season = season;
		}

		private ForumData.Forum_ThemenRow[] GetSpecialThema(string thema, ForumData f_thema)
		{
			return (ForumData.Forum_ThemenRow[])new ArrayList(f_thema.Forum_Themen.Select
				("Titel='"+thema+"'")).ToArray(typeof(ForumData.Forum_ThemenRow));
		}

		private string CreateContentOfCertainTitle(string thema, Member member)
		{
			StringBuilder html = new StringBuilder(); 
            
            ForumData f_inhalt = new ForumData();
			
			List<Member> members = new MemberBL(_dbAccess).GetAllMembers(_UserGroup,true,_season);

			//sort Content
			ForumData.Forum_InhaltRow[] rows = (ForumData.Forum_InhaltRow[])new ArrayList(f_inhalt.Forum_Inhalt.Select
				("","DateTime ASC")).ToArray(typeof(ForumData.Forum_InhaltRow));

			foreach(ForumData.Forum_InhaltRow row in rows)
			{
                html.Append(String.Format("<tr><td class = \"forum\"> {0} berichtete am {1}:</td><td class = \"forum\">{2}</td></tr>",
                    member.UserName, row.DateTime.ToString(), SplitInhalt(row.Inhalt)));
			}
			return html.ToString();
		}

		private string SplitInhalt(string inhalt)
		{
			string new_inhalt = string.Empty;
			int separator = 100;
			int	stringParts = inhalt.Length / separator;
			
			if(stringParts>0)
			{
				for(int i = 0; i<stringParts;i++)
				{
					new_inhalt = new_inhalt + inhalt.Substring(0,separator) + "<br/>";
					inhalt = inhalt.Remove(0,separator);
				}
				new_inhalt = new_inhalt + inhalt;
			}
			else
				new_inhalt = inhalt;
                
			return new_inhalt;
		}
		
		public string CreateInhalt(string thema, Member member)
		{
			return 	String.Format("<table class=\"forum\">{0}</table>",	CreateContentOfCertainTitle(thema,member)); 
		}
	}
}
