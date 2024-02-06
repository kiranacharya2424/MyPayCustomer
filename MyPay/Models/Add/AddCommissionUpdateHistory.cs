using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddCommissionUpdateHistory:CommonAdd
    {

        #region "Enums"

        public enum UpdatedStatuses
        {
            Updated = 1,
            Deleted = 2
        }
        
        #endregion
        #region "Properties"
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Kyc Type")]
        private int _KycType = 0;
        public int KycType
        {
            get { return _KycType; }
            set { _KycType = value; }
        }

        private string _KycTypeName = string.Empty;
        public string KycTypeName
        {
            get { return _KycTypeName; }
            set { _KycTypeName = value; }
        }

        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        private AddCommission.KycTypes _KycTypeEnum = 0;
        public AddCommission.KycTypes KycTypeEnum
        {
            get { return _KycTypeEnum; }
            set { _KycTypeEnum = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type")]
        private int _GenderType = 0;
        public int GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Gender Type Name")]
        private string _GenderTypeName = string.Empty;
        public string GenderTypeName
        {
            get { return _GenderTypeName; }
            set { _GenderTypeName = value; }
        }
        private AddCommission.GenderTypes _GenderTypeEnum = 0;
        public AddCommission.GenderTypes GenderTypeEnum
        {
            get { return _GenderTypeEnum; }
            set { _GenderTypeEnum = value; }
        }
        //MinimumAmount
        [Required(ErrorMessage = "Minimum Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min slab Amount")]
        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
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

        //MaximumAmount
        [Required(ErrorMessage = "Maximum  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max slab Amount")]
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }

        //FixedCommission
        [Required(ErrorMessage = "Fixed Commission  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Fixed Commission value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Fixed Amount")]
        private decimal _FixedCommission = 0;
        public decimal FixedCommission
        {
            get { return _FixedCommission; }
            set { _FixedCommission = value; }
        }

        //PercentageCommission
        [Required(ErrorMessage = "Percentage  Commission  Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Percentage Commission value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage Amount")]
        private decimal _PercentageCommission = 0;
        public decimal PercentageCommission
        {
            get { return _PercentageCommission; }
            set { _PercentageCommission = value; }
        }

        //PercentageRewardPoints
        [Required(ErrorMessage = "Percentage  RewardPoints  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter MP-Coins(%) Credit value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage RewardPoints")]
        private decimal _PercentageRewardPoints = 0;
        public decimal PercentageRewardPoints
        {
            get { return _PercentageRewardPoints; }
            set { _PercentageRewardPoints = value; }
        }

        //PercentageRewardPointsDebit
        [Required(ErrorMessage = "Percentage  RewardPoints  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter MP-Coins(%) Debit value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Percentage RewardPoints")]
        private decimal _PercentageRewardPointsDebit = 0;
        public decimal PercentageRewardPointsDebit
        {
            get { return _PercentageRewardPointsDebit; }
            set { _PercentageRewardPointsDebit = value; }
        }


        //MaximumAllowed
        [Required(ErrorMessage = "Maximum Allowed Amount is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum Allowed value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max Allowed Amount")]
        private decimal _MaximumAllowed = 0;
        public decimal MaximumAllowed
        {
            get { return _MaximumAllowed; }
            set { _MaximumAllowed = value; }
        }

        //MinimumAllowed
        [Required(ErrorMessage = "Minimum Allowed Amount is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum Allowed value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min Allowed Amount")]
        private decimal _MinimumAllowed = 0;
        public decimal MinimumAllowed
        {
            get { return _MinimumAllowed; }
            set { _MinimumAllowed = value; }
        }
        //ServiceCharge
        [Required(ErrorMessage = "Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Service Charge")]
        private decimal _ServiceCharge = 0;
        public decimal ServiceCharge
        {
            get { return _ServiceCharge; }
            set { _ServiceCharge = value; }
        }
        //FromDate
        [Required(ErrorMessage = "From Date is  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter From Date")]
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
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter To Date")]
        [Display(Name = "To Date")]
        private DateTime _ToDate = System.DateTime.UtcNow;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        //ServiceId
        private int _ServiceId = 0;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
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
        //ScheduleStatus
        private string _ScheduleStatus = String.Empty;
        public string ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }
        private string _ToDateDT = string.Empty;
        public string ToDateDT
        {
            get { return _ToDateDT; }
            set { _ToDateDT = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        //StatusName 
        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }
        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //CommissionId
        private Int64 _CommissionId = 0;
        public Int64 CommissionId
        {
            get { return _CommissionId; }
            set { _CommissionId = value; }
        }

        private UpdatedStatuses _StatusEnum = 0;
        public UpdatedStatuses StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }

        //EnumScheduleStatus
        private AddCommission.ScheduleStatuses _EnumScheduleStatus = 0;
        public AddCommission.ScheduleStatuses EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
        }

        //MaximumAllowedSC
        [Required(ErrorMessage = "Maximum Allowed Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Maximum Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Max Allowed Service Charge")]
        private decimal _MaximumAllowedSC = 0;
        public decimal MaximumAllowedSC
        {
            get { return _MaximumAllowedSC; }
            set { _MaximumAllowedSC = value; }
        }

        //MinimumAllowed
        [Required(ErrorMessage = "Minimum Allowed Service Charge is Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter Minimum Service Charge value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Min Allowed Amount")]
        private decimal _MinimumAllowedSC = 0;
        public decimal MinimumAllowedSC
        {
            get { return _MinimumAllowedSC; }
            set { _MinimumAllowedSC = value; }
        }

        #endregion


    }
}