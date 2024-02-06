using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.Models.Add
{
    public class AddApiFlightSwitchSettings : CommonAdd
    {
        public Nullable<int> CheckVersion { get; set; }
        public Nullable<int> CheckDevicecode { get; set; }
        public Nullable<int> CheckAndroidVersion { get; set; }
        public Nullable<int> CheckTimestamp { get; set; }
        public Nullable<int> CheckHash { get; set; }
        public Nullable<int> CheckPlatform { get; set; }
        public Nullable<System.DateTime> ScheduleStartTime { get; set; }
        public Nullable<System.DateTime> ScheduleEndTime { get; set; }
        public string SchedulerMessage { get; set; }
        public Nullable<bool> ScheduleStatus { get; set; }
        public string AndroidVersion { get; set; }
        public string IOSVersion { get; set; }
        public Nullable<System.DateTime> MPCoinsDateSettings { get; set; }
        public Nullable<System.DateTime> ActivityMonitorLastUpdate { get; set; }
        public Nullable<int> FlightSwitchType { get; set; }

    }
}
