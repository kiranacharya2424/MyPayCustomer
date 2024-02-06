using MyPay.Models.Common;

namespace MyPay.Models.Get.KhanePani
{
    public class GetVendor_API_ServiceGroup_KhanePani_Commit : CommonGet
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
        // due_amount
        private string _due_amount = string.Empty;
        public string due_amount
        {
            get { return _due_amount; }
            set { _due_amount = value; }
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
        private string _detailsstring;
        public string detailsstring
        {
            get { return _detailsstring; }
            set { _detailsstring = value; }
        }
        // object
        private detail _detailsobject = new detail();
        public detail detailsobject
        {
            get { return _detailsobject; }
            set { _detailsobject = value; }
        }
    }
    public class detail
    {
        // message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        // due_amount
        private string _due_amount = string.Empty;
        public string due_amount
        {
            get { return _due_amount; }
            set { _due_amount = value; }
        }
    }

}