using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddKYCStatusHistory:CommonAdd
    {
        #region "Properties"

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //IsAdmin
        private bool _IsAdmin = false;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        //Remarks
        private string _Remarks = string.Empty;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        //KYCStatus
        private int _KYCStatus = 0;
        public int KYCStatus
        {
            get { return _KYCStatus; }
            set { _KYCStatus = value; }
        }

        //KYCStatusName
        private string _KYCStatusName = string.Empty;
        public string KYCStatusName
        {
            get { return _KYCStatusName; }
            set { _KYCStatusName = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }

        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }

        #endregion
    }
}