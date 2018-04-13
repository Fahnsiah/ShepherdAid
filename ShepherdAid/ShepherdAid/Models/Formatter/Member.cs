using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(MemberAttribute))]
    public partial class Member
    {
    }
    public partial class MemberAttribute
    {
        [Required]
        [Display(Name = "Institution Name")]
        public int InstitutionID { get; set; }

        [Required]
        [Display(Name = "Member ID")]
        public string MemberID { get; set; }

        [Required]
        [Display(Name = "Salutation")]
        public int SalutationTypeID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Gender Type")]
        public int GenderTypeID { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public int MaritalStatusTypeID { get; set; }

        [Required]
        [Display(Name = "Residence Address")]
        [DataType(DataType.MultilineText)]
        public string ResidentAddress { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [Display(Name = "Office Phone")]
        [DataType(DataType.PhoneNumber)]
        public string OfficePhone { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public int NationalityTypeID { get; set; }

        [Display(Name = "County")]
        public Nullable<int> CountyID { get; set; }

        public string Region { get; set; }

        [Required]
        [Display(Name = "Member Type")]
        public int MemberTypeID { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusTypeID { get; set; }

        [Display(Name = "Photo Path")]
        public string PhotoPath { get; set; }
    }
}