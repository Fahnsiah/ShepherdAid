//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShepherdAid.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public Payment()
        {
            this.MemberPayments = new HashSet<MemberPayment>();
        }
    
        public int ID { get; set; }
        public int CurrencyTypeID { get; set; }
        public decimal AmountPaid { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual ICollection<MemberPayment> MemberPayments { get; set; }
    }
}