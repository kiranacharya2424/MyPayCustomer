using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral
{
    public class GetVendor_API_IMEGeneralInsurance_Detail
    {
        // error_code
        private string _error_code = string.Empty;
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        // error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        // details
        private string _details = string.Empty;
        public string details
        {
            get { return _details; }
            set { _details = value; }
        }
        // Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        // status
        private bool _status = true;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string policy_no { get; set; }
        public string name { get; set; }
        public string premium_amount { get; set; }
        public string fine_amount { get; set; }
        public string total_fine { get; set; }
        public string rebate_amount { get; set; }
        public string amount { get; set; }
        public string payment_date { get; set; }
        public string due_date { get; set; }
        public string next_due_date { get; set; }
        public string policy_status { get; set; }
        public string term { get; set; }
        public string maturity_date { get; set; }
        public string token { get; set; }
        public string unique_id_guid { get; set; }
        public string session_id { get; set; }

        private List<PolicyType> _policy_type = new List<PolicyType>();
        public List<PolicyType> policy_type
        {

            get { return _policy_type; }

            set { _policy_type = value; }
        }

        private List<InsuranceType> _insurance_type = new List<InsuranceType>();
        public List<InsuranceType> insurance_type
        {

            get { return _insurance_type; }

            set { _insurance_type = value; }
        }

        private List<Branches> _branches = new List<Branches>();
        public List<Branches> branches
        {

            get { return _branches; }

            set { _branches = value; }
        }
    }
    public class PolicyType
    {
        // name
        private string _label = string.Empty;
        public string label
        {
            get { return _label; }
            set { _label = value; }
        }
        private string _value = string.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

       
    }
    public class InsuranceType
    {
        // name
        private string _label = string.Empty;
        public string label
        {
            get { return _label; }
            set { _label = value; }
        }
        private string _value = string.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
    public class Branches
    {
        // name
        private string _label = string.Empty;
        public string label
        {
            get { return _label; }
            set { _label = value; }
        }
        private string _value = string.Empty;
        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}
