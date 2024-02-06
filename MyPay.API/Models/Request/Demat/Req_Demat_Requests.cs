using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Demat_Requests : CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _ClientID = String.Empty;
        public string ClientID
        {
            get { return _ClientID; }
            set { _ClientID = value; }
        }
    }
}