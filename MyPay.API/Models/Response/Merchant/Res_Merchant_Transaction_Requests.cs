using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.MyPayPayments
{
    public class Res_Merchant_Transaction_Requests  
    {

        private string _MerchantTransactionId = string.Empty;
        public string MerchantTransactionId
        {
            get { return _MerchantTransactionId; }
            set { _MerchantTransactionId = value; }
        }
        private string _RedirectURL = string.Empty;
        public string RedirectURL
        {
            get { return _RedirectURL; }
            set { _RedirectURL = value; }
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