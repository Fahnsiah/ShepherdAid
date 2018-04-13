using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(PositionTypeAttribute))]
    public partial class PositionType
    {
    }
    public partial class PositionTypeAttribute
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Institution")]
        public int InstitutionID { get; set; }

        [Required]
        [Display(Name = "Position Name")]
        public string Name { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
    }
}