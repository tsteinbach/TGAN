//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TGAN_Svc.Models
{
    using System;
    
    public partial class GetGamesWithTeamName_Result
    {
        public System.Guid ID { get; set; }
        public System.DateTime Zeit { get; set; }
        public string Heim_Team { get; set; }
        public string Gast_Team { get; set; }
        public System.Guid SpieltagID { get; set; }
        public int Spiel { get; set; }
        public string Result { get; set; }
        public Nullable<System.Guid> TeamID_home { get; set; }
        public Nullable<System.Guid> TeamID_away { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public string AwayTeam { get; set; }
        public string HomeTeam { get; set; }
    }
}