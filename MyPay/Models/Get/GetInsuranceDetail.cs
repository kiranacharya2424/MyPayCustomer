using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetInsuranceDetail:CommonGet
    {
        #region "Properties"
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //CustomerId
        private string _CustomerId = string.Empty;
        public string CustomerId
        {
            get { return _CustomerId; }
            set { _CustomerId = value; }
        }

        //MobileNumber
        private string _MobileNumber = string.Empty;
        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value; }
        }

        //InsuranceType
        private int _InsuranceType = 0;
        public int InsuranceType
        {
            get { return _InsuranceType; }
            set { _InsuranceType = value; }
        }

        //VendorType
        private int _VendorType = 0;
        public int VendorType
        {
            get { return _VendorType; }
            set { _VendorType = value; }
        }

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }
        #endregion
    }
}