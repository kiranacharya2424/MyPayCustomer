using MyPay.Models.Common;

using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;

namespace MyPay.Models.Add

{
    public class AddUsersDeviceRegistration : CommonAdd

    {

         
        private Int64 _MemberId = 0;
        public Int64 MemberId

        {

            get { return _MemberId; }

            set { _MemberId = value; }

        }
        private Int32 _SequenceNo = 0;
        public Int32 SequenceNo

        {

            get { return _SequenceNo; }

            set { _SequenceNo = value; }

        }
        private string _IpAddress = Common.Common.GetUserIP();

        public string IpAddress

        {

            get { return _IpAddress; }

            set { _IpAddress = value; }

        }
         
        private string _DeviceCode = string.Empty;

        public string DeviceCode

        {

            get { return _DeviceCode; }

            set { _DeviceCode = value; }

        }

        private string _IMIE = string.Empty;

        public string IMIE

        {

            get { return _IMIE; }

            set { _IMIE = value; }

        }

        private string _PlatForm = string.Empty;

        public string PlatForm

        {

            get { return _PlatForm; }

            set { _PlatForm = value; }

        }
         
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt

        {

            get { return _CreatedDatedt; }

            set { _CreatedDatedt = value; }

        }
        private string _PreviousDeviceCode = string.Empty;
        public string PreviousDeviceCode

        {

            get { return _PreviousDeviceCode; }

            set { _PreviousDeviceCode = value; }

        }
    }

}