using MyPay.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyPay.Models.Add
{
    public class GetUserLoginWithPin
    {



        private int _CheckDelete = 2;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }

        private string _ContactNumber = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        private string _Email = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _RefCode = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        private string _DeviceId= "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }

        private string _JwtToken = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string JwtToken
        {
            get { return _JwtToken; }
            set { _JwtToken = value; }
        }




        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }




    }


}