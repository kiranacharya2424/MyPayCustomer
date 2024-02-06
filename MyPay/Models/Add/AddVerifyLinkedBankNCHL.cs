using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddVerifyLinkedBankNCHL
    {
        //participantId
        private string _participantId = string.Empty;
        public string participantId
        {
            get { return _participantId; }
            set { _participantId = value; }
        }

        //appId
        private string _appId = string.Empty;
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        //otpCode
        private string _otpCode = string.Empty;
        public string otpCode
        {
            get { return _otpCode; }
            set { _otpCode = value; }
        }

        //validationId 
        private string _validationId = string.Empty;
        public string validationId
        {
            get { return _validationId; }
            set { _validationId = value; }
        }

        //refId 
        private string _refId = string.Empty;
        public string refId
        {
            get { return _refId; }
            set { _refId = value; }
        }

        //otpValidationId 
        private string _otpValidationId = string.Empty;
        public string otpValidationId
        {
            get { return _otpValidationId; }
            set { _otpValidationId = value; }
        }

        //virtualPrivateAddress 
        private string _virtualPrivateAddress = string.Empty;
        public string virtualPrivateAddress
        {
            get { return _virtualPrivateAddress; }
            set { _virtualPrivateAddress = value; }
        }

        //secretKey 
        private string _secretKey = string.Empty;
        public string secretKey
        {
            get { return _secretKey; }
            set { _secretKey = value; }
        }

        //token 
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

    }
}