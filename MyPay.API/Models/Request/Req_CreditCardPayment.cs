using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_CreditCardPayment : CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Amount
        //private Double _Amount = 0;
        //public Double Amount
        //{
        //    get { return _Amount; }
        //    set { _Amount = value; }
        //}

        public  string BankName { get; set; }

        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Code
        private string _Code = string.Empty;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        //CardNumber
        private string _CardNumber = string.Empty;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //ServiceCharge
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
    }
}