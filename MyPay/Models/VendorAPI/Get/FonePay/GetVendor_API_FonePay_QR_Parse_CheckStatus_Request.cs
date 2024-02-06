using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.FonePay
{
    public class GetVendor_API_FonePay_QR_Parse_CheckStatus_Request
    {
        public string issuerBin { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public string qrRequestMessage { get; set; }
    }
}