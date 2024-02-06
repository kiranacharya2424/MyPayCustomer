using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_CIT_Category_Request
    {
        private string _category = string.Empty;
        public string category
        {
            get { return _category; }
            set { _category = value; }
        }
        
    }
}