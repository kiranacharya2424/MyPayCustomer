using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddCouponsScratched : CommonAdd
    {

        #region "Properties"

        //CouponId
        private int _CouponId = 0;
        public int CouponId
        {
            get { return _CouponId; }
            set { _CouponId = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        

        //MemberName
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }
        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }
        //CouponCode
        private string _CouponCode = string.Empty;
        public string CouponCode
        {
            get { return _CouponCode; }
            set { _CouponCode = value; }
        }
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
        //CouponAmount
        [Required(ErrorMessage = "Coupon Amount  Required")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter CouponAmount")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "CouponAmount")]
        private decimal _CouponAmount = 0;
        public decimal CouponAmount
        {
            get { return _CouponAmount; }
            set { _CouponAmount = value; }
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
        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        //CouponType
        private int _CouponType = 0;
        public int CouponType
        {
            get { return _CouponType; }
            set { _CouponType = value; }
        } //CouponType
        
        private string _CouponTypeName = string.Empty;
        public string CouponTypeName
        {
            get { return _CouponTypeName; }
            set { _CouponTypeName = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
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
        //IsUsed
        private int _IsUsed = 0;
        public int IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }

        //IsExpired
        private int _IsExpired = 0;
        public int IsExpired
        {
            get { return _IsExpired; }
            set { _IsExpired = value; }
        }
        //IsScratched
        private int _IsScratched = 0;
        public int IsScratched
        {
            get { return _IsScratched; }
            set { _IsScratched = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _UniqueTransactionId = string.Empty;
        public string UniqueTransactionId
        {
            get { return _UniqueTransactionId; }
            set { _UniqueTransactionId = value; }
        }
        private string _ParentTransactionId = string.Empty;
        public string ParentTransactionId
        {
            get { return _ParentTransactionId; }
            set { _ParentTransactionId = value; }
        }

        //UsedByTransactionId
        private string _UsedByTransactionId = string.Empty;
        public string UsedByTransactionId
        {
            get { return _UsedByTransactionId; }
            set { _UsedByTransactionId = value; }
        }
        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
        }
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }
        #endregion

    }
}