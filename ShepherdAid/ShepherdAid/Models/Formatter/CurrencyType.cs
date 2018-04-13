using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    public partial class CurrencyType
    {
    }
    public partial class CurrencyTypeAttribute
    {
        [Required]
        [Display(Name="Currency Name")]
        public string CurrencyName { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string ShortName { get; set; }
    }
}