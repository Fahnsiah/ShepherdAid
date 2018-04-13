using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(MemberTypeAttribution))]
    public partial class MemberType
    {
    }

    public partial class MemberTypeAttribution
    {
        
        public int ID { get; set; }

        [Required,Display(Name="Member Type")]
        public string TypeName { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }

        [Display(Name="Institution")]
        public Nullable<int> InstitutionID { get; set; }
    }
}