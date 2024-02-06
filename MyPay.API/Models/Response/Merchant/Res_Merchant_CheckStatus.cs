using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Merchant
{
    public class Res_Merchant_CheckStatus
    {
        private string _TransactionStatus = String.Empty;
        public string TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }
        private string _TransactionUniqueID = String.Empty;
        public string TransactionUniqueID
        {
            get { return _TransactionUniqueID; }
            set { _TransactionUniqueID = value; }
        }

        private Int32 _StatusCode = 0;
        public Int32 StatusCode
        {
            get { return _StatusCode; }
            set { _StatusCode = value; }
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