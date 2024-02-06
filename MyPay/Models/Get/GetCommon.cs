using MyPay.Models.Add;
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyPay.Models.Get
{
    public  class GetCommonCable:CommonGet
    {
        public string ReferenceNumber { get; set; }
        public string TotalAmount { get; set; }
        public string Staus { get; set; }
        public string TransactionDate { get; set; }
        public string ResponseCode { get; set; }
        public string BasePrice { get; set; }


        private string _TrannsactionId { get; set; }
        public string TransactionId
        {
            get
            {
                return
                _TrannsactionId;
            }
            set { _TrannsactionId = value; }
        }
    }


    public class GetTicketInvoiceCommon : CommonGet 
    {
          public object TicketResponse { get; set; }
        //public InvoiceResponse InvoiceResponse { get; set; }
        public string BasePrice { get; set; }
        public string VatTax { get; set; }
        public string TotalAmount { get; set; }
        public string ReferenceId { get; set; }
        public string TicketMessage { get; set; }
        public string VatBillMessage { get; set; }
        public string Username { get; set; }
        public string QRCode { get; internal set; }
        public string BarCode { get; internal set; }
        public string PassengerType { get; internal set; }
        public string TicketNumber { get; internal set; }
        public string ValidUntil { get; internal set; }
        public string TripType { get; internal set; }
    }

    public class InvoiceResponses:CommonGet
    {
        public double BasePrice { get; set; }
        public double VatTax { get; set; }
        public double TotalAmount { get; set; }
        public string ReferenceId { get; set; }
        public string TicketMessage { get; set; }
        public string VatBillMessage { get; set; }
        public string UserName { get; set; }

    }


    public class TicketResponse:CommonGet
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public string ValidUntil { get; set; }
        public double Price { get; set; }
        public string TicketNumber { get; set; }
    }
    

}