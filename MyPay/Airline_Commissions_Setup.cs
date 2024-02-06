//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPay
{
    using System;
    using System.Collections.Generic;
    
    public partial class Airline_Commissions_Setup
    {
        public int Id { get; set; }
        public int FromSectorId { get; set; }
        public int ToSectorId { get; set; }
        public int AirlineId { get; set; }
        public int AirlineClassId { get; set; }
        public bool IsActive { get; set; }
        public decimal Cashback_Percentage { get; set; }
        public decimal MPCoinsDebit { get; set; }
        public decimal MPCoinsCredit { get; set; }
        public decimal ServiceCharge { get; set; }
        public bool IsCashbackPerTicket { get; set; }
        public decimal MinServiceCharge { get; set; }
        public decimal MaxServiceCharge { get; set; }
        public int KycType { get; set; }
        public int GenderType { get; set; }
        public decimal MaximumCashbackAllowed { get; set; }
        public decimal MinimumCashbackAllowed { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
