using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_GetUserInfo : CommonProp
    {
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //RecieverMemberId
        private string _RecieverMemberId = string.Empty;
        public string RecieverMemberId
        {
            get { return _RecieverMemberId; }
            set { _RecieverMemberId = value; }
        }
    }
}