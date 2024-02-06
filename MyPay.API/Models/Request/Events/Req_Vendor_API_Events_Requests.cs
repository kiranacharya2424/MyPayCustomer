using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_Vendor_API_Events_Requests : CommonProp
    {
         
        // session_id
        private string _SessionId = string.Empty;
        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        // MemberId
        private string _MemberId = string.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        // amount
        private string _Amount = string.Empty;
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        // reference
        private string _ReferenceNo = string.Empty;
        public string ReferenceNo
        {
            get { return _ReferenceNo; }
            set { _ReferenceNo = value; }
        }
        private int _PageSize = 100;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private int _PageNumber = 1;
        public int PageNumber
        {
            get { return _PageNumber; }
            set { _PageNumber = value; }
        }
        private string _SearchVal = string.Empty;
        public string SearchVal
        {
            get { return _SearchVal; }
            set { _SearchVal = value; }
        }
        private string _SortOrder = string.Empty;
        public string SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        private string _DateFrom = string.Empty;
        public string DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }
        private string _DateTo = string.Empty;
        public string DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }
        private string _SortBy = string.Empty;
        public string SortBy
        {
            get { return _SortBy; }
            set { _SortBy = value; }
        }
    }
}