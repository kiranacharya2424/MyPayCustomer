using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Add;
using MyPay.Models.Get;
namespace MyPay.API.Models
{
    public class Res_TransactionDetails : CommonResponse
    {
        // Transactions
        private AddTransaction _Transactions = new AddTransaction();
        public AddTransaction Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; }
        } 
    }
}