using MyPay.Models.Response.WebResponse.Common;
using MyPay.Models.VendorAPI.Get.Insurance.Sanima;
using MyPay.Models.VendorAPI.Get.Insurance.Shikhar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_Insurance: WebCommonResponse
    {
        // _NecoInsurance_categories 
        private List<string> _PolicyCategories;
        public List<string> PolicyCategories
        {
            get { return _PolicyCategories; }
            set { _PolicyCategories = value; }
        }
        private List<string> _Branches;
        public List<string> Branches
        {
            get { return _Branches; }
            set { _Branches = value; }
        }

        public string ContactNo { get; set; }
        public string DebitNoteNo { get; set; }
        public int SessionId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PayableAmount { get; set; }

        public string Paymode { get; set; }
        public string TransactionId { get; set; }
        public string ProductName { get; set; }
        public string CustomerId { get; set; }
        public string NextDueDate { get; set; }
        public string CurrentDueDate { get; set; }
        
        public string InvoiceNumber { get; set; }
        public string Amount { get; set; }
        public string CustomerName { get; set; }
        public string FineAmount { get; set; }
        public string TotalAmount { get; set; }
        public string InstallmentNo { get; set; }

        public string PolicyNo { get; set; }
        public string PremiumAmount { get; set; }
        public string TotalFine { get; set; }
        public string RebateAmount { get; set; }
        public string PaymentDate { get; set; }
        public string DueDate { get; set; }
        public string PolicyStatus { get; set; }
        public string Term { get; set; }
        public string MaturityDate { get; set; }
        public string Token { get; set; }
        public string UniqueIdGuid { get; set; }

        public string PlanCode { get; set; }
        public string AdjustmentAmount { get; set; }

        public SanimaLifeDetail detail { get; set; }

        public ShikharDetail shikhardetail { get; set; }

        public string ProformaNo { get; set; }
        public string TpPremium { get; set; }
        public string SumInsured { get; set; }
        public string Insured { get; set; }
        public string ClassName { get; set; }
    }
}