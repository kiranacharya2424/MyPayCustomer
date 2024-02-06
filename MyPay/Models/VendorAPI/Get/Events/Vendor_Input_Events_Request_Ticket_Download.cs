using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Events_Request_Ticket_Download
    {

        private string _orderId = string.Empty;
        public string orderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private string _merchantCode = string.Empty;
        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        } 
       
    }
}