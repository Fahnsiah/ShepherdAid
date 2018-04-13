using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(AspNetGroupRoleAttributes))]
    public partial class AspNetGroupRole
    {
    }

    public partial class AspNetGroupRoleAttributes
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Group Name")]
        public int GroupID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleID { get; set; }
    }
}