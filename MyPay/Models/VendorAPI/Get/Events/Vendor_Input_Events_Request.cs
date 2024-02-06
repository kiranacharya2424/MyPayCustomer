using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class Vendor_Input_Events_Request
    {

       
        private int _pageSize =100;
        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        private int _pageNumber = 1;
        public int pageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }
        private string _searchVal = string.Empty;
        public string searchVal
        {
            get { return _searchVal; }
            set { _searchVal = value; }
        }
        private string _sortOrder = string.Empty;
        public string sortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }
        private string _dateFrom = string.Empty;
        public string dateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; }
        }
        private string _dateTo = string.Empty;
        public string dateTo
        {
            get { return _dateTo; }
            set { _dateTo = value; }
        } 
        private string _sortBy = string.Empty;
        public string sortBy
        {
            get { return _sortBy; }
            set { _sortBy = value; }
        }

    }
}