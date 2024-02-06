using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_ServiceGroup_Nea_Details_Requests : CommonProp
    {
        // SC_No
        private string _Consumer_SC_No = string.Empty;
        public string Consumer_SC_No
        {
            get { return _Consumer_SC_No; }
            set { _Consumer_SC_No = value; }
        }
        // ConsumerId
        private string _ConsumerId = string.Empty;
        public string ConsumerId
        {
            get { return _ConsumerId; }
            set { _ConsumerId = value; }
        }
        // ReferenceNo
        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        // OfficeCode
        private string _OfficeCode = string.Empty;
        public string OfficeCode
        {
            get { return _OfficeCode; }
            set { _OfficeCode = value; }
        }

        // PhoneNumber
        private string _PhoneNumber;
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
        }
    }
}