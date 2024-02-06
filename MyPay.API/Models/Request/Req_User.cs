using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_User : CommonProp
    {
        //Lat
        private string _Lat = string.Empty;
        public string Lat
        {
            get { return _Lat; }
            set { _Lat = value; }
        }
        //Lon
        private string _Lon = string.Empty;
        public string Lon
        {
            get { return _Lon; }
            set { _Lon = value; }
        }
        //ContactNumber
        private string _ContactNumber = string.Empty;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set { _ContactNumber = value; }
        }

        //PhoneExt
        private string _PhoneExt = string.Empty;
        public string PhoneExt
        {
            get { return _PhoneExt; }
            set { _PhoneExt = value; }
        }

        //RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

        //VerificationOtp
        private string _VerificationOtp = string.Empty;
        public string VerificationOtp
        {
            get { return _VerificationOtp; }
            set { _VerificationOtp = value; }
        }
        //DeviceId
        private string _DeviceId = string.Empty;
        public string DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
    }
}