using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_CableCarBook : WebCommonProp
    {

        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        // amount
        private string _Amount = "";
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

        // TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }
        //user
        private string _User = string.Empty;
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }
        private string _CustomerWalletId = string.Empty;
        public string CustomerWalletId
        {
            get { return _CustomerWalletId; }
            set { _CustomerWalletId = value; }
        }
        private string _CustomerEmail = string.Empty;
        public string CustomerEmail
        {
            get { return _CustomerEmail; }
            set { _CustomerEmail = value; }
        }
        
        private string _TicketCategoryName = string.Empty;
        public string TicketCategoryName
        {
            get { return _TicketCategoryName; }
            set { _TicketCategoryName = value; }
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
        private int _PaymentMethodId = 0;
        public int PaymentMethodId
        {
            get { return _PaymentMethodId; }
            set { _PaymentMethodId = value; }
        }
        private string _Data = "";
        public string Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

    }
}