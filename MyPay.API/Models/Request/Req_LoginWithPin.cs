using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_LoginWithPin:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }


        //Pin
        private string _Pin = "";
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

    }
}