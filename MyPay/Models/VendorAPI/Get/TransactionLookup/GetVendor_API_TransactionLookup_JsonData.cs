namespace MyPay.Models.Get
{
    public class GetVendor_API_TransactionLookup_JsonData
    {

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }

        // state
        private string _state = "";
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }

        // details
        private string _details = "";
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }

        // reference
        private string _reference = "";
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        // response_id
        private int _response_id = 0;
        public int response_id
        {
            get { return _response_id; }
            set { _response_id = value; }
        }


        // amount
        private decimal _amount = 0;
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}