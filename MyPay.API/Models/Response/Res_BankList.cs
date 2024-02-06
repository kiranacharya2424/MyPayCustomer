using MyPay.Models.Add;
using MyPay.Models.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.State
{
    public class Res_BankList : CommonResponse
    {
        //AddState
        private List<BankList> _data = new List<BankList>();
        public List<BankList> data
        {
            get { return _data; }
            set { _data = value; }
        }

    }
}