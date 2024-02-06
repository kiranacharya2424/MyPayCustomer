using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_FlightPassanger:WebCommonProp
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
        private string _ContactName = String.Empty;

        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }
        private string _ContactPhone = String.Empty;
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; }
        }

        private string _ContactEmail = String.Empty;
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; }
        }
        private string _PassengersClassString = String.Empty;
        public string PassengersClassString
        {
            get { return _PassengersClassString; }
            set { _PassengersClassString = value; }
        }
        // IsMobile
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
        private string _FlightId = String.Empty;
        public string FlightId
        {
            get { return _FlightId; }
            set { _FlightId = value; }
        }

        public class PassengerList
        {
            public string Lastname { get; set; }
            public string Type { get; set; }
            public string Title { get; set; }
            public string Gender { get; set; }
            public string Firstname { get; set; }
            public string Nationality { get; set; }
        }
    }
}