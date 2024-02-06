using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_RewardPoints: CommonProp
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

        //DateFrom
        private string _DateFrom = string.Empty;
        public string DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }

        //DateTo
        private string _DateTo = string.Empty;
        public string DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }
    }
}