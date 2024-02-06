using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetNotificationCampaign : CommonGet
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
        //CheckKycStatus
        private int _CheckKycStatus = -1;
        public int CheckKycStatus
        {
            get { return _CheckKycStatus; }
            set { _CheckKycStatus = value; }
        }

        //CheckDeviceTypeStatus
        private int _CheckDeviceTypeStatus = 0;
        public int CheckDeviceTypeStatus
        {
            get { return _CheckDeviceTypeStatus; }
            set { _CheckDeviceTypeStatus = value; }
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
          private string _URL;
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        //for address and geography
        private int _Geography = 0;
        public int Geography
        {
            get { return _Geography; }
            set { _Geography = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Province Type")]
        private string _Province;
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "District Type")]
        private string _District;
        public string District
        {
            get { return _District; }
            set { _District = value; }
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
                HT.Add("CheckKycStatus", CheckKycStatus);
                HT.Add("CheckGenderStatus", CheckGenderStatus);
                HT.Add("CheckOldUserStatus", CheckOldUserStatus);
                HT.Add("CheckDeviceTypeStatus", CheckDeviceTypeStatus);
                HT.Add("CheckRedirectType", CheckRedirectType);
               

                dt = obj.GetDataFromStoredProcedure("sp_NotificationCampaign_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
    }
}