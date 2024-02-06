using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetAirlineCommission:CommonGet
    {
        #region "Properties"

        //ServiceId
        private int _AirlineId = 0;
        public int AirlineId
        {
            get { return _AirlineId; }
            set { _AirlineId = value; }
        }
        private string _AirlineName = string.Empty;
        public string AirlineName
        {
            get { return _AirlineName; }
            set { _AirlineName = value; }
        }
        private int _FromSectorId = 0;
        public int FromSectorId
        {
            get { return _FromSectorId; }
            set { _FromSectorId = value; }
        }
        private string _FromSectorName = string.Empty;
        public string FromSectorName
        {
            get { return _FromSectorName; }
            set { _FromSectorName = value; }
        }
        private int _ToSectorId = 0;
        public int ToSectorId
        {
            get { return _ToSectorId; }
            set { _ToSectorId = value; }
        }
        private string _ToSectorName = string.Empty;
        public string ToSectorName
        {
            get { return _ToSectorName; }
            set { _ToSectorName = value; }
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