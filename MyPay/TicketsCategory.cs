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
    
    public partial class TicketsCategory
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public string LogoImage { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Position { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
