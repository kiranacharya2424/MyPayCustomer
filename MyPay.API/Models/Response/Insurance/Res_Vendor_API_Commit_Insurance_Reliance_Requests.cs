using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_Commit_Insurance_Reliance_Requests: CommonResponse
    {
        // Id
        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }

        private RelianceInsuranceDetailResponse _InsuranceDetails = new RelianceInsuranceDetailResponse();
        public RelianceInsuranceDetailResponse InsuranceDetails
        {
            get { return _InsuranceDetails; }
            set { _InsuranceDetails = value; }
        }
    }

    public class RelianceInsuranceExtraDataResponse
    {
    }

    public class RelianceInsuranceDetailResponse
    {
        public string TransactionId { get; set; }
        public string ProductName { get; set; }
        public string CustomerId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string NextDueDate { get; set; }
        public string FineAmount { get; set; }
    }
}