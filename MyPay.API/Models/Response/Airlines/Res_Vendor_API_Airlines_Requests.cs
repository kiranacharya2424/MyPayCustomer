using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Requests : CommonResponse
    {
         
        public string Commission { get; set; }
        public string FlightID { get; set; }
        public string FlightFareTotal { get; set; }
        public string AdultCommission { get; set; }
        public string InboundCommission { get; set; }
        public string InboundAdultCommission { get; set; }
        public string ChildCommission { get; set; }
        public string TTL { get; set; }
        public string InboundChildCommission { get; set; }
        public string InboundFlightID { get; set; }
        public string InboundFlightFareTotal { get; set; }
        public string ApiMessage { get; set; }

    }
}