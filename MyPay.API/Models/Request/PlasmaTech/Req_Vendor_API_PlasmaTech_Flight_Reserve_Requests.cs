using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.PlasmaTech
{
    public class Req_Vendor_API_PlasmaTech_Flight_Reserve_Requests : CommonProp
    {
       
        private string _FlightID = string.Empty;
        public string FlightID
        {
            get { return _FlightID; }
            set { _FlightID = value; }
        }
        private string _ReturnFlightID = string.Empty;
        public string ReturnFlightID
        {
            get { return _ReturnFlightID; }
            set { _ReturnFlightID = value; }
        }
        private string _BookingID = String.Empty;
        public string BookingID
        {
            get { return _BookingID; }
            set { _BookingID = value; }
        }
    }
}