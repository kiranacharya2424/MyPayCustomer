using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_Internet:WebCommonProp
    {
        private string _OfferName = String.Empty;
        public string OfferName
        {
            get { return _OfferName; }
            set { _OfferName = value; }
        }

        private string _PlanType = String.Empty;
        public string PlanType
        {
            get { return _PlanType; }
            set { _PlanType = value; }
        }

        private string _stb = String.Empty;
        public string stb
        {
            get { return _stb; }
            set { _stb = value; }
        }

        private string _Number = String.Empty;
        public string Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        
        private string _IsVolumeBased = String.Empty;
        public string IsVolumeBased
        {
            get { return _IsVolumeBased; }
            set { _IsVolumeBased = value; }
        }

        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }

        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

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
        private string _PaymentID = String.Empty;
        public string PaymentID
        {
            get { return _PaymentID; }
            set { _PaymentID = value; }
        }

        private string _Month = String.Empty;
        public string Month
        {
            get { return _Month; }
            set { _Month = value; }
        }

        private string _Package = String.Empty;
        public string Package
        {
            get { return _Package; }
            set { _Package = value; }
        }

        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }

        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _Duration = String.Empty;
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        private string _package_id = String.Empty;
        public string package_id
        {
            get { return _package_id; }
            set { _package_id = value; }
        }

        private string _Service = String.Empty;
        public string Service
        {
            get { return _Service; }
            set { _Service = value; }
        }
        private string _RequestID = String.Empty;
        public string RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }

        private string _Address = String.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _CustomerName = String.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        private string _ContactNumber = String.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        private string _STB_OR_CAS_ID = String.Empty;
        public string STB_OR_CAS_ID
        {
            get { return _STB_OR_CAS_ID; }
            set { _STB_OR_CAS_ID = value; }
        }
        private string _Old_Ward_Number = String.Empty;
        public string Old_Ward_Number
        {
            get { return _Old_Ward_Number; }
            set { _Old_Ward_Number = value; }
        }

        private string _Mobile_Number_1 = String.Empty;
        public string Mobile_Number_1
        {
            get { return _Mobile_Number_1; }
            set { _Mobile_Number_1 = value; }
        }
    }
}