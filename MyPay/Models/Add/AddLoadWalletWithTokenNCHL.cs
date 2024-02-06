using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddLoadWalletWithTokenNCHL
    {
        private string _participantId="";
        public string participantId { get => _participantId; set => _participantId = value; }
        private string _mandateToken="";
        public string mandateToken { get => _mandateToken; set => _mandateToken = value; }
        private string _userIdentifier="";
        public string userIdentifier { get => _userIdentifier; set => _userIdentifier = value; }
        private decimal _amount =0;
        public decimal amount { get => _amount; set => _amount = value; }
        private string _appId="";
        public string appId { get => _appId; set => _appId = value; }
        private string _instructionId="";
        public string instructionId { get => _instructionId; set => _instructionId = value; }
        private string _refId="";
        public string refId { get => _refId; set => _refId = value; }
        private string _particulars="";
        public string particulars { get => _particulars; set => _particulars = value; }
        private string _remarks="";
        public string remarks { get => _remarks; set => _remarks = value; }
        private string _addnField1="";
        public string addnField1 { get => _addnField1; set => _addnField1 = value; }
        private string _addnField2="";
        public string addnField2 { get => _addnField2; set => _addnField2 = value; }
        private string _token="";
        public string token { get => _token; set => _token = value; } 

        
    }
}