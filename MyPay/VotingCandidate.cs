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
    
    public partial class VotingCandidate
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public long VotingCompetitionID { get; set; }
        public int ContentestNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string EmailID { get; set; }
        public string ContactNo { get; set; }
        public long CountryId { get; set; }
        public string CountryName { get; set; }
        public long ZipCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public int TotalVotes { get; set; }
        public int Rank { get; set; }
        public decimal TotalAmount { get; set; }
        public int Gender { get; set; }
        public string Age { get; set; }
        public int FreeVotes { get; set; }
    }
}
