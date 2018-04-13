using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(NationalityTypeAttribute))]
    public partial class NationalityType
    {
    }


    public partial class NationalityTypeAttribute
    {

        public int ID { get; set; }

        [Required, Display(Name = "Nationality")]
        public string TypeName { get; set; }

        [Required, Display(Name = "Country")]
        public string Value { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}