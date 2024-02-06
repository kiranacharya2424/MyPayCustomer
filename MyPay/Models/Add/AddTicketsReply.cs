using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Add
{
    public class AddTicketsReply:CommonAdd
    {
        #region "Enums"
        public enum Types
        {
            Sender = 1,
            Reciever = 2
        }
        #endregion

        #region "Properties"
       
        //TicketId
        private string _TicketId = string.Empty;
        public string TicketId
        {
            get { return _TicketId; }
            set { _TicketId = value; }
        }

        //Type
        private int _Type = 0;
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        //IsMainMessage
        private bool _IsMainMessage = false;
        public bool IsMainMessage
        {
            get { return _IsMainMessage; }
            set { _IsMainMessage = value; }
        }


        //Name
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        //Message
        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        //Title
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        //AttachFile
        private string _AttachFile = string.Empty;
        public string AttachFile
        {
            get { return _AttachFile; }
            set { _AttachFile = value; }
        }

        //IsAdmin
        private bool _IsAdmin = false;
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }

        //IsNote
        private bool _IsNote = false;
        public bool IsNote
        {
            get { return _IsNote; }
            set { _IsNote = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        #endregion

        #region "Get Methods" 
        public Int64 TotalTicketReplyCount(string TicketId)
        {
            Int64 data = 0;
            try
            {
                CommonHelpers obj = new CommonHelpers();
                string Result = "";
                string str = "select count(0) from TicketsReply  with(nolock) where IsNote=0 and TicketId = " + TicketId + "";
                Result = obj.GetScalarValueWithValue(str);
                if (!string.IsNullOrEmpty(Result) && Result != "0")
                {
                    data = Convert.ToInt32(Result);
                }
            }
            catch (Exception ex)
            {
                data = 0;
            }
            return data;
        }
        #endregion
    }
}