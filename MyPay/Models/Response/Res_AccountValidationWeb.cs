using MyPay.Models.Get;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response
{
    public class Res_AccountValidationWeb 
    {
        //branchId
        private string _branchId = string.Empty;
        public string branchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        //responseMessage
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        //responseCode
        private string _responseCode = string.Empty;
        public string responseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }
    }
}