using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_Country:CommonProp
    {
        private string _SearchCodes = String.Empty;
        public string SearchCodes
        {
            get { return _SearchCodes; }
            set { _SearchCodes = value; }
        }
    }
}