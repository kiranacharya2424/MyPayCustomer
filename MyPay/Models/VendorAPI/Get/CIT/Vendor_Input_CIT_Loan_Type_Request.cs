using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_CIT_Loan_Type_Request
    {

        private string _appGroup = string.Empty;
        public string appGroup
        {
            get { return _appGroup; }
            set { _appGroup = value; }
        }
        private string _fieldName = string.Empty;
        public string fieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }
    }
}