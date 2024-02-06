using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Remittance
{
    public class Req_Remittance_AccountValidation : CommonProp
    {
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
        private string _Reference = String.Empty;
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
        private string _bankId = string.Empty;
        public string bankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }
        private string _accountId = string.Empty;
        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
        private string _accountName = string.Empty;
        public string accountName
        {
            get { return _accountName; }
            set { _accountName = value; }
        }



    }
}