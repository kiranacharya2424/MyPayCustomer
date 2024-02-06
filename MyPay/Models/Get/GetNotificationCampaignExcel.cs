using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetNotificationCampaignExcel:CommonGet
    {
        #region "Properties"

        //MemberId
        private Int64 _MemberId = 0;// (int)clsData.BooleanValue.Both;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //CheckSentStatus
        private int _CheckSentStatus = -1;
        public int CheckSentStatus
        {
            get { return _CheckSentStatus; }
            set { _CheckSentStatus = value; }
        }
        private int _CheckCompleted = 2;
        public int CheckCompleted
        {
            get { return _CheckCompleted; }
            set { _CheckCompleted = value; }
        }

        //CheckDeviceTypeStatus
        private int _CheckDeviceTypeStatus = 0;
        public int CheckDeviceTypeStatus
        {
            get { return _CheckDeviceTypeStatus; }
            set { _CheckDeviceTypeStatus = value; }
        }

        //CheckRedirectType
        private int _CheckRedirectType = 0;
        public int CheckRedirectType
        {
            get { return _CheckRedirectType; }
            set { _CheckRedirectType = value; }
        }
        private string _ScheduleDateTime = "";
        public string ScheduleDateTime
        {
            get { return _ScheduleDateTime; }
            set { _ScheduleDateTime = value; }
        }
        //CheckGenderStatus
        private int _CheckGenderStatus = -1;
        public int CheckGenderStatus
        {
            get { return _CheckGenderStatus; }
            set { _CheckGenderStatus = value; }
        }

        //CheckOldUserStatus
        private int _CheckOldUserStatus = -1;
        public int CheckOldUserStatus
        {
            get { return _CheckOldUserStatus; }
            set { _CheckOldUserStatus = value; }
        }

        //CheckKycStatus
        private int _CheckKycStatus = -1;
        public int CheckKycStatus
        {
            get { return _CheckKycStatus; }
            set { _CheckKycStatus = value; }
        }
        #endregion

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
                HT.Add("CreatedBy", CreatedBy);
                HT.Add("CheckSentStatus", CheckSentStatus);
                HT.Add("CheckDeviceTypeStatus", CheckDeviceTypeStatus);
                HT.Add("CheckRedirectType", CheckRedirectType);
                dt = obj.GetDataFromStoredProcedure("sp_NotificationCampaignExcel_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
    }
}