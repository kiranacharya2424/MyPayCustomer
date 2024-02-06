using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Airlines_CancelFlight_Requests : CommonProp
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
        private string _CancelTickets = String.Empty;
        public string CancelTickets
        {
            get { return _CancelTickets; }
            set { _CancelTickets = value; }
        } 
    }
}