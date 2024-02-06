using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddRequestFund:CommonAdd
    {
        #region "Enums"
        public enum RequestStatuses
        {
            Pending = 0,
            Accepted = 1,
            Rejected = 2
        }
        #endregion

        #region "Properties"
        //RequestStatus
        private int _RequestStatus = 0;
        public int RequestStatus
        {
            get { return _RequestStatus; }
            set { _RequestStatus = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //SenderMemberId
        private Int64 _SenderMemberId = 0;
        public Int64 SenderMemberId
        {
            get { return _SenderMemberId; }
            set { _SenderMemberId = value; }
        }
        //SenderMemberName
        private string _SenderMemberName = string.Empty;
        public string SenderMemberName
        {
            get { return _SenderMemberName; }
            set { _SenderMemberName = value; }
        }
        //SenderPhoneNumber
        private string _SenderPhoneNumber = string.Empty;
        public string SenderPhoneNumber
        {
            get { return _SenderPhoneNumber; }
            set { _SenderPhoneNumber = value; }
        }
        //IpAddress
        private string _IpAddress = string.Empty;
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        //Amount
        private decimal _Amount = 0;
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        //CreatedDatedt
        private string _CreatedDatedt = string.Empty;
        public string CreatedDatedt
        {
            get { return _CreatedDatedt; }
            set { _CreatedDatedt = value; }
        }

        //UpdatedDatedt
        private string _UpdatedDatedt = string.Empty;
        public string UpdatedDatedt
        {
            get { return _UpdatedDatedt; }
            set { _UpdatedDatedt = value; }
        }

        //StatusName
        private string _StatusName = string.Empty;
        public string StatusName
        {
            get { return _StatusName; }
            set { _StatusName = value; }
        }

        //ReceiverMemberName
        private string _ReceiverMemberName = string.Empty;
        public string ReceiverMemberName
        {
            get { return _ReceiverMemberName; }
            set { _ReceiverMemberName = value; }
        }
        //ReceiverPhoneNumber
        private string _ReceiverPhoneNumber = string.Empty;
        public string ReceiverPhoneNumber
        {
            get { return _ReceiverPhoneNumber; }
            set { _ReceiverPhoneNumber = value; }
        }


        //TotalPending
        private decimal _TotalPending = 0;
        public decimal TotalPending
        {
            get { return _TotalPending; }
            set { _TotalPending = value; }
        }
        //TotalAccepted
        private decimal _TotalAccepted = 0;
        public decimal TotalAccepted
        {
            get { return _TotalAccepted; }
            set { _TotalAccepted = value; }
        }
        //TotalRejected
        private decimal _TotalRejected = 0;
        public decimal TotalRejected
        {
            get { return _TotalRejected; }
            set { _TotalRejected = value; }
        }
        //FilterTotalCount
        private Int32 _FilterTotalCount = 0;
        public Int32 FilterTotalCount
        {
            get { return _FilterTotalCount; }
            set { _FilterTotalCount = value; }
        }


        #endregion

       
    }
}