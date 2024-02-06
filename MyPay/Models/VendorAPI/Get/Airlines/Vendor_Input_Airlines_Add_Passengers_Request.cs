using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Airlines_Add_Passengers_Request
    {

        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        public string booking_id { get; set; }
        public string contact_name { get; set; }
        public string contact_phone { get; set; }
        public List<Passenger> passengers { get; set; }
    }
    public class Passenger
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
    }

}