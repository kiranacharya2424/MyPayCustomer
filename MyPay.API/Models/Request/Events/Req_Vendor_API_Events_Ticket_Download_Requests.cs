using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Events_Ticket_Download_Requests : CommonProp
    {

        private string _MerchantCode = string.Empty;
        public string MerchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }
        
        private string _OrderId = string.Empty;
        public string OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

       
    }
}