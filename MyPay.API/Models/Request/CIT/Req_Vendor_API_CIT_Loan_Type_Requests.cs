using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_CIT_Loan_Type_Requests : CommonProp
    {
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _AppGroup = string.Empty;
        public string AppGroup
        {
            get { return _AppGroup; }
            set { _AppGroup = value; }
        }
        private string _FieldName = string.Empty;
        public string FieldName
        {
            get { return _FieldName; }
            set { _FieldName = value; }
        }

    }
}