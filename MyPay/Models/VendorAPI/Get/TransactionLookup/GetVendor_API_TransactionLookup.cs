namespace MyPay.Models.Get
{
    public class GetVendor_API_TransactionLookup
    {
        // reference
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }
        // error_code
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        // details
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }
        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
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

        // jsonData
        private string _jsonData = string.Empty;
        public string jsonData
        {
            get { return _jsonData; }
            set { _jsonData = value; }
        }

        // amount
        private decimal _amount = 0;
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // response_id
        private string _response_id = string.Empty;
        public string response_id
        {
            get { return _response_id; }
            set { _response_id = value; }
        }

    }
}