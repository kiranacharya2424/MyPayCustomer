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
    
    public partial class sp_ProviderLogosList_Datatable_Result
    {
        public long Id { get; set; }
        public string ProviderName { get; set; }
        public long ProviderServiceCategoryId { get; set; }
        public string ProviderServiceName { get; set; }
        public long ProviderTypeId { get; set; }
        public string ProviderLogoURL { get; set; }
        public bool IsActive { get; set; }
        public string ServiceAPIName { get; set; }
        public string WebURL { get; set; }
        public bool IsServiceDown { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public string WebClientURL { get; set; }
        public int SortOrder { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> FilterTotalCount { get; set; }
    }
}
