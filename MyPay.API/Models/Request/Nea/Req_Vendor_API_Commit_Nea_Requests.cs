using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Commit_Nea_Requests : CommonProp
    {


        // session_id
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        // amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        // reference
        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        // PhoneNumber
       /* private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        // Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }*/
    }
}