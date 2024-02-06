using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Merchant_Transaction_Requests : CommonProp
    {
        private string _OrderId = String.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        } 
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
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
        private string _MerchantTransactionId = string.Empty;
        public string MerchantTransactionId
        {
            get { return _MerchantTransactionId; }
            set { _MerchantTransactionId = value; }
        }
        private string _GatewayTransactionId = string.Empty;
        public string GatewayTransactionId
        {
            get { return _GatewayTransactionId; }
            set { _GatewayTransactionId = value; }
        }

        private string _ReturnUrl = string.Empty;
        public string ReturnUrl
        {
            get { return _ReturnUrl; }
            set { _ReturnUrl = value; }
        }
    }
}