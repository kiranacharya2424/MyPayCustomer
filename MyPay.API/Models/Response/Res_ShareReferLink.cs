using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models
{
    public class Res_ShareReferLink:CommonResponse
    {
        //_RefCode
        private string _RefCode = string.Empty;
        public string RefCode
        {
            get { return _RefCode; }
            set { _RefCode = value; }
        }
        //MemberId
        private Int64 _MemberId = 0;
        public Int64 MemberId
        {
            get { return _MemberId; }
            set { _MemberId = value; }
        }
        //Platform
        private string _Platform = string.Empty;
        public string Platform
        {
            get { return _Platform; }
            set { _Platform = value; }
        }

        //SharedLinkURL
        private string _SharedLinkURL = string.Empty;
        public string SharedLinkURL
        {
            get { return _SharedLinkURL; }
            set { _SharedLinkURL = value; }
        }
        
    }
}