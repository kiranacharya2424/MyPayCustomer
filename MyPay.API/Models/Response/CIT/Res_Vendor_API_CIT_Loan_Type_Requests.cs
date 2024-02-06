using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.CIT
{
    public class Res_Vendor_API_CIT_Loan_Type_Requests : CommonResponse
    {

        private string responseCode;
        public string ResponseCode
        {
            get => responseCode;
            set => responseCode = value;
        }
        private List<CITLoanType> data;
        public List<CITLoanType> Data
        {
            get => data;
            set => data = value;
        }
    }
    public class CITLoanType
    {
        public int id { get; set; }
        public string option { get; set; }
        public string value { get; set; }
        public string code { get; set; }
    }



}