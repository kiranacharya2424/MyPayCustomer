using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.Events
{
    public class GetVendor_API_Events_CommitMerchant : CommonGet
    {

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
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
        // Message
        private string _merchantTransactionId = string.Empty;
        public string merchantTransactionId
        {
            get { return _merchantTransactionId; }
            set { _merchantTransactionId = value; }
        }
    }
}