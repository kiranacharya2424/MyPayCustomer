
using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyPay.Models.Add
{
    public class CableCommon : CommonAdd
    {
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
        public string RefernceNo { get; set; }
        public string totalamount { get; set; }
        public string staus { get; set; }
        public string transactiondate { get; set; }
        public string ResponseCode { get; set; }
        public string baseprice { get; set; }
        public long CableId { get; set; }

    }

    public class TicketInvoiceCommon  : CommonAdd

    {

        public string BasePrice { get; set; }
        public string VatTax { get; set; }
        public string TotalAmount { get; set; }
        public string ReferenceId { get; set; }
        public string TicketMessage { get; set; }
        public string VatBillMessage { get; set; }
        public string UserName { get; set; }
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public string ValidUntil { get; set; }
        public double Price { get; set; }
        public string TicketNumber { get; set; }
        public object Ticketresponse { get; set; }  
    }


    public class InvoiceResponse
    {
        public string BasePrice { get; set; }
        public string VatTax { get; set; }
        public string TotalAmount { get; set; }
        public string ReferenceId { get; set; }
        public string TicketMessage { get; set; }
        public string VatBillMessage { get; set; }
        public string UserName { get; set; }
    }


    public class Ticketresponse
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public string ValidUntil { get; set; }
        public double Price { get; set; }
        public string TicketNumber { get; set; }
    }
    public class ticketresponse
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
