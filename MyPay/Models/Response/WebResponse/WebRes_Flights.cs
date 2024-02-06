using MyPay.Models.Add;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Flights : WebCommonResponse
    {
        private List<AddFlightBookingDetails> _Bookings = new List<AddFlightBookingDetails>();
        public List<AddFlightBookingDetails> Bookings
        {
            get { return _Bookings; }
            set { _Bookings = value; }
        }

    }
}