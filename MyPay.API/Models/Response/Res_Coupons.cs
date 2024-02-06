using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Coupons
{
    public class Res_Coupons : CommonResponse
    {

        //AddOccupation
        private List<AddCoupons> _data = new List<AddCoupons>();
        public List<AddCoupons> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}