using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_ReferEarnImage
    {
        private List<AddReferEarnImage> _objData = new List<AddReferEarnImage>();
        public List<AddReferEarnImage> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }
    }
}