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
    
    public partial class sp_Notification_Datatable_Result
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string Title { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationDescription { get; set; }
        public int NotificationType { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string IpAddress { get; set; }
        public int SentStatus { get; set; }
        public int ReadStatus { get; set; }
        public string FireBaseRequest { get; set; }
        public string FireBaseResponse { get; set; }
        public string IndiaDate { get; set; }
        public string UpdateIndiaDate { get; set; }
        public string CreatedDatedt { get; set; }
        public string UpdatedDatedt { get; set; }
        public int Sno { get; set; }
        public int FilterTotalCount { get; set; }
    }
}