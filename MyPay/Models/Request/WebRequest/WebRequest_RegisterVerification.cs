﻿using MyPay.Models.Request.WebRequest.Common; 

namespace MyPay.API.Models
{
    public class WebRequest_RegisterVerification:WebCommonProp
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
    }
}