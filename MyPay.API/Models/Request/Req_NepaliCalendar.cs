using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_NepaliCalendar:CommonProp
    {
        //EnglishDate
        private string _EnglishDate = string.Empty;
        public string EnglishDate
        {
            get { return _EnglishDate; }
            set { _EnglishDate = value; }
        }

        //NepaliDate
        private string _NepaliDate = string.Empty;
        public string NepaliDate
        {
            get { return _NepaliDate; }
            set { _NepaliDate = value; }
        }

    }
}