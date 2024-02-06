using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_CouponsScratchedApply : CommonProp
    {
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //ServiceId
        private string _ServiceId = string.Empty;
        public string ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }
        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }
        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
    }
}