using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class GetMarque : CommonGet
    {
        #region "Properties"

        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //Priority
        private int _Priority = 0;

        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
        private int _MarqueFor = 0;

        public int MarqueFor
        {
            get { return _MarqueFor; }
            set { _MarqueFor = value; }
        }

        //Description
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //UpdatedByName
        private string _UpdatedByName = string.Empty;
        public string UpdatedByName
        {
            get { return _UpdatedByName; }
            set { _UpdatedByName = value; }
        }
        private Int64 _UpdatedBy = 0;
        public Int64 UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }

        private string _Link = string.Empty;
        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
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
                dt = obj.GetDataFromStoredProcedure("sp_Marque_Datatable", HT);
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