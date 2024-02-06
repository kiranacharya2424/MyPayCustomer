using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_RemoveUserBankAccount:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //BankId
        private string _BankId = string.Empty;
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }
    }
}