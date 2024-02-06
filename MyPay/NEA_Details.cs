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
    
    public partial class NEA_Details
    {
        public long Id { get; set; }
        public int SessionId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsPaymentDone { get; set; }
        public string Counter { get; set; }
        public string SCNumber { get; set; }
        public System.DateTime PaidDate { get; set; }
        public string PaidBy { get; set; }
        public decimal TotalDueAmount { get; set; }
        public string TransactionId { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string ReceiptPDF { get; set; }
        public string BillDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal PayableAmount { get; set; }
        public string DueBillOf { get; set; }
        public decimal ServiceCharge { get; set; }
    }
}
