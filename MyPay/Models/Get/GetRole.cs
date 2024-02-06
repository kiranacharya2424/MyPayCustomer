using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetRole:CommonGet
    {
        #region "Properties"

        //RoleName
        private string _RoleName = string.Empty;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }
        //CheckIsAdminLogin
        private Int32 _CheckIsAdminLogin =2;
        public Int32 CheckIsAdminLogin
        {
            get { return _CheckIsAdminLogin; }
            set { _CheckIsAdminLogin = value; }
        }

        #endregion
    }
}