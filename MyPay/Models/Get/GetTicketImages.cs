using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetTicketImages:CommonGet
    {
        #region "Properties"
        
        //ReplyId
        private Int64 _ReplyId = 0;
        public Int64 ReplyId
        {
            get { return _ReplyId; }
            set { _ReplyId = value; }
        }

        //TicketId
        private string _TicketId = string.Empty;
        public string TicketId
        {
            get { return _TicketId; }
            set { _TicketId = value; }
        }

        //ReplyUniqueId
        private string _ReplyUniqueId = string.Empty;
        public string ReplyUniqueId
        {
            get { return _ReplyUniqueId; }
            set { _ReplyUniqueId = value; }
        }

        //CheckIsAdmin
        private int? _CheckIsAdmin = 2;// (int)clsData.BooleanValue.Both;
        public int? CheckIsAdmin
        {
            get { return _CheckIsAdmin; }
            set { _CheckIsAdmin = value; }
        }

        #endregion
    }
}