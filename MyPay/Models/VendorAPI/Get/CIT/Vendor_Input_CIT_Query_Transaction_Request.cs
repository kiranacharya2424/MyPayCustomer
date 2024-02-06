using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_CIT_Query_Transaction_Request
    {

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