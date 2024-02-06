using MyPay.Models.Get.FonePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.FonePay
{
    public class Res_Vendor_API_FonePay_QRRequest_Lookup_Requests : CommonResponse
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
        public string qrRequestMessage { get; set; }
        public FonePay_ServerResponse serverResponse { get; set; }
        public FonePay_FonepayDiscountInformation fonepayDiscountInformation { get; set; }
        public FonePay_FonepayChargeInformation fonepayChargeInformation { get; set; }
    }
}