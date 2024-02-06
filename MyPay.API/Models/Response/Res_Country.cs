using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_Country: CommonResponse
    {
        //AddCountry
        private List<AddCountry> _data = new List<AddCountry>();
        public List<AddCountry> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}