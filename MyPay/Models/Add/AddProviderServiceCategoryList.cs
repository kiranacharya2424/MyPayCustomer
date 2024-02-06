using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddProviderServiceCategoryList
    {
        #region "Properties"
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private string _ProviderCategoryName = string.Empty;
        public string ProviderCategoryName
        {
            get { return _ProviderCategoryName; }
            set { _ProviderCategoryName = value; }
        }
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        private bool _IsDelete = false;
        public bool IsDelete
        {
            get { return _IsDelete; }
            set { _IsDelete = value; }
        }

        private string _Commission = string.Empty;
        public string Commission
        {
            get { return _Commission; }
            set { _Commission = value; }
        }

        private string _MPCoinsCashback = string.Empty;
        public string MPCoinsCashback
        {
            get { return _MPCoinsCashback; }
            set { _MPCoinsCashback = value; }
        }
        private Int32 _RoleId = 0;
        public Int32 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value; }
        }
        #endregion


    }
}