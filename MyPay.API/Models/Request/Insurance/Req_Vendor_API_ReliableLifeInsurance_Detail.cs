﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPay.API.Models.Request.Insurance
{
    public class Req_Vendor_API_ReliableLifeInsurance_Detail : CommonProp
    {
        private string _reference = String.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        private string _PolicyNo = String.Empty;
        public string PolicyNo
        {
            get { return _PolicyNo; }
            set { _PolicyNo = value; }
        }

        private string _dob = String.Empty;
        public string dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        private string _insuranceslug = String.Empty;
        public string insuranceslug
        {
            get { return _insuranceslug; }
            set { _insuranceslug = value; }
        }
    }
}