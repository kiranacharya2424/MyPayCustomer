using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetTokenUnLinkNCHL
    {
        private string _responseCode;
        public string responseCode { get => _responseCode; set => _responseCode = value; }
        private string _responseMessage;
        public string responseMessage { get => _responseMessage; set => _responseMessage = value; }
        private string _participantId;
        public string participantId { get => _participantId; set => _participantId = value; }
        private string _identifier;
        public string identifier { get => _identifier; set => _identifier = value; }
        private string _userIdentifier;
        public string userIdentifier { get => _userIdentifier; set => _userIdentifier = value; }
        private string _mandateToken;
        public string mandateToken { get => _mandateToken; set => _mandateToken = value; }
        private string _cancelReasonCode;
        public string cancelReasonCode { get => _cancelReasonCode; set => _cancelReasonCode = value; }
        private string _cancelReasonMessage;
        public string cancelReasonMessage { get => _cancelReasonMessage; set => _cancelReasonMessage = value; }
        private string _token;
        public string token { get => _token; set => _token = value; }

        //message
        private string _message = string.Empty;
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        //error
        private string _error = string.Empty;
        public string error
        {
            get { return _error; }
            set { _error = value; }
        }
        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
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
        private string _data;
        public string data { get => _data; set => _data = value; }
        private List<Classfielderrorlist> _classfielderrorlist;
        public List<Classfielderrorlist> classfielderrorlist { get => _classfielderrorlist; set => _classfielderrorlist = value; }

    }
    public class Classfielderrorlist
    {
        public string field { get; set; }
        public string message { get; set; }
    }
}