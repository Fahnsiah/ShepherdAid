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
    
    public partial class AspNetGroup
    {
        public AspNetGroup()
        {
            this.AspNetGroupRoles = new HashSet<AspNetGroupRole>();
            this.AspNetUsers = new HashSet<AspNetUser>();
        }
    
        public int ID { get; set; }
        public int InstitutionID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual ICollection<AspNetGroupRole> AspNetGroupRoles { get; set; }
        public virtual Institution Institution { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
