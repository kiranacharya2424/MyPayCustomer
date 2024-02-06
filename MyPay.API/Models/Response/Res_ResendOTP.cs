using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_ResendOTP:CommonResponse
    {
       

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //PhoneExt
        private string _PhoneExt = string.Empty;
        public string PhoneExt
        {
            get { return _PhoneExt; }
            set { _PhoneExt = value; }
        }


        //VerificationOTP
        private string _VerificationOTP = string.Empty;
        public string VerificationOTP
        {
            get { return _VerificationOTP; }
            set { _VerificationOTP = value; }
        }
 
    }
}