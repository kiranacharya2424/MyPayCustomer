using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Airlines_CancelFlight_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }


        private string _cancel_tickets = string.Empty;
        public string cancel_tickets
        {
            get { return _cancel_tickets; }
            set { _cancel_tickets = value; }
        }
         

        private string _booking_id = string.Empty;
        public string booking_id
        {
            get { return _booking_id; }
            set { _booking_id = value; }
        }
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
    }
}