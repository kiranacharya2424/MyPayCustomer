using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetBankTransferErrors
    {
        //responseCode
        private string _responseCode = string.Empty;
        public string responseCode
        {
            get { return _responseCode; }
            set { _responseCode = value; }
        }

        //responseDescription
        private string _responseDescription = string.Empty;
        public string responseDescription
        {
            get { return _responseDescription; }
            set { _responseDescription = value; }
        }

        //billsPaymentDescription
        private string _billsPaymentDescription = string.Empty;
        public string billsPaymentDescription
        {
            get { return _billsPaymentDescription; }
            set { _billsPaymentDescription = value; }
        }

        //billsPaymentResponseCode
        private string _billsPaymentResponseCode = string.Empty;
        public string billsPaymentResponseCode
        {
            get { return _billsPaymentResponseCode; }
            set { _billsPaymentResponseCode = value; }
        }

        //fieldErrors
        private List<Res_FieldErrors> _fieldErrors = new List<Res_FieldErrors>();
        public List<Res_FieldErrors> fieldErrors
        {
            get { return _fieldErrors; }
            set { _fieldErrors = value; }
        }
    }

    public class Res_FieldErrors
    {
        //field
        private string _field = string.Empty;
        public string field
        {
            get { return _field; }
            set { _field = value; }
        }

        //message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}