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
    
    public partial class AspNetGroupRole
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string RoleID { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual AspNetGroup AspNetGroup { get; set; }
        public virtual AspNetRole AspNetRole { get; set; }
    }
}
