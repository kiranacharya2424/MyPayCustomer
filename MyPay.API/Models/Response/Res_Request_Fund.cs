using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_Request_Fund : CommonResponse
    {
        // RequestFund
        private List<AddRequestFund> _RequestFund = new List<AddRequestFund>();
        public List<AddRequestFund> RequestFund
        {
            get { return _RequestFund; }
            set { _RequestFund = value; }
        } 
    }
}