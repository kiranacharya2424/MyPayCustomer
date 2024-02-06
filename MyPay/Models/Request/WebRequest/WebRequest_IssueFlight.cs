using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_IssueFlight:WebCommonProp
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
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }

        private string _ContactName = String.Empty;
        public string ContactName 
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        private string _ContactEmail = String.Empty;
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; }
        }
        private string _ContactMobile = String.Empty;
        public string ContactMobile
        {
            get { return _ContactMobile; }
            set { _ContactMobile = value; }
        }

        public string _PassengerDetail = String.Empty;
        public string PassengerDetail
        {
            get { return _PassengerDetail; }
            set { _PassengerDetail = value; }
        }
    }
}