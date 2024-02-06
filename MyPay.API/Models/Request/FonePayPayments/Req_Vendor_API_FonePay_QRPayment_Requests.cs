using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_FonePay_QRPayment_Requests : CommonProp
    {
        private string _Value = String.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        } 
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _token = String.Empty;
        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _ReceiverAccountNumber = String.Empty;
        public string ReceiverAccountNumber
        {
            get { return _ReceiverAccountNumber; }
            set { _ReceiverAccountNumber = value; }
        }
        private string _SenderAccountNumber = String.Empty;
        public string SenderAccountNumber
        {
            get { return _SenderAccountNumber; }
            set { _SenderAccountNumber = value; }
        }
        private string _SenderName = String.Empty;
        public string SenderName
        {
            get { return _SenderName; }
            set { _SenderName = value; }
        }
        private string _SenderMobile = String.Empty;
        public string SenderMobile
        {
            get { return _SenderMobile; }
            set { _SenderMobile = value; }
        }
        private string _qrRequestMessage = String.Empty;
        public string qrRequestMessage
        {
            get { return _qrRequestMessage; }
            set { _qrRequestMessage = value; }
        }

        private string _Remarks = String.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        private string _Purpose = String.Empty;
        public string Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; }
        }

    }
}