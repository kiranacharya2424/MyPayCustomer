using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_CouponsScratchNow : CommonResponse
    {
        //Data
        private AddCouponsScratched _data = new AddCouponsScratched();
        public AddCouponsScratched data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}