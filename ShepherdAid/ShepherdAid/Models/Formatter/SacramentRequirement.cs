using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(SacramentRequirementAttribute))]
    public partial class SacramentRequirement
    {
    }

    public partial class SacramentRequirementAttribute
    {
        public int ID { get; set; }

        [Required, Display(Name="Sacrament")]
        public int SacramentID { get; set; }


        [Required, Display(Name = "Requirement")]
        public int RequirementID { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}