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
    
    public partial class sp_EstatementPDFToken_Get_Result
    {
        public Nullable<long> Row { get; set; }
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string Token { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
