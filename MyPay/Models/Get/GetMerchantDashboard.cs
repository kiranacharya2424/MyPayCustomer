using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetMerchantDashboard 
    {
        #region "Properties"

        //Type
        private string _Type = string.Empty;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private string _MerchantUniqueId = string.Empty;
        public string MerchantUniqueId
        {
            get { return _MerchantUniqueId; }
            set { _MerchantUniqueId = value; }
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
                HT.Add("Type", Type);
                dt = obj.GetDataFromStoredProcedure("sp_MerchantDashboard_Get", HT);
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