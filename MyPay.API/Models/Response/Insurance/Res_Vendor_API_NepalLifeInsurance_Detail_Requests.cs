using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_NepalLifeInsurance_Detail_Requests:CommonResponse
    {
        public string CustomerName { get; set; }
        public string PolicyNo { get; set; }
        public string DueDate { get; set; }
        public string PremiumAmount { get; set; }
        public string Amount { get; set; }
        public string FineAmount { get; set; }
        public string RebateAmount { get; set; }
        public string SessionId { get; set; }

        private NepalLifeInsuranceErrorResponse _ErrorResponse = new NepalLifeInsuranceErrorResponse();


    }
    public class NepalLifeInsuranceErrorResponse
    {
        public bool Status { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
        public string ErrorData { get; set; }
        public string State { get; set; }
    }
}