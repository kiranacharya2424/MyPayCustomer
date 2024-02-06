using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_FlightLookup:WebCommonProp
    {
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _FlightType = String.Empty;
        public string FlightType
        {
            get { return _FlightType; }
            set { _FlightType = value; }
        }
        private string _TripType = String.Empty;
        public string TripType
        {
            get { return _TripType; }
            set { _TripType = value; }
        }
        private string _FlightDate = String.Empty;
        public string FlightDate
        {
            get { return _FlightDate; }
            set { _FlightDate = value; }
        }
        private string _ReturnDate = String.Empty;
        public string ReturnDate
        {
            get { return _ReturnDate; }
            set { _ReturnDate = value; }
        }
        private string _Adult = String.Empty;
        public string Adult
        {
            get { return _Adult; }
            set { _Adult = value; }
        }
        private string _Child = String.Empty;
        public string Child
        {
            get { return _Child; }
            set { _Child = value; }
        }
        private string _FromDeparture = String.Empty;
        public string FromDeparture
        {
            get { return _FromDeparture; }
            set { _FromDeparture = value; }
        }
        private string _ToArrival = String.Empty;
        public string ToArrival
        {
            get { return _ToArrival; }
            set { _ToArrival = value; }
        }
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}