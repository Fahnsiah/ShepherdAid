using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShepherdAid.Models
{
    [MetadataType(typeof(MemberSacramentAttribute))]
    public partial class MemberSacrament
    {
    }

    public partial class MemberSacramentAttribute
    {
        public int ID { get; set; }

        [Required, Display(Name="Member")]
        public int MemberID { get; set; }

        [Required, Display(Name = "Sacrament")]
        public int SacramentID { get; set; }

        [Required, Display(Name = "Administered By")]
        public string AdministeredBy { get; set; }

        [Required, Display(Name = "Institution")]
        public int InstitutionID { get; set; }


        [Required, DataType(DataType.Date), Display(Name = "Date Administered")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateAdministered { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    }
}