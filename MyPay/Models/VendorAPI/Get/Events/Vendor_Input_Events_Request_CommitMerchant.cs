using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Events_Request_CommitMerchant
    {

        private string _orderId = string.Empty;
        public string orderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        } 
        private string _merchantId = string.Empty;
        public string merchantId
        {
            get { return _merchantId; }
            set { _merchantId = value; }
        }
        private string _userName = string.Empty;
        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _password = string.Empty;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}