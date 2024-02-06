using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Insurance
{
    public class Res_Vendor_API_SagarmathaInsurance_Lookup_Requests:CommonResponse
    {
        public string ContactNo { get; set; }
        public string DebitNoteNo { get; set; }
        public int SessionId { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string PayableAmount { get; set; }
    }

}