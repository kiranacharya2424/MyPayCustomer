using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetPaymentInstrument
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
        private List<dataInstruments> _data = new List<dataInstruments>();
        public List<dataInstruments> data
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

    public class dataInstruments
    {
        //InstitutionName
        private string _InstitutionName = "";
        public string InstitutionName
        {
            get { return _InstitutionName; }
            set { _InstitutionName = value; }
        }

        //InstrumentName
        private string _InstrumentName = "";
        public string InstrumentName
        {
            get { return _InstrumentName; }
            set { _InstrumentName = value; }
        }

        //InstrumentCode
        private string _InstrumentCode = "";
        public string InstrumentCode
        {
            get { return _InstrumentCode; }
            set { _InstrumentCode = value; }
        }

        //InstrumentValue
        private string _InstrumentValue = "";
        public string InstrumentValue
        {
            get { return _InstrumentValue; }
            set { _InstrumentValue = value; }
        }

        //LogoUrl
        private string _LogoUrl = "";
        public string LogoUrl
        {
            get { return _LogoUrl; }
            set { _LogoUrl = value; }
        }

        //BankUrl
        private string _BankUrl = "";
        public string BankUrl
        {
            get { return _BankUrl; }
            set { _BankUrl = value; }
        }

        //BankType
        private string _BankType = "";
        public string BankType
        {
            get { return _BankType; }
            set { _BankType = value; }
        }
    }

    public class Errors_Instruments
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