using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddUserBankDetail:CommonAdd
    {
        public enum UserBankType
        {
            NCHL = 1,
            NPS = 2
        }
        #region "Properties"
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

        //ICON_NAME
        private string _ICON_NAME = string.Empty;
        public string ICON_NAME
        {
            get { return _ICON_NAME; }
            set { _ICON_NAME = value; }
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

        //IsPrimary
        private bool _IsPrimary = false;
        public bool IsPrimary
        {
            get { return _IsPrimary; }
            set { _IsPrimary = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        //UpdatedDateDt
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        //BankTransferType
        private int _BankTransferType = (int)UserBankType.NPS;
        public int BankTransferType
        {
            get { return _BankTransferType; }
            set { _BankTransferType = value; }
        }
        //TransactionId
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        #endregion
    }
}