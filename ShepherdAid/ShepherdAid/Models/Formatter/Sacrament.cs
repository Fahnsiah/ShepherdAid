using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(SacramentAttribution))]
    public partial class Sacrament
    {
    }

    public partial class SacramentAttribution
    {
        public int ID { get; set; }

        [Required, Display(Name="Sacrament")]
        public string Name { get; set; }

        [Required, Display(Name = "Applied Once")]
        public bool AppliedOnce { get; set; }

        [Required, Display(Name = "Not On Same Day")]
        public bool NotOnSameDay { get; set; }
        public string Description { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}