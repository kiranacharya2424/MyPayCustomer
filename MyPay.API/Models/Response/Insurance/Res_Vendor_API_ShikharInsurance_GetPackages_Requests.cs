using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_ShikharInsurance_GetPackages_Requests:CommonResponse
    {
        public ShikharDetail Detail = new ShikharDetail();

       
    }

    public class ShikharDetail
    {
        public List<ShikharPolicies> policies = new List<ShikharPolicies>();
    }

    public class ShikharPolicies
    {
        public string label { get; set; }
        public string value { get; set; }

    }
}