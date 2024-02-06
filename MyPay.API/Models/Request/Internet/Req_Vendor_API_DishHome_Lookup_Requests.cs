using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.Internet.DishHome
{
    public class Req_Vendor_API_DishHome_Lookup_Requests : CommonProp
    {
        private string _Reference = String.Empty;
        public string Reference
        {
            get { return _Reference; }
            set { _Reference = value; }
        }
        private string _CustomerID = String.Empty;
        public string CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }
        private string _CustomerName = String.Empty;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }
        private string _SessionId = String.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        private string _Amount = String.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        private string _PackageName = String.Empty;
        public string PackageName
        {
            get { return _PackageName; }
            set { _PackageName = value; }
        }
        private string _Status = String.Empty;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Duration = String.Empty;
        public string Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
    }
}