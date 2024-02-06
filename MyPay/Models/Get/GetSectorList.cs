using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class GetSectorList
    {
        #region "Properties"
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _SectorCode = string.Empty;
        public string SectorCode
        {
            get { return _SectorCode; }
            set { _SectorCode = value; }
        }
        private string _SectorName = string.Empty;
        public string SectorName
        {
            get { return _SectorName; }
            set { _SectorName = value; }
        }
        private int _CheckActive = 2;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        //Take
        private int _Take = 0;
        public int Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        //Skip
        private int _Skip = 0;
        public int Skip
        {
            get { return _Skip; }
            set { _Skip = value; }
        }
        private Int32 _RoleId = 0;
        public Int32 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        #endregion

        //public DataTable GetData(string sortColumn, string sortOrder, int OffsetValue, int PagingSize, string SearchText)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Common.CommonHelpers obj = new Common.CommonHelpers();
        //        Hashtable HT = new Hashtable();
        //        HT.Add("SearchText", SearchText);
        //        HT.Add("PagingSize", PagingSize);
        //        HT.Add("OffsetValue", OffsetValue);
        //        HT.Add("sortColumn", sortColumn);
        //        HT.Add("sortOrder", sortOrder);
        //        HT.Add("CheckActive", CheckActive);
        //        dt = obj.GetDataFromStoredProcedure("sp_AirlinesList_Datatable", HT);
        //    }
        //    catch (Exception ex)
        //    {
        //        //DataRecieved = false;
        //    }
        //    return dt;
        //}

    }
}