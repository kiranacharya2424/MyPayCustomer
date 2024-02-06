using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get.Events
{
    public class GetVendor_API_Events_Ticket : CommonGet
    {

        // status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }
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
        // Message
        private Order _data = new Order();
        public Order data
        {
            get { return _data; }
            set { _data = value; }
        }

        public List<string> _errors;
        public List<string> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

    }
    public class Order
    {
        private string _merchantCode = string.Empty;
        public string merchantCode
        {
            get { return _merchantCode; }
            set { _merchantCode = value; }
        }
        private string _orderId = string.Empty;
        public string orderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }
        private int _paymentMethodId = 0;
        public int paymentMethodId
        {
            get { return _paymentMethodId; }
            set { _paymentMethodId = value; }
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

    }
}