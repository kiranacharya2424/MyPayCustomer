using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetUsersDeviceRegistration : CommonGet
    {
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }
        private Int32 _SequenceNo = 0;
        public Int32 SequenceNo
        {
            get { return _SequenceNo; }
            set { _SequenceNo = value; }
        }
        private string _IMIE = string.Empty;
        public string IMIE
        {
            get { return _IMIE; }
            set { _IMIE = value; }
        }
        private string _PlatForm = string.Empty;
        public string PlatForm
        {
            get { return _PlatForm; }
            set { _PlatForm = value; }
        }

        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
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
                HT.Add("MemberId", MemberId);
                HT.Add("CheckActive", CheckActive);
                HT.Add("CheckApprovedByAdmin", CheckApprovedByAdmin);
                HT.Add("CheckDelete", CheckDelete);
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CreatedByName", CreatedByName);
                HT.Add("IMIE", IMIE);
                HT.Add("DeviceCode", DeviceCode);
                HT.Add("PlatForm", PlatForm);
                HT.Add("SequenceNo", SequenceNo);
                HT.Add("IpAddress", IpAddress);
                dt = obj.GetDataFromStoredProcedure("sp_UsersDeviceRegistration_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

    }
}