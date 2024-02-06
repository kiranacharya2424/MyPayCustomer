using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_ChangePinWithOTP : CommonProp
    {
        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        //_Phonenumber
        private string _Phonenumber = string.Empty;
        public string Phonenumber
        {
            get { return _Phonenumber; }
            set { _Phonenumber = value; }
        }

        //Pin
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }

        //ConfirmPin
        private string _ConfirmPin = string.Empty;
        public string ConfirmPin
        {
            get { return _ConfirmPin; }
            set { _ConfirmPin = value; }
        }
        //OTP
        private string _OTP = string.Empty;
        public string OTP
        {
            get { return _OTP; }
            set { _OTP = value; }
        }

    }
}