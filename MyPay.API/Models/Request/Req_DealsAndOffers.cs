using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_DealsAndOffers: CommonProp
    {
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
    }
}