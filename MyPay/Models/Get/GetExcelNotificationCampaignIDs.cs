using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetExcelNotificationCampaignIDs: CommonGet
    {
        #region "Properties"

        //NotificationCampaignID
        private Int64 _NotificationCampaignID = 0;
        public Int64 NotificationCampaignID
        {
            get { return _NotificationCampaignID; }
            set { _NotificationCampaignID = value; }
        }

        private string _ScheduleDateTime = "";
        public string ScheduleDateTime
        {
            get { return _ScheduleDateTime; }
            set { _ScheduleDateTime = value; }
        }


        #endregion
    }
}