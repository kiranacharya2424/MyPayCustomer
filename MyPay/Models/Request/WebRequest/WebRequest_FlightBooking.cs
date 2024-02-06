using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Request.WebRequest.Common;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_FlightBooking:WebCommonProp
    {
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _BookingID = String.Empty;
        public string BookingID
        {
            get { return _BookingID; }
            set { _BookingID = value; }
        }
        private string _FlightID = String.Empty;
        public string FlightID
        {
            get { return _FlightID; }
            set { _FlightID = value; }
        }
        private string _ReturnFlightID = String.Empty;
        public string ReturnFlightID
        {
            get { return _ReturnFlightID; }
            set { _ReturnFlightID = value; }
        }
        private string _FareTotal = String.Empty;
        public string FareTotal
        {
            get { return _FareTotal; }
            set { _FareTotal = value; }
        }
        // IsMobile
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}