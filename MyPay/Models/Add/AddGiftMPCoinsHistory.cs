using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddGiftMPCoinsHistory:CommonAdd
    {
        #region "Enums"

        public enum GiftMPCoinsStatus
        {
            Success = 1,
            Failed = 2
        }

        #endregion

        #region "Properties"
        //ContactNumber
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Contact Number")]
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //Prize
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Prize")]
        private decimal _Prize = 0;
        public decimal Prize
        {
            get { return _Prize; }
            set { _Prize = value; }
        }

        //TransactionId
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Transaction Id")]
        private string _TransactionId = string.Empty;
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        //Remarks
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Remarks")]
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        //Sign
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Sign")]
        private int _Sign = 0;
        public int Sign
        {
            get { return _Sign; }
            set { _Sign = value; }
        }

        //MemberId
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "MemberId")]
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //MemberName
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Member Name")]
        private string _MemberName = string.Empty;
        public string MemberName
        {
            get { return _MemberName; }
            set { _MemberName = value; }
        }

        //Status
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Status")]
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

        //CreatedDatedt
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        private AddGiftMPCoinsHistory.GiftMPCoinsStatus _StatusEnum = 0;
        public AddGiftMPCoinsHistory.GiftMPCoinsStatus StatusEnum
        {
            get { return _StatusEnum; }
            set { _StatusEnum = value; }
        }

        #endregion
    }
}