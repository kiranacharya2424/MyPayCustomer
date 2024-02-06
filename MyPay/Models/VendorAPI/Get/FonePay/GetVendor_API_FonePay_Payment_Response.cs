using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{ 
    public class GetVendor_API_FonePay_Payment_Response 
    {
        public string transactionIdentifier { get; set; }
        public string actionCode { get; set; }
        public string approvalCode { get; set; }
        public string responseCode { get; set; }
        public string traceId { get; set; }
        public string transmissionDateTime { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public string merchantCategoryCode { get; set; }
        public string responseMessage { get; set; }
        public FonePay_CardAcceptor cardAcceptor { get; set; }
        public string WalletBalance { get; set; }

    }

}