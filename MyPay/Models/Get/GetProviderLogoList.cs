using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetProviderLogoList  
    {
        #region "Properties"

        //Id
        private Int64 _Id = 0;
        public Int64 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        //ProviderName
        private string _ProviderName = string.Empty;
        public string ProviderName
        {
            get { return _ProviderName; }
            set { _ProviderName = value; }
        }

        //ProviderTypeId
        private Int32 _ProviderTypeId = 0;
        public Int32 ProviderTypeId
        {
            get { return _ProviderTypeId; }
            set { _ProviderTypeId = value; }
        }

        //ProviderServiceCategoryId
        private Int32 _ProviderServiceCategoryId = 0;
        public Int32 ProviderServiceCategoryId
        {
            get { return _ProviderServiceCategoryId; }
            set { _ProviderServiceCategoryId = value; }
        }
        //ProviderServiceName
        private string _ProviderServiceName = string.Empty;
        public string ProviderServiceName
        {
            get { return _ProviderServiceName; }
            set { _ProviderServiceName = value; }
        }

        //ProviderLogoURL
        private string _ProviderLogoURL = string.Empty;
        public string ProviderLogoURL
        {
            get { return _ProviderLogoURL; }
            set { _ProviderLogoURL = value; }
        }
        //IsActive
        private Int32 _IsActive = 2;
        public Int32 IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        //Take
        private Int32 _Take = 0;
        public Int32 Take
        {
            get { return _Take; }
            set { _Take = value; }
        }

        #endregion
    }
}