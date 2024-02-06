using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response
{
    public class GetCreditCardTransactionId
    {
        //Code
        private string _Code = string.Empty;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        //SCharge
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
    }
}