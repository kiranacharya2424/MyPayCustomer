using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.Events
{
    public class GetVendor_API_Events_Commit : CommonGet
    {

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
         // status
        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        // Message
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }

        private EventsCommit _data = new EventsCommit();
        public EventsCommit data
        {
            get { return _data; }
            set { _data = value; }
        }
        private List<string> _errors = new List<string>();
        public List<string> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        public string TransactionUniqueId { get; set; }


    }
 
    public class EventsCommit
    {
       private string _merchantCode = String.Empty;
        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        }

       
        private string _orderId = String.Empty;
        public string orderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
        private string _transactionId = String.Empty;
        public string transactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }
        private string _remarks = String.Empty;
        public string remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }


        /*
         , , , , PayableAmt
         */

        private int _NoOfVotes;

        public int NoOfVotes
        {
            get { return _NoOfVotes; }
            set { _NoOfVotes = value; }
        }

        private string _ContestantName;

        public string ContestantName
        {
            get { return _ContestantName; }
            set { _ContestantName = value; }
        }


        private string _SubContestName;

        public string SubContestName
        {
            get { return _SubContestName; }
            set { _SubContestName = value; }
        }

        private string _ContestName;

        public string ContestName
        {
            get { return _ContestName; }
            set { _ContestName = value; }
        }

        private decimal _PayableAmt;
        public decimal PayableAmt
        {
            get { return _PayableAmt; }
            set { _PayableAmt = value; }
        }



    }
}