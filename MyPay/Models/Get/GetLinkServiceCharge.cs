using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetLinkServiceCharge
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
        private List<dataServiceCharge> _data = new List<dataServiceCharge>();
        public List<dataServiceCharge> data
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

    public class dataServiceCharge
    {
        //InstitutionName
        private string _AmountFrom = "";
        public string AmountFrom
        {
            get { return _AmountFrom; }
            set { _AmountFrom = value; }
        }

        //AmountTo
        private string _AmountTo = "";
        public string AmountTo
        {
            get { return _AmountTo; }
            set { _AmountTo = value; }
        }

        //CommissionType
        private string _CommissionType = "";
        public string CommissionType
        {
            get { return _CommissionType; }
            set { _CommissionType = value; }
        }

        //CommissionValue
        private string _CommissionValue = "";
        public string CommissionValue
        {
            get { return _CommissionValue; }
            set { _CommissionValue = value; }
        }

      
    }

  
}