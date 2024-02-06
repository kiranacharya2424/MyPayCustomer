using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class GetAuthUser
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

        //exp

        private long _exp = 0;
        public long exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
    }
}