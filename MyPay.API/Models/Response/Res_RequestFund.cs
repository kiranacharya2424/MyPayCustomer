using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_RequestFund : CommonResponse
    { 
        // RequestFundList
        private List<AddRequestFund> _RequestFundList = new List<AddRequestFund>();
        public List<AddRequestFund> RequestFundList
        {
            get { return _RequestFundList; }
            set { _RequestFundList = value; }
        } 
    }
}