using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_SuryaLifeInsurance_Detail_Requests:CommonResponse
    {
        public string PolicyNo { get; set; }
        public string PlanCode { get; set; }
        public string PayMode { get; set; }
        public string Name { get; set; }
        public string PremiumAmount { get; set; }
        public string FineAmount { get; set; }
        public string AdjustmentAmount { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }
        public string DueDate { get; set; }
        public string NextDueDate { get; set; }
        public string PolicyStatus { get; set; }
        public string Term { get; set; }
        public string MaturityDate { get; set; }
        public string SessionId { get; set; }
    }
}