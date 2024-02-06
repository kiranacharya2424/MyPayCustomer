using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Events_Request_Details
    {

       
        private int _eventId = 0;
        public int eventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }
        
        private string _eventDate = string.Empty;
        public string eventDate
        {
            get { return _eventDate; }
            set { _eventDate = value; }
        }

        private string _merchantCode = string.Empty;
        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        }
    }
}