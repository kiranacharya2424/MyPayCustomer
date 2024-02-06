using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_BankBranch : CommonProp
    {
        //BankId
        private string _BankId = string.Empty;
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }
    }
}