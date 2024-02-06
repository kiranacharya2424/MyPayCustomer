using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_EarnedCoins:WebCommonProp
    {
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

        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}