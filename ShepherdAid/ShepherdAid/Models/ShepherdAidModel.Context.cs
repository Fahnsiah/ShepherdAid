﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShepherdAid.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ShepherdAidDBEntities : DbContext
    {
        public ShepherdAidDBEntities()
            : base("name=ShepherdAidDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetGroupRole> AspNetGroupRoles { get; set; }
        public virtual DbSet<AspNetGroup> AspNetGroups { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<GenderType> GenderTypes { get; set; }
        public virtual DbSet<InstitutionGroup> InstitutionGroups { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<MaritalStatusType> MaritalStatusTypes { get; set; }
        public virtual DbSet<MemberDocument> MemberDocuments { get; set; }
        public virtual DbSet<MemberObligation> MemberObligations { get; set; }
        public virtual DbSet<MemberPayment> MemberPayments { get; set; }
        public virtual DbSet<MemberPhoto> MemberPhotos { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberSacrament> MemberSacraments { get; set; }
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<NationalityType> NationalityTypes { get; set; }
        public virtual DbSet<ObligationStatusType> ObligationStatusTypes { get; set; }
        public virtual DbSet<ObligationType> ObligationTypes { get; set; }
        public virtual DbSet<OrganizationGenderType> OrganizationGenderTypes { get; set; }
        public virtual DbSet<OrganizationLeader> OrganizationLeaders { get; set; }
        public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; }
        public virtual DbSet<OrganizationPositionType> OrganizationPositionTypes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationStatusType> OrganizationStatusTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PositionType> PositionTypes { get; set; }
        public virtual DbSet<RecurranceType> RecurranceTypes { get; set; }
        public virtual DbSet<SacramentRequirement> SacramentRequirements { get; set; }
        public virtual DbSet<SalutationType> SalutationTypes { get; set; }
        public virtual DbSet<StatusType> StatusTypes { get; set; }
        public virtual DbSet<SystemVariable> SystemVariables { get; set; }
        public virtual DbSet<Tracker> Trackers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<Sacrament> Sacraments { get; set; }
    
        public virtual ObjectResult<ReportMembersList_Result> ReportMembersList(Nullable<int> institutionID)
        {
            var institutionIDParameter = institutionID.HasValue ?
                new ObjectParameter("InstitutionID", institutionID) :
                new ObjectParameter("InstitutionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReportMembersList_Result>("ReportMembersList", institutionIDParameter);
        }
    
        public virtual ObjectResult<ReportParishMembersCount_Result> ReportParishMembersCount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReportParishMembersCount_Result>("ReportParishMembersCount");
        }
    
        public virtual ObjectResult<spUserAssignedRoles_Result> spUserAssignedRoles(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spUserAssignedRoles_Result>("spUserAssignedRoles", userIDParameter);
        }
    
        public virtual ObjectResult<spUserAvailableRoles_Result> spUserAvailableRoles(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spUserAvailableRoles_Result>("spUserAvailableRoles", userIDParameter);
        }
    
        public virtual ObjectResult<spObligationSummary_Result> spObligationSummary(Nullable<int> obligationID)
        {
            var obligationIDParameter = obligationID.HasValue ?
                new ObjectParameter("ObligationID", obligationID) :
                new ObjectParameter("ObligationID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spObligationSummary_Result>("spObligationSummary", obligationIDParameter);
        }
    
        public virtual int spAssignGroupAssignedRoleToUser(string roleID, string userID, string userName)
        {
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAssignGroupAssignedRoleToUser", roleIDParameter, userIDParameter, userNameParameter);
        }
    
        public virtual int spAssignRoleToUser(string userID, string roleID, string userName)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAssignRoleToUser", userIDParameter, roleIDParameter, userNameParameter);
        }
    
        public virtual int spDeleteAddedGroupRole(Nullable<int> groupID, string roleID)
        {
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("GroupID", groupID) :
                new ObjectParameter("GroupID", typeof(int));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteAddedGroupRole", groupIDParameter, roleIDParameter);
        }
    
        public virtual int spDeleteAddedUserRole(string userID, string roleID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spDeleteAddedUserRole", userIDParameter, roleIDParameter);
        }
    
        public virtual ObjectResult<spFilterMembersList_Result> spFilterMembersList(string memberID, string firstName, string lastName)
        {
            var memberIDParameter = memberID != null ?
                new ObjectParameter("MemberID", memberID) :
                new ObjectParameter("MemberID", typeof(string));
    
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spFilterMembersList_Result>("spFilterMembersList", memberIDParameter, firstNameParameter, lastNameParameter);
        }
    
        public virtual ObjectResult<spGetAssignedRoles_Result> spGetAssignedRoles(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetAssignedRoles_Result>("spGetAssignedRoles", userIDParameter);
        }
    
        public virtual ObjectResult<spGetAvailableRoles_Result> spGetAvailableRoles(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetAvailableRoles_Result>("spGetAvailableRoles", userIDParameter);
        }
    
        public virtual ObjectResult<spGroupAssignedRoles_Result> spGroupAssignedRoles(Nullable<int> groupID)
        {
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("GroupID", groupID) :
                new ObjectParameter("GroupID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGroupAssignedRoles_Result>("spGroupAssignedRoles", groupIDParameter);
        }
    
        public virtual ObjectResult<spGroupAvailableRoles_Result> spGroupAvailableRoles(Nullable<int> groupID)
        {
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("GroupID", groupID) :
                new ObjectParameter("GroupID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGroupAvailableRoles_Result>("spGroupAvailableRoles", groupIDParameter);
        }
    
        public virtual ObjectResult<spMemberActiveObligations_Result> spMemberActiveObligations(Nullable<int> memberID)
        {
            var memberIDParameter = memberID.HasValue ?
                new ObjectParameter("MemberID", memberID) :
                new ObjectParameter("MemberID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spMemberActiveObligations_Result>("spMemberActiveObligations", memberIDParameter);
        }
    
        public virtual int spMemberObligations(Nullable<int> memberID)
        {
            var memberIDParameter = memberID.HasValue ?
                new ObjectParameter("MemberID", memberID) :
                new ObjectParameter("MemberID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spMemberObligations", memberIDParameter);
        }
    
        public virtual ObjectResult<spOrganizationLeaderMembers_Result> spOrganizationLeaderMembers(Nullable<int> institutionID, Nullable<int> organizationID, string search)
        {
            var institutionIDParameter = institutionID.HasValue ?
                new ObjectParameter("InstitutionID", institutionID) :
                new ObjectParameter("InstitutionID", typeof(int));
    
            var organizationIDParameter = organizationID.HasValue ?
                new ObjectParameter("OrganizationID", organizationID) :
                new ObjectParameter("OrganizationID", typeof(int));
    
            var searchParameter = search != null ?
                new ObjectParameter("Search", search) :
                new ObjectParameter("Search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spOrganizationLeaderMembers_Result>("spOrganizationLeaderMembers", institutionIDParameter, organizationIDParameter, searchParameter);
        }
    
        public virtual ObjectResult<spOrganizationNonMembers_Result> spOrganizationNonMembers(Nullable<int> institutionID, Nullable<int> organizationID, string search)
        {
            var institutionIDParameter = institutionID.HasValue ?
                new ObjectParameter("InstitutionID", institutionID) :
                new ObjectParameter("InstitutionID", typeof(int));
    
            var organizationIDParameter = organizationID.HasValue ?
                new ObjectParameter("OrganizationID", organizationID) :
                new ObjectParameter("OrganizationID", typeof(int));
    
            var searchParameter = search != null ?
                new ObjectParameter("Search", search) :
                new ObjectParameter("Search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spOrganizationNonMembers_Result>("spOrganizationNonMembers", institutionIDParameter, organizationIDParameter, searchParameter);
        }
    
        public virtual int spRemoveUserFromRole(string userID, string roleID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spRemoveUserFromRole", userIDParameter, roleIDParameter);
        }
    
        public virtual int spRevokeGroupRevokedRoleFromUser(string roleID, string userID)
        {
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spRevokeGroupRevokedRoleFromUser", roleIDParameter, userIDParameter);
        }
    
        public virtual int spRevokeGroupRole(Nullable<int> groupID, string roleID)
        {
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("GroupID", groupID) :
                new ObjectParameter("GroupID", typeof(int));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spRevokeGroupRole", groupIDParameter, roleIDParameter);
        }
    
        public virtual int spRevokeUserRole(string userID, string roleID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spRevokeUserRole", userIDParameter, roleIDParameter);
        }
    
        public virtual ObjectResult<spApplicationUsers_Result> spApplicationUsers(Nullable<int> clientID, Nullable<int> institutionGroupID, Nullable<int> institutionID)
        {
            var clientIDParameter = clientID.HasValue ?
                new ObjectParameter("ClientID", clientID) :
                new ObjectParameter("ClientID", typeof(int));
    
            var institutionGroupIDParameter = institutionGroupID.HasValue ?
                new ObjectParameter("InstitutionGroupID", institutionGroupID) :
                new ObjectParameter("InstitutionGroupID", typeof(int));
    
            var institutionIDParameter = institutionID.HasValue ?
                new ObjectParameter("InstitutionID", institutionID) :
                new ObjectParameter("InstitutionID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spApplicationUsers_Result>("spApplicationUsers", clientIDParameter, institutionGroupIDParameter, institutionIDParameter);
        }
    
        public virtual int spAddGroupRoles(Nullable<int> groupID, string roleID, string username)
        {
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("GroupID", groupID) :
                new ObjectParameter("GroupID", typeof(int));
    
            var roleIDParameter = roleID != null ?
                new ObjectParameter("RoleID", roleID) :
                new ObjectParameter("RoleID", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddGroupRoles", groupIDParameter, roleIDParameter, usernameParameter);
        }
    
        public virtual ObjectResult<spMemberPaymentList_Result> spMemberPaymentList(Nullable<int> memberID)
        {
            var memberIDParameter = memberID.HasValue ?
                new ObjectParameter("MemberID", memberID) :
                new ObjectParameter("MemberID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spMemberPaymentList_Result>("spMemberPaymentList", memberIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> spAddCreatedUserRoles(Nullable<int> groupID, string userID)
        {
            var groupIDParameter = groupID.HasValue ?
                new ObjectParameter("GroupID", groupID) :
                new ObjectParameter("GroupID", typeof(int));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("spAddCreatedUserRoles", groupIDParameter, userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<bool>> spIsSacramentApplicable(Nullable<int> memberID, Nullable<int> sacramentID)
        {
            var memberIDParameter = memberID.HasValue ?
                new ObjectParameter("MemberID", memberID) :
                new ObjectParameter("MemberID", typeof(int));
    
            var sacramentIDParameter = sacramentID.HasValue ?
                new ObjectParameter("SacramentID", sacramentID) :
                new ObjectParameter("SacramentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<bool>>("spIsSacramentApplicable", memberIDParameter, sacramentIDParameter);
        }
    }
}