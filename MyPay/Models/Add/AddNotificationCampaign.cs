using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;
using MyPay.Models.VendorAPI.VendorRequest_CommonHelper;

namespace MyPay.Models.Add
{
    public class AddNotificationCampaign : CommonAdd
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
            All = -1,
            Verified = 1,
            NotVerified = 0
        }

        public enum OldUserStatus
        {
            All = 0,
            Old = 1,
            New = 2
        }
        public enum DeviceType
        {
            All = 0,
            Android = 1,
            IOS = 2
        }
        public enum GenderStatuss
        {
            All = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum SentStatuses
        {
            Pending = 0,
            Sent = 1,
            Progress = 2,
            Blocked = 3
        }
        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }
        /* public enum AgeGroup
         {
             All = 0,
             Child = 1,
             YoungAdult = 2,
             MiddleAged = 3,
             OlderAdult = 4,
         }*/
        public enum GeographyType
        {
            All = 0,
            Province = 1,
            District = 2

        }
        #endregion

        #region "Properties"
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
        private int _NotificationType = (int)VendorApi_CommonHelper.KhaltiAPIName.MyPay_Notification;
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
        //NotificationImage
        private string _NotificationImage = string.Empty;
        public string NotificationImage
        {
            get { return _NotificationImage; }
            set { _NotificationImage = value; }
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
        private KycStatuss _KycStatusEnum = KycStatuss.All;
        public KycStatuss KycStatusEnum
        {
            get { return _KycStatusEnum; }
            set { _KycStatusEnum = value; }
        }
        private SentStatuses _SentStatusEnum = 0;
        public SentStatuses SentStatusEnum
        {
            get { return _SentStatusEnum; }
            set { _SentStatusEnum = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Status")]
        private int _GenderStatus = 0;
        public int GenderStatus
        {
            get { return _GenderStatus; }
            set { _GenderStatus = value; }
        }

        //SentDate
        private DateTime _SentDate = DateTime.UtcNow;
        public DateTime SentDate
        {
            get { return _SentDate; }
            set { _SentDate = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "User Type Name")]
        private string _UserTypeName = string.Empty;
        public string UserTypeName
        {
            get { return _UserTypeName; }
            set { _UserTypeName = value; }
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
        private OldUserStatus _OldUserStatusEnum = 0;
        public OldUserStatus OldUserStatusEnum
        {
            get { return _OldUserStatusEnum; }
            set { _OldUserStatusEnum = value; }
        }
        private DeviceType _DeviceTypeEnum = 0;
        public DeviceType DeviceTypeEnum
        {
            get { return _DeviceTypeEnum; }
            set { _DeviceTypeEnum = value; }
        }

        /*  private AgeGroup _AgeGroupEnum = 0;
          public AgeGroup AgeGroupEnum
          {
              get { return _AgeGroupEnum; }
              set { _AgeGroupEnum = value; }
          }
  */
        //for Address
          private GeographyType _GeographyTypeEnum = 0;
          public GeographyType GeographyTypeEnum
          {
              get { return _GeographyTypeEnum; }
              set { _GeographyTypeEnum = value; }
          }
 

        private string _DeviceTypeStatusName = string.Empty;
        public string DeviceTypeStatusName
        {
            get { return _DeviceTypeStatusName; }
            set { _DeviceTypeStatusName = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "IsOldUserStatus")]
        private int _IsOldUserStatus = 0;
        public int IsOldUserStatus
        {
            get { return _IsOldUserStatus; }
            set { _IsOldUserStatus = value; }
        }

        /*  [DisplayFormat(ConvertEmptyStringToNull = false)]
          [Display(Name = "AgeGroup")]
          private int _AgeGroup = 0;
          public int AgeGroups
          {
              get { return _AgeGroup; }
              set { _AgeGroup = value; }
          }*/

        //for Address geography
        [DisplayFormat(ConvertEmptyStringToNull = false)]
         [Display(Name = "Geography Type")]
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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "DeviceTypeStatus")]
        private int _DeviceTypeStatus = 0;
        public int DeviceTypeStatus
        {
            get { return _DeviceTypeStatus; }
            set { _DeviceTypeStatus = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "OldUserStatus Name")]
        private string _IsOldUserStatusName = string.Empty;
        public string IsOldUserStatusName
        {
            get { return _IsOldUserStatusName; }
            set { _IsOldUserStatusName = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Total Notification Users")]
        private Int64 _TotalNotificationUsers = 0;
        public Int64 TotalNotificationUsers
        {
            get { return _TotalNotificationUsers; }
            set { _TotalNotificationUsers = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Total Notification Sent")]
        private Int64 _TotalNotificationSent = 0;
        public Int64 TotalNotificationSent
        {
            get { return _TotalNotificationSent; }
            set { _TotalNotificationSent = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "CurrentSkipIndex")]
        private int _CurrentSkipIndex = 0;
        public int CurrentSkipIndex
        {
            get { return _CurrentSkipIndex; }
            set { _CurrentSkipIndex = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "IsCompleted")]
        private int _IsCompleted = 0;
        public int IsCompleted
        {
            get { return _IsCompleted; }
            set { _IsCompleted = value; }
        }

        //NotificationRedirectType
        private Int32 _NotificationRedirectType = 0;
        public Int32 NotificationRedirectType
        {
            get { return _NotificationRedirectType; }
            set { _NotificationRedirectType = value; }
        }

        //NotificationRedirectTypeName
        private string _NotificationRedirectTypeName = string.Empty;
        public string NotificationRedirectTypeName
        {
            get { return _NotificationRedirectTypeName; }
            set { _NotificationRedirectTypeName = value; }
        }

        //ScheduleDateTime
        [Required(ErrorMessage = "Schedule Date is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Schedule Date")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Schedule Date")]
        private DateTime _ScheduleDateTime = System.DateTime.UtcNow;
        public DateTime ScheduleDateTime
        {
            get { return _ScheduleDateTime; }
            set { _ScheduleDateTime = value; }
        }

        private string _ScheduleDateTimeDT = string.Empty;
        public string ScheduleDateTimeDT
        {
            get { return _ScheduleDateTimeDT; }
            set { _ScheduleDateTimeDT = value; }
        }
        private string _URL = string.Empty;
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        #endregion

        #region GetMethods
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
                dt = obj.GetDataFromStoredProcedure("sp_NotificationCampaign_Datatable", HT);
            }
            catch (Exception ex)
            {
                //DataRecieved = false;
            }
            return dt;
        }
        public float CountNotificationCampaignCheck(string serviceId, int GenderStatus, int KycStatus, decimal MinimumAmount, decimal MaximumAmount, Int64 Id)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(Id) from NotificationCampaign with(nolock) where Id != '" + Id.ToString() + "' and   GenderStatus='" + GenderStatus + "' and  KycStatus='" + KycStatus + "'  and IsActive=1 and IsDeleted=0   ";
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