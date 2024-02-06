using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_SUBISU_Requests : CommonProp
    {
        private string _CustomerID = String.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        private string _SessionID = String.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }
        private string _OfferName = String.Empty;
        public string OfferName
        {
            get { return _OfferName; }
            set { _OfferName = value; }
        }
        private string _Number = String.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }


        private string _stb = String.Empty;
        public string stb
        {
            get { return _stb; }
            set { _stb = value; }
        }
        private string _PlanType = String.Empty;
        public string PlanType
        {
            get { return _PlanType; }
            set { _PlanType = value; }
        }
    }
}