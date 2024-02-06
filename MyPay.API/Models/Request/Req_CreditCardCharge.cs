using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_CreditCardCharge : CommonProp
    {
      

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Code
        private string _Code = string.Empty;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
    }
}