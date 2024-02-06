using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddNotificationCampaignIDs:CommonAdd
    {

        #region "Properties"
        //MemberId
        private Int64 _NotificationCampaignId = 0;
        public Int64 NotificationCampaignId
        {
            get { return _NotificationCampaignId; }
            set { _NotificationCampaignId = value; }
        }

        //OffsetValue
        private int _OffsetValue = 0;
        public int OffsetValue
        {
            get { return _OffsetValue; }
            set { _OffsetValue = value; }
        }
        //ReadStatusName
        private int _PagingSize = 0;
        public int PagingSize
        {
            get { return _PagingSize; }
            set { _PagingSize = value; }
        }
        //_Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        //NotificationMessage
        private int _NotificationMessage = 0;
        public int NotificationMessage
        {
            get { return _NotificationMessage; }
            set { _NotificationMessage = value; }
        }
        //NotificationDeviceIDs
        private string _NotificationDeviceIDs = string.Empty;
        public string NotificationDeviceIDs
        {
            get { return _NotificationDeviceIDs; }
            set { _NotificationDeviceIDs = value; }
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

        //FireBaseRequest
        private string _FireBaseRequest = string.Empty;
        public string FireBaseRequest
        {
            get { return _FireBaseRequest; }
            set { _FireBaseRequest = value; }
        }
        //FireBaseResponse
        private string _FireBaseResponse = string.Empty;
        public string FireBaseResponse
        {
            get { return _FireBaseResponse; }
            set { _FireBaseResponse = value; }
        }
        private DateTime _ScheduleDateTime = System.DateTime.UtcNow;
        public DateTime ScheduleDateTime
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