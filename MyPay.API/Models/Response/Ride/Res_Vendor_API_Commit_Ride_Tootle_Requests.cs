using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Response.Ride
{
    public class Res_Vendor_API_Commit_Ride_Tootle_Requests:CommonResponse
    {
        //Id
        private string _Id = string.Empty;
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        // TransactionDatetime
        private string _TransactionDatetime = System.DateTime.UtcNow.ToString("dd MMM yy hh:mm:ss tt");
        public string TransactionDatetime
        {
            get { return _TransactionDatetime; }
            set { _TransactionDatetime = value; }
        }
        private string _WalletBalance = string.Empty;
        public string WalletBalance
        {
            get { return _WalletBalance; }
            set { _WalletBalance = value; }
        }
        private RideDetailResponse _RideDetails = new RideDetailResponse();
        public RideDetailResponse RideDetails
        {
            get { return _RideDetails; }
            set { _RideDetails = value; }
        }
    }
    public class RideExtraDataResponse
    {
    }

    public class RideDetailResponse
    {
        public string Message { get; set; }
    }
}