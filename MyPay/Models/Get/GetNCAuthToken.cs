using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetNCAuthToken
    {
        #region "Properties"    

        //access_token
        private string _access_token = "";
        public string access_token
        {
            get { return _access_token; }
            set { _access_token = value; }
        }

        //token_type
        private string _token_type = "";
        public string token_type
        {
            get { return _token_type; }
            set { _token_type = value; }
        }

        //refresh_token
        private string _refresh_token = "";
        public string refresh_token
        {
            get { return _refresh_token; }
            set { _refresh_token = value; }
        }

        //expires_in
        private string _expires_in = "";
        public string expires_in
        {
            get { return _expires_in; }
            set { _expires_in = value; }
        }

        //scope
        private string _scope = "";
        public string scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        //customerdetails
        private string _customerdetails = "";
        public string customerdetails
        {
            get { return _customerdetails; }
            set { _customerdetails = value; }
        }
        #endregion
    }
}