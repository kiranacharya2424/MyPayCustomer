using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral;

namespace MyPay.Models.VendorAPI.Get.Insurance.Prudential
{
    public class GetVendor_API_PrudentialInsurance_Detail
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
        private bool _status = false;
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

        private PrudentialBranchesDetails _detail = new PrudentialBranchesDetails();
        public PrudentialBranchesDetails detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }



    public class PrudentialBranchesDetails
    {
        private List<PrudentialBranches> _branches = new List<PrudentialBranches>();
        public List<PrudentialBranches> branches
        {

            get { return _branches; }

            set { _branches = value; }
        }
    }
    public class PrudentialBranches
    {
        // name
        private string _branch_name = string.Empty;
        public string branch_name
        {
            get { return _branch_name; }
            set { _branch_name = value; }
        }
        private string _id = string.Empty;
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}
