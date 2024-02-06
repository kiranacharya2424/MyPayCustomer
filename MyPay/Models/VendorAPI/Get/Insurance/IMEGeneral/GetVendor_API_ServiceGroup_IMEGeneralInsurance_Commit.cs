using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral
{
    public class GetVendor_API_ServiceGroup_IMEGeneralInsurance_Commit
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
        private string _Detail = string.Empty;
        public string Detail
        {
            get { return _Detail; }
            set { _Detail = value; }
        }

    }
}
