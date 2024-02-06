using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddTransactionLimitHistory : CommonAdd
    {

        #region Properties
        //TransferLimitPerTransaction
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

        //TransactionTransferType
        private int _TransactionTransferType = 0;
        public int TransactionTransferType
        {
            get { return _TransactionTransferType; }
            set { _TransactionTransferType = value; }
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
        private AddTransactionLimit.TransactionTransferTypeEnum _TransactionTransferTypeEnums = 0;
        public AddTransactionLimit.TransactionTransferTypeEnum TransactionTransferTypeEnums
        {
            get { return _TransactionTransferTypeEnums; }
            set { _TransactionTransferTypeEnums = value; }
        }

        //TransactionLimitId
        private Int64 _TransactionLimitId = 0;
        public Int64 TransactionLimitId
        {
            get { return _TransactionLimitId; }
            set { _TransactionLimitId = value; }
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


        //StatusName
        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }


        //TransferLimitTypeName
        private string _TransferLimitTypeName = string.Empty;
        public string TransferLimitTypeName
        {
            get { return _TransferLimitTypeName; }
            set { _TransferLimitTypeName = value; }
        }

        private AddTransactionLimit.KycTypes _KycTypeEnum = 0;
        public AddTransactionLimit.KycTypes KycTypeEnum
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
        #endregion
    }
}