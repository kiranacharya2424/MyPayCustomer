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
    
    public partial class BalanceHistoryMerchant
    {
        public long Id { get; set; }
        public decimal TotalBalance { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int Type { get; set; }
        public long UpdatedBy { get; set; }
        public long MerchantCount { get; set; }
        public long ActiveMerchant { get; set; }
        public long InActiveMerchant { get; set; }
    }
}