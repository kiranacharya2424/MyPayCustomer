using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_BankAccountMakePrimary : WebCommonProp
    {
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _BankCode = string.Empty;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        private string _BankName = string.Empty;
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }
        private string _BranchId = string.Empty;
        public string BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        private string _BranchName = string.Empty;
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
        }
        private string _AccountNumber = string.Empty;
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}