using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(CountyAttribute))]
    public partial class County
    {
    }

    public partial class CountyAttribute
    {
        public int ID { get; set; }

        [Required, Display(Name="County Code")]
        public string Code { get; set; }

        [Required, Display(Name = "County Name")]
        public string Name { get; set; }
        public System.DateTime DateRecorded { get; set; }
        public string RecordedBy { get; set; }
    }
}