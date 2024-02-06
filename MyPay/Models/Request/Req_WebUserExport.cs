using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_WebUserExport
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

        private string _LastName = String.Empty;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
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
        private string _TotalAmount = String.Empty;
        public string TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private string _Gender = String.Empty;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        private string _CreatedDate = String.Empty;
        public string CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        private string _IsKycApproved = string.Empty;
        public string IsKycApproved
        {
            get { return _IsKycApproved; }
            set { _IsKycApproved = value; }
        }

    }

}