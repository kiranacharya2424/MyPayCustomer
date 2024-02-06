using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_Vendor_API_ServiceCharge_Nea_Requests : CommonResponse
    {
        // service_charge
        private string _service_charge = string.Empty;
        public string service_charge
        {
            get { return _service_charge; }
            set { _service_charge = value; }
        }

    }
}