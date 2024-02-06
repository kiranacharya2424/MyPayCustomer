using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetNotificationCampaignHistory : CommonGet
    {
        #region "Properties"

        //CheckSentStatus
        private int _CheckSentStatus = 0;
        public int CheckSentStatus
        {
            get { return _CheckSentStatus; }
            set { _CheckSentStatus = value; }
        }

        //CheckKycStatus
        private int _CheckKycStatus = 0;
        public int CheckKycStatus
        {
            get { return _CheckKycStatus; }
            set { _CheckKycStatus = value; }
        }


        //CheckGenderStat
        private int _CheckGenderStatus = 0;
        public int CheckGenderStatus
        {
            get { return _CheckGenderStatus; }
            set { _CheckGenderStatus = value; }
        }

        //CheckOldUserSta
        private int _CheckOldUserStatus = 0;
        public int CheckOldUserStatus
        {
            get { return _CheckOldUserStatus; }
            set { _CheckOldUserStatus = value; }
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
                dt = obj.GetDataFromStoredProcedure("sp_NotificationCampaignHistory_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

    }
}