using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Req_API_Get_Payment_Requests : CommonProp
    {
        private Int32 _ServiceID = 0;
        public Int32 ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }
        private Int64 _MemberID = 0;
        public Int64 MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        public string Reference { get; internal set; }
    }


    public class Req_API_Get_CableTicket_Requests : CommonProp
    {
        private Int32 _ServiceID = 0;
        public Int32 ServiceID
        {
            get { return _ServiceID; }
            set { _ServiceID = value; }
        }
        //private string _MemberID;
        //public string MemberID
        //{
        //    get { return _MemberID; }
        //    set { _MemberID = value; }
        //}

        public string Reference { get; internal set; }
    }
}