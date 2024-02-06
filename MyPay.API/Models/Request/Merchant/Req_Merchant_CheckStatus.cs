using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Merchant
{
    public class Req_Merchant_CheckStatus:CommonProp
    {
        private string _TransactionId = String.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }


        private string _MerchantId = String.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Password = String.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


        private string _AuthTokenString = string.Empty;
        public string AuthTokenString
        {
            get { return _AuthTokenString; }
            set { _AuthTokenString = value; }
        }

        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _TransactionReference = string.Empty;
        public string TransactionReference
        {
            get { return _TransactionReference; }
            set { _TransactionReference = value; }
        }
    }
}