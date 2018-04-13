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
    
    public partial class OrganizationPositionType
    {
        public OrganizationPositionType()
        {
            this.OrganizationLeaders = new HashSet<OrganizationLeader>();
        }
    
        public int ID { get; set; }
        public int InstitutionID { get; set; }
        public int OrgranizationID { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual Institution Institution { get; set; }
        public virtual ICollection<OrganizationLeader> OrganizationLeaders { get; set; }
        public virtual Organization Organization { get; set; }
    }
}