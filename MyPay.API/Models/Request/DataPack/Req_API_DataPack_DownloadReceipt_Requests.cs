using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.DataPack
{
    public class Req_API_DataPack_DownloadReceipt_Requests:CommonProp
    {
        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //MemberId
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Reference
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}