using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddTransactionLimit : CommonAdd
    {
        #region Enum
        public enum TransactionTransferTypeEnum
        {
            Load_Wallet_From_Bank = 1,
            Pay_And_Deposit_From_Linked_Bank = 2,
            Bank_Transfer_From_Wallet = 3,
            Wallet_To_Wallet_Transfer = 4,
            Load_via_Card = 5
        }
        public enum KycTypes
        {
            NotVerified = 0,
            Verified = 1
        }
        #endregion


        #region Properties
        //TransferLimitPerTransaction
        private decimal _TransferLimitPerTransaction = 0;
        public decimal TransferLimitPerTransaction
        {
            get { return _TransferLimitPerTransaction; }
            set { _TransferLimitPerTransaction = value; }
        }

        //TransferLimitPerDay
        private decimal _TransferLimitPerDay = 0;
        public decimal TransferLimitPerDay
        {
            get { return _TransferLimitPerDay; }
            set { _TransferLimitPerDay = value; }
        }

        //TransferLimitPerMonth
        private decimal _TransferLimitPerMonth = 0;
        public decimal TransferLimitPerMonth
        {
            get { return _TransferLimitPerMonth; }
            set { _TransferLimitPerMonth = value; }
        }

        //TransactionTransferTypeName
        private string _TransactionTransferTypeName = string.Empty;
        public string TransactionTransferTypeName
        {
            get { return _TransactionTransferTypeName; }
            set { _TransactionTransferTypeName = value; }
        }
        //TransactionTransferType
        private int _TransactionTransferType = 0;
        public int TransactionTransferType
        {
            get { return _TransactionTransferType; }
            set { _TransactionTransferType = value; }
        }
        private KycTypes _KycTypeEnum = 0;
        public KycTypes KycTypeEnum
        {
            get { return _KycTypeEnum; }
            set { _KycTypeEnum = value; }
        }
        //KycStatus
        private int _KycStatus = 0;
        public int KycStatus
        {
            get { return _KycStatus; }
            set { _KycStatus = value; }
        }
        //KycStatusName
        private string _KycStatusName = string.Empty;
        public string KycStatusName
        {
            get { return _KycStatusName; }
            set { _KycStatusName = value; }
        }
        //TransferLimitPerMonthTransactionCount
        private Int64 _TransferLimitPerMonthTransactionCount = 0;
        public Int64 TransferLimitPerMonthTransactionCount
        {
            get { return _TransferLimitPerMonthTransactionCount; }
            set { _TransferLimitPerMonthTransactionCount = value; }
        }


        //TransferLimitPerDayTransactionCount
        private Int64 _TransferLimitPerDayTransactionCount = 0;
        public Int64 TransferLimitPerDayTransactionCount
        {
            get { return _TransferLimitPerDayTransactionCount; }
            set { _TransferLimitPerDayTransactionCount = value; }
        }

        //TransactionTransferTypeEnum
        private TransactionTransferTypeEnum _TransactionTransferTypeEnums = 0;
        public TransactionTransferTypeEnum TransactionTransferTypeEnums
        {
            get { return _TransactionTransferTypeEnums; }
            set { _TransactionTransferTypeEnums = value; }
        }

        //UpdatedDateDt
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        #endregion

        #region GetMethods
        public string GetTransactionLimit(Int32 ServiceTypeId, Int32 TransactionTransferType, Int64 MemberId, decimal TransactionAmount, int Sign = 0)
        {
            string ReturnMessage = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("TransactionTransferType", TransactionTransferType.ToString());
                HT.Add("MemberId", MemberId.ToString());
                HT.Add("ServiceTypeId", ServiceTypeId.ToString());
                HT.Add("TransactionAmount", TransactionAmount.ToString());
                HT.Add("SignStatus", Sign.ToString());
                dt = obj.GetDataFromStoredProcedure(Common.Common.StoreProcedures.sp_TransactionLimitCheck_Get, HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ReturnMessage = dt.Rows[0]["UserMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return ReturnMessage;
        }

        #endregion

    }
}