using MyPay.Models.Add;
using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_User:WebCommonProp
    {

        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private string _FirstName = String.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _ContactNumber = String.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        private string _Email = String.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _StartDate = String.Empty;
        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private string _ToDate = String.Empty;
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        private int _IsKycApproved = 1;
        public int IsKycApproved
        {
            get { return _IsKycApproved; }
            set { _IsKycApproved = value; }
        }

        private List<AddUser> _objData = new List<AddUser>();
        public List<AddUser> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }

        private string _LastName = String.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _MiddleName = String.Empty;
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        private string _RefCode = String.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }
        private string _AlternateNumber = String.Empty;
        public string AlternateNumber
        {
            get { return _AlternateNumber; }
            set { _AlternateNumber = value; }
        }

        private string _DateOfBirth = String.Empty;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        private string _Gender = String.Empty;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        private string _OTP = String.Empty;
        public string OTP
        {
            get { return _OTP; }
            set { _OTP = value; }
        }
    }

}