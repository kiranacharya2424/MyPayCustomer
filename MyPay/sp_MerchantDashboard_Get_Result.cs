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
    
    public partial class sp_MerchantDashboard_Get_Result
    {
        public Nullable<int> AllTransactions { get; set; }
        public Nullable<int> TodayTransactions { get; set; }
        public Nullable<int> ThisWeekTransactions { get; set; }
        public Nullable<int> ThisMonthTransactions { get; set; }
        public int AllOrders { get; set; }
        public int TodayOrders { get; set; }
        public int ThisWeekOrders { get; set; }
        public int ThisMonthOrders { get; set; }
        public int TodayCreditTransactions { get; set; }
        public int TodayDebitTransactions { get; set; }
        public int ThisMonthCreditTransactions { get; set; }
        public int ThisMonthDebitTransactions { get; set; }
        public string apikey { get; set; }
        public int WalletBalance { get; set; }
        public int TotalBalance { get; set; }
        public int UserWalletBalance { get; set; }
        public string apipassword { get; set; }
        public string secretkey { get; set; }
    }
}