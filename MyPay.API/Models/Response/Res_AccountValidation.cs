using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_AccountValidation:CommonResponse
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
        //branchName
        private string _branchName = string.Empty;
        public string branchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }
        //branchName
        private string _MatchPercentate = string.Empty;
        public string MatchPercentate
        {
            get { return _MatchPercentate; }
            set { _MatchPercentate = value; }
        }
    }
}