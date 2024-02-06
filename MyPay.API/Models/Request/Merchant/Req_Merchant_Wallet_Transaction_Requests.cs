using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Merchant_Wallet_Transaction_Requests : CommonProp
    { 
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
        private string _Remarks = String.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        } 
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _AuthTokenString = string.Empty;
        public string AuthTokenString
        {
            get { return _AuthTokenString; }
            set { _AuthTokenString = value; }
        }
    }
}