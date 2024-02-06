using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    public class TransactionLimits
    {
        private long _Id;
        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
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

        //KycStatus
        private int _KycStatus = 0;
        public int KycStatus
        {
            get { return _KycStatus; }
            set { _KycStatus = value; }
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
        private Add.AddTransactionLimit.TransactionTransferTypeEnum _TransactionTransferTypeEnums = 0;
        public Add.AddTransactionLimit.TransactionTransferTypeEnum TransactionTransferTypeEnums
        {
            get { return _TransactionTransferTypeEnums; }
            set { _TransactionTransferTypeEnums = value; }
        }

        private System.DateTime _CreatedDate = System.DateTime.UtcNow;
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private System.DateTime _UpdatedDate = System.DateTime.UtcNow;
        public DateTime UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }
        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //CreatedBy
        private int _CreatedBy = 0;// (int)clsData.BooleanValue.Both;
        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }
        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }
        public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        {
            DataTable dt = new DataTable();
            try
            {
                Common.CommonHelpers obj = new Common.CommonHelpers();
                Hashtable HT = new Hashtable();
                HT.Add("SearchText", SearchText);
                HT.Add("PagingSize", PagingSize);
                HT.Add("OffsetValue", OffsetValue);
                HT.Add("sortColumn", sortColumn);
                HT.Add("sortOrder", sortOrder);
                HT.Add("KycType", KycStatus);
                HT.Add("CheckDelete", CheckDelete);
                dt = obj.GetDataFromStoredProcedure(Common.Common.StoreProcedures.sp_TransactionLimit_Datatable, HT);
            }
            catch (Exception ex)
            {
                DataRecieved = false;
            }
            return dt;
        }

    }
}