using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    public class EnumDropDowns
    {
        //Text
        private string _Text = string.Empty;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        //Id
        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
    }
}