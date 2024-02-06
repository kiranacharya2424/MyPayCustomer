using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Airlines
{
    public class Res_Vendor_API_Airlines_Booking_Details_Requests : CommonResponse
    { 

        private List<AddFlightBookingDetails> _InBounds = new List<AddFlightBookingDetails>();
        public  List<AddFlightBookingDetails> InBounds
        {
            get { return _InBounds; }
            set { _InBounds = value; }
        }


        private List<AddFlightBookingDetails> _OutBounds = new List<AddFlightBookingDetails>();
        public  List<AddFlightBookingDetails> OutBounds
        {
            get { return _OutBounds; }
            set { _OutBounds = value; }
        }

    }
}