using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_CIT_LoanType_Requests : CommonResponse
    { 
        private string responseCode;
        public string ResponseCode
        {
            get => responseCode;
            set => responseCode = value;
        }
         
    }
}