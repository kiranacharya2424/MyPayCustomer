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
    
    public partial class sp_Commission_Get_Result
    {
        public Nullable<long> Row { get; set; }
        public long Id { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal FixedCommission { get; set; }
        public decimal PercentageCommission { get; set; }
        public decimal PercentageRewardPoints { get; set; }
        public decimal PercentageRewardPointsDebit { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public decimal MinimumAllowed { get; set; }
        public decimal MaximumAllowed { get; set; }
        public int ServiceId { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int KycType { get; set; }
        public int GenderType { get; set; }
        public decimal ServiceCharge { get; set; }
        public long UpdatedBy { get; set; }
        public decimal MinimumAllowedSC { get; set; }
        public decimal MaximumAllowedSC { get; set; }
        public string IndiaDate { get; set; }
    }
}