using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetFundTransferToken
    {
        //code
        private string _code = "";
        public string code
        {
            get { return _code; }
            set { _code = value; }
        }



        //message
        private string _message = "";
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        //errors
        private List<Errors_Instruments> _errors = new List<Errors_Instruments>();
        public List<Errors_Instruments> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        //data
        private dataBankTransferToken _data = new dataBankTransferToken();
        public dataBankTransferToken data
        {
            get { return _data; }
            set { _data = value; }
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
    }

    public class dataBankTransferToken
    {
        //AccessToken
        private string _AccessToken = "";
        public string AccessToken
        {
            get { return _AccessToken; }
            set { _AccessToken = value; }
        }

        //TokenType
        private string _TokenType = "";
        public string TokenType
        {
            get { return _TokenType; }
            set { _TokenType = value; }
        }

        //CreatedTimestamp
        private string _CreatedTimestamp = "";
        public string CreatedTimestamp
        {
            get { return _CreatedTimestamp; }
            set { _CreatedTimestamp = value; }
        }

        //ExpiryTimestamp
        private string _ExpiryTimestamp = "";
        public string ExpiryTimestamp
        {
            get { return _ExpiryTimestamp; }
            set { _ExpiryTimestamp = value; }
        }

      
    }

}