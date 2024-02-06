using System;
using System.Collections.Generic;

namespace MyPay.Models.Get
{
    public class GetLoadWalletWithTokenNCHL
    {
        private string _responseCode = "";
        public string responseCode { get => _responseCode; set => _responseCode = value; }
        private string _responseMessage = "";
        public string responseMessage { get => _responseMessage; set => _responseMessage = value; }
        private string _recDate = "";
        public string recDate { get => _recDate; set => _recDate = value; }
        private string _participantId = "";
        public string participantId { get => _participantId; set => _participantId = value; }
        private string _paymentToken = "";
        public string paymentToken { get => _paymentToken; set => _paymentToken = value; }
        private decimal _amount = 0;
        public decimal amount { get => _amount; set => _amount = value; }
        private decimal _chargeAmount = 0;
        public decimal chargeAmount { get => _chargeAmount; set => _chargeAmount = value; }
        private string _chargeLiability = "";
        public string chargeLiability { get => _chargeLiability; set => _chargeLiability = value; }
        private string _appId = "";
        public string appId { get => _appId; set => _appId = value; }
        private string _instructionId = "";
        public string instructionId { get => _instructionId; set => _instructionId = value; }
        private string _refId = "";
        public string refId { get => _refId; set => _refId = value; }

        private string _particulars = "";

        public string particulars { get => _particulars; set => _particulars = value; }
        private string _remarks = "";
        public string remarks { get => _remarks; set => _remarks = value; }
        private string _addnField1 = "";
        public string addnField1 { get => _addnField1; set => _addnField1 = value; }
        private string _addnField2 = "";
        public string addnField2 { get => _addnField2; set => _addnField2 = value; }
        private string _secondaryAuthorizationRequired = "N";
        public string secondaryAuthorizationRequired { get => _secondaryAuthorizationRequired; set => _secondaryAuthorizationRequired = value; }
        private string _token = "";
        public string token { get => _token; set => _token = value; }
        private List<object> _responseErrors = new List<object>();
        public List<object> responseErrors { get; set; }

        private string _debitStatus = "";
        public string debitStatus { get => _debitStatus; set => _debitStatus = value; }

        private string _creditStatus = "";
        public string creditStatus { get => _creditStatus; set => _creditStatus = value; }

        private string _debitDescription = "";
        public string debitDescription { get => _debitDescription; set => _debitDescription = value; }

        private string _creditDescription = "";
        public string creditDescription { get => _creditDescription; set => _creditDescription = value; }


        private Int64 _txnId = 0;
        public Int64 txnId { get => _txnId; set => _txnId = value; }

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


}