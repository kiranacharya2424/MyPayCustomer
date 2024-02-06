using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request.MyPayment
{
    public class Res_MyPayment: CommonResponse

    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}