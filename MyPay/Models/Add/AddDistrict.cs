using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddDistrict
    {
        #region "Properties"

        //District
        private string _District = string.Empty;
        public string District
        {
            get { return _District; }
            set { _District = value; }
        }

        //DistrictCode 
        private string _DistrictCode = string.Empty;
        public string DistrictCode
        {
            get { return _DistrictCode; }
            set { _DistrictCode = value; }
        }

        //ProvinceCode
        private string _ProvinceCode = string.Empty;
        public string ProvinceCode
        {
            get { return _ProvinceCode; }
            set { _ProvinceCode = value; }
        }
        #endregion
    }
}