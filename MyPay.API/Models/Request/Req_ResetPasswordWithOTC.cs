using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_ResetPasswordWithOTC:CommonProp
    {
        //Password
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        //OTC
        private string _OTC = string.Empty;
        public string OTC
        {
            get { return _OTC; }
            set { _OTC = value; }
        }

    }
}