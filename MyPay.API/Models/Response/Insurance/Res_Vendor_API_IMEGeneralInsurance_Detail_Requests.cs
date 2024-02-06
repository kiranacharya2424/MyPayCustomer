using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.VendorAPI.Get.Insurance.IMEGeneral;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_IMEGeneralInsurance_Detail_Requests : CommonResponse
    {
        public string PolicyNo { get; set; }
        public string Name { get; set; }
        public string PremiumAmount { get; set; }
        public string FineAmount { get; set; }
        public string TotalFine { get; set; }
        public string RebateAmount { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }
        public string DueDate { get; set; }
        public string NextDueDate { get; set; }
        public string PolicyStatus { get; set; }
        public string Term { get; set; }
        public string MaturityDate { get; set; }
        public string Address { get; set; }
        public string PayMode { get; set; }
        public string TransactionId { get; set; }
        public string ProductName { get; set; }
        public string CustomerId { get; set; }
        public string CurrentDueDate { get; set; }

        public string Token { get; set; }
        public string UniqueIdGuid { get; set; }
        public string SessionId { get; set; }

        private IMEGeneralInsuranceErrorResponse _ErrorResponse = new IMEGeneralInsuranceErrorResponse();
        public IMEGeneralInsuranceErrorResponse ErrorResponse
        {
            get { return _ErrorResponse; }
            set { _ErrorResponse = value; }
        }

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
    public class IMEGeneralInsuranceErrorResponse
    {
        public bool Status { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
        public string NextDueDate { get; set; }
        public string State { get; set; }
    }
}
