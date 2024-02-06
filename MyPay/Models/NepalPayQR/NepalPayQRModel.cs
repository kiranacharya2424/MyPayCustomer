using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyPay.Models.NepalPayQR
{
    public class NepalPayQRModel
    {
        public string DataRecieved { get; set; }
        public CommonDBResonse AddNepalPayQRDetail(ValidateResponse model, string memId, string merchantPostalcode, string qrString, string payerEmailAddress, string debtorAgentBranch, string createdby, string flag)
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = AddSetObject(model, memId, merchantPostalcode, qrString, payerEmailAddress, debtorAgentBranch, createdby, flag);
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_NepalPayQR_Detail", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable AddSetObject(ValidateResponse model, string memId, string merchantPostalcode, string qrString, string payerEmailAddress, string debtorAgentBranch, string createdby, string flag)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();

            HT.Add("flag", "i");
            HT.Add("instructionId", model.instructionId);
            HT.Add("validationTraceId", model.validationTraceId);
            HT.Add("Amount", model.amount);
            HT.Add("interchangeFee", model.interchangeFee);
            HT.Add("transactionFee", model.transactionFee);
            HT.Add("merchantName", model.merchantName);
            HT.Add("merchantCity", model.merchantCity);
            HT.Add("merchantCountryCode", model.merchantCountryCode);
            HT.Add("merchantBillNo", model.merchantBillNo);
            HT.Add("merchantTxnRef", model.merchantTxnRef);
            HT.Add("merchantPostalcode", merchantPostalcode);
            HT.Add("merchantPan", model.merchantPan);
            HT.Add("qrType", model.qrType);
            HT.Add("qrString", qrString);
            HT.Add("merchantCategoryCode", model.merchantCategoryCode);
            HT.Add("payerName", model.payerName);
            HT.Add("payerPanId", model.payerPanId);
            HT.Add("payerMobileNumber", model.payerMobileNumber);
            HT.Add("payerEmailAddress", payerEmailAddress);
            HT.Add("debtorAccount", model.debtorAccount);
            HT.Add("debtorAgent", model.debtorAgent);
            HT.Add("debtorAgentBranch", debtorAgentBranch);
            HT.Add("encKeySerial", model.encKeySerial);
            HT.Add("token", model.token);
            HT.Add("MemberId", memId);
            HT.Add("createdby", createdby);
            HT.Add("issuerid", model.issuerId);
            HT.Add("narration", model.narration);
            HT.Add("instrument", model.instrument);
            HT.Add("acquirerid", model.acquirerId);

            return HT;
        }

        public CommonDBResonse AddNepalPayQRRefund(RefundResponseFromIssuerToNPI model, string memId, string createdby, string OrginalWalletTransactionId)
        {
            CommonDBResonse ResultId = new CommonDBResonse();
            try
            {
                DataRecieved = "false";
                CommonHelpers obj = new CommonHelpers();
                System.Collections.Hashtable HT = AddSetObjectRefund(model, memId, createdby, OrginalWalletTransactionId);
                ResultId = obj.ExecuteProcedureGetValueBusSewa("sp_NepalPayQR_Detail", HT);
            }
            catch (Exception ex)
            {
                // DataRecieved = "false";
            }
            return ResultId;
        }
        public System.Collections.Hashtable AddSetObjectRefund(RefundResponseFromIssuerToNPI model, string memId, string createdby, string OrginalWalletTransactionId)
        {
            System.Collections.Hashtable HT = new System.Collections.Hashtable();
            HT.Add("flag", "ref_i");
            HT.Add("MemberId", memId);
            HT.Add("OrginalTxnId", model.orgnNQrTxnId);
            HT.Add("RefundedTxnId", model.refundNQrTxnId);
            HT.Add("InstructionId", model.instructionId);
            HT.Add("Amount", model.amount);
            HT.Add("RefundType", model.refundType);
            HT.Add("MyPayOrginalTxnId", OrginalWalletTransactionId);
            HT.Add("IssuerId", model.issuerId);
            HT.Add("TransactionFee", model.transactionFee);
            HT.Add("RefundReasonMessage", model.refundReasonMessage);
            HT.Add("PayerPanId", model.payerPanId);
            HT.Add("PayerMobileNumber", model.payerMobileNumber);
            HT.Add("createdby", createdby);
            return HT;
        }


    }
    public class NepalQRAuthjsonrequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }

    }
    public class jsonrequest_IssuerToNPI
    {
        public string instructionId { get; set; }
        public string qrString { get; set; }

    }
    public class GetDataFromNepalQRPay
    {
        public string message { get; set; }
        public string Id { get; set; }
        public bool IsException { get; set; }
        public string TransactionId { get; set; }
        public string MerchantOrderTxnId { get; set; }

    }

    public class NepalQRAuth
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string customerdetails { get; set; }
    }

    public class jsonrequest_IssuerToNPI_refund
    {
        public string orgnNQrTxnId { get; set; }
        public string issuerId { get; set; }
        public string refundType { get; set; }
        public decimal amount { get; set; }
        public string refundReasonCode { get; set; }
        public string refundReasonMessage { get; set; }
        public string instructionId { get; set; }
        public string token { get; set; }
        public string transactionFee { get; set; }
        public string refundCancellationFlg { get; set; }
        public string refundNQrTxnId { get; set; }
        public string payerPanId { get; set; }
        public string payerMobileNumber { get; set; }

    }

    public class ValidateResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string validationTraceId { get; set; }
        public string instructionId { get; set; }
        public string qrType { get; set; }
        public string acquirerId { get; set; }
        public string acquirerCountryCode { get; set; }
        public string merchantPan { get; set; }
        public int merchantCategoryCode { get; set; }
        public decimal amount { get; set; }
        public decimal transactionFee { get; set; }
        public string currencyCode { get; set; }
        public string merchantName { get; set; }
        public string merchantCity { get; set; }
        public string merchantCountryCode { get; set; }
        public string merchantBillNo { get; set; }
        public string merchantTxnRef { get; set; }
        public string merchantPostalcode { get; set; }
        public string terminal { get; set; }
        public string qrString { get; set; }
        public string encKeySerial { get; set; }
        public string network { get; set; }
        public string token { get; set; }
        public string tokenString { get; set; }
        public decimal interchangeFee { get; set; }
        public string issuerId { get; set; }
        public string issuerName { get; set; }
        public string debtorAgent { get; set; }
        public string debtorAccount { get; set; }
        public string payerEmailAddress { get; set; }
        public string payerMobileNumber { get; set; }
        public string payerPanId { get; set; }
        public string payerName { get; set; }
        public string debtorAgentBranch { get; set; }
        public string localTransactionDateTime { get; set; }
        public string narration { get; set; }
        public string instrument { get; set; }

    }


    public class ValidateResponse1
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string validationTraceId { get; set; }
        public string instructionId { get; set; }
        public string qrType { get; set; }
        public string acquirerId { get; set; }
        public string acquirerCountryCode { get; set; }
        public string merchantPan { get; set; }
        public int merchantCategoryCode { get; set; }
        public string amount { get; set; }
        public string transactionFee { get; set; }
        public string currencyCode { get; set; }
        public string merchantName { get; set; }
        public string merchantCity { get; set; }
        public string merchantCountryCode { get; set; }
        public string merchantBillNo { get; set; }
        public string merchantTxnRef { get; set; }
        public string merchantPostalcode { get; set; }
        public string terminal { get; set; }
        public string qrString { get; set; }
        public string encKeySerial { get; set; }
        public string network { get; set; }
        public string token { get; set; }
        public string tokenString { get; set; }
        public string interchangeFee { get; set; }
        public string issuerId { get; set; }
        public string issuerName { get; set; }
        public string debtorAgent { get; set; }
        public string debtorAccount { get; set; }
        public string payerEmailAddress { get; set; }
        public string payerMobileNumber { get; set; }
        public string payerPanId { get; set; }
        public string payerName { get; set; }
        public string debtorAgentBranch { get; set; }
        public string localTransactionDateTime { get; set; }
        public string narration { get; set; }
        public string instrument { get; set; }

    }
    public class NPITOAcquierJsonRequest
    {
        public string validationTraceId { get; set; }
        public string instructionId { get; set; }

        public string acquirerId { get; set; }
        public string acquirerCountryCode { get; set; }
        public string merchantPan { get; set; }
        public int merchantCategoryCode { get; set; }
        public string qrType { get; set; }
        public decimal amount { get; set; }
        public decimal transactionFee { get; set; }
        public decimal interchangeFee { get; set; }
        public string network { get; set; }
        public string currencyCode { get; set; }
        public string merchantBillNo { get; set; }
        public string merchantTxnRef { get; set; }
        public string merchantCountryCode { get; set; }
        public string merchantCity { get; set; }
        public string merchantName { get; set; }
        public string terminal { get; set; }
        public string encKeySerial { get; set; }
        public string tokenString { get; set; }
        public string token { get; set; }
        public string issuerId { get; set; }
        public string payerName { get; set; }
        public string payerPanId { get; set; }
        public string payerMobileNumber { get; set; }
        public string debtorAccount { get; set; }
        public string debtorAgent { get; set; }
        public string debtorAgentBranch { get; set; }
        public string localTransactionDateTime { get; set; }
        public string narration { get; set; }
        public string instrument { get; set; }
    }

    public class ValidateResponseError
    {
        public string error { get; set; }
        public string error_description { get; set; }

    }
    public class PaymentResponse
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public string responseMessage { get; set; }
        public string nQrTxnId { get; set; }
        public string instructionId { get; set; }
        public string validationTraceId { get; set; }
        public string acquirerId { get; set; }
        public string merchantPan { get; set; }
        public string qrType { get; set; }
        public decimal amount { get; set; }
        public decimal interchangeFee { get; set; }
        public decimal transactionFee { get; set; }
        public string currencyCode { get; set; }
        public string merchantBillNo { get; set; }
        public string payerName { get; set; }
        public string payerPanId { get; set; }
        public string payerMobileNumber { get; set; }
        public object payerEmailAddress { get; set; }
        public string issuerId { get; set; }
        public string debtorAccount { get; set; }
        public string debtorAgent { get; set; }
        public string debtorAgentBranch { get; set; }
        public string narration { get; set; }
        public string acquirerCountryCode { get; set; }
        public string merchantName { get; set; }
        public string merchantCity { get; set; }
        public string merchantCountryCode { get; set; }
        public string localTransactionDateTime { get; set; }
        public int merchantCategoryCode { get; set; }
        public string merchantTxnRef { get; set; }
        public string instrument { get; set; }
        public string terminal { get; set; }
        public string encKeySerial { get; set; }
        public string token { get; set; }
        public string merchantPostalCode { get; set; }
        public string addnField1 { get; set; }
        public string addnField2 { get; set; }
        public string addnField10 { get; set; }
        public string sessionSrlNo { get; set; }
        public string creditStatus { get; set; }
        public string debitStatus { get; set; }
        public string debitDescription { get; set; }
        public string rcreTime { get; set; }

    }

    public class RefundResponseFromIssuerToNPI
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string orgnNQrTxnId { get; set; }
        public string issuerId { get; set; }
        public string refundType { get; set; }
        public decimal amount { get; set; }
        public string transactionFee { get; set; }
        public string refundReasonCode { get; set; }
        public string refundReasonMessage { get; set; }
        public string instructionId { get; set; }
        public string refundCancellationFlg { get; set; }
        public string refundNQrTxnId { get; set; }
        public string payerPanId { get; set; }
        public string payerMobileNumber { get; set; }
        public string Token { get; set; }
    }


    public class NepalQRCheckStatusJson
    {
        //public string validationtraceid { get; set; }
        public string issuerId { get; set; }
        public string merchantId { get; set; }
        //public string nQrTxnId { get; set; }
        public string instructionId { get; set; }

    }
    public class NepalQRCheckStatusResponse
    {
        public string timestamp { get; set; }
        public string responseCode { get; set; }
        public string responseStatus { get; set; }
        public object responseMessage { get; set; }
        public string responseDescription { get; set; }
        public responsecheckstatus[] responseBody { get; set; }
    }


    //public class responsecheckstatus
    //{
    //    public string nQrTxnId { get; set; }
    //    public string tranType { get; set; }
    //    public string instructionId { get; set; }
    //    public string issuerId { get; set; }
    //    public string acquirerId { get; set; }
    //    public double amount { get; set; }
    //    public double transactionFee { get; set; }
    //    public double interchangeFee { get; set; }
    //    public string merchantName { get; set; }
    //    public string merchantBillNo { get; set; }
    //    public string merchantTxnRef { get; set; }
    //    public string payerName { get; set; }
    //    public string payerMobileNumber { get; set; }
    //    public string debitStatus { get; set; }
    //    public string creditStatus { get; set; }
    //    public string sessionSrlNo { get; set; }
    //    public string recDate { get; set; }
    //    public string terminal { get; set; }
    //    public string network { get; set; }
    //    public string issuerNetwork { get; set; }
    //}

    public class responsecheckstatus
    {
        public string sessionSrlNo { get; set; }
        public string recDate { get; set; }
        public string instructionId { get; set; }
        public string nQrTxnId { get; set; }
        public string acquirerId { get; set; }
        public string issuerId { get; set; }
        public string network { get; set; }
        public string issuerNetwork { get; set; }
        public float amount { get; set; }
        public float interchangeFee { get; set; }
        public float transactionFee { get; set; }
        public string debitStatus { get; set; }
        public string creditStatus { get; set; }
        public string payerName { get; set; }
        public string tranType { get; set; }
        public string payerMobileNumber { get; set; }
        public string merchantName { get; set; }
        public string merchantTxnRef { get; set; }
        public object terminal { get; set; }
        public string merchantBillNo { get; set; }
        public object instrument { get; set; }
        public object validationTraceId { get; set; }
        public object merchantPan { get; set; }
        public object nfcTxnId { get; set; }
    }

  

}