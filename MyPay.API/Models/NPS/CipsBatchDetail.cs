using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS
{
    public class CipsBatchDetail
    {
        public int id { get; set; }
        public string batchId { get; set; }
        public DateTime recDate { get; set; }
        public string channelId { get; set; }
        public object ipsBatchId { get; set; }
        public object fileName { get; set; }
        public double batchAmount { get; set; }
        public int batchCount { get; set; }
        public double batchChargeAmount { get; set; }
        public string batchCrncy { get; set; }
        public string categoryPurpose { get; set; }
        public string debtorAgent { get; set; }
        public string debtorBranch { get; set; }
        public string debtorName { get; set; }
        public string debtorAccount { get; set; }
        public string debtorIdType { get; set; }
        public string debtorIdValue { get; set; }
        public string debtorAddress { get; set; }
        public string debtorPhone { get; set; }
        public string debtorMobile { get; set; }
        public string debtorEmail { get; set; }
        public string rcreUserId { get; set; }
        public DateTime rcreTime { get; set; }
        public string debitStatus { get; set; }
        public object debitReasonCode { get; set; }
        public int isoTxnId { get; set; }
        public object sessionSrlNo { get; set; }
        public object settlementDate { get; set; }
        public int corporateId { get; set; }
        public object initBranchId { get; set; }
        public object debitReasonDesc { get; set; }
        public object txnResponse { get; set; }
    }
}
