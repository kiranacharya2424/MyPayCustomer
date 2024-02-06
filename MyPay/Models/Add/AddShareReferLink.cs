using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MyPay.Models.Add
{
    public class AddShareReferLink : CommonAdd
    {
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        private string _RefCode = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }


        private string _IPAddress = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        } 
        private string _PhoneNumber = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }
        private string _Platform = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }
        private string _SharedLinkURL = string.Empty;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SharedLinkURL
        {
            get { return _SharedLinkURL; }
            set { _SharedLinkURL = value; }
        }
        private bool  _IsOpened = false;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsOpened
        {
            get { return _IsOpened; }
            set { _IsOpened = value; }
        } 
      
        #region GetMethods
        public float CountSharedLinkCheck(string RefCode, Int64 Id)
        {
            float data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "SELECT count(0) FROM ShareReferLink with(nolock) where RefCode = " + RefCode + "  and IsActive= 1 and IsDeleted = 0";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = (float.Parse(Result));
                }
            }
            catch (Exception ex)
            {
            }
            return data;
        }
        #endregion
    }
}