using System;
using System.Collections.Generic;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_StatusFlight_Requests : CommonResponse
    {
        public FlightDetail details { get; set; }

    }

    public class FlightDetail
    {
        public string OutBoundStatus { get; set; }
        public string InBoundStatus { get; set; } 

    }

}   