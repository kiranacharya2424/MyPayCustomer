﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_GetRedeemPoints: CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
    }
}