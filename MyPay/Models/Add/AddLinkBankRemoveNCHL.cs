using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddLinkBankRemoveNCHL
    {
        private string _participantId="";
        public string participantId { get => _participantId; set => _participantId = value; }
        private string _mandateToken="";
        public string mandateToken { get => _mandateToken; set => _mandateToken = value; }
        private string _identifier = "";
        public string identifier { get => _identifier; set => _identifier = value; }
        private string _userIdentifier="";
        public string userIdentifier { get => _userIdentifier; set => _userIdentifier = value; } 
        private string _token = "";
        public string token { get => _token; set => _token = value; }
        private string _cancelReasonCode = "";
        public string cancelReasonCode { get => _cancelReasonCode; set => _cancelReasonCode = value; }
        private string _cancelReasonMessage = "";
        public string cancelReasonMessage { get => _cancelReasonMessage; set => _cancelReasonMessage = value; }


    }
}