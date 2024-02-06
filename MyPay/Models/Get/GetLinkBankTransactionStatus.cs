using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetLinkBankTransactionStatus
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
        private dataLinkBankTransactionStatus _data = new dataLinkBankTransactionStatus();
        public dataLinkBankTransactionStatus data
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

    public class dataLinkBankTransactionStatus
    {

        //GatewayTransactionId
        private string _GatewayTransactionId = "";
        public string GatewayTransactionId
        {
            get { return _GatewayTransactionId; }
            set { _GatewayTransactionId = value; }
        }

        //TransactionId
        private string _TransactionId = "";
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //Amount
        private string _Amount = "";
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //TransactionStatus
        private string _TransactionStatus = "";
        public string TransactionStatus
        {
            get { return _TransactionStatus; }
            set { _TransactionStatus = value; }
        }

        //TransactionRemarks
        private string _TransactionRemarks = "";
        public string TransactionRemarks
        {
            get { return _TransactionRemarks; }
            set { _TransactionRemarks = value; }
        }

        //TransactionRemarks2
        private string _TransactionRemarks2 = "";
        public string TransactionRemarks2
        {
            get { return _TransactionRemarks2; }
            set { _TransactionRemarks2 = value; }
        }

        //TransactionRemarks3"
        private string _TransactionRemarks3 = "";
        public string TransactionRemarks3
        {
            get { return _TransactionRemarks3; }
            set { _TransactionRemarks3 = value; }
        }


    }

  
}