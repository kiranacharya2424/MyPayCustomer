using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response
{
    public class GetCreditCardIssuerList
    {
        //Code
        private string _Code = string.Empty;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Logo
        private string _Logo = string.Empty;
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }
    }
}