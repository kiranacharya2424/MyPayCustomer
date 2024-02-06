using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Smsc : CommonProp
    {
       

        //PhoneNumber
        private string _PhoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        // ********* WE ARE NOT USING OTP KEYWORD HERE TO PREVENT INTRUDERS FROM HACKING ************ //

        //Digits
        private string _Digits = string.Empty;
        public string Digits
        {
            get { return _Digits; }
            set { _Digits = value; }
        }
         
    }
}