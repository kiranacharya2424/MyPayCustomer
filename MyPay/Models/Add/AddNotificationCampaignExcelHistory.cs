using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddNotificationCampaignExcelHistory: CommonAdd
    {
        #region "Enums"

        public enum NotificationCampaignStatus
        {
            Success = 1,
            Failure = 2,
            Cancelled = 3
        }


        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }
        #endregion

        #region "Properties"
        //NotificationCampaignExcelId
        private Int64 _NotificationCampaignExcelId = 0;
        public Int64 NotificationCampaignExcelId
        {
            get { return _NotificationCampaignExcelId; }
            set { _NotificationCampaignExcelId = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //SentStatus
        private int _SentStatus = 0;
        public int SentStatus
        {
            get { return _SentStatus; }
            set { _SentStatus = value; }
        }
        //SentStatusName
        private string _SentStatusName = string.Empty;
        public string SentStatusName
        {
            get { return _SentStatusName; }
            set { _SentStatusName = value; }
        }
        //NotificationType
        private int _NotificationType = 0;
        public int NotificationType
        {
            get { return _NotificationType; }
            set { _NotificationType = value; }
        }
        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //NotificationMessage
        private string _NotificationMessage = string.Empty;
        public string NotificationMessage
        {
            get { return _NotificationMessage; }
            set { _NotificationMessage = value; }
        }
        //NotificationDescription
        private string _NotificationDescription = string.Empty;
        public string NotificationDescription
        {
            get { return _NotificationDescription; }
            set { _NotificationDescription = value; }
        }
        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        //CreatedDatedt
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        //UpdatedDatedt
        private string _UpdatedDatedt = string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }
        }

        private ScheduleStatuses _EnumScheduleStatus = 0;
        public ScheduleStatuses EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
        }
        //SentDate
        private DateTime _SentDate = DateTime.UtcNow;
        public DateTime SentDate
        {
            get { return _SentDate; }
            set { _SentDate = value; }
        }
        #endregion
    }
}