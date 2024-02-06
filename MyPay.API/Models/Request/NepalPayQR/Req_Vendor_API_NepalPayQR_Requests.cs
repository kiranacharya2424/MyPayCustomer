using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.NepalPayQR
{
    public class Req_Vendor_API_NepalPayQR_Requests : CommonProp
    {
        //public string Reference { get; set; }
        //public string instructionId { get; set; }
        public string qrString { get; set; }


    }


    public class NPITOpaymentRequest : CommonProp
    {
        public string Reference { get; set; }
        public string qrString { get; set; }
        public string amount { get; set; }
        public string qrtype { get; set; }
        public string merchantName { get; set; }

    }
        public class NPITOAcquierRequest : CommonProp
    {
        public string Reference { get; set; }
        public string validationTraceId { get; set; }
        public string instructionId { get; set; }
        public string acquirerId { get; set; }
        public string acquirerCountryCode { get; set; }
        public string merchantPan { get; set; }
        public int merchantCategoryCode { get; set; }
        public string qrType { get; set; }
        public string amount { get; set; }
        public string transactionFee { get; set; }
        public string interchangeFee { get; set; }
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
        public string issuerId { get; set; }
        public string issuerName { get; set; }
        public string debtorAgent { get; set; }
        public string debtorAccount { get; set; }
        public string payerEmailAddress { get; set; }
        public string payerMobileNumber { get; set; }
        public string payerPanId { get; set; }
        public string payerName { get; set; }
        public string debtorAgentBranch { get; set; }
        public string narration { get; set; }
        public string qrString { get; set; }
        public string merchantPostalcode { get; set; }
        public string token { get; set; }
        public string localTransactionDateTime { get; set; }
        public string instrument { get; set; }

    }
    public class NepalQRAuthResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string customerdetails { get; set; }
    }

    //public class NepalQRAuthjsonrequest
    //{
    //    public string username { get; set; }
    //    public string password { get; set; }
    //    public string grant_type { get; set; }

    //}

    public class NepalQRRefundRequest 
    {
        public string Reference { get; set; }
        public string orgnNQrTxnId { get; set; }
        public string issuerId { get; set; }
        public string refundType { get; set; }
        public string amount { get; set; }
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

    public class NepalQRCheckStatusRequest:CommonProp
    {
        public string validationtraceid { get; set; }
        public string acquirerid { get; set; }
        public string merchantid { get; set; }
        public string nQrTxnId { get; set; }
        public string instructionId { get; set; }
       
    }


}