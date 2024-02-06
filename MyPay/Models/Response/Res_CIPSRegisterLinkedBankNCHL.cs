using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response
{
    public class Res_CIPSRegisterLinkedBankNCHL
    {
        private string _responseCode="";

        public string responseCode { get => _responseCode; set => _responseCode = value; }
        private string _responseMessage = "";
        public string responseMessage { get => _responseMessage; set => _responseMessage = value; }
        private Res_CIPSRegisterData _data = new Res_CIPSRegisterData();
        public Res_CIPSRegisterData data { get => _data; set => _data = value; }
        private List<object> _error = new List<object>();
        public List<object> error { get => _error; set => _error = value; }
    }
    public class Res_CIPSRegisterData
    {
        private string _identifier="";

        public string identifier { get => _identifier; set => _identifier = value; }
        private string _participantId = "";
        public string participantId { get => _participantId; set => _participantId = value; }
        private string _entryId = "";
        public string entryId { get => _entryId; set => _entryId = value; }
        private string _token = "";
        public string token { get => _token; set => _token = value; }
    }


}