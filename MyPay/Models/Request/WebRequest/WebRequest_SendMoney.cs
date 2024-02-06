using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_SendMoney : WebCommonProp
    {
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _BankId = string.Empty;
        public string BankId
        {
            get { return _BankId; }
            set { _BankId = value; }
        }
        private string _AccountId = "";
        public string AccountId
        {
            get { return _AccountId; }
            set { _AccountId = value; }
        }
        private string _AccountName = string.Empty;
        public string AccountName
        {
            get { return _AccountName; }
            set { _AccountName = value; }
        }
        private string _RecipientPhone = string.Empty;
        public string RecipientPhone
        {
            get { return _RecipientPhone; }
            set { _RecipientPhone = value; }
        }

        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        private string _Pin = "";
        public string Pin
        {
            get { return _Pin; }
            set { _Pin = value; }
        }
        private string _Mpin = "";
        public string Mpin
        {
            get { return _Mpin; }
            set { _Mpin = value; }
        }
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _AccountNumber = "";
        public string AccountNumber
        {
            get { return _AccountNumber; }
            set { _AccountNumber = value; }
        }
        private string _BranchName = string.Empty;
        public string BranchName
        {
            get { return _BranchName; }
            set { _BranchName = value; }
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
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _Referenceno = string.Empty;
        public string Referenceno
        {
            get { return _Referenceno; }
            set { _Referenceno = value; }
        }
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}