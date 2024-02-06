using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_CouponsScratched : CommonResponse
    {
        //Data
        private List<AddCouponsScratched> _data = new List<AddCouponsScratched>();
        public List<AddCouponsScratched> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}