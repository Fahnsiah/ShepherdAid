//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Organization
    {
        public Organization()
        {
            this.OrganizationLeaders = new HashSet<OrganizationLeader>();
            this.OrganizationMembers = new HashSet<OrganizationMember>();
            this.OrganizationPositionTypes = new HashSet<OrganizationPositionType>();
        }
    
        public int ID { get; set; }
        public int InstitutionID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public System.DateTime DateEstablished { get; set; }
        public string Purpose { get; set; }
        public string OfficePhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public int OrganizationGenderTypeID { get; set; }
        public int OrganizationStatusTypeID { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual Institution Institution { get; set; }
        public virtual OrganizationGenderType OrganizationGenderType { get; set; }
        public virtual ICollection<OrganizationLeader> OrganizationLeaders { get; set; }
        public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; }
        public virtual ICollection<OrganizationPositionType> OrganizationPositionTypes { get; set; }
        public virtual OrganizationStatusType OrganizationStatusType { get; set; }
    }
}
