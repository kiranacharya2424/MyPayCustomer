﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetFundTransferRequest
    {
        //code
        private string _code = "";
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }



        //message
        private string _message = "";
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        //errors
        private List<Errors_Instruments> _errors = new List<Errors_Instruments>();
        public List<Errors_Instruments> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        //data
        private datafundtransferrequest _data = new datafundtransferrequest();
        public datafundtransferrequest data
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

    public class datafundtransferrequest
    {
        //MerchantId
        private string _MerchantId = "";
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //MerchantTxnId
        private string _MerchantTxnId = "";
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
        }

        //TransactionId
        private string _TransactionId = "";
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //TransactionStatus
        private string _TransactionStatus = "";
        public string TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }

      
    }

  
}