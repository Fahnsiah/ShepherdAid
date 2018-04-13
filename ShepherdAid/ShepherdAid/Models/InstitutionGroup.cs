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
    
    public partial class InstitutionGroup
    {
        public InstitutionGroup()
        {
            this.Institutions = new HashSet<Institution>();
        }
    
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }
        public string OfficePhone { get; set; }
        public string MobilePhone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ICollection<Institution> Institutions { get; set; }
    }
}
