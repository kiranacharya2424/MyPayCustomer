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
    
    public partial class sp_getfromReceipts_Result
    {
        public long id { get; set; }
        public Nullable<long> memberID { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string table1Logo { get; set; }
        public string table2Logo { get; set; }
        public string table1JSONContent { get; set; }
        public string table2JSONContent { get; set; }
        public string contactNumber { get; set; }
        public string fullname { get; set; }
        public string TxnID { get; set; }
        public string TxnType { get; set; }
        public string PaidFor { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<long> serviceID { get; set; }
    }
}