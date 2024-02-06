using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_Insurance:WebCommonProp
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

        // policy_type <Fresh OR Renew>
        private string _policy_type = string.Empty;
        public string policy_type
        {
            get { return _policy_type; }
            set { _policy_type = value; }
        }

        //customer_name
        private string _customer_name = string.Empty;
        public string customer_name
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        //policy_category <Any one value from categories list>
        private string _policy_category = string.Empty;
        public string policy_category
        {
            get { return _policy_category; }
            set { _policy_category = value; }
        }

        //amount
        private string _amount = string.Empty;
        public string amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        //policy_number <Optional Field | 12 >
        private string _policy_number = string.Empty;
        public string policy_number
        {
            get { return _policy_number; }
            set { _policy_number = value; }
        }

        //mobile_number
        private string _mobile_number = string.Empty;
        public string mobile_number
        {
            get { return _mobile_number; }
            set { _mobile_number = value; }
        }

        //service_name
        private string _service_name = string.Empty;
        public string service_name
        {
            get { return _service_name; }
            set { _service_name = value; }
        }

        //insurance_slug
        private string _insurance_slug = string.Empty;
        public string insurance_slug
        {
            get { return _insurance_slug; }
            set { _insurance_slug = value; }
        }

        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }

        //SessionId
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //InsuranceSlug
        private string _InsuranceSlug = string.Empty;
        public string InsuranceSlug
        {
            get { return _InsuranceSlug; }
            set { _InsuranceSlug = value; }
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
        private string _PolicyNo = string.Empty;
        public string PolicyNo
        {
            get { return _PolicyNo; }
            set { _PolicyNo = value; }
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

        // PolicyName
        private string _PolicyName = string.Empty;
        public string PolicyName
        {
            get { return _PolicyName; }
            set { _PolicyName = value; }
        }

        //RequestId
        private string _RequestId = String.Empty;
        public string RequestId
        {
            get { return _RequestId; }
            set { _RequestId = value; }
        }
    }
}