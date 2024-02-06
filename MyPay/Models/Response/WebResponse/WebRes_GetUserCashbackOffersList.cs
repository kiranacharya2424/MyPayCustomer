using MyPay.Models.Add;
using MyPay.Models.Response.WebResponse.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Response.WebResponse
{
    public class WebRes_GetUserCashbackOffersList : WebCommonResponse
    {
        // Banners
        private List<AddOfferBanners> _data = new List<AddOfferBanners>();
        public List<AddOfferBanners> data
        {
            get { return _data; }
            set { _data = value; }
        }

        // ProviderList
        private List<ProviderServiceCategoryList> _ProviderList = new List<ProviderServiceCategoryList>();
        public List<ProviderServiceCategoryList> ProviderList
        {
            get { return _ProviderList; }
            set { _ProviderList = value; }
        }

        // MarqueList
        private List<AddMarque> _MarqueList = new List<AddMarque>();
        public List<AddMarque> MarqueList
        {
            get { return _MarqueList; }
            set { _MarqueList = value; }
        }

        // ReferEarn
        private List<AddReferEarnImage> _ReferEarndata = new List<AddReferEarnImage>();
        public List<AddReferEarnImage> ReferEarndata
        {
            get { return _ReferEarndata; }
            set { _ReferEarndata = value; }
        }


        //ProviderServiceCategoryList
        private List<ProviderServiceCategoryList> _list = new List<ProviderServiceCategoryList>();
        public List<ProviderServiceCategoryList> list
        {
            get { return _list; }
            set { _list = value; }
        }
    }
}