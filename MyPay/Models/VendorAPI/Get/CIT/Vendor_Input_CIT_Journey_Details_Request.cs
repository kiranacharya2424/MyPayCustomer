using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_CIT_Journey_Details_Request
    {

        private string _appCode = string.Empty;
        public string appCode
        {
            get { return _appCode; }
            set { _appCode = value; }
        }
    }
}