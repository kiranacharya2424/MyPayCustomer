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
    
    public partial class sp_verification_Get_Result
    {
        public int Row { get; set; }
        public long Id { get; set; }
        public int Type { get; set; }
        public string Otp { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsVerified { get; set; }
        public string PhoneExtension { get; set; }
        public int VerificationType { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string IndiaDate { get; set; }
    }
}
