using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_Commit_Insurance_Shikhar_Requests:CommonProp
    {
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        // CustomerCode
        private string _CustomerCode = string.Empty;
        public string CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        // CustomerName
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        // Address
        private string _Address = string.Empty;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        // ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        // Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        // PolicyType
        private string _PolicyType = string.Empty;
        public string PolicyType
        {
            get { return _PolicyType; }
            set { _PolicyType = value; }
        }

        // PolicyNumber
        private string _PolicyNumber = string.Empty;
        public string PolicyNumber
        {
            get { return _PolicyNumber; }
            set { _PolicyNumber = value; }
        }

        // Branch
        private string _Branch = string.Empty;
        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        // PolicyDescription
        private string _PolicyDescription = string.Empty;
        public string PolicyDescription
        {
            get { return _PolicyDescription; }
            set { _PolicyDescription = value; }
        }

        // Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }

        // PolicyName
        private string _PolicyName = string.Empty;
        public string PolicyName
        {
            get { return _PolicyName; }
            set { _PolicyName = value; }
        }

        // Amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
    }
}