﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_RecentPaymentsSave : CommonProp
    {
       //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        //TransactionId
        private string _TransactionId = "";
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

    }
}