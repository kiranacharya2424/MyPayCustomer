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
    
    public partial class WalletTransaction
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
        public string ContactNumber { get; set; }
        public string RecieverName { get; set; }
        public long RecieverId { get; set; }
        public string RecieverContactNumber { get; set; }
        public decimal CashBack { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal RewardPoint { get; set; }
        public string GatewayStatus { get; set; }
        public long UpdateBy { get; set; }
        public string UpdateByName { get; set; }
        public int TransferType { get; set; }
        public string Platform { get; set; }
        public string DeviceCode { get; set; }
        public decimal NetAmount { get; set; }
        public string RecieverBankCode { get; set; }
        public string RecieverAccountNo { get; set; }
        public string RecieverBranch { get; set; }
        public string SenderBankCode { get; set; }
        public string SenderAccountNo { get; set; }
        public string SenderBranch { get; set; }
        public string BatchTransactionId { get; set; }
        public string TxnInstructionId { get; set; }
        public string CustomerID { get; set; }
        public string IpAddress { get; set; }
        public string ResponseCode { get; set; }
        public int WalletType { get; set; }
        public string Purpose { get; set; }
        public string SenderBankName { get; set; }
        public string RecieverBankName { get; set; }
        public string SenderBranchName { get; set; }
        public string RecieverBranchName { get; set; }
        public int VendorType { get; set; }
        public string VendorResponsePin { get; set; }
        public string VendorResponseSerial { get; set; }
        public decimal ReceiverAmount { get; set; }
        public string RefCode { get; set; }
        public decimal MPCoinsDebit { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal RewardPointBalance { get; set; }
        public string VendorJsonLookup { get; set; }
        public long MerchantMemberId { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponDiscount { get; set; }
        public string MerchantId { get; set; }
        public string MerchantOrganization { get; set; }
        public int RoleId { get; set; }
        public string WalletImage { get; set; }
        public bool IsFavourite { get; set; }
        public string AdditionalInfo1 { get; set; }
        public string AdditionalInfo2 { get; set; }
    }
}
