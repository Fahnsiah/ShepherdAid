using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(OrganizationGenderTypeAttribute))]
    public partial class OrganizationGenderType
    {
    }


    public partial class OrganizationGenderTypeAttribute
    {


        public int ID { get; set; }

        [Required]
        [Display(Name="Ogranization Gender")]
        public string TypeName { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}