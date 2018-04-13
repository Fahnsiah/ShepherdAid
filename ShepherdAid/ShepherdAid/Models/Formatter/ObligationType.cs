using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(ObligationTypeAttribute))]
    public partial class ObligationType
    {
    }
    public partial class ObligationTypeAttribute
    {
        [Required]
        [Display(Name="Obligation")]
        public string ObligationName { get; set; }
    }
}