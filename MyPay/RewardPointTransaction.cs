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
    
    public partial class RewardPointTransaction
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string MemberName { get; set; }
        public decimal Amount { get; set; }
        public int Sign { get; set; }
        public int Type { get; set; }
        public string Remarks { get; set; }
        public string Description { get; set; }
        public string TransactionUniqueId { get; set; }
        public decimal CurrentBalance { get; set; }
        public int Status { get; set; }
        public string VendorTransactionId { get; set; }
        public string Reference { get; set; }
        public string ParentTransactionId { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long Sno { get; set; }
        public decimal RewardPointBalance { get; set; }
        public int VendorServiceID { get; set; }
    }
}
