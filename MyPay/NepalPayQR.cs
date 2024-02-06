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
    
    public partial class NepalPayQR
    {
        public long Id { get; set; }
        public string instructionId { get; set; }
        public long MemberId { get; set; }
        public string validationTraceId { get; set; }
        public decimal Amount { get; set; }
        public decimal interchangeFee { get; set; }
        public decimal transactionFee { get; set; }
        public string merchantName { get; set; }
        public string merchantCity { get; set; }
        public string merchantCountryCode { get; set; }
        public string merchantBillNo { get; set; }
        public string merchantTxnRef { get; set; }
        public string merchantPostalcode { get; set; }
        public string merchantPan { get; set; }
        public string qrType { get; set; }
        public string qrString { get; set; }
        public string merchantCategoryCode { get; set; }
        public string payerName { get; set; }
        public string payerPanId { get; set; }
        public string payerMobileNumber { get; set; }
        public string payerEmailAddress { get; set; }
        public string debtorAccount { get; set; }
        public string debtorAgent { get; set; }
        public string debtorAgentBranch { get; set; }
        public string encKeySerial { get; set; }
        public string token { get; set; }
        public string transactionStatus { get; set; }
        public System.DateTime createddata { get; set; }
        public string createdby { get; set; }
        public Nullable<System.DateTime> updateddate { get; set; }
        public string updatedby { get; set; }
        public string WalletTransactionId { get; set; }
        public string nQrTxnId { get; set; }
        public string issuerid { get; set; }
        public string narration { get; set; }
        public string instrument { get; set; }
        public string acquirerid { get; set; }
        public string sessionSrlNo { get; set; }
        public string creditStatus { get; set; }
        public string debitStatus { get; set; }
        public string debitDescription { get; set; }
        public string NepalPayQRTxnDatetime { get; set; }
    }
}
