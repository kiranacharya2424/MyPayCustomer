using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddRegisterLinkedBankNCHL
    {
        private string _identifier = "";
        public string identifier { get => _identifier; set => _identifier = value; }
        private string _amount = "";
        public string amount { get => _amount; set => _amount = value; }
        private string _debitType = "";
        public string debitType { get => _debitType; set => _debitType = value; }
        private string _bankName = "";
        public string bankName { get => _bankName; set => _bankName = value; }
        private string _mobileNo = "";
        public string mobileNo { get => _mobileNo; set => _mobileNo = value; }
        private string _mandateStartDate = "";
        public string mandateStartDate { get => _mandateStartDate; set => _mandateStartDate = value; }
        private string _mandateExpiryDate = "";
        public string mandateExpiryDate { get => _mandateExpiryDate; set => _mandateExpiryDate = value; }
        private string _mandateTokenNickName = "";
        public string mandateTokenNickName { get => _mandateTokenNickName; set => _mandateTokenNickName = value; }
        private string _mandateTokenType = "";
        public string mandateTokenType { get => _mandateTokenType; set => _mandateTokenType = value; }
        private string _frequency = "";
        public string frequency { get => _frequency; set => _frequency = value; }
        private string _entryId = "0";
        public string entryId { get => _entryId; set => _entryId = value; }
        private string _token = "";
        public string token { get => _token; set => _token = value; }
        private string _participantId = "";
        public string participantId { get => _participantId; set => _participantId = value; }
        private string _bankId = "";
        public string bankId { get => _bankId; set => _bankId = value; }
        private string _userIdentifier = "";
        public string userIdentifier { get => _userIdentifier; set => _userIdentifier = value; }
        private string _mandateToken = "";
        public string mandateToken { get => _mandateToken; set => _mandateToken = value; }
        private string _email = "";
        public string email { get => _email; set => _email = value; }
        private bool _autoRenewal = false;
        public bool autoRenewal { get => _autoRenewal; set => _autoRenewal = value; } 

        private string _authToken = "";
        public string authToken { get => _authToken; set => _authToken = value; }
    }
}