using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(MemberObligationAttribute))]
    public partial class MemberObligation
    {
    }
    public partial class MemberObligationAttribute
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Member ID")]
        public int MemberID { get; set; }

        [Required]
        [Display(Name = "Obligation Type")]
        public int ObligationTypeID { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyTypeID { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateStarted { get; set; }

        [Display(Name="Date Adjusted")]
        public System.DateTime DateAdjusted { get; set; }

        [Display(Name = "Adjusted Balance")]
        [DataType(DataType.Currency)]
        public decimal AdjustedBalance { get; set; }

        [Required]
        [Display(Name="Recurrence Type")]
        public int RecurranceTypeID { get; set; }

        [Display(Name="Obligation Status")]
        public int ObligationStatusTypeID { get; set; }
    }
}