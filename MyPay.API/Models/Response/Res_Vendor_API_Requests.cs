using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_Vendor_API_Requests : CommonResponse
    {
      
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        // WalletBalance
        private decimal _WalletBalance = 0;
        public decimal WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        // Pin
        private string _Pin = string.Empty;
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        // Serial
        private string _Serial = string.Empty;
        public string Serial
        {
            get { return _Serial; }
            set { _Serial = value; }
        }

        public string ReferenceNo { get;  set; }
    }

    public class Res_Vendor_CableAPI_Requests : CommonResponse
    {
        public string ReferenceNo { get; set; }
        public object ticketResponse { get; set; }
        public object InvoiceResponse { get; set; }

        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }

    }


    public class Res_Out_CableAPI_Requests : CommonResponse
    {
        public string ReferenceNo { get; set; }


        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }

    }

    public class Res_Vendor_CableAPI_TicketResponse:CommonResponse
    {

        public object ticketResponse { get; set; }
        public object InvoiceResponse { get; set; }

    }

}