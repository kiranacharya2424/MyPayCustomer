using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Request
{
    public class Req_UserBankDetail: CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //BankCode
        private string _BankCode = string.Empty;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        //BankName
        private string _BankName = string.Empty;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        //BranchId
        private string _BranchId = string.Empty;
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }

        //BranchName
        private string _BranchName = string.Empty;
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }

        //AccountNumber
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }


        //LogoUrl
        private string _LogoUrl = string.Empty;
        public string LogoUrl
        {
            get { return _LogoUrl; }
            set { _LogoUrl = value; }
        }
    }
}