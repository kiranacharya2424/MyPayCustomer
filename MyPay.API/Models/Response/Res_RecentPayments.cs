using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_RecentPayments : CommonResponse
    {
        // Transactions
        private List<AddRecentPayments> _Payments = new List<AddRecentPayments>();
        public List<AddRecentPayments> Payments
        {
            get { return _Payments; }
            set { _Payments = value; }
        } 
    }
}