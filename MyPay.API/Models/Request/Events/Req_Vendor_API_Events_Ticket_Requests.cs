using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{

    public class Req_Vendor_API_Events_Ticket_Requests : CommonProp
    {


        // session_id
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        // amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        // reference
        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        private string _MerchantCode = string.Empty;
        public string MerchantCode
        {
            get { return _MerchantCode; }
            set { _MerchantCode = value; }
        }
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private string _CustomerMobile = string.Empty;
        public string CustomerMobile
        {
            get { return _CustomerMobile; }
            set { _CustomerMobile = value; }
        }
        private string _CustomerEmail = string.Empty;
        public string CustomerEmail
        {
            get { return _CustomerEmail; }
            set { _CustomerEmail = value; }
        }
        private int _EventId = 0;
        public int EventId
        {
            get { return _EventId; }
            set { _EventId = value; }
        }
        private int _TicketCategoryId = 0;
        public int TicketCategoryId
        {
            get { return _TicketCategoryId; }
            set { _TicketCategoryId = value; }
        }
        private string _TicketCategoryName = string.Empty;
        public string TicketCategoryName
        {
            get { return _TicketCategoryName; }
            set { _TicketCategoryName = value; }
        }
        private string _EventDate = string.Empty;
        public string EventDate
        {
            get { return _EventDate; }
            set { _EventDate = value; }
        }
        private decimal _TicketRate = 0;
        public decimal TicketRate
        {
            get { return _TicketRate; }
            set { _TicketRate = value; }
        }
        private int _NoOfTicket = 0;
        public int NoOfTicket
        {
            get { return _NoOfTicket; }
            set { _NoOfTicket = value; }
        }
        private decimal _TotalPrice = 0;
        public decimal TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
        private int _PaymentMethodId = 0;
        public int PaymentMethodId
        {
            get { return _PaymentMethodId; }
            set { _PaymentMethodId = value; }
        }
        private string _ApiClientCode = string.Empty;
        public string ApiClientCode
        {
            get { return _ApiClientCode; }
            set { _ApiClientCode = value; }
        }
       
    }

    public class Req_Vendor_API_Events_Ticket_RequestsV2 : Req_Vendor_API_Events_Ticket_Requests {
        private string _Value = String.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private string _paymentMethodName = String.Empty;
        public string paymentMethodName
        {
            get { return _paymentMethodName; }
            set { _paymentMethodName = value; }

        }

        private string _PaymentMerchantId = string.Empty;
        public string PaymentMerchantId
        {
            get { return _PaymentMerchantId; }
            set { _PaymentMerchantId = value; }
        }

    }
}