﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVerifyLinkBankNCHL
    {
        //_participantId
        private string _participantId = "";
        public string participantId
        {
            get { return _participantId; }
            set { _participantId = value; }
        }


        //appId_
        private string _appId = "";
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        //message
        private string _message = "";
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        //data
        private dataVerifyBankLinkNCHL _data = new dataVerifyBankLinkNCHL();
        public dataVerifyBankLinkNCHL data
        {
            get { return _data; }
            set { _data = value; }
        }

        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        //Details
        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }


        //ReponseCode
        private string _responseCode = "";
        public string responseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }

        private string _token = "";
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _ReponseCode = 0;
        public int ReponseCode
        {
            get { return _ReponseCode; }
            set { _ReponseCode = value; }
        }

    }

    public class dataVerifyBankLinkNCHL
    {

        private string _virtualPrivateAddress = "";
        public string virtualPrivateAddress
        {
            get { return _virtualPrivateAddress; }
            set { _virtualPrivateAddress = value; }
        }
        private string _secretKey = "";
        public string secretKey
        {
            get { return _secretKey; }
            set { _secretKey = value; }
        }

        private string _customerState = "";
        public string customerState
        {
            get { return _customerState; }
            set { _customerState = value; }
        }

        private string _validationId = "";
        public string validationId
        {
            get { return _validationId; }
            set { _validationId = value; }
        } 
        private bool _microPaymentRequired = false;
        public bool microPaymentRequired
        {
            get { return _microPaymentRequired; }
            set { _microPaymentRequired = value; }
        }
        
    }

  
}