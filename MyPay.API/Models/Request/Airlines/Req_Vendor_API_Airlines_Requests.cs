using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Airlines_Requests : CommonProp
    {
        
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
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
        private string _PassengerDetail = string.Empty;
        public string PassengerDetail
        {
            get { return _PassengerDetail; }
            set { _PassengerDetail = value; }
        }
    }
}