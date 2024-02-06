using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Miscellaneous
{
    public class Users
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //CheckActive
        private int _CheckActive = 2;// (int)clsData.BooleanValue.Both;
        public int CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }

        //CheckApprovedByadmin
        private int _CheckApprovedByAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }

        //CheckDelete
        private int _CheckDelete = 2;// (int)clsData.BooleanValue.Both;
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        //DataRecieved
        private bool _DataRecieved = false;
        public bool DataRecieved
        {
            get { return _DataRecieved; }
            set { _DataRecieved = value; }
        }

        //IsKYCApproved
        private int _IsKYCApproved = 0;
        public int IsKYCApproved
        {
            get { return _IsKYCApproved; }
            set { _IsKYCApproved = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //StartDate
        private string _StartDate = string.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        //EndDate
        private string _EndDate = string.Empty;
        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

     


    }
}