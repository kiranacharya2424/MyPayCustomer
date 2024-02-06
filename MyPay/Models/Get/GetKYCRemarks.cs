using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetKYCRemarks : CommonGet
    {
        #region "Properties"
        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
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
                HT.Add("Title", Title);
                HT.Add("Id", Id);
                dt = obj.GetDataFromStoredProcedure("sp_KYCRemarks_Datatable", HT);
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