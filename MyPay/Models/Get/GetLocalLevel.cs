using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetLocalLevel
    {
        #region "Properties"

        //LocalLevel
        private string _LocalLevel = string.Empty;
        public string LocalLevel
        {
            get { return _LocalLevel; }
            set { _LocalLevel = value; }
        }

        //LocalLevelCode
        private string _LocalLevelCode = string.Empty;
        public string LocalLevelCode
        {
            get { return _LocalLevelCode; }
            set { _LocalLevelCode = value; }
        }

        //DistrictCode 
        private string _DistrictCode = string.Empty;
        public string DistrictCode
        {
            get { return _DistrictCode; }
            set { _DistrictCode = value; }
        }


        #endregion
    }
}