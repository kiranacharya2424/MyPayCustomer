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
    
    public partial class Vendor_API_Requests
    {
        public long Id { get; set; }
        public string Req_Input { get; set; }
        public string Res_Output { get; set; }
        public string Req_URL { get; set; }
        public string Req_Token { get; set; }
        public string Req_ReferenceNo { get; set; }
        public string Req_Khalti_ReferenceNo { get; set; }
        public string Req_Khalti_Input { get; set; }
        public string Res_Khalti_Output { get; set; }
        public string Req_Khalti_URL { get; set; }
        public bool Res_Khalti_Status { get; set; }
        public string Res_Khalti_State { get; set; }
        public string Res_Khalti_Message { get; set; }
        public string Res_Khalti_Id { get; set; }
        public string Res_TraceId { get; set; }
        public int VendorType { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int IsDeleted { get; set; }
        public int IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public string TransactionUniqueId { get; set; }
        public long MemberId { get; set; }
        public string MemberName { get; set; }
        public string DeviceCode { get; set; }
        public string IpAddress { get; set; }
        public string PlatForm { get; set; }
        public int VendorApiType { get; set; }
    }
}
