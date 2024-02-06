using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_ReferEarnImage: CommonResponse
    {
        // ReferEarn
        private List<AddReferEarnImage> _data = new List<AddReferEarnImage>();
        public List<AddReferEarnImage> data
        {
            get { return _data; }
            set { _data = value; }
        }
         
    }
}