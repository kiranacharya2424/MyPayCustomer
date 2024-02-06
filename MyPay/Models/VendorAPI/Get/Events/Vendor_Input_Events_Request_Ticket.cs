using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Events_Request_Ticket
    {
        private string _MerchantCode = string.Empty;
        public string merchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }
        private string _CustomerName = string.Empty;
        public string customerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private string _CustomerMobile = string.Empty;
        public string customerMobile
        {
            get { return _CustomerMobile; }
            set { _CustomerMobile = value; }
        }
        private string _CustomerEmail = string.Empty;
        public string customerEmail
        {
            get { return _CustomerEmail; }
            set { _CustomerEmail = value; }
        }
        private int _EventId = 0;
        public int eventId
        {
            get { return _EventId; }
            set { _EventId = value; }
        }
        private int _TicketCategoryId = 0;
        public int ticketCategoryId
        {
            get { return _TicketCategoryId; }
            set { _TicketCategoryId = value; }
        }
        private string _EventDate = string.Empty;
        public string eventDate
        {
            get { return _EventDate; }
            set { _EventDate = value; }
        }
        private decimal _TicketRate = 0;
        public decimal ticketRate
        {
            get { return _TicketRate; }
            set { _TicketRate = value; }
        }
        private int _NoOfTicket = 0;
        public int noOfTicket
        {
            get { return _NoOfTicket; }
            set { _NoOfTicket = value; }
        }
        private decimal _TotalPrice = 0;
        public decimal totalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
        private int _PaymentMethodId = 0;
        public int paymentMethodId
        {
            get { return _PaymentMethodId; }
            set { _PaymentMethodId = value; }
        }
        private string _ApiClientCode = string.Empty;
        public string apiClientCode
        {
            get { return _ApiClientCode; }
            set { _ApiClientCode = value; }
        }

        //Added by Roshan
        private string _CouponCode = string.Empty;
        public string couponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }

    }
}