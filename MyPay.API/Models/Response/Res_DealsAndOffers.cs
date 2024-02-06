using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_DealsAndOffers: CommonResponse
    {
        //Data
        private List<AddDealsandOffers> _data = new List<AddDealsandOffers>();
        public List<AddDealsandOffers> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}