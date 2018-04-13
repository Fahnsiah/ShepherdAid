using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(InstitutionAttribute))]
    public partial class Institution
    {

    }
    public partial class InstitutionAttribute
    {
        [Required]
        [Display(Name="Inst. Group")]
        public int InstitutionGroupID { get; set; }

        [Required]
        [Display(Name="Inst. Name")]
        public string InstitutionName { get; set; }
        public string Initial { get; set; }

        [Required]
        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }


        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required]
        public string Address { get; set; }
        public string Website { get; set; }

        [Display(Name="Status")]
        public int StatusTypeID { get; set; }
    }
}