using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response
{
    public class Res_OfferBanners: CommonResponse
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
    }
}