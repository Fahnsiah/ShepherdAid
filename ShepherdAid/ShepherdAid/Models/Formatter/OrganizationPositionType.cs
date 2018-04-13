using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{

    [MetadataType(typeof(OrganizationPositionTypeAtribute))]
    public partial class OrganizationPositionType
    {
    }

    public partial class OrganizationPositionTypeAtribute
    {
        public int ID { get; set; }

        [Required, Display(Name="Institution")]
        public int InstitutionID { get; set; }

        [Required, Display(Name = "Organization")]   
        public int OrgranizationID { get; set; }

        [Required, Display(Name = "Position Name")]   
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter only positive integer Number")]
        public int Priority { get; set; }
        public string Description { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }

        public virtual Institution Institution { get; set; }
        public virtual Organization Organization { get; set; }
    }
}