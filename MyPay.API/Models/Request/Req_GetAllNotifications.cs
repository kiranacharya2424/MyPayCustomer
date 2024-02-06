using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_GetAllNotifications:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Take
        private string _Take = "";
        public string Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private string _Skip = "";
        public string Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
    }
}