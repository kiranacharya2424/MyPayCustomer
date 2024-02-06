using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Events_Request_Commit
    {

       
        private int _paymentMethodId = 0;
        public int paymentMethodId
        {
            get { return _paymentMethodId; }
            set { _paymentMethodId = value; }
        }
        
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
        private string _txnId = string.Empty;
        public string txnId
        {
            get { return _txnId; }
            set { _txnId = value; }
        }
        private string _ApiClientCode = string.Empty;
        public string apiClientCode
        {
            get { return _ApiClientCode; }
            set { _ApiClientCode = value; }
        }
    }
}