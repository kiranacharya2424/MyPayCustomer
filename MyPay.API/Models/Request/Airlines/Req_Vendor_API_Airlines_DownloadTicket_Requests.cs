using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Airlines_DownloadTicket_Requests : CommonProp
    {
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _LogID = String.Empty;
        public string LogID
        {
            get { return _LogID; }
            set { _LogID = value; }
        }
    }

    public class Req_VendorCableCarDownload : CommonProp
    {
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _LogID = String.Empty;
        public bool status;

        public string LogID
        {
            get { return _LogID; }
            set { _LogID = value; }
        }

        public int ReponseCode { get; set; }
        public string FilePath { get;  set; }
        public string Message { get; set; }

        public string TransactionId { get;  set; }  




    }

}