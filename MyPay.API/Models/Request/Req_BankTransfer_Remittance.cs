using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request
{
    public class Req_BankTransfer_Remittance : CommonProp
    {
        ////MemberId
        //private Int64 _MemberId = 0;
        //public Int64 MemberId
        //{
        //    get { return _MemberId; }
        //    set { _MemberId = value; }
        //}

        private string _UserName = String.Empty;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Password = String.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        //Name
        private string _AccountName = string.Empty;
        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }

        //AccountNumber
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }

        //BranchName
        private string _BranchName = string.Empty;
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }


        //BankName
        private string _BankName = string.Empty;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        //BankId
        private string _BankId = string.Empty;
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }

        //Bankcode
        private string _BankCode = string.Empty;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }

        //BranchId
        private string _BranchId = string.Empty;
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
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

        //MerchantId
        private string _MerchantId = String.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
        }

        //Reference
        private string _Reference = string.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        //AuthTokenString
        private string _AuthTokenString = string.Empty;
        public string AuthTokenString
        {
            get { return _AuthTokenString; }
            set { _AuthTokenString = value; }
        }
    }
}
