using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Feedback : CommonProp
    {
        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //Subject
        private string _Subject = string.Empty;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        //UserMessage
        private string _UserMessage = string.Empty;
        public string UserMessage
        {
            get { return _UserMessage; }
            set { _UserMessage = value; }
        }
        //MemberId
        private string _MemberId = "0";
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //FeedbackRating
        private string _FeedbackRating = String.Empty;
        public string FeedbackRating
        {
            get { return _FeedbackRating; }
            set { _FeedbackRating = value; }
        }
    }
}