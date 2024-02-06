using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPay.Models.VendorAPI.Get.Insurance.Prudential;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_PrudentialInsurance_Detail_Requests : CommonResponse
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

        private PrudentialBranchesDetails _detail = new PrudentialBranchesDetails();
        public PrudentialBranchesDetails detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

        private PrudentialInsuranceErrorResponse _ErrorResponse = new PrudentialInsuranceErrorResponse();
        public PrudentialInsuranceErrorResponse ErrorResponse
        {
            get { return _ErrorResponse; }
            set { _ErrorResponse = value; }
        }
    }
    public class PrudentialInsuranceErrorResponse
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
