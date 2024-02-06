using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_Events_Booking_Requests : CommonResponse
    {

       
        // status
        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private List<AddEventDetails> _data = new List<AddEventDetails>();
        public List<AddEventDetails> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
    

    
}
