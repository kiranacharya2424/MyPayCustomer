using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddDealsandOffers: CommonAdd
    {
        #region "Enums"

        public enum CouponStatusEnum
        {
            Success = 1,
            Failure = 2,
            Cancelled = 3
        }

        public enum CouponTypeEnum
        {
            Coupon = 1,
            Wallet = 2,
            MPCoins = 3,
            BetterLuck = 4,
        }

        public enum KycStatussEnum
        {
            All = 0,
            Verified = 1
        }

        public enum GenderStatussEnum
        {
            All = 0,
            Male = 1,
            Female = 2,
            Other = 3
        }

        public enum ScheduleStatusesEnum
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }

        public enum CouponReceivedBy
        {
            Transaction = 1,
            SignUp = 2,
            EmailVerify = 3,
            Voting = 4,
            KYC = 5
        }
        #endregion
        #region "Properties"



        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }



        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        //FromDate
        [Required(ErrorMessage = "From Date is  Required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "From Date")]
        private DateTime _FromDate = System.DateTime.UtcNow;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        //ToDate
        [Required(ErrorMessage = "To Date is  Required")]
        [Display(Name = "To Date")]
        private DateTime _ToDate = System.DateTime.UtcNow;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        //ScheduledDate
        [Required(ErrorMessage = "ScheduledDate is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter ScheduledDate Date")]
        [Display(Name = "ScheduledDate Date")]
        private DateTime _ScheduledDate = System.DateTime.UtcNow;
        public DateTime ScheduledDate
        {
            get { return _ScheduledDate; }
            set { _ScheduledDate = value; }
        }


        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Kyc Status")]
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

        private KycStatussEnum _KycStatusEnum = 0;
        public KycStatussEnum KycStatusEnum
        {
            get { return _KycStatusEnum; }
            set { _KycStatusEnum = value; }
        }
        private int _CouponType = 0;
        public int CouponType
        {
            get { return _CouponType; }
            set { _CouponType = value; }
        }
        private string _CouponTypeName = string.Empty;
        public string CouponTypeName
        {
            get { return _CouponTypeName; }
            set { _CouponTypeName = value; }
        }
        private CouponTypeEnum _CouponTypesEnum = 0;
        public CouponTypeEnum CouponTypesEnum
        {
            get { return _CouponTypesEnum; }
            set { _CouponTypesEnum = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Status")]
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
        private GenderStatussEnum _GenderStatusEnum = 0;
        public GenderStatussEnum GenderStatusEnum
        {
            get { return _GenderStatusEnum; }
            set { _GenderStatusEnum = value; }
        }


        //Amount
        [Required(ErrorMessage = "Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Amount")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Amount")]
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //CouponPercentage
        [Required(ErrorMessage = "Coupon Percentage  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter CouponPercentage")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "CouponPercentage")]
        private decimal _CouponPercentage = 0;
        public decimal CouponPercentage
        {
            get { return _CouponPercentage; }
            set { _CouponPercentage = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "CouponsCount")]
        private int _CouponsCount = 0;
        public int CouponsCount
        {
            get { return _CouponsCount; }
            set { _CouponsCount = value; }
        }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "CouponsUsedCount")]
        private int _CouponsUsedCount = 0;
        public int CouponsUsedCount
        {
            get { return _CouponsUsedCount; }
            set { _CouponsUsedCount = value; }
        }
        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        //UpdatedDateDt
        private string _UpdatedDateDt = string.Empty;
        public string UpdatedDateDt
        {
            get { return _UpdatedDateDt; }
            set { _UpdatedDateDt = value; }
        }

        //ServiceId
        private int _ServiceId = 0;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

        //ScheduleStatus
        private string _ScheduleStatus = String.Empty;
        public string ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }

        //ServiceName
        private string _ServiceName = String.Empty;
        public string ServiceName
        {
            get { return _ServiceName; }
            set { _ServiceName = value; }
        }
        private string _FromDateDT = string.Empty;
        public string FromDateDT
        {
            get { return _FromDateDT; }
            set { _FromDateDT = value; }
        }
        private string _ToDateDT = string.Empty;
        public string ToDateDT
        {
            get { return _ToDateDT; }
            set { _ToDateDT = value; }
        }
        private string _UpdateIndiaDate = string.Empty;
        public string UpdateIndiaDate
        {
            get { return _UpdateIndiaDate; }
            set { _UpdateIndiaDate = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //EnumScheduleStatus
        private ScheduleStatusesEnum _EnumScheduleStatus = 0;
        public ScheduleStatusesEnum EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
        }

        //TransactionCount
        private Int64 _TransactionCount = 0;
        public Int64 TransactionCount
        {
            get { return _TransactionCount; }
            set { _TransactionCount = value; }
        }

        //TransactionVolume
        private decimal _TransactionVolume = 0;
        public decimal TransactionVolume
        {
            get { return _TransactionVolume; }
            set { _TransactionVolume = value; }
        }

        //IsOneTime
        private int _IsOneTime = 0;
        public int IsOneTime
        {
            get { return _IsOneTime; }
            set { _IsOneTime = value; }
        }

        //IsOneTime
        private int _IsOneTimePerDay = 0;
        public int IsOneTimePerDay
        {
            get { return _IsOneTimePerDay; }
            set { _IsOneTimePerDay = value; }
        }



        //_ApplyType
        private int _ApplyType = 0;
        public int ApplyType
        {
            get { return _ApplyType; }
            set { _ApplyType = value; }
        }
        //_ApplyTypeName
        private string _ApplyTypeName = "";
        public string ApplyTypeName
        {
            get { return _ApplyTypeName; }
            set { _ApplyTypeName = value; }
        }
        //EnumApplyType
        private CouponReceivedBy _EnumApplyType = 0;
        public CouponReceivedBy EnumApplyType
        {
            get { return _EnumApplyType; }
            set { _EnumApplyType = value; }
        }
        //MinimumAmount
        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
        }

        //_MaximumAmount
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }

        //_PromoCode
        private string _PromoCode = string.Empty;
        public string PromoCode
        {
            get { return _PromoCode; }
            set { _PromoCode = value; }
        }

        //_Image
        private string _Image =string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }
        private string _CoupounValue = string.Empty;
        public string CouponValue { get { return _CoupounValue; } set { _CoupounValue = value; } }

        private string _CouponQuantity = string.Empty;
        public string CouponQuantity { get { return _CouponQuantity; } set { _CouponQuantity = value; } }


        #endregion
    }
}