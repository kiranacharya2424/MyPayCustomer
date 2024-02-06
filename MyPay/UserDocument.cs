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
    
    public partial class UserDocument
    {
        public long Id { get; set; }
        public string reference { get; set; }
        public string @event { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string document_number { get; set; }
        public System.DateTime issue_date { get; set; }
        public System.DateTime expiry_date { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string dob { get; set; }
        public int selected_type { get; set; }
        public int document { get; set; }
        public int document_visibility { get; set; }
        public int document_must_not_be_expired { get; set; }
        public string document_proof { get; set; }
        public long MemberId { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string Reason { get; set; }
        public int ReasonType { get; set; }
        public string amlreference { get; set; }
        public string additionalproof { get; set; }
        public string addressproof { get; set; }
        public string CreatedByName { get; set; }
    }
}
