using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(StatusTypeAttribute))]
    public partial class StatusType
    {
    }
    public partial class StatusTypeAttribute
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string StatusName { get; set; }
    }
}