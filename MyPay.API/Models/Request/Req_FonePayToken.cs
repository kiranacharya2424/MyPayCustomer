using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_FonePayToken
    {
        //BankId
        private string _grant_type = string.Empty;
        public string grant_type
        {
            get { return _grant_type; }
            set { _grant_type = value; }
        }

        //BankId
        private string _username = string.Empty;
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        //BankId
        private string _password = string.Empty;
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}