using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(AspNetGroupAttributes))]
    public partial class AspNetGroup
    {
    }
    public partial class AspNetGroupAttributes
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Institution Name")]
        public string InstitutionID { get; set; }

        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}