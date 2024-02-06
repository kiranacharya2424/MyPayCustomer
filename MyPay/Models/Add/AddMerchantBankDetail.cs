using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddMerchantBankDetail:CommonAdd
    {
        #region "Properties"
        //MerchantId
        private string _MerchantId = string.Empty;
        public string MerchantId
        {
            get { return _MerchantId; }
            set { _MerchantId = value; }
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

        //LinkedBankTransactionId
        private string _LinkedBankTransactionId = string.Empty;
        public string LinkedBankTransactionId
        {
            get { return _LinkedBankTransactionId; }
            set { _LinkedBankTransactionId = value; }
        }
        //LinkedBankToken
        private string _LinkedBankToken = string.Empty;
        public string LinkedBankToken
        {
            get { return _LinkedBankToken; }
            set { _LinkedBankToken = value; }
        }

        //IsVerified
        private bool _IsVerified = false;
        public bool IsVerified
        {
            get { return _IsVerified; }
            set { _IsVerified = value; }
        }
        public bool DataRecieved = false;

        public bool UpdateLinkedBankTransactionId(long Id, string TransactionId)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("TransactionId", TransactionId);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_MerchantLinkedBankTxnId_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }
        public bool UpdateLinkedBankToken(long Id, string token)
        {
            try
            {
                DataRecieved = false;
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("Id", Id);
                HT.Add("token", token);
                string ResultId = obj.ExecuteProcedureGetReturnValue("sp_MerchantLinkedBankToken_Update", HT);
                if (!string.IsNullOrEmpty(ResultId) && ResultId != "0")
                {
                    Id = Convert.ToInt64(ResultId);
                    DataRecieved = true;
                }
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return DataRecieved;
        }

        #endregion
    }
}