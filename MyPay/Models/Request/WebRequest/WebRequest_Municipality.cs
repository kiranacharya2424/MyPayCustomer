using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_Municipality: WebCommonProp
    {
        //DistrictCode
        private string _DistrictCode = string.Empty;
        public string DistrictCode
        {
            get { return _DistrictCode; }
            set { _DistrictCode = value; }
        }
    }
}