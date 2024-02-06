using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_UserAuthorization: CommonResponse
    {
        //Token
        private string _Token = string.Empty;
        public string Token
        {
            get { return _Token; }
            set { _Token = value; }
        }
        //ExpirationTime
        private string _ExpirationTime = string.Empty;
        public string ExpirationTime
        {
            get { return _ExpirationTime; }
            set { _ExpirationTime = value; }
        }
    }
}