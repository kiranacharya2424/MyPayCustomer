using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_ServiceCharge_KhanePani_Requests : CommonProp
    {


        // Counter
        private string _Counter = string.Empty;
        public string Counter
        {
            get { return _Counter; }
            set { _Counter = value; }
        }
        // amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}