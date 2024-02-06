using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_UserPersonalUpdate:CommonProp
    {
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }


        //FirstName
        private string _FirstName = string.Empty;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        //Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        

        //AlternateNumber
        private string _AlternateNumber = string.Empty;
        public string AlternateNumber
        {
            get { return _AlternateNumber; }
            set { _AlternateNumber = value; }
        }


        //RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        //LastName
        private string _LastName = string.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        //MiddleName
        private string _MiddleName = string.Empty;
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        //DateOfBirth
        private string _DateOfBirth = string.Empty;
        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        //Gender
        private string _Gender = string.Empty;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
    }
}