using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class GetBalanceHistory : CommonGet
    {
        #region "Properties"
        //TotalBalance 
        private decimal _TotalBalance = 0;
        public decimal TotalBalance
        {
            get { return _TotalBalance; }
            set { _TotalBalance = value; }
        }

        //Type 
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //CheckTodayDate 
        private string _CheckTodayDate = string.Empty;
        public string CheckTodayDate
        {
            get { return _CheckTodayDate; }
            set { _CheckTodayDate = value; }
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
                HT.Add("Type", Type);
                dt = obj.GetDataFromStoredProcedure("sp_BalanceHistory_Datatable", HT);
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