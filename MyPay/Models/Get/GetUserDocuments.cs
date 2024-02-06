using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPay.Models.Common;

namespace MyPay.Models.Get
{
    public class GetUserDocuments: CommonGet
    {
        #region "Properties"
        

        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }

        //@event
        private string _event = string.Empty;
        public string @event
        {
            get { return _event; }
            set { _event = value; }
        }

        //refrence
        private string _reference = string.Empty;
        public string reference
        {
            get { return _reference; }
            set { _reference = value; }
        }

        //email
        private string _email = string.Empty;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        //CheckNotRejected
        private int _CheckNotRejected = 0;
        public int CheckNotRejected
        {
            get { return _CheckNotRejected; }
            set { _CheckNotRejected = value; }
        }

        #endregion
    }
}