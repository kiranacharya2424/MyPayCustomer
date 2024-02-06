using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_OfferBanners:CommonProp
    {
        //IsHome
        private string _IsHome = string.Empty;
        public string IsHome
        {
            get { return _IsHome; }
            set { _IsHome = value; }
        }
    }
}