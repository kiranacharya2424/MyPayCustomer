using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetCommission:CommonGet
    {
        #region "Properties"

        //ServiceId
        private int _ServiceId = 0;
        public int ServiceId
        {
            get { return _ServiceId; }
            set { _ServiceId = value; }
        }

        //MinimumAmount
        private decimal _MinimumAmount = 0;
        public decimal MinimumAmount
        {
            get { return _MinimumAmount; }
            set { _MinimumAmount = value; }
        }
        //MaximumAmount
        private decimal _MaximumAmount = 0;
        public decimal MaximumAmount
        {
            get { return _MaximumAmount; }
            set { _MaximumAmount = value; }
        }
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private int _KycType = 0;
        public int KycType
        {
            get { return _KycType; }
            set { _KycType = value; }
        }
        //GenderType
        private int _GenderType = 0;
        public int GenderType
        {
            get { return _GenderType; }
            set { _GenderType = value; }
        }
        //Running
        private string _Running = string.Empty; 
        public string Running
        {
            get { return _Running; }
            set { _Running = value; }
        }
        #endregion
    }
}