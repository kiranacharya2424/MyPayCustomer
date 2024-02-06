using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_ProviderListWithCashback:CommonResponse
    {
        //AddState
        private List<ProviderServiceCategoryList> _data = new List<ProviderServiceCategoryList>();
        public List<ProviderServiceCategoryList> data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}