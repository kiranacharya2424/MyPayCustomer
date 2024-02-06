using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.FonePay
{

    public class GetVendor_API_FonePay_QR_Parse_CheckStatus_Response
    {
        public string transactionIdentifier { get; set; }
        public string actionCode { get; set; }
        public string traceId { get; set; }
        public string transmissionDateTime { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public string merchantCategoryCode { get; set; }
        public FonePay_CardAcceptor cardAcceptor { get; set; }
    }

     
}