using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddTicketImages:CommonAdd
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

        //Image
        private string _Image = string.Empty;
        public string Image
        {
            get { return _Image; }
            set { _Image = value; }
        }

        //IsAdmin
        private bool _IsAdmin = false;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        #endregion
    }
}