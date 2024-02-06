using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_ArhantInsurance_Detail_Requests:CommonResponse
    {
        public string ProformaNo { get; set; }
        public string TpPremium { get; set; }
        public string SumInsured { get; set; }
        public string Insured { get; set; }
        public string ClassName { get; set; }
        public string Amount { get; set; }
    }
}