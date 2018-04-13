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
    
    public partial class MemberObligation
    {
        public MemberObligation()
        {
            this.MemberPayments = new HashSet<MemberPayment>();
        }
    
        public int ID { get; set; }
        public int MemberID { get; set; }
        public int ObligationTypeID { get; set; }
        public int CurrencyTypeID { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime DateStarted { get; set; }
        public System.DateTime DateAdjusted { get; set; }
        public decimal AdjustedBalance { get; set; }
        public int RecurranceTypeID { get; set; }
        public int ObligationStatusTypeID { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
    
        public virtual CurrencyType CurrencyType { get; set; }
        public virtual Member Member { get; set; }
        public virtual ObligationStatusType ObligationStatusType { get; set; }
        public virtual ObligationType ObligationType { get; set; }
        public virtual RecurranceType RecurranceType { get; set; }
        public virtual ICollection<MemberPayment> MemberPayments { get; set; }
    }
}
