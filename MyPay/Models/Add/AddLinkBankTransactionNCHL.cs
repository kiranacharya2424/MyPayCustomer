using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddLinkBankTransactionNCHL
    {
        private string _participantId="";
        public string participantId { get => _participantId; set => _participantId = value; }

        private string _paymentToken = "";
        public string paymentToken { get => _paymentToken; set => _paymentToken = value; }
        private decimal _amount =0;
        public decimal amount { get => _amount; set => _amount = value; }
        private string _appId="";
        public string appId { get => _appId; set => _appId = value; }
     
        private string _token="";
        public string token { get => _token; set => _token = value; }
        
    }
}