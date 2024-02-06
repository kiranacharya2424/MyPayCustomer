using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_JAGRITICABLETV_Requests : CommonProp
    {
        private string _CustomerId = String.Empty;
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }
        private string _CustomerName = String.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private string _Package = String.Empty;
        public string Package
        {
            get { return _Package; }
            set { _Package = value; }
        }
        private string _STB_OR_CAS_ID = String.Empty;
        public string  STB_OR_CAS_ID
        {
            get { return _STB_OR_CAS_ID; }
            set { _STB_OR_CAS_ID = value; }
        }
        private string _Old_Ward_Number = String.Empty;
        public string  Old_Ward_Number
        {
            get { return _Old_Ward_Number; }
            set { _Old_Ward_Number = value; }
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
        private string _Mobile_Number_1 = String.Empty;
        public string Mobile_Number_1
        {
            get { return _Mobile_Number_1; }
            set { _Mobile_Number_1 = value; }
        }
        private string _PackageType = String.Empty;
        public string PackageType
        {
            get { return _PackageType; }
            set { _PackageType = value; }
        }
    }
}