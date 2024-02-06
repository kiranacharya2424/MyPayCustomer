using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddNotification:CommonAdd
    {

        #region "Properties"
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //ReadStatus
        private int _ReadStatus = 0;
        public int ReadStatus
        {
            get { return _ReadStatus; }
            set { _ReadStatus = value; }
        }
        //ReadStatusName
        private string _ReadStatusName = string.Empty;
        public string ReadStatusName
        {
            get { return _ReadStatusName; }
            set { _ReadStatusName = value; }
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
        //NotificationRedirectType
        private int _NotificationRedirectType = 0;
        public int NotificationRedirectType
        {
            get { return _NotificationRedirectType; }
            set { _NotificationRedirectType = value; }
        }


        //NotificationImage
        private string _NotificationImage = string.Empty;
        public string NotificationImage
        {
            get { return _NotificationImage; }
            set { _NotificationImage = value; }
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

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
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
                HT.Add("MemberId", MemberId);
                HT.Add("StartDate", StartDate);
                HT.Add("EndDate", EndDate);
                dt = obj.GetDataFromStoredProcedure("sp_Notification_Datatable", HT);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Common.CommonHelpers obj1 = new Common.CommonHelpers();
                    DataTable dtCounter = obj1.GetDataFromStoredProcedure("sp_Notification_DatatableCounter", HT);
                    if (dtCounter != null && dtCounter.Rows.Count > 0)
                    {
                        dt.Rows[0]["FilterTotalCount"] = dtCounter.Rows[0]["FilterTotalCount"].ToString();
                        //dt.Rows[0]["TotalSuccess"] = dtCounter.Rows[0]["TotalSuccess"].ToString();
                        //dt.Rows[0]["TotalPending"] = dtCounter.Rows[0]["TotalPending"].ToString();
                        //dt.Rows[0]["TotalFailed"] = dtCounter.Rows[0]["TotalFailed"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }

    }
}