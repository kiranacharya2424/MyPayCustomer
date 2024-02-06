using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static MyPay.Models.Add.AddSettings;

namespace MyPay.Models.Add
{
    public class AddSettingsHistory:CommonAdd
    {
        #region "Properties"
        [Required(ErrorMessage = "Registration Commission Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //RegistrationCommission
        private decimal _RegistrationCommission = 0;
        public decimal RegistrationCommission
        {
            get { return _RegistrationCommission; }
            set { _RegistrationCommission = value; }
        }
        [Required(ErrorMessage = "SignUpBonus Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //SignUpBonus
        private decimal _SignUpBonus = 0;
        public decimal SignUpBonus
        {
            get { return _SignUpBonus; }
            set { _SignUpBonus = value; }
        }
        [Required(ErrorMessage = "KYC Commission Required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        //KYCCommission
        private decimal _KYCCommission = 0;
        public decimal KYCCommission
        {
            get { return _KYCCommission; }
            set { _KYCCommission = value; }
        }
        [Required(ErrorMessage = "Transaction Commission Required")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        //TransactionCommission
        private decimal _TransactionCommission = 0;
        public decimal TransactionCommission
        {
            get { return _TransactionCommission; }
            set { _TransactionCommission = value; }
        }
        [Required(ErrorMessage = "Registration Reward Point Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //RegistrationRewardPoint
        private decimal _RegistrationRewardPoint = 0;
        public decimal RegistrationRewardPoint
        {
            get { return _RegistrationRewardPoint; }
            set { _RegistrationRewardPoint = value; }
        }
        [Required(ErrorMessage = "SignUpBonus Reward Point Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //SignUpBonusRewardPoint
        private decimal _SignUpBonusRewardPoint = 0;
        public decimal SignUpBonusRewardPoint
        {
            get { return _SignUpBonusRewardPoint; }
            set { _SignUpBonusRewardPoint = value; }
        }
        [Required(ErrorMessage = "KYC Reward Point Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //KYCRewardPoint
        private decimal _KYCRewardPoint = 0;
        public decimal KYCRewardPoint
        {
            get { return _KYCRewardPoint; }
            set { _KYCRewardPoint = value; }
        }
        [Required(ErrorMessage = "Transaction Reward Point Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //TransactionRewardPoint
        private decimal _TransactionRewardPoint = 0;
        public decimal TransactionRewardPoint
        {
            get { return _TransactionRewardPoint; }
            set { _TransactionRewardPoint = value; }
        }

        //SettingsId
        private Int64 _SettingsId = 0;
        public Int64 SettingsId
        {
            get { return _SettingsId; }
            set { _SettingsId = value; }
        }
          
        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        [Required(ErrorMessage = "MinAmountTransactionCommission Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //TransactionRewardPoint
        private decimal _MinAmountTransactionCommission = 0;
        public decimal MinAmountTransactionCommission
        {
            get { return _MinAmountTransactionCommission; }
            set { _MinAmountTransactionCommission = value; }
        }

        [Required(ErrorMessage = "MaxAmountTransactionCommission Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //MaxAmountTransactionCommission
        private decimal _MaxAmountTransactionCommission = 0;
        public decimal MaxAmountTransactionCommission
        {
            get { return _MaxAmountTransactionCommission; }
            set { _MaxAmountTransactionCommission = value; }
        }
        [Required(ErrorMessage = "MinRewardPointTransactionCommission Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //MinRewardPointTransactionCommission
        private decimal _MinRewardPointTransactionCommission = 0;
        public decimal MinRewardPointTransactionCommission
        {
            get { return _MinRewardPointTransactionCommission; }
            set { _MinRewardPointTransactionCommission = value; }
        }
        [Required(ErrorMessage = "MaxRewardPointTransactionCommission Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter numeric value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        //MaxRewardPointTransactionCommission
        private decimal _MaxRewardPointTransactionCommission = 0;
        public decimal MaxRewardPointTransactionCommission
        {
            get { return _MaxRewardPointTransactionCommission; }
            set { _MaxRewardPointTransactionCommission = value; }
        }

        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private ReferType _TypeEnum = 0;
        public ReferType TypeEnum
        {
            get { return _TypeEnum; }
            set { _TypeEnum = value; }
        }
        private int _GenderType = 0;
        public int GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }

        private string _GenderTypeName = string.Empty;
        public string GenderTypeName
        {
            get { return _GenderTypeName; }
            set { _GenderTypeName = value; }
        }
        private GenderTypes _GenderTypeEnum = 0;
        public GenderTypes GenderTypeEnum
        {
            get { return _GenderTypeEnum; }
            set { _GenderTypeEnum = value; }
        }
        private int _IsKycApproved = 0;
        public int IsKycApproved
        {
            get { return _IsKycApproved; }
            set { _IsKycApproved = value; }
        }

        //KYCStatusName
        private string _KYCStatusName = string.Empty;
        public string KYCStatusName
        {
            get { return _KYCStatusName; }
            set { _KYCStatusName = value; }
        }

        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }
        #endregion
    }
}
