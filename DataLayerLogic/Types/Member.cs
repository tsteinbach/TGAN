using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayerLogic.Types
{
    public enum MemberTitle
    {
        Präsident = 0,
        Rechtsausschuss = 1, 
        Mitglied = 2, 
        Gast = 3,
        Rechtsausschuss_Protokollführer = 4,
        Schatzmeister = 5,
        Administrator = 6

    }

    public class Member
    {
        private Guid _ID = Guid.Empty;
        private Guid _UserGroupId = Guid.Empty;
        private string _UserName = String.Empty;
        private string _FirstName = String.Empty;
        private string _LastName = String.Empty;
        private string _Adresse = String.Empty;
        private string _PLZ = String.Empty;
        private string _City = String.Empty;
        private MemberTitle? _Title = null;
        private string _EMail = String.Empty;
        private string _Password = String.Empty;
        private DateTime? _Birthday = null;
        private bool _IsAdministrator = false;
        private bool _IsSuperAdmin = false;
        private string _Telefon = null;
        private Guid _MemberFromSeason = Guid.Empty;
        private Guid _MemberToSeason = Guid.Empty;


        
        public Member(Guid Id,Guid userGroupID,string userName, string firstname, string lastname,string adresse ,string plz,
            string city, MemberTitle? title, string email, string password, DateTime? birthday, bool isAdministrator
          , bool isSuperAdmin, string telefon, Guid memberFromSeason, Guid memberToSeason)
        {
            _ID = Id;
            _UserGroupId = userGroupID;
            _UserName = userName;
            _FirstName = firstname;
            _LastName = lastname;
            _Adresse = adresse;
            _PLZ = plz;
            _City = city;
            _Title = title;
            _EMail = email;
            _Password = password;
            _Birthday = birthday;
            _IsAdministrator = isAdministrator;
            _IsSuperAdmin = isSuperAdmin;
            _Telefon = telefon;
            _MemberFromSeason = memberFromSeason;
            _MemberToSeason = memberToSeason;
        }

        public bool _Show = true;
        public bool Show
        {
          get { return _Show; }
          set { _Show = value;}
        }

        public Guid ID
        {
            get { return _ID; }
        }

        public Guid MemberFromSeasonID
        {
          get { return _MemberFromSeason; }
        }

        public Guid MemberToSeasonID
        {
          get { return _MemberToSeason; }
        }

        public Guid UserGroupID
        {
            get { return _UserGroupId; }
        }

        public string UserName
        {
            get { return _UserName; }
        }

        public string FirstName
        {
            get { return _FirstName; }
        }

        public string LastName
        {
            get { return _LastName; }
        }

        public string Adresse
        {
            get { return _Adresse; }
        }
        
        public string PLZ
        {
            get { return _PLZ; }
        }
        
        public string City
        {
            get { return _City; }
        }

        public MemberTitle? Title
        {
            get { return _Title; }            
        }

        public string EMAIL
        {
            get { return _EMail; }
        }
        
        public string Password
        {
            get { return _Password; }
        }

        public string Telefon
        {
            get { return _Telefon; }
        }
        
        public DateTime? Birthday
        {
            get { return _Birthday; }
        }

        public bool IsAdministrator
        {
            get { return _IsAdministrator; }
        }

        public bool IsSuperAdmin
        {
            get { return _IsSuperAdmin; }
        }
    }
}
