using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_LocalLevel : CommonProp
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