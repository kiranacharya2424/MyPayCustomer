using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_RecipientDetail:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //RecipientPhone
        private string _RecipientPhone = string.Empty;
        public string RecipientPhone
        {
            get { return _RecipientPhone; }
            set { _RecipientPhone = value; }
        }

    }
}