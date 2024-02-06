using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetFundAccountValidation
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
        private datafundaccountvalidation _data = new datafundaccountvalidation();
        public datafundaccountvalidation data
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

    public class datafundaccountvalidation
    {
        //AccountName
        private string _AccountName = "";
        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }

        //AccountNumber
        private string _AccountNumber = "";
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }

        //NameMatchPercentage
        private string _NameMatchPercentage = "";
        public string NameMatchPercentage
        {
            get { return _NameMatchPercentage; }
            set { _NameMatchPercentage = value; }
        }

        //MobileNumber
        private string _MobileNumber = "";
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }

      
    }

  
}