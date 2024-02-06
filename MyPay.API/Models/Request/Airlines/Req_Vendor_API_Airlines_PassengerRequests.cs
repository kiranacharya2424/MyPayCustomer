﻿using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Airlines_PassengerRequests : CommonProp
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
        private string _FlightId = String.Empty;
        public string FlightId
        {
            get { return _FlightId; }
            set { _FlightId = value; }
        }
    }


}