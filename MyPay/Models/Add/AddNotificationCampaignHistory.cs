using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddNotificationCampaignHistory : CommonAdd
    {
        #region "Enums"

        public enum NotificationCampaignStatus
        {
            Success = 1,
            Failure = 2,
            Cancelled = 3
        }
        public enum KycStatuss
        {
            All = 0,
            Verified = 1
        }

        public enum GenderStatuss
        {
            All = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }
        #endregion

        #region "Properties"
        //NotificationCampaignId
        private Int64 _NotificationCampaignId = 0;
        public Int64 NotificationCampaignId
        {
            get { return _NotificationCampaignId; }
            set { _NotificationCampaignId = value; }
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
         
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Kyc Type")]
        private int _KycStatus = 0;
        public int KycStatus
        {
            get { return _KycStatus; }
            set { _KycStatus = value; }
        }

        private string _KycStatusName = string.Empty;
        public string KycStatusName
        {
            get { return _KycStatusName; }
            set { _KycStatusName = value; }
        }
        private KycStatuss _KycStatusEnum = 0;
        public KycStatuss KycStatusEnum
        {
            get { return _KycStatusEnum; }
            set { _KycStatusEnum = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type")]
        private int _GenderStatus = 0;
        public int GenderStatus
        {
            get { return _GenderStatus; }
            set { _GenderStatus = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type Name")]
        private string _GenderStatusName = string.Empty;
        public string GenderStatusName
        {
            get { return _GenderStatusName; }
            set { _GenderStatusName = value; }
        }
        private GenderStatuss _GenderStatusEnum = 0;
        public GenderStatuss GenderStatusEnum
        {
            get { return _GenderStatusEnum; }
            set { _GenderStatusEnum = value; }
        }
        private ScheduleStatuses _EnumScheduleStatus = 0;
        public ScheduleStatuses EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "IsOldUserStatus")]
        private int _IsOldUserStatus = 0;
        public int IsOldUserStatus
        {
            get { return _IsOldUserStatus; }
            set { _IsOldUserStatus = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "OldUserStatus Name")]
        private string _IsOldUserStatusName = string.Empty;
        public string IsOldUserStatusName
        {
            get { return _IsOldUserStatusName; }
            set { _IsOldUserStatusName = value; }
        }
        #endregion

        #region GetMethods
       
        public float CountNotificationCampaignCheck(string serviceId, int GenderStatus, int KycStatus, decimal MinimumAmount, decimal MaximumAmount, Int64 Id)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Id) from NotificationCampaign with(nolock) where Id != '" + Id.ToString() + "' and  ServiceId='" + serviceId + "' and  GenderStatus='" + GenderStatus + "' and  KycStatus='" + KycStatus + "' and ((MinimumAmount between " + MinimumAmount + " and " + MaximumAmount + " ) or ((maximumamount between " + MinimumAmount + " and " + MaximumAmount + " ))) and IsActive=1 and IsDeleted=0 and cast(ToDate as date) > cast(getdate() as date ) ";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (float.Parse(Result));
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        #endregion
    }

}