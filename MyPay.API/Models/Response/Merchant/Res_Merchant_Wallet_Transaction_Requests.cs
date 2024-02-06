using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.MyPayPayments
{
    public class Res_Merchant_Wallet_Transaction_Requests  
    {

        private string _MerchantWallet_TransactionId = string.Empty;
        public string MerchantWallet_TransactionId
        {
            get { return _MerchantWallet_TransactionId; }
            set { _MerchantWallet_TransactionId = value; }
        }
        //private string _Sender_TransactionId = string.Empty;
        //public string Sender_TransactionId
        //{
        //    get { return _Sender_TransactionId; }
        //    set { _Sender_TransactionId = value; }
        //}
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