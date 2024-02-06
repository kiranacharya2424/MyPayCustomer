using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class AddAcoountValidation 
    {
        //bankId
        private string _bankId = string.Empty;
        public string bankId
        {
            get { return _bankId; }
            set { _bankId = value; }
        }

        //accountId
        private string _accountId = string.Empty;
        public string accountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        //accountName
        private string _accountName = string.Empty;
        public string accountName
        {
            get { return _accountName; }
            set { _accountName = value; }
        }
    }
}