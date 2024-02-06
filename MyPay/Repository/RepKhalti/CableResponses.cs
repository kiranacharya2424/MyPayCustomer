using MyPay.Models.Common;
using Newtonsoft.Json;
using System;

namespace MyPay.Repository
{
    public class CableResponses
    {
        public string TransactionID { get; set; }

        public string ReferenceNo { get; set; }

        public string ResponseDescription { get; set; }

        public int ResponseCode { get; set; }
    }

    public class TicketResult
    {
        public Ticketresponse[] TicketResponse { get; set; }
        public Invoiceresponse InvoiceResponse { get; set; }
        public ticketresponse ticketresponse { get; set; }
    }
   

    public class Invoiceresponse
    {
        public double BasePrice { get; set; }
        public double VatTax { get; set; }
        public double TotalAmount { get; set; }
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
        public float Price { get; set; }
        public string TicketNumber { get; set; }
    }
    public class ticketresponse
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public string ValidUntil { get; set; }
        public float Price { get; set; }
        public string TicketNumber { get; set; }
    }


  public class CableTicketDetails
    {
        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string PassengerCount { get; set; }
    }
 

    public class Reconcile
    {
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        //responseMessage
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }
        //Details
        private string _Details = string.Empty;
        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }


        //ReponseCode
        private int _ReponseCode = 0;
        public int ReponseCode
        {
            get { return _ReponseCode; }
            set { _ReponseCode = value; }
        }


        //status
        private bool _status = false;
        public bool status
        {
            get { return _status; }
            set { _status = value; }
        }

        //IosVersion
        private string _ios_version = Common.IosVersion;
        public string ios_version
        {
            get { return _ios_version; }
            set { _ios_version = value; }
        }

        //AndroidVersion
        private string _AndroidVersion = Common.AndroidVersion;
        public string AndroidVersion
        {
            get { return _AndroidVersion; }
            set { _AndroidVersion = value; }
        }
        //CouponCode
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
        //IsCouponUnlocked
        private bool _IsCouponUnlocked = false;
        public bool IsCouponUnlocked
        {
            get { return _IsCouponUnlocked; }
            set { _IsCouponUnlocked = value; }
        }
        //TransactionUniqueID
        private string _TransactionUniqueId = "";
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        public ReconcileResponse[] ReconcileResponse { get; set; }

    }

    public class ReconcileResponse
    {
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public double TotalAmount { get; set; }
        public string TransactionDate { get; set; }
        public string Status { get; set; }

    }

    public class FinalTicketRsponse
    {


        public string TripType { get; set; }
        public string PassengerType { get; set; }
        public string QRCode { get; set; }
        public string BarCode { get; set; }
        public string ValidUntil { get; set; }
        public float Price { get; set; }
        public string TicketNumber { get; set; }
        public float BasePrice { get; set; }
        public float VatTax { get; set; }
        public float TotalAmount { get; set; }
        public string ReferenceId { get; set; }
        public string TicketMessage { get; set; }
        public string VatBillMessage { get; set; }
        public string UserName { get; set; }
    }

    public class ResponseData
    {

        public string TransactionID { get; set; }

        public string ReferenceNo { get; set; }

        public string ResponseDescription { get; set; }

        public string ResponseCode { get; set; }

    }







}