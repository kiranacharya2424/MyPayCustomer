using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetRemittanceDashboard
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
                HT.Add("MerchantUniqueId", MerchantUniqueId);
                dt = obj.GetDataFromStoredProcedure("sp_RemittanceDashboard_Get", HT);
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