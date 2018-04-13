using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(OrganizationAttribute))]
    public partial class Organization
    {
    }

    public partial class OrganizationAttribute
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Organization Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Short Name")]
        public string ShortName { get; set; }

        [Required]
        [Display(Name="Date Established")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]//yyyy-MM-dd
        public System.DateTime DateEstablished { get; set; }

        public string Purpose { get; set; }

        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        [Display(Name="Organization Status")]
        public int OrganizationStatusTypeID { get; set; }


        [Required]
        [Display(Name = "Organization Gender")]
        public int OrganizationGenderTypeID { get; set; }
    }
}