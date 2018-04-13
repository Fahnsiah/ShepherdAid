using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(ObligationAttribute))]
    public partial class Obligation
    {
    }
    public partial class ObligationAttribute
    {
        public int ID { get; set; }

        [Required]
        [Display(Name="Obligation Type")]
        public int ObligationTypeID { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyTypeID { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime DateStarted { get; set; }

        [Required]
        [Display(Name = "Date Adjusted")]
        [DataType(DataType.Date)]
        public System.DateTime DateAdjusted { get; set; }

        [Required]
        [Display(Name = "Adjusted Balance")]
        public decimal AdjustedBalance { get; set; }

        [Required]
        [Display(Name = "Recurrance Type")]
        public int RecurranceTypeID { get; set; }

        [Required]
        [Display(Name = "Obligation Status")]
        public int ObligationStatusTypeID { get; set; }
    }
}