using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_CIT_Query_Transaction_Requests : CommonProp
    {
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _InstructionId = string.Empty;
        public string InstructionId
        {
            get { return _InstructionId; }
            set { _InstructionId = value; }
        }
        private string _BatchId = string.Empty;
        public string BatchId
        {
            get { return _BatchId; }
            set { _BatchId = value; }
        }
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        private string _RealTime = string.Empty;
        public string RealTime
        {
            get { return _RealTime; }
            set { _RealTime = value; }
        }

    }
}