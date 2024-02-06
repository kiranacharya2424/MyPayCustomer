using MyPay.Models.Request.WebRequest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Request.WebRequest
{
    public class WebRequest_Events : WebCommonProp
    {
        private string _SessionId = String.Empty;
        public string Session_id
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }
        private string _MemberId = String.Empty;
        public string MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        private Int32 _PageSize = 0;
        public Int32 PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private Int32 _PageNumber = 0;
        public Int32 PageNumber
        {
            get { return _PageNumber; }
            set { _PageNumber = value; }
        }
        private String _SearchVal = string.Empty;
        public String SearchVal
        {
            get { return _SearchVal; }
            set { _SearchVal = value; }
        }
        private String _SortOrder = string.Empty;
        public String SortOrder
        {
            get { return _SortOrder; }
            set { _SortOrder = value; }
        }
        private String _DateFrom = string.Empty;
        public String DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }
        private String _DateTo = string.Empty;
        public String DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }
        private String _SortBy = string.Empty;
        public String SortBy
        {
            get { return _SortBy; }
            set { _SortBy = value; }
        }
        
        
        private Int32 _CheckDelete = 2;
        public Int32 CheckDelete
        {
            get { return _CheckDelete; }
            set { _CheckDelete = value; }
        }
        private Int32 _CheckActive = 2;
        public Int32 CheckActive
        {
            get { return _CheckActive; }
            set { _CheckActive = value; }
        }
        private Int32 _CheckApprovedByAdmin = 2;
        public Int32 CheckApprovedByAdmin
        {
            get { return _CheckApprovedByAdmin; }
            set { _CheckApprovedByAdmin = value; }
        }
        private String _CheckCreatedDate = string.Empty;
        public String CheckCreatedDate
        {
            get { return _CheckCreatedDate; }
            set { _CheckCreatedDate = value; }
        }
        private String _StartDate = string.Empty;
        public String StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }
        private String _EndDate = string.Empty;
        public String EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }


    }
}