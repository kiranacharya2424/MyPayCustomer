using MyPay.API.Models.Response;
using MyPay.Models.Add;
using MyPay.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.State
{
    public class Res_CreditCardIsuuerList : CommonResponse
    {
        //AddState
        private List<GetCreditCardIssuerList> _data = new List<GetCreditCardIssuerList>();
        public List<GetCreditCardIssuerList> data
        {
            get { return _data; }
            set { _data = value; }
        }

    }
}