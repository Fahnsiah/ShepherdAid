using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(MaritalStatusTypeAttribute))]
    public partial class MaritalStatusType
    {
    }

    public partial class MaritalStatusTypeAttribute
    {
        public int ID { get; set; }

        [Required, Display(Name="Marital Status")]
        public string TypeName { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}