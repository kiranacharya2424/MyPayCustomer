using MyPay.Models.Request.WebRequest.Common; 

namespace MyPay.API.Models
{
    public class WebRequest_Login:WebCommonProp
    {

        //PhoneNumber
        private string _PhoneNumber = string.Empty;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }


        //Password
        private string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        //Digits
        private string _Digits = string.Empty;
        public string Digits
        {
            get { return _Digits; }
            set { _Digits = value; }
        }

        //VerificationOtp
        private string _VerificationOtp = string.Empty;
        public string VerificationOtp
        {
            get { return _VerificationOtp; }
            set { _VerificationOtp = value; }
        }

        //RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }

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

        private string _ConfirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }
        private int _MemberId = 0;
        public int MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private bool _IsMobile = false;
        public bool IsMobile
        {
            get { return _IsMobile; }
            set { _IsMobile = value; }
        }
    }
}