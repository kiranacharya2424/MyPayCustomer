using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_DepositByBankTransfer : CommonProp
    {

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //VendorType
        private string _VendorType = "";
        public string VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }


        //BankId
        private string _BankId = string.Empty;
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }
       

        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


    }
}