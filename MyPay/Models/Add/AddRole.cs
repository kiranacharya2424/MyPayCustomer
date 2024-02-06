using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddRole:CommonAdd
    {
        #region "Properties"

        //RoleName
        private string _RoleName = string.Empty;
        public string RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value; }
        }

        //IsAdminLogin
        private bool _IsAdminLogin = true;
        public bool IsAdminLogin
        {
            get { return _IsAdminLogin; }
            set { _IsAdminLogin = value; }
        }
        #endregion
    }
}