using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_MerchantLogin
    {
        //UserName
        private string _UserName = string.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        //RememberMe
        private bool _RememberMe = false;
        public bool RememberMe
        {
            get { return _RememberMe; }
            set { _RememberMe = value; }
        }
        //Email
        private string _Email = String.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
    }
}