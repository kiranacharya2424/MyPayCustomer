using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Ride
{
    public class Req_Vendor_API_Commit_WorldLink_Requests : CommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private string _SessionID = String.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }

        private string _PackageID = String.Empty;
        public string PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }

        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
    }
}