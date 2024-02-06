using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Events
{
    public class Res_Vendor_API_Events_Ticket_Requests : CommonResponse
    {

       
        // status
        private bool _success = false;
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        // Message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        private Order _data = new Order();
        public Order data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
    public class Order
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
        private int _PaymentMethodId = 0;
        public int PaymentMethodId
        {
            get { return _PaymentMethodId; }
            set { _PaymentMethodId = value; }
        }

        private decimal _PayableAmount = 0;
        public decimal PayableAmount
        {
            get { return _PayableAmount; }
            set { _PayableAmount = value; }
        }

        public string _PaymentMerchantId;
        public string PaymentMerchantId
        {
            get { return _PaymentMerchantId; }
            set { _PaymentMerchantId = value; }
        }

        /*
         "merchantCode": "MER000002",
"orderId": "933861352",
"payableAmount": 180.00,
"paymentMethodId": 1,
"paymentMerchantId": "MER80480781"
         */

    }
}
