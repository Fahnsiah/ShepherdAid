using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(OrganizationLeaderAttribute))]
    public partial class OrganizationLeader
    {
    }

        
    public partial class OrganizationLeaderAttribute
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Organization")]
        public int OrganizationID { get; set; }

        [Required]
        [Display(Name="Member")]
        public int MemberID { get; set; }

        [Required, Display(Name="Position")]
        public int OrganizationPositionTypeID { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }

  
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EndDate { get; set; }
        public int StatusID { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}