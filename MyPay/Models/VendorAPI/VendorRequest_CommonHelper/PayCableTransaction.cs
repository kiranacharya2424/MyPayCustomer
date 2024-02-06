namespace MyPay.Models.VendorAPI.VendorRequest_CommonHelper
{
    public class PayCableTransaction
    {
        private string _TransactionId { get; set; }
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        public string User { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerWalletID { get; set; }
        public object TicketDetails { get; set; }
        //  public List<TicketDetails> ticketDetails { get; set; }
        private string _Req_Token = string.Empty;
        public string Req_Token
        {
            get { return _Req_Token; }
            set { _Req_Token = value; }
        }

        //Req_ReferenceNo
        private string _Req_ReferenceNo = string.Empty;
        public string Req_ReferenceNo
        {
            get { return _Req_ReferenceNo; }
            set { _Req_ReferenceNo = value; }
        }
        //Number
        private string _Number = string.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        //Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        //MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }

        }
        private string _ReferenceNo { get; set; }
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }

        private string _Reference { get; set; }
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _Fromdate { get; set; }
        public string FromDate
        {
            get
            { return _Fromdate; }
            set { _Fromdate = value; }
        }
        private string _ToDate { get; set; }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
    }
    public class TicketDetails
    {
        public string PassengerType { get; set; }
        public string PassengerCount { get; set; }
        public string TripType { get; set; }

    }


}