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
    
    public partial class sp_VotingList_Get_Result
    {
        public Nullable<long> Row { get; set; }
        public long Id { get; set; }
        public long VotingCompetitionId { get; set; }
        public string VotingCandidateUniqueId { get; set; }
        public string VotingCandidateName { get; set; }
        public long VotingPackageID { get; set; }
        public int NoofVotes { get; set; }
        public long MemberID { get; set; }
        public string MemberContactNumber { get; set; }
        public string MemberName { get; set; }
        public string PlatForm { get; set; }
        public string DeviceCode { get; set; }
        public string IpAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public string TransactionUniqueId { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public decimal Amount { get; set; }
        public int FreeVotes { get; set; }
        public int PaidVotes { get; set; }
        public string IndiaDate { get; set; }
        public string UpdatedIndiaDate { get; set; }
        public string CreatedDateDt { get; set; }
        public string UpdatedDateDt { get; set; }
        public Nullable<long> Sno { get; set; }
        public string CompetitionName { get; set; }
    }
}