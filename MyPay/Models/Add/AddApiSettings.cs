using MyPay.Models.Common;

using System;

namespace MyPay.Models.Add

{

    public class AddApiSettings : CommonAdd
    {
        public enum LinkBankType
        {
            ALL = 0,
            NCHL = 1,
            NPS = 2
        }

        private Int32 _CheckVersion = 0;
        public Int32 CheckVersion

        {
            get { return _CheckVersion; }
            set { _CheckVersion = value; }
        }


        private Int32 _CheckDevicecode = 0;
        public Int32 CheckDevicecode

        {
            get { return _CheckDevicecode; }
            set { _CheckDevicecode = value; }
        }

        private Int32 _CheckAndroidVersion = 0;
        public Int32 CheckAndroidVersion

        {
            get { return _CheckAndroidVersion; }
            set { _CheckAndroidVersion = value; }
        }


        private Int32 _CheckTimestamp = 0;
        public Int32 CheckTimestamp

        {
            get { return _CheckTimestamp; }
            set { _CheckTimestamp = value; }
        }

        private Int32 _CheckPlatform = 0;
        public Int32 CheckPlatform

        {
            get { return _CheckPlatform; }
            set { _CheckPlatform = value; }
        }


        private Int32 _CheckHash = 0;
        public Int32 CheckHash

        {
            get { return _CheckHash; }
            set { _CheckHash = value; }
        }


        private System.DateTime _ScheduleStartTime = System.DateTime.UtcNow;
        public System.DateTime ScheduleStartTime

        {
            get { return _ScheduleStartTime; }
            set { _ScheduleStartTime = value; }
        }

        private System.DateTime _ScheduleEndTime = System.DateTime.UtcNow;
        public System.DateTime ScheduleEndTime

        {
            get { return _ScheduleEndTime; }
            set { _ScheduleEndTime = value; }
        }
        private string _SchedulerMessage = String.Empty;
        public string SchedulerMessage

        {
            get { return _SchedulerMessage; }
            set { _SchedulerMessage = value; }
        }
        private bool _ScheduleStatus = false;
        public bool ScheduleStatus

        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }
        private string _AndroidVersion = String.Empty;
        public string AndroidVersion

        {
            get { return _AndroidVersion; }
            set { _AndroidVersion = value; }
        }
        private string _IOSVersion = String.Empty;
        public string IOSVersion

        {
            get { return _IOSVersion; }
            set { _IOSVersion = value; }
        }
        private bool _IsTestVendor = false;
        public bool IsTestVendor

        {
            get { return _IsTestVendor; }
            set { _IsTestVendor = value; }
        }
        private int _CheckKhalti = 0;
        public int CheckKhalti

        {
            get { return _CheckKhalti; }
            set { _CheckKhalti = value; }
        }
        private LinkBankType _LinkBankTypeEnum = 0;
        public LinkBankType LinkBankTypeEnum
        {
            get { return _LinkBankTypeEnum; }
            set { _LinkBankTypeEnum = value; }
        }
    }

}