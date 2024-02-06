using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.State
{
    public class Res_CreditCardCharge : CommonResponse
    {
        //AddState
        private string _Charges = string.Empty;
        public string Charges
        {
            get { return _Charges; }
            set { _Charges = value; }
        }

    }
}