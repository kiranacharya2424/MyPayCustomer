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
    
    public partial class CardPaymentTransaction
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
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CardType { get; set; }
        public string CreditStatus { get; set; }
        public string DebitStatus { get; set; }
        public string GatewayStatus { get; set; }
        public string Purpose { get; set; }
        public string ResponseCode { get; set; }
        public int TransferType { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal NetAmount { get; set; }
        public int VendorType { get; set; }
    }
}
