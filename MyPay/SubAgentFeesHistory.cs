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
    
    public partial class SubAgentFeesHistory
    {
        public long Id { get; set; }
        public long SubAgentId { get; set; }
        public string FullName { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal MinimumConversionRate { get; set; }
        public decimal MaximumConversionRate { get; set; }
        public decimal SubAgentFee { get; set; }
        public decimal AdminFees { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsAdmin { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
