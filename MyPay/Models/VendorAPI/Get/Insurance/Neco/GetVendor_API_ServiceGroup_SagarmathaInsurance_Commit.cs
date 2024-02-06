using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Sagarmatha
{
    public class GetVendor_API_ServiceGroup_SagarmathaInsurance_Commit
    {
        // Id
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }
        // credits_consumed
        private string _credits_consumed = string.Empty;
        public string credits_consumed
        {
            get { return _credits_consumed; }
            set { _credits_consumed = value; }
        }
        // credits_available
        private string _credits_available = string.Empty;
        public string credits_available
        {
            get { return _credits_available; }
            set { _credits_available = value; }
        }

        // message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        // state
        private string _state = string.Empty;
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        // WalletBalance
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // UniqueTransactionId
        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        // detailsstring
        private string _detail;
        public string detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
        // detailsstring
        private Sagarmath_Bill_Info _bill_info;
        public Sagarmath_Bill_Info bill_info
        {
            get { return _bill_info; }
            set { _bill_info = value; }
        }
    }
    public class Sagarmath_Bill_Info
    {
        // tax_invoice_no
        private string _tax_invoice_no;
        public string tax_invoice_no
        {
            get { return _tax_invoice_no; }
            set { _tax_invoice_no = value; }
        } 
        // receipt_no
        private string _receipt_no;
        public string receipt_no
        {
            get { return _receipt_no; }
            set { _receipt_no = value; }
        }
        // document_no
        private string _document_no;
        public string document_no
        {
            get { return _document_no; }
            set { _document_no = value; }
        }
    }
}