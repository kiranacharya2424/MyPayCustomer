using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Transaction_Lookup_API_RequestsAll : CommonProp
    {
        //Req_Token
        private string _Req_Token = string.Empty;
        public string Req_Token
        {
            get { return _Req_Token; }
            set { _Req_Token = value; }
        }
         
    }
}