using System;
using System.Collections;
using System.Xml;
using System.Data;

namespace BusinessLayerLogic
{
	#region GetMembers

	/// <summary>
	/// Zusammenfassung für GetMembers
	/// </summary>
	public class GetMembers : ArrayList
	{
		private string m_UserDataSource;
		private string m_excludiveMembers;
		private string xpath = "/dsUserDaten/Users/colUser";
		private string[] m_exclusives
		{
			get{return GetExclusives();}
		}
        
		public GetMembers(string UserDataSource, string excludiveMembers):base()
		{
			m_UserDataSource = UserDataSource;
			m_excludiveMembers = excludiveMembers;
		}

		public void LoadMembers()
		{
			XmlDocument users = new XmlDocument();
			users.Load(m_UserDataSource);
			XmlNodeList list = users.SelectNodes(xpath);
			string name = null;
									

			for(int i =0; i<list.Count;i++)
			{
				name = list.Item(i).InnerText;

				if(!IsExclusive(name))
					this.Add(name);
			}
		}

		private bool IsExclusive(string name)
		{
			for(int i  = 0;i<m_exclusives.Length;i++)
			{
				// Exclusive Members werden nicht mit aufgeführt
				if(name==m_exclusives[i].ToString())
					return true;
			}
			return false;
		}

		private string[] GetExclusives()
		{
			int anz_exclusives = m_excludiveMembers.Split(',').Length;
			string[] exclusives  = new string[anz_exclusives];
			
			for(int i = 0; i<anz_exclusives;i++)
			{
				exclusives[i] = m_excludiveMembers.Split(',').GetValue(i).ToString();
			}
			return exclusives;
		}
	}

	#endregion

	#region GetDataOfMembers

	/// <summary>
	/// Zusammenfassung für GetDataOfMembers
	/// </summary>
	public class GetDataOfMembers
	{
		private XmlNode m_usernode;

		public GetDataOfMembers(string member, string user_data_source)
		{
			string xpath = "/dsUserDaten/Users[colUser='"+member+"']";
			XmlDocument users = new XmlDocument();
			users.Load(user_data_source);
			m_usernode = users.SelectSingleNode(xpath);
		}



		public string FirstName
		{
			get
			{
				return m_usernode.FirstChild.InnerText;
			}
		}

		public string LastName
		{
			get
			{
				return m_usernode.ChildNodes.Item(1).InnerText;
			}
		}

		public string Adresse
		{
			get
			{
				return m_usernode.ChildNodes.Item(2).InnerText;
			}
		}

		public string PLZ
		{
			get
			{
				return m_usernode.ChildNodes.Item(3).InnerText;
			}
		}

		public string Ort
		{
			get
			{
				return m_usernode.ChildNodes.Item(4).InnerText;
			}
		}

		public string EMail
		{
			get
			{
				return m_usernode.ChildNodes.Item(6).InnerText;
			}
		}

		public string Title
		{
			get
			{
				return m_usernode.ChildNodes.Item(5).InnerText;
			}
		}

		public DateTime Birthday
		{
			get
			{
				return Convert.ToDateTime(m_usernode.LastChild.InnerText);
			}
		}

		public static string ShortCutUserName(string userName)
		{
				return UserShortCut(userName); 
		}

		public static ArrayList GetUserShortCutList(ArrayList userContainer)
		{
			ArrayList UserShortcuts = new ArrayList();
			
			for(int i=0;i<userContainer.Count;i++)
				UserShortcuts.Add(UserShortCut(userContainer[i].ToString()) + " = " +  userContainer[i]);

			return UserShortcuts;
		}

		static string UserShortCut(string userName)
		{
			if(userName.Length < 4)
				return userName;
			else
				return userName.Substring(0,1)+
					userName.Substring(1,1)+
					userName.Substring(userName.Length-2,1)+
					userName.Substring(userName.Length-1,1);				
		}


	}

	#endregion

	#region Geburtstagskinder
	public class GetGeburtstagskinder
	{
		private DataTable m_memberdata;
		private DataColumn m_vname;
		private DataColumn m_nachname;
		private DataColumn m_birthday;
		private ArrayList m_members;
		private string m_userarchiv;
		
		public GetGeburtstagskinder(ArrayList members, string userarchiv)
		{
			DataSet ds = new DataSet();
			DataTable dt = ds.Tables.Add();
			DataColumn vname = new DataColumn("Vorname");
			DataColumn nachname = new DataColumn("Nachname");
			DataColumn birthday = new DataColumn("Geburtstag");
			dt.Columns.Add(vname);
			dt.Columns.Add(nachname);
			dt.Columns.Add(birthday);
			
			m_vname = vname;
			m_nachname = nachname;
			m_birthday = birthday;
			m_memberdata = dt;
			m_members = members;
			m_userarchiv = userarchiv;
		}
		
		//doedel
		//selber

		public DataView GetGeburtstagslist()
		{
			for(int i = 0; i<m_members.Count;i++)
				this.FillDataTable(m_members[i].ToString(),m_userarchiv);

			DataView dvmembers = m_memberdata.DefaultView;
			dvmembers.Sort = "Geburtstag ASC";
			return dvmembers;
		}

		private void FillDataTable(string member, string userarchiv)
		{
			GetDataOfMembers memberdata = new GetDataOfMembers(member,userarchiv);
			// Geburtstage der nächsten 7 Tage werden angezeigt
						
			if((DateTime.Today.DayOfYear<=Convert.ToDateTime(memberdata.Birthday).DayOfYear)&&
				(DateTime.Today.AddDays(10).DayOfYear>=Convert.ToDateTime(memberdata.Birthday).DayOfYear))
			{
				DataRow dr = m_memberdata.NewRow();
				dr[m_vname] = memberdata.FirstName;
				dr[m_nachname] = memberdata.LastName;
				dr[m_birthday] = memberdata.Birthday.ToShortDateString();
				m_memberdata.Rows.Add(dr);
			}
		}


	}
	#endregion
}
