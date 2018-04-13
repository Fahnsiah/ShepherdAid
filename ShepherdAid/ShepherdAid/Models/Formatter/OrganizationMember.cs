using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(OrganizationMemberAttribute))]
    public partial class OrganizationMember
    {
    }

    public partial class OrganizationMemberAttribute
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Organization")]
        public int OrganizationID { get; set; }

        [Required]
        [Display(Name="Member")]
        public int MemberID { get; set; }

        [Required]
        [Display(Name = "Membership Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime MembershipDate { get; set; }

        [Required]
        [Display(Name="Member Status")]
        public int StatusID { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }

        public virtual Member Member { get; set; }
        public virtual StatusType StatusType { get; set; }
        public virtual Organization Organization { get; set; }
    }
}