using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_SanimaLifeInsurance_Detail_Requests: CommonResponse
    {
        public string SessionId { get; set; }

        public SanimaLifeDetail Detail = new SanimaLifeDetail();

    }

    public class SanimaLifeDetail
    {
        public string PaymentMode { get; set; }
        public string AssuredName { get; set; }        
        public string PremiumAmount { get; set; }
        public string ProductName { get; set; }
    }
}