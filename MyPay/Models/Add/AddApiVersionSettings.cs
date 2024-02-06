using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyPay.Models.Add

{
    public class AddApiVersionSettings 
    {
        private string _androidVersion = "0.0";
        public string androidVersion

        {
            get { return _androidVersion; }
            set { _androidVersion = value; }
        }

        private string _iosVersion = "0.0";
        public string iosVersion

        {
            get { return _iosVersion; }
            set { _iosVersion = value; }
        }

    }

}