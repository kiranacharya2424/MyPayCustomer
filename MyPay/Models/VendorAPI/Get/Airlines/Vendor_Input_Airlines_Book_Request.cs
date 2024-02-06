using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Airlines_Book_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }


        private string _flight_id = string.Empty;
        public string flight_id
        {
            get { return _flight_id; }
            set { _flight_id = value; }
        }

        private string _return_flight_id = string.Empty;
        public string return_flight_id
        {
            get { return _return_flight_id; }
            set { _return_flight_id = value; }
        }

        private string _booking_id = string.Empty;
        public string booking_id
        {
            get { return _booking_id; }
            set { _booking_id = value; }
        }
    }
}