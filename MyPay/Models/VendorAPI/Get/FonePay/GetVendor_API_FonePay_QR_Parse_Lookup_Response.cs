using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.FonePay
{

    public class GetVendor_API_FonePay_QR_Parse_Lookup_Response
    {
        public string payloadFormatIndicator { get; set; }
        public string pointOfInitiationMethod { get; set; }
        public FonePay_FonepayMerchantAccountInformation fonepayMerchantAccountInformation { get; set; }
        public string merchantCategoryCode { get; set; }
        public string transactionCurrency { get; set; }
        public string transactionAmount { get; set; }
        public string countryCode { get; set; }
        public string merchantName { get; set; }
        public string merchantCity { get; set; }
        public FonePay_AdditionalDataFieldTemplate additionalDataFieldTemplate { get; set; }
        public string cyclicRedundancyCheck { get; set; }
        public FonePay_ServerResponse serverResponse { get; set; }
        public FonePay_FonepayDiscountInformation fonepayDiscountInformation { get; set; }
        public FonePay_FonepayChargeInformation fonepayChargeInformation { get; set; }
    }


    // Root myDeserializedclass FonePay_= JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class FonePay_AdditionalDataFieldTemplate
    {
        public string billNumber { get; set; }
        public string terminalLabel { get; set; }
        public string purposeOfTransaction { get; set; }
        public string discountOrChargeIndicator { get; set; }
        public string discountOrChargeAmount { get; set; }
    }

    public class FonePay_FonepayChargeInformation
    {
        public string chargeAmount { get; set; }
    }

    public class FonePay_FonepayDiscountInformation
    {
        public string discountAmount { get; set; }
        public string merchantDiscountAmount { get; set; }
        public string issuerDiscountAmount { get; set; }
        public string discountLiabiity { get; set; }
    }

    public class FonePay_FonepayMerchantAccountInformation
    {
        public string globallyUniqueIdentifier { get; set; }
        public int merchantQrId { get; set; }
        public string merchantPan { get; set; }
        public string acquiringBin { get; set; }
        public string acquirerBin { get; set; }
        public int websocketId { get; set; }
        public int qrLogId { get; set; }
    }


    public class FonePay_ServerResponse
    {
        public string message { get; set; }
        public bool success { get; set; }
    }


}