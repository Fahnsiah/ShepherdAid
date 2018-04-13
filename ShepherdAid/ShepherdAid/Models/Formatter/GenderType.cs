using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(GenderTypeAttribute))]
    public partial class GenderType
    {
    }

    public partial class GenderTypeAttribute
    {

        public int ID { get; set; }

        [Required]
        [Display(Name="Gender")]
        public string TypeName { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}