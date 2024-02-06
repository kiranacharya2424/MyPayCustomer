using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_District : CommonProp
    {

        //ProvinceCode
        private string _ProvinceCode = string.Empty;
        public string ProvinceCode
        {
            get { return _ProvinceCode; }
            set { _ProvinceCode = value; }
        }
    }
}