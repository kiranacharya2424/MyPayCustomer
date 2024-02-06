using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetLog: CommonGet
    {
        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //DeviceCode
        private string _DeviceCode = string.Empty;
        public string DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        //Action
        private string _Action = string.Empty;
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        //UserId
        private string _UserId = string.Empty;
        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        //MemberId
        private Int64? _MemberId = 0;
        public Int64? MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //CheckIsMobile
        private int? _CheckIsMobile = 2;
        public int? CheckIsMobile
        {
            get { return _CheckIsMobile; }
            set { _CheckIsMobile = value; }
        }

        //CheckAdmin
        private int? _CheckAdmin = 2;
        public int? CheckAdmin
        {
            get { return _CheckAdmin; }
            set { _CheckAdmin = value; }
        }

        //UserMemberId
        private Int64? _UserMemberId = 0;
        public Int64? UserMemberId
        {
            get { return _UserMemberId; }
            set { _UserMemberId = value; }
        }
    }
}