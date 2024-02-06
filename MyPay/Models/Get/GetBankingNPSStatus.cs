using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyPay.Models.Get
{
    public class GetBankingNPSStatus
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
        private List<Errors_NPSStatus> _errors = new List<Errors_NPSStatus>();
        public List<Errors_NPSStatus> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        //data
        private dataNPSStatus _data = new dataNPSStatus();
        public dataNPSStatus data
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

    public class dataNPSStatus
    {
        //Amount
        private string _Amount = "";
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //ServiceCharge
        private string _ServiceCharge = "";
        public string ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }

        //TransactionRemarks 
        private string _TransactionRemarks = "";
        public string TransactionRemarks
        {
            get { return _TransactionRemarks; }
            set { _TransactionRemarks = value; }
        }


        //MerchantTxnId 
        private string _MerchantTxnId = "";
        public string MerchantTxnId
        {
            get { return _MerchantTxnId; }
            set { _MerchantTxnId = value; }
        }

        //CbsMessage
        private string _CbsMessage = "";
        public string CbsMessage
        {
            get { return _CbsMessage; }
            set { _CbsMessage = value; }
        }

        //GatewayReferenceNo 
        private string _GatewayReferenceNo = "";
        public string GatewayReferenceNo
        {
            get { return _GatewayReferenceNo; }
            set { _GatewayReferenceNo = value; }
        }

        //Status 
        private string _Status = "";
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Institution  
        private string _Institution = "";
        public string Institution
        {
            get { return _Institution; }
            set { _Institution = value; }
        }

        //Instrument  
        private string _Instrument = "";
        public string Instrument
        {
            get { return _Instrument; }
            set { _Instrument = value; }
        }

    }

    public class Errors_NPSStatus
    {
        //error_code
        private string _error_code = "";
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        //error_message
        private string _error_message = "";
        public string error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }
    }
}