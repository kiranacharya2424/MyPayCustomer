using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.MyPayPayments
{
    public class Res_Merchant_Transaction_LookupRequests  
    {

        private string _MerchantTransactionId = string.Empty;
        public string MerchantTransactionId
        {
            get { return _MerchantTransactionId; }
            set { _MerchantTransactionId = value; }
        }
        private string _MemberContactNumber = string.Empty;
        public string MemberContactNumber
        {
            get { return _MemberContactNumber; }
            set { _MemberContactNumber = value; }
        }
        private decimal _NetAmount = 0;
        public decimal NetAmount
        {
            get { return _NetAmount; }
            set { _NetAmount = value; }
        }
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private string _GatewayTransactionId = string.Empty;
        public string GatewayTransactionId
        {
            get { return _GatewayTransactionId; }
            set { _GatewayTransactionId = value; }
        }

        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
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