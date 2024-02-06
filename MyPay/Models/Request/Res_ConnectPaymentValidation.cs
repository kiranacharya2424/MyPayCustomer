using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Res_ConnectPaymentValidation
    {
        //merchantId
        private string _merchantId = string.Empty;
        public string merchantId
        {
            get { return _merchantId; }
            set { _merchantId = value; }
        }

        //appId
        private string _appId = string.Empty;
        public string appId
        {
            get { return _appId; }
            set { _appId = value; }
        }

        //referenceId
        private string _referenceId = string.Empty;
        public string referenceId
        {
            get { return _referenceId; }
            set { _referenceId = value; }
        }

        //txnAmt
        private string _txnAmt = string.Empty;
        public string txnAmt
        {
            get { return _txnAmt; }
            set { _txnAmt = value; }
        }

        //token
        private string _token = string.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        //txnId
        private string _txnId = string.Empty;
        public string txnId
        {
            get { return _txnId; }
            set { _txnId = value; }
        }

        //txnDate
        private string _txnDate = string.Empty;
        public string txnDate
        {
            get { return _txnDate; }
            set { _txnDate = value; }
        }

        //txnCrncy
        private string _txnCrncy = string.Empty;
        public string txnCrncy
        {
            get { return _txnCrncy; }
            set { _txnCrncy = value; }
        }


        //chargeAmt
        private string _chargeAmt = string.Empty;
        public string chargeAmt
        {
            get { return _chargeAmt; }
            set { _chargeAmt = value; }
        }

        //chargeLiability
        private string _chargeLiability = string.Empty;
        public string chargeLiability
        {
            get { return _chargeLiability; }
            set { _chargeLiability = value; }
        }

        //refId
        private string _refId = string.Empty;
        public string refId
        {
            get { return _refId; }
            set { _refId = value; }
        }

        //remarks
        private string _remarks = string.Empty;
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

        //particulars
        private string _particulars = string.Empty;
        public string particulars
        {
            get { return _particulars; }
            set { _particulars = value; }
        }

        //status
        private string _status = string.Empty;
        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        //statusDesc
        private string _statusDesc = string.Empty;
        public string statusDesc
        {
            get { return _statusDesc; }
            set { _statusDesc = value; }
        }
    }
}