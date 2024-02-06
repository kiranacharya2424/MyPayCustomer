using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_ReadNotifications:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //NotificationId
        private Int64 _NotificationId = 0;
        public Int64 NotificationId
        {
            get { return _NotificationId; }
            set { _NotificationId = value; }
        }
    }
}