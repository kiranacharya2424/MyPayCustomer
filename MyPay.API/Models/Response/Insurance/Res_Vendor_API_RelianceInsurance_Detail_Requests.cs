using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_RelianceInsurance_Detail_Requests: CommonResponse
    {
        public string Paymode { get; set; }
        public string TransactionId { get; set; }
        public string ProductName { get; set; }
        public string CustomerId { get; set; }
        public string NextDueDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string Amount { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string FineAmount { get; set; }
    }
}