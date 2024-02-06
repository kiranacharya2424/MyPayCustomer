using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_Transactions : CommonResponse
    {
        // Transactions
        private List<AddTransaction> _Transactions = new List<AddTransaction>();
        public List<AddTransaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; }
        } 
    }
}