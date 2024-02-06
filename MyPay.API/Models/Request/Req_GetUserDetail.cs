using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_GetUserDetail:CommonProp
    {
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

    }
}