using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response
{
    public class GetCreditCardCharge
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
        private string _SCharge = string.Empty;
        public string SCharge
        {
            get { return _SCharge; }
            set { _SCharge = value; }
        }
    }
}