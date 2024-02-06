using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.CIT
{
    public class Req_Vendor_API_CIT_Category_Requests : CommonProp
    {
        private string _Category = string.Empty;
        public string Category
        {
            get { return _Category; }
            set { _Category = value; }
        }
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        

    }
}