using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetNotificationCampaignIDs : CommonGet
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
        private string _Province = "";
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }
        private string _District = "";
        public string District
        {
            get { return _District; }
            set { _District = value; }
        }


        #endregion
    }
}