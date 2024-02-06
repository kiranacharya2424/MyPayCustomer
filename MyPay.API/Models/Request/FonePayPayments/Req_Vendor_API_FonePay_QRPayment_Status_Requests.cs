using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_FonePay_QRPayment_Status_Requests : CommonProp
    {
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _issuerBin = String.Empty;
        public string issuerBin
        {
            get { return _issuerBin; }
            set { _issuerBin = value; }
        }
        private string _retrievalReferenceNumber = String.Empty;
        public string retrievalReferenceNumber
        {
            get { return _retrievalReferenceNumber; }
            set { _retrievalReferenceNumber = value; }
        } 
        private string _qrRequestMessage = String.Empty;
        public string qrRequestMessage
        {
            get { return _qrRequestMessage; }
            set { _qrRequestMessage = value; }
        }
    }
}