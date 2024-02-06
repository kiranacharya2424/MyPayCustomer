using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Details_Requests : CommonResponse
    { 

        private List<AddFlightBookingDetails> _Bookings = new List<AddFlightBookingDetails>();
        public List<AddFlightBookingDetails> Bookings
        {
            get { return _Bookings; }
            set { _Bookings = value; }
        }
         

    }
}