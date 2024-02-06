using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Reliance
{
    public class GetVendor_API_ServiceGroup_RelianceInsurance_Commit
    {
        // Id 
        private string _id = String.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
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

        public string credits_consumed { get; set; }
        public string credits_available { get; set; }
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

        public RelianceExtraData extra_data { get; set; }
        public RelianceDetail detail { get; set; }
    }
    public class RelianceExtraData
    {
    }

    public class RelianceDetail
    {
        public string transaction_id { get; set; }
        public string product_name { get; set; }
        public string customer_id { get; set; }
        public string invoice_number { get; set; }
        public string customer_name { get; set; }
        public string next_due_date { get; set; }
        public string fine_amount { get; set; }
    }

}