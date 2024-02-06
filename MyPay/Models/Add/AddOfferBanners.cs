using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddOfferBanners:CommonAdd
    {
        #region "Enums"

        public enum ScheduleStatuses
        {
            Running = 1,
            Scheduled = 2,
            Expired = 3
        }

        public enum ActiveStatuses
        {
            Enabled = 1,
            Disabled = 2
        }
        #endregion

        #region "Properties"

        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //EnumTypeProviders
        private VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName _EnumTypeProviders = 0;
        public VendorAPI.VendorRequest_CommonHelper.VendorApi_CommonHelper.KhaltiAPIName EnumTypeProviders
        {
            get { return _EnumTypeProviders; }
            set { _EnumTypeProviders = value; }
        }

        //FromDate
        private DateTime _FromDate = DateTime.UtcNow;
        public DateTime FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        //ToDate
        private DateTime _ToDate = DateTime.UtcNow;
        public DateTime ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        //BannerImage
        private string _BannerImage = string.Empty;
        public string BannerImage
        {
            get { return _BannerImage; }
            set { _BannerImage = value; }
        }

        //TypeName
        private string _TypeName = string.Empty;
        public string TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value; }
        }

        //IpAddress
        private string _IpAddress = Common.Common.GetUserIP();
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        //Row
        private int _Row = 0;
        public int Row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        //ScheduleStatus
        private string _ScheduleStatus = string.Empty;
        public string ScheduleStatus
        {
            get { return _ScheduleStatus; }
            set { _ScheduleStatus = value; }
        }

        //Priority
        private int _Priority = 0;
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }
    
        private string _URL ;
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }
        #endregion
    }
}