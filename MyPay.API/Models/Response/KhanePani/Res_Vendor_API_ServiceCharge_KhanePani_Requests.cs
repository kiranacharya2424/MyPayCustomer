using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_Vendor_API_ServiceCharge_KhanePani_Requests : CommonResponse
    {
        // amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // service_charge
        private string _service_charge = string.Empty;
        public string service_charge
        {
            get { return _service_charge; }
            set { _service_charge = value; }
        }
    }
}