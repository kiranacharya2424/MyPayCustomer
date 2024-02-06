using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS
{
    public class CipsTransactionDetail
    {
        public int id { get; set; }
        public DateTime recDate { get; set; }
        public string instructionId { get; set; }
        public string endToEndId { get; set; }
        public double amount { get; set; }
        public double chargeAmount { get; set; }
        public string chargeLiability { get; set; }
        public string purpose { get; set; }
        public string creditorAgent { get; set; }
        public string creditorBranch { get; set; }
        public string creditorName { get; set; }
        public string creditorAccount { get; set; }
        public string creditorIdType { get; set; }
        public string creditorIdValue { get; set; }
        public string creditorAddress { get; set; }
        public string creditorPhone { get; set; }
        public string creditorMobile { get; set; }
        public string creditorEmail { get; set; }
        public string addenda1 { get; set; }
        public string addenda2 { get; set; }
        public string addenda3 { get; set; }
        public string addenda4 { get; set; }
        public string freeCode1 { get; set; }
        public string freeCode2 { get; set; }
        public string freeText1 { get; set; }
        public string freeText2 { get; set; }
        public string freeText3 { get; set; }
        public string freeText4 { get; set; }
        public string rcreUserId { get; set; }
        public DateTime rcreTime { get; set; }
        public string ipsBatchId { get; set; }
        public string creditStatus { get; set; }
        public string reasonCode { get; set; }
        public string refId { get; set; }
        public string remarks { get; set; }
        public string particulars { get; set; }
        public int merchantId { get; set; }
        public string appId { get; set; }
        public string appTxnId { get; set; }
        public string beneficiaryId { get; set; }
        public string beneficiaryName { get; set; }
        public string reversalStatus { get; set; }
        public int mode { get; set; }
        public int isoTxnId { get; set; }
        public int batchId { get; set; }
        public string orignBranchId { get; set; }
        public string reasonDesc { get; set; }
        public string txnResponse { get; set; }
        public string freeText5 { get; set; }
        public string freeText6 { get; set; }
        public string freeText7 { get; set; }
        public string billPaymentPostRetryCount { get; set; }
        public bool replaceByOldAppGroup { get; set; }
    }
}
