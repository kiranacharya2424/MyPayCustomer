using MyPay.Models.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request
{
    public class Req_Web_OfferBanners
    {
        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //FromDate
        private string _FromDate = string.Empty;
        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        //ToDate
        private string _ToDate = string.Empty;
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        private List<AddOfferBanners> _objData = new List<AddOfferBanners>();
        public List<AddOfferBanners> objData
        {
            get { return _objData; }
            set { _objData = value; }
        }

        //EnumScheduleStatus
        private AddOfferBanners.ScheduleStatuses _EnumScheduleStatus = 0;
        public AddOfferBanners.ScheduleStatuses EnumScheduleStatus
        {
            get { return _EnumScheduleStatus; }
            set { _EnumScheduleStatus = value; }
        }

        //EnumActiveStatus
        private AddOfferBanners.ActiveStatuses _EnumActiveStatus = 0;
        public AddOfferBanners.ActiveStatuses EnumActiveStatus
        {
            get { return _EnumActiveStatus; }
            set { _EnumActiveStatus = value; }
        }

        //EnumTypeProviders
        private VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName _EnumTypeProviders = 0;
        public VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName EnumTypeProviders
        {
            get { return _EnumTypeProviders; }
            set { _EnumTypeProviders = value; }
        }
    }
}