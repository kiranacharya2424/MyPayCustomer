using MyPay.API.Controllers;
using MyPay.API.Models.NPS;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.NPS.Partner
{
    public class NCHLLodgeReq
    {
        public CipsBatchDetail cipsBatchDetail { get; set; } = new CipsBatchDetail();
        public CipsTransactionDetail cipsTransactionDetail { get; set; } = new CipsTransactionDetail();
        public string token { get; set; } = "";

        public string generateToken() {
            string status = "success";
            using ( GenerateNCHLTokenController tokenService = new GenerateNCHLTokenController()) {
               
                String batchString = "";
                String transactionString = "";
                if (cipsBatchDetail.batchId == null)
                {
                    status = "BatchId or cipsBatchDetail missing";
                }
                if (cipsBatchDetail.debtorAgent == null)
                {
                    status = "debtorAgent or cipsBatchDetail missing";
                }
                if (cipsBatchDetail.debtorBranch == null)
                {
                    status = "debtorBranch or cipsBatchDetail missing";
                }

                if (cipsBatchDetail.debtorAccount == null)
                {
                    status = "debtorAccount or cipsBatchDetail missing";
                }
                //if (cipsBatchDetail.batchAmount ==  0.0)
                //{
                //    status = "batchAmount or cipsBatchDetail missing";
                //}
                if (cipsBatchDetail.batchCrncy == null)
                {
                    status = "batchCrncy or cipsBatchDetail missing";
                }
                batchString = cipsBatchDetail.batchId + "," + cipsBatchDetail.debtorAgent + "," + cipsBatchDetail.debtorBranch + "," + cipsBatchDetail.debtorAccount + "," + (cipsBatchDetail.batchAmount % 1 == 0 ? cipsBatchDetail.batchAmount.ToString() + ".0" : cipsBatchDetail.batchAmount.ToString()) + "," + cipsBatchDetail.batchCrncy;


                if (cipsTransactionDetail.instructionId == null)
                {
                    status = "BatchId or cipsTransactionDetail missing";
                }
                if (cipsTransactionDetail.appId == null)
                {
                    status = "appId or cipsTransactionDetail missing";
                }
                if (cipsTransactionDetail.refId == null)
                {
                    status = "refId or cipsTransactionDetail missing";
                }

                if (status != "success")
                {
                    return status;
                }

                transactionString = cipsTransactionDetail.instructionId + "," + cipsTransactionDetail.appId + "," + cipsTransactionDetail.refId;
                string tokenString = batchString + "," + transactionString + "," + (Common.ApplicationEnvironment.IsProduction ? "SMARTCARD@999" : "MYPAY@999");
                // (Common.ApplicationEnvironment.IsProduction ? "SMARTCARD@999" : "MYPAY@999");

                string token = tokenService.GenerateConnectIPSToken(tokenString);
                this.token = token;

            }

            return status;
        }
    }


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


}
