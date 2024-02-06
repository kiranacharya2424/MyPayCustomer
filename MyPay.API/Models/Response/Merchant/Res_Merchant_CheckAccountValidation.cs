using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Merchant
{
    public class Res_Merchant_CheckAccountValidation  
    {
        private string _AccountStatus = String.Empty;
        public string AccountStatus
        {
            get { return _AccountStatus; }
            set { _AccountStatus = value; }
        }
        private string _FullName = String.Empty;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        private string _ContactNumber = String.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        private bool _IsAccountValidated = false;
        public bool IsAccountValidated
        {
            get { return _IsAccountValidated; }
            set { _IsAccountValidated = value; }
        }
        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        //responseMessage
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }
        //Details
        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }


        //ReponseCode
        private int _ReponseCode = 0;
        public int ReponseCode
        {
            get { return _ReponseCode; }
            set { _ReponseCode = value; }
        }


        //status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}