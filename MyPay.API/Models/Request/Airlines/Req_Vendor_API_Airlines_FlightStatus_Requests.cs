using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Airlines_FlightStatus_Requests : CommonProp
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
}