using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.NPS.Response.Partner
{

    //public class CipsBatchDetail
    //{
    //    public int id { get; set; }
    //    public string batchId { get; set; }
    //    public DateTime recDate { get; set; }
    //    public string channelId { get; set; }
    //    public object ipsBatchId { get; set; }
    //    public object fileName { get; set; }
    //    public double batchAmount { get; set; }
    //    public int batchCount { get; set; }
    //    public double batchChargeAmount { get; set; }
    //    public string batchCrncy { get; set; }
    //    public string categoryPurpose { get; set; }
    //    public string debtorAgent { get; set; }
    //    public string debtorBranch { get; set; }
    //    public string debtorName { get; set; }
    //    public string debtorAccount { get; set; }
    //    public string debtorIdType { get; set; }
    //    public string debtorIdValue { get; set; }
    //    public string debtorAddress { get; set; }
    //    public string debtorPhone { get; set; }
    //    public string debtorMobile { get; set; }
    //    public string debtorEmail { get; set; }
    //    public string rcreUserId { get; set; }
    //    public DateTime rcreTime { get; set; }
    //    public string debitStatus { get; set; }
    //    public object debitReasonCode { get; set; }
    //    public int isoTxnId { get; set; }
    //    public object sessionSrlNo { get; set; }
    //    public object settlementDate { get; set; }
    //    public int corporateId { get; set; }
    //    public object initBranchId { get; set; }
    //    public object debitReasonDesc { get; set; }
    //    public object txnResponse { get; set; }
    //}

    //public class CipsTransactionDetail
    //{
    //    public int id { get; set; }
    //    public DateTime recDate { get; set; }
    //    public string instructionId { get; set; }
    //    public string endToEndId { get; set; }
    //    public double amount { get; set; }
    //    public double chargeAmount { get; set; }
    //    public string chargeLiability { get; set; }
    //    public object purpose { get; set; }
    //    public string creditorAgent { get; set; }
    //    public string creditorBranch { get; set; }
    //    public string creditorName { get; set; }
    //    public string creditorAccount { get; set; }
    //    public object creditorIdType { get; set; }
    //    public object creditorIdValue { get; set; }
    //    public object creditorAddress { get; set; }
    //    public object creditorPhone { get; set; }
    //    public object creditorMobile { get; set; }
    //    public object creditorEmail { get; set; }
    //    public object addenda1 { get; set; }
    //    public object addenda2 { get; set; }
    //    public object addenda3 { get; set; }
    //    public object addenda4 { get; set; }
    //    public object freeCode1 { get; set; }
    //    public object freeCode2 { get; set; }
    //    public object freeText1 { get; set; }
    //    public object freeText2 { get; set; }
    //    public object freeText3 { get; set; }
    //    public object freeText4 { get; set; }
    //    public string rcreUserId { get; set; }
    //    public DateTime rcreTime { get; set; }
    //    public object ipsBatchId { get; set; }
    //    public object creditStatus { get; set; }
    //    public object reasonCode { get; set; }
    //    public string refId { get; set; }
    //    public string remarks { get; set; }
    //    public string particulars { get; set; }
    //    public int merchantId { get; set; }
    //    public string appId { get; set; }
    //    public string appTxnId { get; set; }
    //    public object beneficiaryId { get; set; }
    //    public object beneficiaryName { get; set; }
    //    public object reversalStatus { get; set; }
    //    public int mode { get; set; }
    //    public int isoTxnId { get; set; }
    //    public int batchId { get; set; }
    //    public string orignBranchId { get; set; }
    //    public object reasonDesc { get; set; }
    //    public object txnResponse { get; set; }
    //    public object freeText5 { get; set; }
    //    public object freeText6 { get; set; }
    //    public object freeText7 { get; set; }
    //    public object billPaymentPostRetryCount { get; set; }
    //    public bool replaceByOldAppGroup { get; set; }
    //}

    public class ResponseResult
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public object billsPaymentDescription { get; set; }
        public object billsPaymentResponseCode { get; set; }
        public List<FieldErrors> fieldErrors { get; set; }
    }

    public class NCHLPartnerResp

    {
        public ResponseResult responseResult { get; set; }
        public CipsBatchDetail cipsBatchDetail { get; set; }
        public CipsTransactionDetail cipsTransactionDetail { get; set; }
        public string token { get; set; }

        public bool status { get; set; }

        public string Message { get; set; }
    }

    public class FieldErrors

    {
        public string field { get; set; }

        public string message { get; set; }
    }
}
