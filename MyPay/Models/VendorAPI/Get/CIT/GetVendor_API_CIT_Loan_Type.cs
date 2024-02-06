using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    
    public class GetVendor_API_CIT_Loan_Type : CommonGet
    {
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
      
        private List<LoanType> data;
        public List<LoanType> Data
        {
            get => data;
            set => data = value;
        }
    }
    public class LoanType
    {
        public int id { get; set; }
        public string option { get; set; }
        public string value { get; set; }
        public string code { get; set; }
    }
}