using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Shikhar
{
    public class Vendor_Input_ShikharInsurance_GetPackages_Request
    {
        // token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}