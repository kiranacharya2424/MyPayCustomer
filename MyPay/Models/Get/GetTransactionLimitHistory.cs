using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetTransactionLimitHistory : CommonGet
    {
        #region "Properties"

        //TransactionTransferTypeEnum
        private Add.AddTransactionLimit.TransactionTransferTypeEnum _TransactionTransferTypeEnums = 0;
        public Add.AddTransactionLimit.TransactionTransferTypeEnum TransactionTransferTypeEnums
        {
            get { return _TransactionTransferTypeEnums; }
            set { _TransactionTransferTypeEnums = value; }
        }

        //TransactionLimitId
        private int _TransactionLimitId = 0;
        public int TransactionLimitId
        {
            get { return _TransactionLimitId; }
            set { _TransactionLimitId = value; }
        }
        #endregion
        #region GetMethods

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
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                //HT.Add("KycStatus", KycStatus);
                dt = obj.GetDataFromStoredProcedure("sp_TransactionLimit_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

        #endregion
    }
}