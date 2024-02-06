using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_ConnectPaymentValidation
    {
        //merchantId
        private string _merchantId = string.Empty;
        public string merchantId
        {
            get { return _merchantId; }
            set { _merchantId = value; }
        }

        //appId
        private string _appId = string.Empty;
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        //referenceId
        private string _referenceId = string.Empty;
        public string referenceId
        {
            get { return _referenceId; }
            set { _referenceId = value; }
        }

        //txnAmt
        private string _txnAmt = string.Empty;
        public string txnAmt
        {
            get { return _txnAmt; }
            set { _txnAmt = value; }
        }

        //token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

    }
}