using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_InternetBanking
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
        private List<Errors_InternetBanking> _errors = new List<Errors_InternetBanking>();
        public List<Errors_InternetBanking> errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        //data
        private dataIneternetBanking _data = new dataIneternetBanking();
        public dataIneternetBanking data
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

    public class dataIneternetBanking
    {
        //PaymentLink
        private string _PaymentLink = "";
        public string PaymentLink
        {
            get { return _PaymentLink; }
            set { _PaymentLink = value; }
        }
    }

    public class Errors_InternetBanking
    {
        //error_code
        private string _error_code = "";
        public string error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }

        //error_message
        private string _error_message = "";
        public string error_message
        {
            get { return _error_message; }
            set { _error_message = value; }
        }
    }
}