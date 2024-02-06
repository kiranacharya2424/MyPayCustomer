using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetState
    {
        #region "Properties"

        //Province
        private string _Province = string.Empty;
        public string Province
        {
            get { return _Province; }
            set { _Province = value; }
        }

        //ProvinceCode
        private string _ProvinceCode = string.Empty;
        public string ProvinceCode
        {
            get { return _ProvinceCode; }
            set { _ProvinceCode = value; }
        }

        //CountryCode 
        private string _CountryCode = string.Empty;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }


        #endregion
    }
}