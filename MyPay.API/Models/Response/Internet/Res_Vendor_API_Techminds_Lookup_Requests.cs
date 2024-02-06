using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.ClassiTech
{
    public class Res_Vendor_API_Techminds_Lookup_Requests : CommonResponse
    {
        // session_id
        private string _SessionID = string.Empty;
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        } 
        // Techminds_Data
        private Techminds_Data _Techminds_Data = new Techminds_Data();
        public Techminds_Data Techminds_Data
        {

            get { return _Techminds_Data; }

            set { _Techminds_Data = value; }
        }
        // Techminds_Plans
        private dynamic _Available_Plans = new Techminds_Plans();
        public dynamic Available_Plans
        {

            get { return _Available_Plans; }

            set { _Available_Plans = value; }
        }
    }
    public class Techminds_Data
    {
        // CustomerName
        private string _CustomerName = string.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        // ExpirationDate
        private string _ExpirationDate = string.Empty;
        public string ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; }
        }
        // MobileNumber
        private string _MobileNumber = string.Empty;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }
        //PreviousBalance
        private string _PreviousBalance = string.Empty;
        public string PreviousBalance
        {
            get { return _PreviousBalance; }
            set { _PreviousBalance = value; }
        }
        // Email
        private string _Email = string.Empty;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        // MonthlyCharge
        private string _MonthlyCharge = string.Empty;
        public string MonthlyCharge
        {
            get { return _MonthlyCharge; }
            set { _MonthlyCharge = value; }
        }
    }
    public class Techminds_Plans
    { 
        private string _Plan_12Month = string.Empty;
        public string Plan_12Month
        {
            get { return _Plan_12Month; }
            set { _Plan_12Month = value; }
        }
        private string _Plan_6Month = string.Empty;
        public string Plan_6Month
        {
            get { return _Plan_6Month; }
            set { _Plan_6Month = value; }
        }
        private string _Plan_3Month = string.Empty;
        public string Plan_3Month
        {
            get { return _Plan_3Month; }
            set { _Plan_3Month = value; }
        }
        private string _Plan_1Month = string.Empty;
        public string Plan_1Month
        {
            get { return _Plan_1Month; }
            set { _Plan_1Month = value; }
        }

        private string _Plan_15Days = string.Empty;
        public string Plan_15Days
        {
            get { return _Plan_15Days; }
            set { _Plan_15Days = value; }
        }

        private string _Plan_180Days = string.Empty;
        public string Plan_180Days
        {
            get { return _Plan_180Days; }
            set { _Plan_180Days = value; }
        }

        private string _Plan_30Days = string.Empty;
        public string  Plan_30Days
        {
            get { return _Plan_30Days; }
            set { _Plan_30Days = value; }
        }

        private string _Plan_60Days = string.Empty;
        public string  Plan_60Days
        {
            get { return _Plan_60Days; }
            set { _Plan_60Days = value; }
        }

        private string _Plan_90Days = string.Empty;
        public string  Plan_90Days
        {
            get { return _Plan_90Days; }
            set { _Plan_90Days = value; }
        }
    }

}