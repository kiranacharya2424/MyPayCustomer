using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Horoscope
{
    public class Req_Vendor_API_Horoscope_Details:CommonProp
    {

        private string _Frequency = String.Empty;
        public string Frequency
        {
            get { return _Frequency; }
            set { _Frequency = value; }
        }
    }
    public class getcurrenthoroscope
    {

        private string _frequency = String.Empty;
        public string frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }
    }
}