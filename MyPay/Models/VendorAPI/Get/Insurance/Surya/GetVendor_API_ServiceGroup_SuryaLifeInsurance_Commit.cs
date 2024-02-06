using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.VendorAPI.Get.Insurance.Surya
{
    public class GetVendor_API_ServiceGroup_SuryaLifeInsurance_Commit
    {
        // Id 
        private int _id = 0;
        public int id
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

        // Detail
        private string _detail = string.Empty;
        public string detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

        public SuryaLifeExtraData extra_data { get; set; }
    }

    public class SuryaLifeExtraData
    {
        public string customer_name { get; set; }
        public string fine_amount { get; set; }
        public string adjustment_amount { get; set; }
        public string premium_amount { get; set; }
        public string payment_date { get; set; }
        public string next_due_date { get; set; }
        public string policy_no { get; set; }
        public string total_amount { get; set; }
       
    }
}