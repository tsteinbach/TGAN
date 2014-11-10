﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class TGANAnalyticsEntities : DbContext
    {
        public TGANAnalyticsEntities()
            : base("name=TGANAnalyticsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<UserGroup> UserGroups { get; set; }
    
        public virtual ObjectResult<AnalyticsNeuner_Result> AnalyticsNeuner(Nullable<System.Guid> userGroupId)
        {
            var userGroupIdParameter = userGroupId.HasValue ?
                new ObjectParameter("UserGroupId", userGroupId) :
                new ObjectParameter("UserGroupId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AnalyticsNeuner_Result>("AnalyticsNeuner", userGroupIdParameter);
        }
    
        public virtual ObjectResult<AnalyticsEchte_Result> AnalyticsEchte(Nullable<System.Guid> userGroupId)
        {
            var userGroupIdParameter = userGroupId.HasValue ?
                new ObjectParameter("UserGroupId", userGroupId) :
                new ObjectParameter("UserGroupId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AnalyticsEchte_Result>("AnalyticsEchte", userGroupIdParameter);
        }
    
        public virtual ObjectResult<AnalyticsTendency_Result> AnalyticsTendency(Nullable<System.Guid> userGroupId)
        {
            var userGroupIdParameter = userGroupId.HasValue ?
                new ObjectParameter("UserGroupId", userGroupId) :
                new ObjectParameter("UserGroupId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AnalyticsTendency_Result>("AnalyticsTendency", userGroupIdParameter);
        }
    
        public virtual ObjectResult<AnalyticsUnechte_Result> AnalyticsUnechte(Nullable<System.Guid> userGroupId)
        {
            var userGroupIdParameter = userGroupId.HasValue ?
                new ObjectParameter("UserGroupId", userGroupId) :
                new ObjectParameter("UserGroupId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AnalyticsUnechte_Result>("AnalyticsUnechte", userGroupIdParameter);
        }
    }
}
