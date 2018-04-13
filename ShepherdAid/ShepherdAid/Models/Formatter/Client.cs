using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models.Formatter
{

    [MetadataType(typeof(ClientAttribute))]
    public partial class Client
    {
    }
    public partial class ClientAttribute
    {

        [Required]
        [Display(Name="Client Name")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Initial { get; set; }

        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<InstitutionGroup> InstitutionGroups { get; set; }
    }
}