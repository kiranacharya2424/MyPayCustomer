using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Add
{
    public class AddFeedback:CommonAdd
    {
     
        #region "Properties"

        //TransactionUniqueId
        private string _TransactionUniqueId = string.Empty;
        public string TransactionUniqueId
        {
            get { return _TransactionUniqueId; }
            set { _TransactionUniqueId = value; }
        }

        //Subject
        private string _Subject = string.Empty;
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        //UserMessage
        private string _UserMessage = string.Empty;
        public string UserMessage
        {
            get { return _UserMessage; }
            set { _UserMessage = value; }
        }
         
        //Status
        private int _Status = 0;
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //CreatedDateDt
        private string _CreatedDateDt = string.Empty;
        public string CreatedDateDt
        {
            get { return _CreatedDateDt; }
            set { _CreatedDateDt = value; }
        }
        #endregion
    }
}